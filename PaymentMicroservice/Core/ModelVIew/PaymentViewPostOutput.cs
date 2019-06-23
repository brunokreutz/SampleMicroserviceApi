namespace PaymentMicroservice.Core.ModelVIew
{
    public class PaymentViewPostOutput
    {
        public double NetValue { get; set; }
        public double SourceAccountBalance { get; set; }
        public double DestinationAccountBalance { get; set; }

        public PaymentViewPostOutput(double netValue, double sourceAccountBalance, double destinationAccountBalance)
        {
            NetValue = netValue;
            SourceAccountBalance = sourceAccountBalance;
            DestinationAccountBalance = destinationAccountBalance;
        }
    }
}
