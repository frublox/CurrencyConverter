using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Windows.Forms;



namespace CurrencyConverter
{
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

        public static string HttpGet(string uri)
        {
            var response = WebRequest.Create(uri).GetResponse();
            var sr = new StreamReader(response.GetResponseStream());
            return sr.ReadToEnd();
        }
    }    
}
