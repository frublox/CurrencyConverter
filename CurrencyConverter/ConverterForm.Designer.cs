namespace CurrencyConverter
{
    partial class ConverterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputPanel = new System.Windows.Forms.Panel();
            this.currencyIn = new System.Windows.Forms.ComboBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.currencyOut = new System.Windows.Forms.ComboBox();
            this.currencyOutput = new System.Windows.Forms.TextBox();
            this.currencyInput = new System.Windows.Forms.TextBox();
            this.inputPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.Controls.Add(this.currencyIn);
            this.inputPanel.Controls.Add(this.convertButton);
            this.inputPanel.Controls.Add(this.currencyOut);
            this.inputPanel.Controls.Add(this.currencyOutput);
            this.inputPanel.Controls.Add(this.currencyInput);
            this.inputPanel.Location = new System.Drawing.Point(12, 12);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(215, 83);
            this.inputPanel.TabIndex = 0;
            // 
            // currencyIn
            // 
            this.currencyIn.FormattingEnabled = true;
            this.currencyIn.Items.AddRange(new object[] {
            "BTC",
            "LTC",
            "DOGE",
            "USD"});
            this.currencyIn.Location = new System.Drawing.Point(148, 3);
            this.currencyIn.Name = "currencyIn";
            this.currencyIn.Size = new System.Drawing.Size(53, 21);
            this.currencyIn.TabIndex = 5;
            // 
            // convertButton
            // 
            this.convertButton.Location = new System.Drawing.Point(4, 55);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(75, 23);
            this.convertButton.TabIndex = 4;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // currencyOut
            // 
            this.currencyOut.FormattingEnabled = true;
            this.currencyOut.Items.AddRange(new object[] {
            "BTC",
            "LTC",
            "DOGE",
            "USD"});
            this.currencyOut.Location = new System.Drawing.Point(148, 29);
            this.currencyOut.Name = "currencyOut";
            this.currencyOut.Size = new System.Drawing.Size(53, 21);
            this.currencyOut.TabIndex = 3;
            // 
            // currencyOutput
            // 
            this.currencyOutput.Enabled = false;
            this.currencyOutput.Location = new System.Drawing.Point(4, 29);
            this.currencyOutput.Name = "currencyOutput";
            this.currencyOutput.Size = new System.Drawing.Size(127, 20);
            this.currencyOutput.TabIndex = 2;
            // 
            // currencyInput
            // 
            this.currencyInput.Location = new System.Drawing.Point(3, 3);
            this.currencyInput.Name = "currencyInput";
            this.currencyInput.Size = new System.Drawing.Size(128, 20);
            this.currencyInput.TabIndex = 1;
            // 
            // PriceCheckerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 107);
            this.Controls.Add(this.inputPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(255, 141);
            this.MinimumSize = new System.Drawing.Size(255, 141);
            this.Name = "PriceCheckerForm";
            this.ShowIcon = false;
            this.Text = "Currency Converter";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PriceCheckerForm_KeyPress);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.TextBox currencyInput;
        internal System.Windows.Forms.TextBox currencyOutput;
        internal System.Windows.Forms.ComboBox currencyOut;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.ComboBox currencyIn;
    }
}

