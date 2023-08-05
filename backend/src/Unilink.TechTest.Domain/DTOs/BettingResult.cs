namespace Unilink.TechTest.Domain.DTOs
{
    public class GameResultDto
    {
        public RouletteDto Game { get; set; }
        public decimal Amount { get; set; } = 0;
        public bool WinNumber { get; set; }
        public bool WinNumberEven { get; set; }
        public bool WinColor { get; set; }
        public BalanceDto Balance { get; set; }
        public bool Win { get; set; }
    }
}