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
            Currency @in = getCurrency(currencyIn);
            Currency @out = getCurrency(currencyOut);

            string amountAsString = currencyInput.Text;
            decimal amount = 0;

            if (decimal.TryParse(amountAsString, out amount))
            {
                decimal result = Currency.Convert(@in, @out, amount);
                currencyOutput.Text = result.ToString("F6");
            }
            else
            {
                currencyOutput.Text = "Invalid number!";
            }
        }

        private Currency parseCurrency(string str)
        {
            var eCurrency = (ECurrency)Enum.Parse(typeof(ECurrency), str);
            return new Currency(eCurrency);
        }

        private Currency getCurrency(ComboBox comboBox)
        {
            var selected = comboBox.SelectedItem.ToString();
            return parseCurrency(selected);
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            calculateOutput();
        }

        private void swapButton_Click(object sender, EventArgs e)
        {
            swapCurrencies();
        }

        private void input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                calculateOutput();
            }
        }
    }
}
