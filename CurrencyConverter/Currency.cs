using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace CurrencyConverter
{
    enum ECurrency
    {
        BTC,
        LTC,
        DOGE,
        USD,
        EUR
    };

    class Currency
    {
        private readonly int significance;

        private readonly ECurrency eCurrency;
        private static readonly List<ECurrency> Markets = new List<ECurrency>(
            new ECurrency[]
            {
                ECurrency.LTC, ECurrency.BTC, ECurrency.EUR, ECurrency.USD
            }
        );

        /// <summary>
        /// Constructs a Currency object from an ECurrency value.
        /// </summary>
        /// <param name="eCurrency"></param>
        public Currency(ECurrency eCurrency)
        {
            this.eCurrency = eCurrency;

            if (Markets.Contains(eCurrency))
                significance = Markets.IndexOf(eCurrency) + 1;
            else
                significance = 0;
        }

        /// <summary>
        /// Returns the marketId needed to find the conversion rate between to currencies.
        /// 
        /// If @in does not have its own market or is less significant than @out, then @out's market must be 
        /// used for the conversion instead. In this case, swapNeeded is set to true so that the caller knows that the actual
        /// conversion rate is the reciprocal of the output.
        /// </summary>
        /// <param name="@in"></param>
        /// <param name="@out"></param>
        /// <param name="swapNeeded">
        /// True if the conversion rate for this marketid needs to be reciprocated to get the correct rate.
        /// </param>
        /// <returns></returns>
        public static string GetMarketId(Currency @in, Currency @out, out bool swapNeeded)
        {
            Currency currency;
            Currency market;
            swapNeeded = false;

            if (@out.significance > @in.significance)
            {
                currency = @in;
                market = @out;
                swapNeeded = true;
            }
            else
            {
                currency = @out;
                market = @in;
            }

            return currency + "_" + market;
        }

        /// <summary>
        /// Gets the latest price for the specified market.
        /// </summary>
        /// <param name="in"></param>
        /// <param name="out"></param>
        /// <returns>The latest trade price for the market.</returns>
        static decimal GetPrice(Currency @in, Currency @out)
        {
            if (@in.Equals(@out))
                return 1;

            var swapNeeded = false;
            var marketId = GetMarketId(@in, @out, out swapNeeded);

            var response = Program.HttpGet("https://www.cryptsy.com/api/v2/markets/" + marketId);

            var json = JObject.Parse(response);
            var price = decimal.Parse(json["data"]["last_trade"]["price"].ToString(), NumberStyles.Float);

            if (swapNeeded)
                return 1 / price;
            else
                return price;
        }

        /// <summary>
        /// Converts an amount from one currency to another.
        /// </summary>
        /// <param name="in">Currency to convert from</param>
        /// <param name="out">Currency to convert to</param>
        /// <param name="amount">Amount to convert</param>
        /// <returns></returns>
        public static decimal Convert(Currency @in, Currency @out, decimal amount)
        {
            if (amount <= 0)
                return 0;

            var price = GetPrice(@in, @out);
            return price * amount;
        }

        public override string ToString()
        {
            return eCurrency.ToString();
        }

        public override bool Equals(object obj)
        {
            var otherECurrency = ((Currency)obj).eCurrency;
            return eCurrency == otherECurrency;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
