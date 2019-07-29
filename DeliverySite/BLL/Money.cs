using System;

namespace Delivery.BLL
{
    public struct Money : IComparable
    {
// Внутреннее представление - количество копеек
        private long value;
// Конструкторы
        public Money(double value)
        {
            this.value = (long) Math.Round(100*value + 0.00000000001, 0);
        }

        public Money(long high, byte low)
        {
            if (low < 0 || low > 99)
                throw new ArgumentException();
            if (high >= 0)
                value = 100*high + low;
            else
                value = 100*high - low;
        }

// Вспомогательный конструктор
        private Money(long copecks)
        {
            value = copecks;
        }

        public static Money Zerro
        {
            get { return new Money(0); }
        }

        public decimal DecimalAmount
        {
            get { return ((decimal) value)/100; }
            set { this.value = (long) value; }
        }

// Количество рублей
        public long High
        {
            get { return value/100; }
        }

// Количество копеек
        public byte Low
        {
            get { return (byte) (Math.Abs(value)%100); }
        }

        public static Money Null
        {
            get { return ToMoney(-1); }
        }

        public int CompareTo(object obj)
        {
            return DecimalAmount.CompareTo(((Money) obj).DecimalAmount);
        }

        public static Money ToMoney(object val)
        {
            if (val is Money) return (Money) val;
            var total = Zerro;
            if (val != DBNull.Value && val != null)
            {
                var dVal = Convert.ToDecimal(val);
                total = (Money) (double) dVal;
            }
            return total;
        }

        public static Money ToMoney(decimal val)
        {
            Money total;
            total = (Money) (double) val;
            return total;
        }

// Абсолютная величина
        public Money Abs()
        {
            return new Money(Math.Abs(value));
        }

// Сложение - функциональная форма
        public Money Add(Money r)
        {
            return new Money(value + r.value);
        }

// Вычитание - функциональная форма 
        public Money Subtract(Money r)
        {
            return new Money(value - r.value);
        }

// Умножение - функциональная форма
        public Money Multiply(double value)
        {
            var round = Math.Round(this.value*value + 0.000000001, 0);
            return new Money((long) round);
        }

// Деление - функциональная форма
        public Money Divide(double value)
        {
            return new Money((long) Math.Round(this.value/value + 0.000000001, 0));
        }

// Остаток от деления нацело - функциональная форма
        public long GetRemainder(uint n)
        {
            return value%n;
        }

// Сравнение - функциональная форма
        public int CompareTo(Money r)
        {
            if (value < r.value)
                return -1;
            if (value == r.value)
                return 0;
            return 1;
        }

// Деление на одинаковые части
// Количество частей должно быть не меньше 2
        public Money[] Share(uint n)
        {
            if (n < 2)
                throw new ArgumentException();
            var lowResult = new Money(value/n);
            var highResult =
                lowResult.value >= 0 ? new Money(lowResult.value + 1) : new Money(lowResult.value - 1);
            var results = new Money[n];
            var remainder = Math.Abs(value%n);
            for (long i = 0; i < remainder; i++)
                results[i] = highResult;
            for (var i = remainder; i < n; i++)
                results[i] = lowResult;
            return results;
        }

// Деление пропорционально коэффициентам
// Количество коэффициентов должно быть не меньше 2
        public Money[] Allocate(params uint[] ratios)
        {
            if (ratios.Length < 2)
                throw new ArgumentException();
            long total = 0;
            for (var i = 0; i < ratios.Length; i++)
                total += ratios[i];
            var remainder = value;
            var results = new Money[ratios.Length];
            for (var i = 0; i < results.Length; i++)
            {
                results[i] = new Money(value*ratios[i]/total);
                remainder -= results[i].value;
            }
            if (remainder > 0)
            {
                for (var i = 0; i < remainder; i++)
                    results[i].value++;
            }
            else
            {
                for (var i = 0; i > remainder; i--)
                    results[i].value--;
            }
            return results;
        }

// Перекрытые методы Object
        public override bool Equals(object value)
        {
            try
            {
                return this == (Money) value;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return High + "," + (Low > 9 ? Low.ToString() : "0" + Low);
        }

// Преобразования в строку аналогично double
        public string ToString(IFormatProvider provider)
        {
            if (provider is IMoneyToStringProvider)
// здесь - формирование числа прописью
                return ((IMoneyToStringProvider) provider).MoneyToString(this);
            return ((double) this).ToString(provider);
        }

        public string ToString(string format)
        {
            return ((double) this).ToString(format);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return ((double) this).ToString(format, provider);
        }

// Унарные операторы
        public static Money operator +(Money r)
        {
            return r;
        }

        public static Money operator -(Money r)
        {
            return new Money(-r.value);
        }

        public static Money operator ++(Money r)
        {
            return new Money(r.value++);
        }

        public static Money operator --(Money r)
        {
            return new Money(r.value--);
        }

// Бинарные операторы
        public static Money operator +(Money a, Money b)
        {
            return new Money(a.value + b.value);
        }

        public static Money operator -(Money a, Money b)
        {
            return new Money(a.value - b.value);
        }

        public static Money operator *(double a, Money b)
        {
            return new Money((long) Math.Round(a*b.value + 0.0000000000, 0));
        }

        public static Money operator *(Money a, double b)
        {
            return new Money((long) Math.Round(a.value*b + 0.0000000000, 0));
        }

        public static Money operator /(Money a, double b)
        {
            return new Money((long) Math.Round(a.value/b + 0.0000000000, 0));
        }

        public static Money operator %(Money a, uint b)
        {
            return new Money(a.value%b);
        }

        public static bool operator ==(Money a, Money b)
        {
            return a.value == b.value;
        }

        public static bool operator !=(Money a, Money b)
        {
            return a.value != b.value;
        }

        public static bool operator >(Money a, Money b)
        {
            return a.value > b.value;
        }

        public static bool operator <(Money a, Money b)
        {
            return a.value < b.value;
        }

        public static bool operator >=(Money a, Money b)
        {
            return a.value >= b.value;
        }

        public static bool operator <=(Money a, Money b)
        {
            return a.value <= b.value;
        }

// Операторы преобразования
        public static implicit operator double(Money r)
        {
            return (double) r.value/100;
        }

        public static explicit operator Money(double d)
        {
            return new Money(d);
        }

        public static Money Parse(string amount)
        {
            return ToMoney(decimal.Parse(amount));
        }
    }

// Интерфейс специализированного провайдера преобразования денег в строковое представление
    public interface IMoneyToStringProvider : IFormatProvider
    {
        string MoneyToString(Money m);
    }

// Преобразование числа в строку = число прописью
    public class NumberToRussianString
    {
// Род единицы измерения
        public enum WordGender
        {
            Masculine, // мужской
            Feminine, // женский
            Neuter // средний
        };

// Варианты написания единицы измерения 
        public enum WordMode
        {
            Mode1, // рубль
            Mode2_4, // рубля
            Mode0_5 // рублей
        };

// Строковые представления чисел
        private const string number0 = "ноль";

        private static readonly string[] number1 =
        {"один", "одна", "одно"};

        private static readonly string[] number2 =
        {"два", "две", "два"};

        private static readonly string[] number3_9 =
        {"три", "четыре", "пять", "шесть", "семь", "восемь", "девять"};

        private static readonly string[] number10_19 =
        {
            "десять", "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать",
            "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать"
        };

        private static readonly string[] number20_90 =
        {"двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто"};

        private static readonly string[] number100_900 =
        {"сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот"};

        private static readonly string[,] ternaries =
        {
            {"тысяча", "тысячи", "тысяч"},
            {"миллион", "миллиона", "миллионов"},
            {"миллиард", "миллиарда", "миллиардов"},
            {"триллион", "триллиона", "триллионов"},
            {"биллион", "биллиона", "биллионов"}
        };

        private static readonly WordGender[] TernaryGenders =
        {
            WordGender.Feminine, // тысяча - женский
            WordGender.Masculine, // миллион - мужской
            WordGender.Masculine, // миллиард - мужской
            WordGender.Masculine, // триллион - мужской
            WordGender.Masculine // биллион - мужской
        };

// Функция преобразования 3-значного числа, заданного в виде строки,
// с учетом рода (мужской, женский или средний).
// Род учитывается для корректного формирования концовки:
// "один" (рубль) или "одна" (тысяча)
// version 2
// 15.11.02 - updated
        private static string TernaryToString(long ternary, WordGender gender)
// private static string TernaryToString(int ternary, WordGender gender)
// (end) 15.11.02 - updated
        {
            var s = "";
// version 2
// 15.11.02 - updated
// учитываются только последние 3 разряда, т.е. 0..999 
//long t = ternary % 1000;
//int digit2 = (int) (t / 100);
//int digit1 = (int) ((t % 100) / 10);
//int digit0 = (int) (t % 10);
            var digit2 = ternary/100;
            var digit1 = (ternary%100)/10;
            var digit0 = ternary%10;
// (end) 15.11.02 - updated
// сотни
            while (digit2 >= 10) digit2 %= 10;
            if (digit2 > 0)
                s = number100_900[digit2 - 1] + " ";
            if (digit1 > 1)
            {
                s += number20_90[digit1 - 2] + " ";
                if (digit0 >= 3)
                    s += number3_9[digit0 - 3] + " ";
                else
                {
                    if (digit0 == 1) s += number1[(int) gender] + " ";
                    if (digit0 == 2) s += number2[(int) gender] + " ";
                }
            }
            else if (digit1 == 1)
                s += number10_19[digit0] + " ";
            else
            {
                if (digit0 >= 3)
                    s += number3_9[digit0 - 3] + " ";
                else if (digit0 > 0)
                {
                    if (digit0 == 1) s += number1[(int) gender] + " ";
                    if (digit0 == 2) s += number2[(int) gender] + " ";
                }
            }
            return s.TrimEnd();
        }

//
        private static string TernaryToString(long value, byte ternaryIndex)
        {
// version 2
// 15.11.02 - updated
//long ternary = value;
//for (byte i = 0; i < ternaryIndex; i++) 
// ternary /= 1000;
            for (byte i = 0; i < ternaryIndex; i++)
                value /= 1000;
// учитываются только последние 3 разряда, т.е. 0..999 
            var ternary = (int) (value%1000);
// (end) 15.11.02 - updated
            if (ternary == 0)
                return "";
            ternaryIndex--;
            return TernaryToString(ternary, TernaryGenders[ternaryIndex]) + " " +
                   ternaries[ternaryIndex, (int) GetWordMode(ternary)] + " ";
        }

// Функция возвращает число прописью с учетом рода единицы измерения
        public static string NumberToString(long value, WordGender gender)
        {
// version 2 
// 15.11.02 - updated
// if (value <= 0) 
            if (value < 0)
// (end) 15.11.02 - updated
                return "";
// version 2 
// 15.11.02 - added
            if (value == 0)
                return number0;
// (end) 15.11.02 - added
            return TernaryToString(value, 5) +
                   TernaryToString(value, 4) +
                   TernaryToString(value, 3) +
                   TernaryToString(value, 2) +
                   TernaryToString(value, 1) +
                   TernaryToString(value, gender);
// (end) 15.11.02 - updated
        }

// Определение варианта написания единицы измерения по 3-х значному числу
        public static WordMode GetWordMode(long number)
        {
// достаточно проверять только последние 2 цифры,
// т.к. разные падежи единицы измерения раскладываются
// 0 рублей, 1 рубль, 2-4 рубля, 5-20 рублей, 
// дальше - аналогично первому десятку 
            var digit1 = (int) (number%100)/10;
            var digit0 = (int) (number%10);
            if (digit1 == 1)
                return WordMode.Mode0_5;
            if (digit0 == 1)
                return WordMode.Mode1;
            if (2 <= digit0 && digit0 <= 4)
                return WordMode.Mode2_4;
            return WordMode.Mode0_5;
        }
    }

// Преобразование денег в сумму прописью
    public abstract class MoneyToStringProviderBase : IMoneyToStringProvider
    {
// отображение копеек в виде цифр ? - 00
        private readonly bool digitHigh = false;

        private readonly bool digitLow = true;
        // сокращенное написание рублей ? - рублей/руб.
        private readonly bool shortHigh;
// сокращенное написание копеек ? - копеек/коп.
        private readonly bool shortLow;
// Конструктор
        protected MoneyToStringProviderBase(bool shortHigh, bool shortLow, bool digitLow)
        {
            this.shortHigh = shortHigh;
            this.shortLow = shortLow;
            this.digitLow = digitLow;
        }

        protected MoneyToStringProviderBase(bool shortHigh, bool shortLow, bool digitLow, bool digitHigh)
        {
            this.shortHigh = shortHigh;
            this.shortLow = shortLow;
            this.digitLow = digitLow;
            this.digitHigh = digitHigh;
        }

        // Реализация интерфейса IMoneyToStringProvider
        // Метод родительского интерфейса IFormatProvider
        public object GetFormat(Type formatType)
        {
            if (formatType != typeof (RoubleToStringProvider))
                return null;
            return this;
        }

// Функция возвращает число рублей и копеек прописью
        public string MoneyToString(Money m)
        {
            var r = m.High;
            long c = m.Low;
            return string.Format("{0} {1} {2} {3}",
                digitHigh
                    ? string.Format("{0:d2}", r)
                    :NumberToRussianString.NumberToString(r, GetGender(true)),
                shortHigh
                    ? GetShortName(true)
                    : GetName(NumberToRussianString.GetWordMode(r), true),
                digitLow
                    ? string.Format("{0:d2}", c)
                    : NumberToRussianString.NumberToString(c, GetGender(false)),
                shortLow
                    ? GetShortName(false)
                    : GetName(NumberToRussianString.GetWordMode(c), false));
        }

        protected abstract NumberToRussianString.WordGender GetGender(bool high);
// Функция возвращает наименование денежной единицы в соответствующей форме 
// (1) рубль / (2) рубля / (5) рублей
        protected abstract string GetName(NumberToRussianString.WordMode wordMode, bool high);
// Функция возвращает сокращенное наименование денежной единицы 
        protected abstract string GetShortName(bool high);
    }

// Преобразование русских денег (рубли + копейки) в сумму прописью
    public class RoubleToStringProvider : MoneyToStringProviderBase
    {
// варианты написания рублей
        private static readonly string[] roubles =
        {"рубль", "рубля", "рублей"};

// варианты написания копеек
        private static readonly string[] copecks =
        {"копейка", "копейки", "копеек"};

        public RoubleToStringProvider(bool shortRoubles, bool shortCopecks, bool digitCopecks, bool digitRoubles) :
            base(shortRoubles, shortCopecks, digitCopecks, digitRoubles)
        {
        }

        protected override NumberToRussianString.WordGender GetGender(bool high)
        {
            return high ? NumberToRussianString.WordGender.Masculine : NumberToRussianString.WordGender.Feminine;
        }

        protected override string GetName(NumberToRussianString.WordMode wordMode, bool high)
        {
            return high ? roubles[(int) wordMode] : copecks[(int) wordMode];
        }

        protected override string GetShortName(bool high)
        {
            return high ? "руб." : "коп.";
        }
    }

// Преобразование американских денег (доллары + центы) в сумму прописью
    public class DollarToStringProvider : MoneyToStringProviderBase
    {
// варианты написания долларов
        private static readonly string[] dollars =
        {"доллар", "доллара", "долларов"};

// варианты написания центов
        private static readonly string[] cents =
        {"цент", "цента", "центов"};

        public DollarToStringProvider(bool shortDollar, bool shortCent, bool digitCent) :
            base(shortDollar, shortCent, digitCent)
        {
        }

        protected override NumberToRussianString.WordGender GetGender(bool high)
        {
            return NumberToRussianString.WordGender.Masculine;
        }

        protected override string GetName(NumberToRussianString.WordMode wordMode, bool high)
        {
            return high ? dollars[(int) wordMode] : cents[(int) wordMode];
        }

        protected override string GetShortName(bool high)
        {
            return high ? "дол." : "ц.";
        }
    }

    public static class MoneyHelper
    {
        public static string ToRussianString(decimal numb, bool isDigit = false)
        {
           return ToRussianString(Convert.ToDouble(numb), isDigit);
        }

        public static string ToRussianString(double numb, bool isDigit = false)
        {
            var moneyProv = !isDigit ? new RoubleToStringProvider(false, false, true, false) : new RoubleToStringProvider(false, false, true, true);
            var money = new Money(Convert.ToDouble(numb));
            return moneyProv.MoneyToString(money);
        }
    }
}
