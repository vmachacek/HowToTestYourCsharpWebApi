using System;

namespace HowToTestYourCsharpWebApi.Api.ExternalApi
{
    public class Money
    {
        public Money(string currency, decimal amount)
        {
            this.Currency = currency;
            this.Amount = amount;
        }

        public Money()
        {
        }

        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}