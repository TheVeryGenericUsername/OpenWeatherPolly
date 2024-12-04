namespace OpenWeatherPolly
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtCity = new TextBox();
            lblWeather = new Label();
            btnExit = new Button();
            btnFetchWeather = new Button();
            SuspendLayout();
            // 
            // txtCity
            // 
            txtCity.Font = new Font("Segoe UI", 16F);
            txtCity.Location = new Point(24, 160);
            txtCity.Multiline = true;
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(256, 40);
            txtCity.TabIndex = 0;
            txtCity.Text = "City Name";
            txtCity.Click += txtCity_Text_Click;
            // 
            // lblWeather
            // 
            lblWeather.Font = new Font("Segoe UI", 16F);
            lblWeather.Location = new Point(32, 32);
            lblWeather.Name = "lblWeather";
            lblWeather.Size = new Size(528, 104);
            lblWeather.TabIndex = 1;
            lblWeather.Text = "Weather";
            lblWeather.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Segoe UI", 16F);
            btnExit.Location = new Point(504, 160);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(56, 40);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnFetchWeather
            // 
            btnFetchWeather.Font = new Font("Segoe UI", 16F);
            btnFetchWeather.Location = new Point(296, 160);
            btnFetchWeather.Name = "btnFetchWeather";
            btnFetchWeather.Size = new Size(192, 40);
            btnFetchWeather.TabIndex = 3;
            btnFetchWeather.Text = "Fetch Weather";
            btnFetchWeather.UseVisualStyleBackColor = true;
            btnFetchWeather.Click += btnFetchWeather_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(584, 217);
            Controls.Add(btnFetchWeather);
            Controls.Add(btnExit);
            Controls.Add(lblWeather);
            Controls.Add(txtCity);
            Name = "Form1";
            Text = "Narrate Weather App";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCity;
        private Label lblWeather;
        private Button btnExit;
        private Button btnFetchWeather;
    }
}
