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
		public string PortName { get; set; }
		public string BaudRate { get; set; }
		public string Parity { get; set; }
		public string StopBits { get; set; }
		public string DataBits { get; set; }

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
