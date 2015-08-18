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

            initFields();
        }

        private void initFields()
        {
            currencyIn.SelectedIndex = 0;
            currencyOut.SelectedIndex = 3;
            currencyInput.Text = "0";
        }

        private void calculateOutput()
        {
            Currency inputCurrency = getInputCurrency();
            Currency outputCurrency = getOutputCurrency();

            string text = currencyInput.Text;
            decimal amount = 0;

            if (decimal.TryParse(text, out amount))
            {
                var output = Program.Convert(inputCurrency, outputCurrency, amount);
                currencyOutput.Text = output.ToString();
            }
        }

        private Currency getInputCurrency()
        {
            var selected = currencyOut.SelectedItem.ToString();
            return Program.StrToCurrency[selected];
        }

        private Currency getOutputCurrency()
        {
            var selected = currencyIn.SelectedItem.ToString();
            return Program.StrToCurrency[selected];
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
    }
}
