using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;

using Newtonsoft.Json.Linq;

namespace CurrencyConverter
{
    static class Program
    {
        /// <summary>
        /// A mapping between strings of currency names and corresponding Currency values.
        /// </summary>
        public static Dictionary<string, Currency> StrToCurrency = new Dictionary<string, Currency>();

        private static Currency[] majorCurrencies = {
            Currency.BTC,
            Currency.USD,
            Currency.EUR
        };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitStrToCurrency();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConverterForm());
        }

        static void InitStrToCurrency()
        {
            string[] strings = Enum.GetNames(typeof(Currency));
            Currency[] currencies = (Currency[])Enum.GetValues(typeof(Currency));

            for (int i = 0; i < currencies.Length; i++)
            {
                StrToCurrency.Add(strings[i], currencies[i]);
            }
        }

        static string HttpGet(string uri)
        {
            WebResponse response = WebRequest.Create(uri).GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            return sr.ReadToEnd();
        }

        static string GetMarketId(Currency from, Currency to, out bool swapNeeded)
        {
            Currency currency;
            Currency market;
            if (from == Currency.USD || from == Currency.BTC || from == Currency.EUR)
            {
                market = from;
                currency = to;
                swapNeeded = true;
            } else
            {
                market = to;
                currency = from;
                swapNeeded = false;
            }

            return currency.ToString() + "_" + market.ToString();
        }

        /// <summary>
        /// Gets the latest price for the specified market.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>The latest trade price for the market.</returns>
        static decimal GetPrice(Currency from, Currency to)
        {
            if (from.Equals(to))
                return 1;

            bool swapNeeded = false;
            string marketId = GetMarketId(from, to, out swapNeeded);

            string response = HttpGet("https://www.cryptsy.com/api/v2/markets/" + marketId);

            JObject json = JObject.Parse(response);
            decimal price = decimal.Parse(json["data"]["last_trade"]["price"].ToString(), NumberStyles.Float);

            if (swapNeeded)
                return price;
            else
                return 1 / price;
        }

        /// <summary>
        /// Converts an amount from one currency to another.
        /// </summary>
        /// <param name="fromCurrency">Currency to convert from</param>
        /// <param name="toCurrency">Currency to convert to</param>
        /// <param name="amount">Amount to convert</param>
        /// <returns></returns>
        public static decimal Convert(Currency fromCurrency, Currency toCurrency, decimal amount)
        {
            if (amount == 0)
                return 0;

            decimal price = GetPrice(fromCurrency, toCurrency);
            return price * amount;
        }
    }

    enum Currency {
        BTC,
        LTC,
        DOGE,
        USD,
        EUR
    };
}
