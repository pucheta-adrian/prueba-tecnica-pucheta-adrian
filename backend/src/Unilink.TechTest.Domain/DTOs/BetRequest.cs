using Unilink.TechTest.Domain.Entities;

namespace Unilink.TechTest.Domain.DTOs
{
    public class BetRequest
    {
        public string username { get; set; }
        public decimal betAmount { get; set; }
        public int? number { get; set; }
        public bool? numberEven { get; set; }
        public RouletteColorEnum? color { get; set; }
    }
}