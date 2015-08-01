using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCComm;

namespace ICMemory
{
	public partial class SerialSetup : Form
	{
		public CommunicationManager oComm = new CommunicationManager();
		public SerialPortSetup oSerial = new SerialPortSetup();

		public SerialSetup()
		{
			InitializeComponent();
			LoadValues();
		}

		private void LoadValues()
		{
			oSerial.LoadPortNames(this.cmboPort);
		}

		private void btnSetPort_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.cmboPort.Text))
				this.oComm.PortName = this.cmboPort.Text;
			if (!string.IsNullOrEmpty(this.cmboBaud.Text))
				this.oComm.BaudRate = this.cmboBaud.Text;
		}
	}
}
