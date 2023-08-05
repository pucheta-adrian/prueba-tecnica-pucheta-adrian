using Unilink.TechTest.Domain.Entities;

namespace Unilink.TechTest.Domain.DTOs
{
    public class RouletteDto
    {
        public int Number { get; set; }
        public bool IsEvenNumber { get; set; }
        public RouletteColorEnum Color { get; set; }
    }
}