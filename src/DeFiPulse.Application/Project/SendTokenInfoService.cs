using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace DeFiPulse.Project;

public class SendTokenInfoService : DeFiPulseAppService, ISendTokenInfoService
{
    private readonly ISendTokenInfoRepository _sendTokenInfoRepository;
    private readonly ICoinSendContractService _coinSendContractService;

    public SendTokenInfoService(ISendTokenInfoRepository sendTokenInfoRepository,
        ICoinSendContractService contractService)
    {
        _sendTokenInfoRepository = sendTokenInfoRepository;
        _coinSendContractService = contractService;
    }

    [HttpPost]
    public async Task<MessageResult> CreateAsync(string walletAddress)
    {
        MessageResult messageResult = new MessageResult();

        //验证地址
        if (string.IsNullOrEmpty(walletAddress))
        {
            messageResult.IsSuccess = false;
            messageResult.Code = Convert.ToInt32(CodeStatus.InvalidAddress);
            messageResult.Message = "invalid address";
            return messageResult;
        }

        var chainType = ChainType.Mainchain;
        if (walletAddress.Contains("ELF_"))
        {
            if (walletAddress.Split('_')[2] != "AELF")
            {
                chainType = ChainType.Sidechain;
            }

            walletAddress = walletAddress.Split('_')[1];
        }
        //1.验证当前钱包地址是否已经领取过代币

        var gotTokenBefore = await _sendTokenInfoRepository.GetAsync(walletAddress);
        if (gotTokenBefore != null)
        {
            messageResult.IsSuccess = false;
            messageResult.Message = $"You have received the test tokens and cannot receive it again";
            messageResult.Code = Convert.ToInt32(CodeStatus.HadReceived);
            return messageResult;
        }

        //2.验证代币是否充足
        var isAdequate = await _coinSendContractService.CoinIsAdequate(chainType);
        if (!isAdequate.IsSuccess)
        {
            return isAdequate;
        }

        //3.发代币
        var sendCoinResult = await _coinSendContractService.SendCoin(walletAddress, chainType);
        if (!sendCoinResult.IsSuccess)
        {
            return sendCoinResult;
        }

        messageResult.Message = sendCoinResult.Message;

        try
        {
            //4.将数据存入数据库
            var stid = new SendTokenInfo()
            {
                WalletAddress = walletAddress,
                IsSendSuccess = true,
                SendCoinValue = 2000
            };
            stid.SetId(walletAddress);

            var result = await _sendTokenInfoRepository.InsertAsync(stid);
            if (result == null)
            {
                messageResult.IsSuccess = false;
                messageResult.Message = $"system error";
                messageResult.Code = Convert.ToInt32(CodeStatus.SystemError);
            }
        }
        catch (Exception ex)
        {
            messageResult.IsSuccess = false;
            messageResult.Message = $"system error";
            messageResult.Code = Convert.ToInt32(CodeStatus.SystemError);
        }

        return messageResult;
    }
}