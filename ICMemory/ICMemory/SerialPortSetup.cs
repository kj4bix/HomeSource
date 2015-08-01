using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMemory
{
	[System.Serializable]

	public class SerialPortSetup
	{
		private string sPortName = string.Empty;

		public string PortName
		{
			get { return sPortName; }
			set { sPortName = value; }
		}
		private string sBaudRate = string.Empty;

		public string BaudRate
		{
			get { return sBaudRate; }
			set { sBaudRate = value; }
		}
		private string sParity = string.Empty;

		public string Parity
		{
			get { return sParity; }
			set { sParity = value; }
		}
		private string sStopBits = string.Empty;

		public string StopBits
		{
			get { return sStopBits; }
			set { sStopBits = value; }
		}
		private string sDataBits = string.Empty;

		public string DataBits
		{
			get { return sDataBits; }
			set { sDataBits = value; }
		}

		public SerialPortSetup()
		{
			Init();
		}

		private void Init()
		{
			PortName = "COM5";
			BaudRate = "9600";
			Parity = "None";
			StopBits = "1";
			DataBits = "8";
			return;
		}

		public bool LoadValues()
		{
			return true;
		}

		public bool LoadPortNames(ComboBox cmbo)
		{
			foreach (string str in SerialPort.GetPortNames())
			{
				cmbo.Items.Add(str);
			}
			return true;
		}
	}
}
