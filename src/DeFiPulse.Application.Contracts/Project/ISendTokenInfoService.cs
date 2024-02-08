using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DeFiPulse.Project
{
    public interface ISendTokenInfoService: IApplicationService
    {
        Task<MessageResult> CreateAsync(string walletAddress);
    }
}