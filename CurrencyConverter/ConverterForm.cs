using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurrencyConverter
{
    public partial class ConverterForm : Form
    {
        public ConverterForm()
        {
            InitializeComponent();

            init();
        }

        private void init()
        {
            currencyIn.SelectedIndex = 0;
            currencyOut.SelectedIndex = 3;
            currencyInput.Text = "1";
        }

        private void swapCurrencies()
        {
            int temp = currencyIn.SelectedIndex;
            currencyIn.SelectedIndex = currencyOut.SelectedIndex;
            currencyOut.SelectedIndex = temp;
        }

        private void calculateOutput()
        {
            Currency @in = getInputCurrency();
            Currency @out = getOutputCurrency();

            string amountAsString = currencyInput.Text;
            decimal amount = 0;

            if (decimal.TryParse(amountAsString, out amount))
            {
                var output = Program.Convert(@in, @out, amount);
                currencyOutput.Text = output.ToString();
            }
            else
            {
                currencyOutput.Text = "Please enter a valid number.";
            }
        }

        private Currency toCurrency(string currencyAsString)
        {
            return new Currency((ECurrency)Enum.Parse(typeof(ECurrency), currencyAsString));
        }

        private Currency getInputCurrency()
        {
            var selected = currencyOut.SelectedItem.ToString();
            return toCurrency(selected);
        }

        private Currency getOutputCurrency()
        {
            var selected = currencyIn.SelectedItem.ToString();
            return toCurrency(selected);
        }

        private void PriceCheckerForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                calculateOutput();
            }
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            calculateOutput();
        }

        private void swapButton_Click(object sender, EventArgs e)
        {
            swapCurrencies();
        }
    }
}
