using System;
using System.Threading.Tasks;
using AElf;
using AElf.Client;
using AElf.Client.Dto;
using AElf.Client.MultiToken;
using Google.Protobuf;
using Address = AElf.Types.Address;
using Microsoft.Extensions.Options;

namespace DeFiPulse.Project;

public class CoinSendContractService : ICoinSendContractService
{
    private string BaseUrlForMainchain = string.Empty;
    private string BaseUrlForSidechain = string.Empty;
    private string PrivateKey = string.Empty;
    private string _address = string.Empty;
    private AElfClient _clientForMainchain { get; }
    private AElfClient _clientForSidechain { get; }
    private readonly ApiConfigOptions _apiconfig;

    public CoinSendContractService(IOptionsSnapshot<ApiConfigOptions> apiOptions)
    {
        _apiconfig = apiOptions.Value;
        BaseUrlForMainchain = string.IsNullOrEmpty(_apiconfig.BaseUrlForMainchain)
            ? _apiconfig.BaseUrl
            : _apiconfig.BaseUrlForMainchain;
        BaseUrlForSidechain = _apiconfig.BaseUrlForSidechain;
        PrivateKey = _apiconfig.PrivateKey;
        _clientForMainchain = new AElfClient(BaseUrlForMainchain);
        _clientForSidechain = new AElfClient(BaseUrlForSidechain);
        _address = _clientForMainchain.GetAddressFromPrivateKey(PrivateKey); //通过privatekey拿到自己的钱包地址
    }

    public async Task<MessageResult> CoinIsAdequate(ChainType chainType)
    {
        var messageResult = new MessageResult();
        try
        {
            messageResult = await PerformCoinIsAdequateAsync(chainType == ChainType.Mainchain
                ? _clientForMainchain
                : _clientForSidechain);
        }
        catch (Exception ex)
        {
            messageResult.IsSuccess = false;
            messageResult.Code = Convert.ToInt32(CodeStatus.SystemError);
            messageResult.Message = $"valid balance error! {ex.Message}";
        }

        return messageResult;
    }

    private async Task<MessageResult> PerformCoinIsAdequateAsync(AElfClient client)
    {
        var messageResult = new MessageResult();
        var toAddress =
            await client.GetContractAddressByNameAsync(
                HashHelper.ComputeFrom("AElf.ContractNames.Token")); //发送交易地址（合约地址）
        var paramGetBalance = new GetBalanceInput
        {
            Symbol = "ELF",
            Owner = new AElf.Client.Proto.Address { Value = Address.FromBase58(_address).Value }
        };

        var transactionGetBalance =
            await client.GenerateTransactionAsync(_address, toAddress.ToBase58(), "GetBalance",
                paramGetBalance);
        //签名
        var txWithSignGetBalance = client.SignTransaction(PrivateKey, transactionGetBalance);
        //执行请求
        var transactionGetBalanceResult = await client.ExecuteTransactionAsync(new ExecuteTransactionDto
        {
            RawTransaction = txWithSignGetBalance.ToByteArray().ToHex()
        });
        //获取余额信息
        var balance =
            GetBalanceOutput.Parser.ParseFrom(
                ByteArrayHelper.HexStringToByteArray(transactionGetBalanceResult));

        Console.WriteLine($"the balance is:{balance.Balance}");
        if (balance.Balance < 100000000)
        {
            messageResult.IsSuccess = false;
            messageResult.Code = Convert.ToInt32(CodeStatus.BalanceNotAdequate);
        }

        return messageResult;
    }

    public async Task<MessageResult> SendCoin(string walletAddress, ChainType chainType)
    {
        MessageResult messageResult = new MessageResult();
        try
        {
            messageResult = await PerformSendCoinAsync(chainType == ChainType.Mainchain
                ? _clientForMainchain
                : _clientForSidechain, walletAddress);
        }
        catch (Exception ex)
        {
            messageResult.IsSuccess = false;
            messageResult.Code = Convert.ToInt32(CodeStatus.SystemError);
            messageResult.Message = $"fail to send conis!{ex.Message}";
        }

        return messageResult;
    }

    private async Task<MessageResult> PerformSendCoinAsync(AElfClient client, string walletAddress)
    {
        MessageResult messageResult = new MessageResult();

        _address = client.GetAddressFromPrivateKey(PrivateKey); //通过privatekey拿到自己的钱包地址
        var toAddress =
            await client.GetContractAddressByNameAsync(
                HashHelper.ComputeFrom("AElf.ContractNames.Token")); //交易地址（合约地址）

        //相关参数
        var param = new TransferInput
        {
            To = new AElf.Client.Proto.Address { Value = Address.FromBase58(walletAddress).Value },
            Symbol = "ELF", //代币类型
            Amount = _apiconfig.SendCount * 100000000L //代币数量
        };

        //发起交易
        var transaction =
            await client.GenerateTransactionAsync(_address, toAddress.ToBase58(), "Transfer", param);
        //签名
        var txWithSign = client.SignTransaction(PrivateKey, transaction);

        //执行交易
        var result = await client.SendTransactionAsync(new SendTransactionInput
        {
            RawTransaction = txWithSign.ToByteArray().ToHex()
        });
        messageResult.Message = result.TransactionId;
        return messageResult;
    }
}