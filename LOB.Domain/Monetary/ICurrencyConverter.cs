using System;

namespace LOB.Domain.Monetary {
    public interface ICurrencyConverter {
        double GetRate(CurrencyCodes fromCode, CurrencyCodes toCode, DateTime asOn);
        double GetRate(string fromCode, string toCode, DateTime asOn);
    }
}