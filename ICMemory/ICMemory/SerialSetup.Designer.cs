namespace ICMemory
{
	partial class SerialSetup
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
			this.label1 = new System.Windows.Forms.Label();
			this.cmboPort = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmboBaud = new System.Windows.Forms.ComboBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(26, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Port";
			// 
			// cmboPort
			// 
			this.cmboPort.FormattingEnabled = true;
			this.cmboPort.Location = new System.Drawing.Point(88, 5);
			this.cmboPort.Name = "cmboPort";
			this.cmboPort.Size = new System.Drawing.Size(72, 21);
			this.cmboPort.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Baud Rate";
			// 
			// cmboBaud
			// 
			this.cmboBaud.FormattingEnabled = true;
			this.cmboBaud.Location = new System.Drawing.Point(88, 45);
			this.cmboBaud.Name = "cmboBaud";
			this.cmboBaud.Size = new System.Drawing.Size(72, 21);
			this.cmboBaud.TabIndex = 3;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(16, 84);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnSetPort_Click);
			// 
			// SerialSetup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 140);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.cmboBaud);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmboPort);
			this.Controls.Add(this.label1);
			this.Name = "SerialSetup";
			this.Text = "Port Setup";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmboPort;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmboBaud;
		private System.Windows.Forms.Button btnOK;
	}
}