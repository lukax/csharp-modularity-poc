/*
 * This Money class gives you the ability to work with money of multiple currencies
 * as if it were numbers.
 * It looks and behaves like a decimal.
 * Super light: Only a 64bit double and 16bit int are used to persist an instance.
 * Super fast: Access to the internal double value for fast calculations.
 * Currency codes are used to get everything from the MS Globalization classes.
 * All lookups happen from a singleton dictionary.
 * Formatting and significant digits are automatically handled.
 * An allocation function also allows even distribution of Money.
 * 
 * References:
 * Martin Fowler patterns
 * Making Money with C# : http://www.lindelauf.com/?p=17
 * http://www.codeproject.com/Articles/28244/A-Money-type-for-the-CLR?msg=3679755
 * A few other articles on the web around representing money types
 * http://en.wikipedia.org/wiki/ISO_4217
 * http://www.currency-iso.org/iso_index/iso_tables/iso_tables_a1.htm
 * 
 * Important!
 * Although the .Amount property wraps the class as Decimal, this Money class uses double to store the Money value internally.
 * Only 15 decimal digits of accuracy are guaranteed! (16 if the first digit is smaller than 9)
 * It should be fairly simple to replace the internal double with a decimal if this is not sufficient and performance is not an issue.
 * Decimal operations are MUCH slower than double (average of 15x)
 * http://stackoverflow.com/questions/366852/c-sharp-decimal-datatype-performance
 * Use the .InternalAmount property to get to the double member.
 * All the Money comparison operators use the Decimal wrapper with significant digits for the currency.
 * All the Money arithmatic (+-/*) operators use the internal double value.
 */

using System;
using System.Globalization;

namespace LOB.Domain.Monetary {
    [Serializable]
    public class Money : IComparable<Money>, IEquatable<Money>, IComparable {
        private double _amount;
        private CurrencyCodes _currencyCode;
        #region Constructors

        public Money()
                : this(0d, LocalCurrencyCode) { }
        public Money(string currencyCode)
                : this((CurrencyCodes)Enum.Parse(typeof(CurrencyCodes), currencyCode)) { }
        public Money(CurrencyCodes currencyCode)
                : this(0d, currencyCode) { }
        public Money(long amount)
                : this(amount, LocalCurrencyCode) { }
        public Money(decimal amount)
                : this(amount, LocalCurrencyCode) { }
        public Money(double amount)
                : this(amount, LocalCurrencyCode) { }
        public Money(long amount, string currencyCode)
                : this(amount, (CurrencyCodes)Enum.Parse(typeof(CurrencyCodes), currencyCode)) { }
        public Money(decimal amount, string currencyCode)
                : this(amount, (CurrencyCodes)Enum.Parse(typeof(CurrencyCodes), currencyCode)) { }
        public Money(double amount, string currencyCode)
                : this(amount, (CurrencyCodes)Enum.Parse(typeof(CurrencyCodes), currencyCode)) { }
        public Money(long amount, CurrencyCodes currencyCode)
                : this((double)amount, currencyCode) { }
        public Money(decimal amount, CurrencyCodes currencyCode)
                : this((double)amount, currencyCode) { }
        public Money(double amount, CurrencyCodes currencyCode) {
            this._currencyCode = currencyCode;
            this._amount = amount;
        }

        #endregion
        #region Public Properties

        public static bool AllowImplicitConversion = false;
        /// <summary>
        ///     Represents the ISO code for the currency
        /// </summary>
        /// <returns>An Int16 with the ISO code for the current currency</returns>
        public Int16 ISOCode {
            get { return (Int16)_currencyCode; }
            set { _currencyCode = (CurrencyCodes)value; }
        }

        /// <summary>
        ///     Accesses the internal representation of the value of the Money
        /// </summary>
        /// <returns>A decimal with the internal amount stored for this Money.</returns>
        public double InternalAmount {
            get { return _amount; }
            set { _amount = value; }
        }

        /// <summary>
        ///     Rounds the amount to the number of significant decimal digits
        ///     of the associated currency using MidpointRounding.AwayFromZero.
        /// </summary>
        /// <returns>A decimal with the amount rounded to the significant number of decimal digits.</returns>
        public decimal Amount {
            get { return Decimal.Round((Decimal)_amount, DecimalDigits, MidpointRounding.AwayFromZero); }
        }

        /// <summary>
        ///     Truncates the amount to the number of significant decimal digits
        ///     of the associated currency.
        /// </summary>
        /// <returns>A decimal with the amount truncated to the significant number of decimal digits.</returns>
        public decimal TruncatedAmount {
            get { return (decimal)((long)Math.Truncate(_amount * DecimalDigits)) / DecimalDigits; }
        }

        public string CurrencyCode {
            get { return Currency.Get(_currencyCode).Code; }
        }

        public string CurrencySymbol {
            get { return Currency.Get(_currencyCode).Symbol; }
        }

        public string CurrencyName {
            get { return Currency.Get(_currencyCode).EnglishName; }
        }

        /// <summary>
        ///     Gets the number of decimal digits for the associated currency.
        /// </summary>
        /// <returns>An int containing the number of decimal digits.</returns>
        public int DecimalDigits {
            get { return Currency.Get(_currencyCode).NumberFormat.CurrencyDecimalDigits; }
        }

        /// <summary>
        ///     Gets the CurrentCulture from the CultureInfo object and creates a CurrencyCodes enum object.
        /// </summary>
        /// <returns>The CurrencyCodes enum of the current locale.</returns>
        public static CurrencyCodes LocalCurrencyCode {
            get { return (CurrencyCodes)Enum.Parse(typeof(CurrencyCodes), new RegionInfo(CultureInfo.CurrentCulture.LCID).ISOCurrencySymbol); }
        }

        public static ICurrencyConverter Converter { get; set; }

        #endregion
        #region Money Operators

        public int CompareTo(object obj) {
            if(obj == null) return 1;
            if(!(obj is Money)) throw new ArgumentException("Argument must be Money");
            return CompareTo((Money)obj);
        }

        public int CompareTo(Money other) {
            if(this < other) return -1;
            if(this > other) return 1;
            return 0;
        }
        public bool Equals(Money other) {
            if(ReferenceEquals(other, null)) return false;
            if(AllowImplicitConversion)
                return Amount == other.Convert(_currencyCode).Amount
                       && other.Amount == Convert(other._currencyCode).Amount;
            else return ((CurrencySymbol == other.CurrencySymbol) && (Amount == other.Amount));
        }
        public override int GetHashCode() { return Amount.GetHashCode() ^ _currencyCode.GetHashCode(); }

        public override bool Equals(object obj) { return (obj is Money) && Equals((Money)obj); }

        public static bool operator ==(Money first, Money second) {
            if(ReferenceEquals(first, second)) return true;
            if(ReferenceEquals(first, null) || ReferenceEquals(second, null)) return false;
            return first.Amount == second.ConvertOrCheck(first._currencyCode).Amount
                   && second.Amount == first.ConvertOrCheck(second._currencyCode).Amount;
        }

        public static bool operator !=(Money first, Money second) { return !first.Equals(second); }

        public static bool operator >(Money first, Money second) {
            return first.Amount > second.ConvertOrCheck(first._currencyCode).Amount
                   && second.Amount < first.Convert(second._currencyCode).Amount;
        }

        public static bool operator >=(Money first, Money second) {
            return first.Amount >= second.ConvertOrCheck(first._currencyCode).Amount
                   && second.Amount <= first.Convert(second._currencyCode).Amount;
        }

        public static bool operator <=(Money first, Money second) {
            return first.Amount <= second.ConvertOrCheck(first._currencyCode).Amount
                   && second.Amount >= first.Convert(second._currencyCode).Amount;
        }

        public static bool operator <(Money first, Money second) {
            return first.Amount < second.ConvertOrCheck(first._currencyCode).Amount
                   && second.Amount > first.Convert(second._currencyCode).Amount;
        }

        public static Money operator +(Money first, Money second) { return new Money(first._amount + second.ConvertOrCheck(first._currencyCode)._amount, first._currencyCode); }

        public static Money operator -(Money first, Money second) { return new Money(first._amount - second.ConvertOrCheck(first._currencyCode)._amount, first._currencyCode); }

        public static Money operator *(Money first, Money second) { return new Money(first._amount * second.ConvertOrCheck(first._currencyCode)._amount, first._currencyCode); }

        public static Money operator /(Money first, Money second) { return new Money(first._amount / second.ConvertOrCheck(first._currencyCode)._amount, first._currencyCode); }

        #endregion
        #region Cast Operators

        public static implicit operator Money(long amount) { return new Money(amount, LocalCurrencyCode); }

        public static implicit operator Money(decimal amount) { return new Money(amount, LocalCurrencyCode); }

        public static implicit operator Money(double amount) { return new Money(amount, LocalCurrencyCode); }

        public static bool operator ==(Money money, long value) {
            if(ReferenceEquals(money, null) || ReferenceEquals(value, null)) return false;
            return (money.Amount == value);
        }
        public static bool operator !=(Money money, long value) { return !(money == value); }

        public static bool operator ==(Money money, decimal value) {
            if(ReferenceEquals(money, null) || ReferenceEquals(value, null)) return false;
            return (money.Amount == value);
        }
        public static bool operator !=(Money money, decimal value) { return !(money == value); }

        public static bool operator ==(Money money, double value) {
            if(ReferenceEquals(money, null) || ReferenceEquals(value, null)) return false;
            return (money.Amount == (decimal)value);
        }
        public static bool operator !=(Money money, double value) { return !(money == value); }

        public static Money operator +(Money money, long value) { return money + (double)value; }
        public static Money operator +(Money money, decimal value) { return money + (double)value; }
        public static Money operator +(Money money, double value) {
            if(money == null) throw new ArgumentNullException("money");
            return new Money(money._amount + value, money._currencyCode);
        }

        public static Money operator -(Money money, long value) { return money - (double)value; }
        public static Money operator -(Money money, decimal value) { return money - (double)value; }
        public static Money operator -(Money money, double value) {
            if(money == null) throw new ArgumentNullException("money");
            return new Money(money._amount - value, money._currencyCode);
        }

        public static Money operator *(Money money, long value) { return money * (double)value; }
        public static Money operator *(Money money, decimal value) { return money * (double)value; }
        public static Money operator *(Money money, double value) {
            if(money == null) throw new ArgumentNullException("money");
            return new Money(money._amount * value, money._currencyCode);
        }

        public static Money operator /(Money money, long value) { return money / (double)value; }
        public static Money operator /(Money money, decimal value) { return money / (double)value; }
        public static Money operator /(Money money, double value) {
            if(money == null) throw new ArgumentNullException("money");
            return new Money(money._amount / value, money._currencyCode);
        }

        #endregion
        #region Functions

        public Money Copy() { return new Money(Amount, _currencyCode); }

        public Money Clone() { return new Money(_currencyCode); }

        public string ToString(bool genericFormatter) { return ToString("C", genericFormatter); }

        public string ToString(string format = "C", bool genericFormatter = false) {
            if(genericFormatter) {
                var formatter = (NumberFormatInfo)Currency.Get(LocalCurrencyCode).NumberFormat.Clone();
                formatter.CurrencySymbol = CurrencyCode;
                return Amount.ToString(format, formatter);
            }
            else return Amount.ToString(format, Currency.Get(_currencyCode).NumberFormat);
        }

        /// <summary>
        ///     Evenly distributes the amount over n parts, resolving remainders that occur due to rounding
        ///     errors, thereby garuanteeing the postcondition: result->sum(r|r.amount) = this.amount and
        ///     x elements in result are greater than at least one of the other elements, where x = amount mod n.
        /// </summary>
        /// <param name="n">Number of parts over which the amount is to be distibuted.</param>
        /// <returns>Array with distributed Money amounts.</returns>
        public Money[] Allocate(int n) {
            double cents = Math.Pow(10, DecimalDigits);
            double lowResult = ((long)Math.Truncate(_amount / n * cents)) / cents;
            double highResult = lowResult + 1.0d / cents;
            var results = new Money[n];
            var remainder = (int)((_amount * cents) % n);
            for(int i = 0; i < remainder; i++) results[i] = new Money((Decimal)highResult, _currencyCode);
            for(int i = remainder; i < n; i++) results[i] = new Money((Decimal)lowResult, _currencyCode);
            return results;
        }

        public Money Convert(CurrencyCodes toCurrency) {
            if(_currencyCode == toCurrency) return this;
            if(Converter == null) throw new Exception("You need to assign an ICurrencyconverter to Money.Converter to automatically convert different currencies.");
            return Convert(toCurrency, Converter.GetRate(_currencyCode, toCurrency, DateTime.Now));
        }

        public Money Convert(CurrencyCodes toCurrency, double rate) { return new Money(_amount * rate, toCurrency); }

        private Money ConvertOrCheck(CurrencyCodes toCurrency) {
            if(_currencyCode == toCurrency) return this;
            else if(AllowImplicitConversion) return Convert(toCurrency);
            else throw new InvalidOperationException("Money type mismatch");
        }

        #endregion
    }
}