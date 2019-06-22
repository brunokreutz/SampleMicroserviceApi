using System.ComponentModel.DataAnnotations;

namespace MicroserviceSimpleAPI.Core.Models
{
    public class Fee
    {
        [Key]
        public int Id { get; set; }
        public int NumberOfPortions { get; set; }
        public double Value { get; set; }

    }
}
