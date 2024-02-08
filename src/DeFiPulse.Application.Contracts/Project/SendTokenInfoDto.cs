namespace DeFiPulse.Project
{
    public class SendTokenInfoDto
    {
        public string WalletAddress { get; set; }
        public int SendCoinValue { get; set; }

        public bool IsSendSuccess { get; set; }
    }
}