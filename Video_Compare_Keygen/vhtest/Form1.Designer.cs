namespace vhtest
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.rp = new System.Windows.Forms.RadioButton();
            this.re = new System.Windows.Forms.RadioButton();
            this.rh = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(27, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(270, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(27, 68);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(270, 20);
            this.textBox3.TabIndex = 4;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rp
            // 
            this.rp.AutoSize = true;
            this.rp.Checked = true;
            this.rp.Location = new System.Drawing.Point(27, 42);
            this.rp.Name = "rp";
            this.rp.Size = new System.Drawing.Size(41, 17);
            this.rp.TabIndex = 5;
            this.rp.TabStop = true;
            this.rp.Text = "Pro";
            this.rp.UseVisualStyleBackColor = true;
            this.rp.CheckedChanged += new System.EventHandler(this.rh_CheckedChanged);
            // 
            // re
            // 
            this.re.AutoSize = true;
            this.re.Location = new System.Drawing.Point(134, 42);
            this.re.Name = "re";
            this.re.Size = new System.Drawing.Size(55, 17);
            this.re.TabIndex = 6;
            this.re.Text = "Expert";
            this.re.UseVisualStyleBackColor = true;
            this.re.CheckedChanged += new System.EventHandler(this.rh_CheckedChanged);
            // 
            // rh
            // 
            this.rh.AutoSize = true;
            this.rh.Location = new System.Drawing.Point(235, 42);
            this.rh.Name = "rh";
            this.rh.Size = new System.Drawing.Size(53, 17);
            this.rh.TabIndex = 7;
            this.rh.Text = "Home";
            this.rh.UseVisualStyleBackColor = true;
            this.rh.CheckedChanged += new System.EventHandler(this.rh_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 100);
            this.Controls.Add(this.rh);
            this.Controls.Add(this.re);
            this.Controls.Add(this.rp);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Video Comparer 1.05 Keygen";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.RadioButton rp;
        private System.Windows.Forms.RadioButton re;
        private System.Windows.Forms.RadioButton rh;
    }
}

