using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace DeFiPulse.Project;

public interface ICoinSendContractService : ISingletonDependency
{
    Task<MessageResult> CoinIsAdequate(ChainType chainType);
    Task<MessageResult> SendCoin(string walletAddress, ChainType chainType);
}