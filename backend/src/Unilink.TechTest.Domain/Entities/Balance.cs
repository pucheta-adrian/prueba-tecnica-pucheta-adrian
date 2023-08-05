using System;

namespace Unilink.TechTest.Domain.Entities
{
    public class Balance
    {
        public int BalanceId { get; set; }
        public string UserName { get; set; }
        public decimal Amount { get; set; } = 0;
        public DateTime UpdatedAt { get; set; }
    }
}