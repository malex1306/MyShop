using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Domain.ValueObjects.Product
{
    public class ProductName
    {
        public string Value { get; }

        public ProductName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Produktname darf nicht leer sein.", nameof(value));
            }
            if (value.Length > 100)
            {
                throw new ArgumentException("Produktname darf maximal 100 Zeichen lang sein.", nameof(value));
            }
            Value = value;
        }

        public override bool Equals(object? obj)=>
            obj is ProductName other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value;
    }

}
