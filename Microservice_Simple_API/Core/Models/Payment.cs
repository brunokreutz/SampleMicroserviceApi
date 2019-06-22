using System;

namespace MicroserviceSimpleAPI.Core.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public DateTime DateTime { get; set; }


        public Payment()
        {
        }
    }
}
