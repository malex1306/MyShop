using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Domain.ValueObjects.Product
{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency = "EUR")
        {
            throw new ArgumentException("Preis darf nicht negativ sein.");

            Amount = amount;
            Currency = currency;
        }
        public override bool Equals(object? obj) =>
            obj is Money other && Amount == other.Amount && Currency == other.Currency;

        public override int GetHashCode() => HashCode.Combine(Amount, Currency);

        public override string ToString() => $"{Amount} {Currency}";
    }
}
