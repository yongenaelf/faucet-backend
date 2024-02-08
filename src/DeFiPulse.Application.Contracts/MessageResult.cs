namespace DeFiPulse
{
    public class MessageResult
    {
        public bool IsSuccess { get; set; } = true;
        public int Code { get; set; } = 1;
        public string Message { get; set; } = string.Empty;
    }
}