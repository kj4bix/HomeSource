namespace ICMemory
{
	partial class MainForm
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
			this.btnSetup = new System.Windows.Forms.Button();
			this.btnReadMemory = new System.Windows.Forms.Button();
			this.rtbDisplay = new System.Windows.Forms.RichTextBox();
			this.btnReadFile = new System.Windows.Forms.Button();
			this.btnWriteMemory = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnSetup
			// 
			this.btnSetup.Location = new System.Drawing.Point(13, 13);
			this.btnSetup.Name = "btnSetup";
			this.btnSetup.Size = new System.Drawing.Size(75, 23);
			this.btnSetup.TabIndex = 0;
			this.btnSetup.Text = "Setup";
			this.btnSetup.UseVisualStyleBackColor = true;
			this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
			// 
			// btnReadMemory
			// 
			this.btnReadMemory.Location = new System.Drawing.Point(94, 13);
			this.btnReadMemory.Name = "btnReadMemory";
			this.btnReadMemory.Size = new System.Drawing.Size(106, 23);
			this.btnReadMemory.TabIndex = 2;
			this.btnReadMemory.Text = "Read Memory";
			this.btnReadMemory.UseVisualStyleBackColor = true;
			this.btnReadMemory.Click += new System.EventHandler(this.btnReadMemory_Click);
			// 
			// rtbDisplay
			// 
			this.rtbDisplay.Location = new System.Drawing.Point(11, 95);
			this.rtbDisplay.Name = "rtbDisplay";
			this.rtbDisplay.Size = new System.Drawing.Size(543, 234);
			this.rtbDisplay.TabIndex = 4;
			this.rtbDisplay.Text = "";
			// 
			// btnReadFile
			// 
			this.btnReadFile.Location = new System.Drawing.Point(206, 13);
			this.btnReadFile.Name = "btnReadFile";
			this.btnReadFile.Size = new System.Drawing.Size(106, 23);
			this.btnReadFile.TabIndex = 5;
			this.btnReadFile.Text = "Read File";
			this.btnReadFile.UseVisualStyleBackColor = true;
			this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
			// 
			// btnWriteMemory
			// 
			this.btnWriteMemory.Location = new System.Drawing.Point(319, 13);
			this.btnWriteMemory.Name = "btnWriteMemory";
			this.btnWriteMemory.Size = new System.Drawing.Size(106, 23);
			this.btnWriteMemory.TabIndex = 6;
			this.btnWriteMemory.Text = "Write Memory";
			this.btnWriteMemory.UseVisualStyleBackColor = true;
			this.btnWriteMemory.Click += new System.EventHandler(this.btnWriteMemory_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(565, 425);
			this.Controls.Add(this.btnWriteMemory);
			this.Controls.Add(this.btnReadFile);
			this.Controls.Add(this.rtbDisplay);
			this.Controls.Add(this.btnReadMemory);
			this.Controls.Add(this.btnSetup);
			this.Name = "Form1";
			this.Text = "IC Memory Manager";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnSetup;
		private System.Windows.Forms.Button btnReadMemory;
		private System.Windows.Forms.RichTextBox rtbDisplay;
		private System.Windows.Forms.Button btnReadFile;
		private System.Windows.Forms.Button btnWriteMemory;
	}
}

