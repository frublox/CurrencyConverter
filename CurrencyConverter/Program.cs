using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;

using Newtonsoft.Json.Linq;

namespace CurrencyConverter
{
    /// <summary>
    /// A raw enum of currencies. Should only be used for constructing Currency instances.
    /// </summary>
    enum ECurrency
    {
        BTC,
        LTC,
        DOGE,
        USD,
        EUR
    };

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConverterForm());
        }

        static string HttpGet(string uri)
        {
            var response = WebRequest.Create(uri).GetResponse();
            var sr = new StreamReader(response.GetResponseStream());
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Returns the marketid needed to find the conversion rate between to currencies.
        /// 
        /// If @out does not have its own market or is less significant than @out, then @in's market must be 
        /// used instead. In this case, swapNeeded is set to true so that the caller knows that the actual
        /// conversion rate is the reciprocal of the output.
        /// </summary>
        /// <param name="@in"></param>
        /// <param name="@out"></param>
        /// <param name="swapNeeded">
        /// True if the conversion rate for this marketid needs to be reciprocated to get the correct rate.
        /// </param>
        /// <returns></returns>
        static string GetMarketId(Currency @in, Currency @out, out bool swapNeeded)
        {
            Currency currency;
            Currency market;

            if (@out.value > @in.value)
            {
                currency = @in;
                market = @out;
                swapNeeded = true;
            } else
            {
                currency = @out;
                market = @in;
                swapNeeded = false;
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

            var response = HttpGet("https://www.cryptsy.com/api/v2/markets/" + marketId);

            var json = JObject.Parse(response);
            var price = decimal.Parse(json["data"]["last_trade"]["price"].ToString(), NumberStyles.Float);

            if (swapNeeded)
                return 1 /price;
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
            if (amount == 0)
                return 0;

            var price = GetPrice(@in, @out);
            return price * amount;
        }
    }

    class Currency
    {
        public readonly ECurrency currency;
        public readonly int value;

        public static readonly List<ECurrency> Markets = new List<ECurrency>(
            new ECurrency[] {
                ECurrency.LTC, ECurrency.BTC, ECurrency.USD
            }
        );

        public Currency(ECurrency currency)
        {
            this.currency = currency;

            if (HasOwnMarket())
                value = Markets.IndexOf(currency) + 1;
            else
                value = 0;
        }

        public bool HasOwnMarket()
        {
            return Markets.Contains(currency);
        }

        public override string ToString()
        {
            return currency.ToString();
        }

        public override bool Equals(object other)
        {
            return currency == ((Currency)other).currency;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
         
    }

    
}
