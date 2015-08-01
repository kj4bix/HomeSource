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
using System.Timers;
using PCComm;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ICMemory
{
	public partial class Form1 : Form
	{
		List<IcomMemory> oMemories = new List<IcomMemory>();
		public enum ResponseType { FB, VAL }

		public CommunicationManager oComm = new CommunicationManager();
		public SerialPortSetup oSerialSettings = new SerialPortSetup();
		string transType = string.Empty;
		private ResponseType _responseType;

		public ResponseType CurrentResponseType
		{
			get { return _responseType; }
			set { _responseType = value; }
		}
		private string sResult = string.Empty;
		private string sPrevious = string.Empty;
		private string sError = string.Empty;
	
		public Form1()
		{
			InitializeComponent();
			oComm.Display = false;
			oComm.DisplayWindow = this.rtbDisplay;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			SetDefaults();
			SetControlState();
		}

		private void SetDefaults()
		{
			;
		}
		private void SetControlState()
		{
			;
		}

		private bool OpenPort()
		{
			if (!string.IsNullOrEmpty(this.oSerialSettings.PortName))
				oComm.PortName = this.oSerialSettings.PortName;
			if (!string.IsNullOrEmpty(this.oSerialSettings.Parity))
				oComm.Parity = this.oSerialSettings.Parity;
			if (!string.IsNullOrEmpty(this.oSerialSettings.StopBits))
				oComm.StopBits = this.oSerialSettings.StopBits;
			if (!string.IsNullOrEmpty(this.oSerialSettings.DataBits))
				oComm.DataBits = this.oSerialSettings.DataBits;
			if (!string.IsNullOrEmpty(this.oSerialSettings.BaudRate))
				oComm.BaudRate = this.oSerialSettings.BaudRate;

			return (oComm.OpenPort());
		}


		private void btnWriteMemory_Click(object sender, EventArgs e)
		{
			this.OpenPort();
			this.oComm.CurrentTransmissionType = CommunicationManager.TransmissionType.Hex;

			// Spin through the memory banks, looking for matching banks in oMemories data
			string sCommand = string.Empty;
			for (int iMem=1; iMem<=5; iMem++)
			{
				// Switch to Memory bank
				sCommand = "FE FE 70 E0 08 A0 " + iMem.ToString("D2") + " FD";
				this.oComm.WriteData(sCommand);
				this.CurrentResponseType = ResponseType.FB;
				string sResult = this.WaitResponse();

				// Loop on the individual entries (99 on a 7000)
				for (int i=1; i<= 99; i++)
				{
					sCommand = "FE FE 70 E0 08 00 " + i.ToString("D2") + " FD";
					this.CurrentResponseType = ResponseType.FB;
					this.oComm.WriteData(sCommand);
					sResult = this.WaitResponse();

					// Clear what is there
					sCommand = "FE FE 70 E0 0B FD";
					this.CurrentResponseType = ResponseType.FB;
					this.oComm.WriteData(sCommand);
					sResult = this.WaitResponse();

					// Do we have data for this bank/memory?
					string sQuery = string.Empty;
					IcomMemory oMem = this.oMemories.Find(q => q.Bank == iMem.ToString() && q.MemChannel == i.ToString());
					if (oMem == null) continue;

					// Set the frequency
					sCommand = "FE FE 70 E0 00 ";
					string sOutput = ConvertFrequency(oMem.Frequency);
					sCommand += sOutput;
					sCommand += " FD";
					this.CurrentResponseType = ResponseType.FB;
					this.oComm.WriteData(sCommand);
					// No response from setting the frequency

					// Set the mode and filter width
					sCommand = "FE FE 70 E0 01 ";
					sOutput = ConvertMode(oMem.Mode);
					sCommand += sOutput + " ";
					sOutput = ConvertFilter(oMem.Filter);
					sCommand += sOutput + " FD";
					this.CurrentResponseType = ResponseType.FB;
					this.oComm.WriteData(sCommand);









				}
			}

		}
		
		private void btnReadMemory_Click(object sender, EventArgs e)
		{
			this.OpenPort();

			List<string> oList = new List<string>();

			this.oComm.CurrentTransmissionType = CommunicationManager.TransmissionType.Hex;
			// Let's iterate the memory banks 
			string sCommand = string.Empty;
			for (int iMem=1; iMem <= 5; iMem++) // 1, 2, 3, 4, 5 for A-E (for the 7000)
			{
				// Switch to Memory Bank
				// Command looks like this: "FE FE 70 E0 08 A0 01 FD";
				sCommand = "FE FE 70 E0 08 A0 " + iMem.ToString("D2") + " FD";
				this.oComm.WriteData(sCommand);
				this.CurrentResponseType = ResponseType.FB;
				string sResult = this.WaitResponse();
				// Now loop on the individual entries (99 on a 7000)
				for (int i = 1; i <= 99; i++)
				{
					// Looks like this: "FE FE 70 E0 08 00 01 FD"
					sCommand = "FE FE 70 E0 08 00 " + i.ToString("D2") + " FD";
					this.CurrentResponseType = ResponseType.FB;
					this.oComm.WriteData(sCommand);
					sResult = this.WaitResponse();

					// Execute command and have it wait for confirmation and a result
					string sQuery = string.Empty;
					IcomMemory oMem = new IcomMemory();
					oMem.Bank = iMem.ToString();
					oMem.MemChannel = i.ToString();

					// Get the frequency
					sQuery = "FE FE 70 E0 03 FD";
					this.CurrentResponseType = ResponseType.VAL;
					this.oComm.WriteData(sQuery);
					sResult = this.WaitResponse();
					if (!ConvertFrequency(sResult, ref oMem)) continue;
					// Get the mode
					sQuery = "FE FE 70 E0 04 FD";
					this.CurrentResponseType = ResponseType.VAL;
					this.oComm.WriteData(sQuery);
					sResult = this.WaitResponse();
					if (!ConvertMode(sResult, ref oMem)) continue;
					// Offset?
					sQuery = "FE FE 70 E0 0C FD";
					this.CurrentResponseType = ResponseType.VAL;
					this.oComm.WriteData(sQuery);
					sResult = this.WaitResponse();
					if (!ConvertOffset(sResult, ref oMem)) continue;
					// Tone
					sQuery = "FE FE 70 E0 1B 00 FD";
					this.CurrentResponseType = ResponseType.VAL;
					this.oComm.WriteData(sQuery);
					sResult = this.WaitResponse();
					if (!ConvertTone(sResult, ref oMem)) continue;


					oMemories.Add(oMem);
				}
			}
			foreach (IcomMemory oMem in oMemories)
			{
				oList.Add(oMem.Bank + "," + oMem.MemChannel + "," + oMem.Frequency + "," + oMem.Mode + "," + oMem.Filter + "," + 
					oMem.Offset + "," + oMem.Tone);
			}

			System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<IcomMemory>));
			TextWriter Filestream = new StreamWriter(@"c:\temp\Memories.xml");
			writer.Serialize(Filestream, oMemories);
			Filestream.Close();
			

			System.IO.File.WriteAllLines(@"c:\temp\freq.txt", oList.ToArray());
			this.oComm.Close();
		}

		private bool ConvertTone(string sInput, ref IcomMemory oMem)
		{
			string sTone = string.Empty;
			if (sInput == string.Empty) return false;

			// Tone is in this format: "FE FE 70 E0 1B 00 followed by the tone frequency like this
			//  32 1.1 would be 321.1 hz
			string[] sValues = sInput.Split(' ');
			if (sValues.Length < 10) return false;
			int iTens = Convert.ToInt32(sValues[7]);
			int iTenths = Convert.ToInt32(sValues[8]);
			double dTon = iTens*10.0 + iTenths*0.1;
			sTone = dTon.ToString();
			oMem.Tone = sTone;
			return true;
		}

		private bool ConvertOffset(string sInput, ref IcomMemory oMem)
		{
			string sOffset = string.Empty;
			if (sInput == string.Empty) return false;

			// Offset should be in this format: "FE FE 70 E0 OC followed by the frequency like this
			// 21 43 65 where these numbers are the location of the digits
			// 

			string[] sValues = sInput.Split(' ');
			if (sValues.Length < 9) return false; // Should be 9 in the string
			int iHundreds = Convert.ToInt32(sValues[5]);
			int iTenThousands = Convert.ToInt32(sValues[6]);
			int iMeg = Convert.ToInt32(sValues[7]);
			int iFrequency = iHundreds * 100 + iTenThousands * 10000 + iMeg * 1000000;
			double dFrequency = iFrequency / 1000000.0;
			sOffset = dFrequency.ToString();
			oMem.Offset = sOffset;
			return true;
		}

		private string ConvertMode(string sInput)
		{
			string sOutput = string.Empty;
			int iMode = 0;
			switch (sInput)
			{
				case "LSB":
					iMode = 0;
					break;
				case "USB":
					iMode = 1;
					break;
				case "AM":
					iMode = 2;
					break;
				case "CW":
					iMode = 3;
					break;
				case "RTTY":
					iMode = 4;
					break;
				case "FM":
					iMode = 5;
					break;
				case "WFM":
					iMode = 6;
					break;
				case "CW-R":
					iMode = 7;
					break;
				case "RTTY-R":
					iMode = 8;
					break;
			}
			sOutput = iMode.ToString("D2");
			return sOutput;
		}

		private bool ConvertMode(string sInput, ref IcomMemory oMem)
		{
			string sMode = string.Empty;
			if (sInput == string.Empty) return false;

			string[] sValues = sInput.Split(' ');
			if (sValues.Length < 7) return false;

			string sValue = sValues[5];
			if (string.IsNullOrEmpty(sValue)) return false; 
			if (sValue == "FF") return false; // Blank Memory

			// Mode should be in this format: "FE FE 70 E0 04 XX FD"
			int iMode = Convert.ToInt32(sValue);
			switch (iMode)
			{
				case 0:
					sMode = "LSB";
					break;
				case 1:
					sMode = "USB";
					break;
				case 2:
					sMode = "AM";
					break;
				case 3:
					sMode = "CW";
					break;
				case 4:
					sMode = "RTTY";
					break;
				case 5:
					sMode = "FM";
					break;
				case 6:
					sMode = "WFM";
					break;
				case 7:
					sMode = "CW-R";
					break;
				case 8:
					sMode = "RTTY-R";
					break;
			}
			oMem.Mode = sMode;
			sValue = sValues[6];
			if (string.IsNullOrEmpty(sValue)) return true;

			string sWidth = string.Empty;
			int iFilter = Convert.ToInt32(sValue);
			if (iFilter > 0 && iFilter < 4)
				sWidth = "Filter  " + iFilter.ToString();
			oMem.Filter = sWidth;
			return true;
		}
		private string ConvertFilter(string sFilter)
		{
			int iFilter = 0;
			string sWidth = sFilter.Substring(sFilter.Length - 1, 1);
			iFilter = Convert.ToInt32(sWidth);
			string sOutput = iFilter.ToString("D2");
			return sOutput;
		}

		private string ConvertFrequency(string sFrequency)
		{
			double dFrequency = Convert.ToDouble(sFrequency);
			string[] sValues = sFrequency.Split('.');
			int iMegs = Convert.ToInt32(sValues[0]);
			int iDecimal = Convert.ToInt32(sValues[1]);
			string sMegs = iMegs.ToString();
			sMegs = sMegs.PadLeft(4, '0');
			string sDecimal = iDecimal.ToString();
			sDecimal = sDecimal.PadRight(6, '0');
			string sOutput = string.Empty;
			sOutput = sDecimal.Substring(5, 2);
			sOutput += " ";
			sOutput += sDecimal.Substring(3, 2);
			sOutput += " ";
			sOutput += sDecimal.Substring(1, 2);
			sOutput += " ";
			sOutput += sMegs.Substring(1, 2);
			sOutput += " ";
			sOutput += sMegs.Substring(2, 2);
			return sOutput;
		}
		private bool ConvertFrequency(string sInput, ref IcomMemory oMem)
		{
			string sFrequency = string.Empty;
			if (sInput == string.Empty) return false;

			// Frequency should be in this format: "FE FE 70 E0 03 followed by the frequency like this
			// 21 43 65 87 09 where these numbers are the location of the digits
			// 0987654321 for example

			string[] sValues = sInput.Split(' ');
			if (sValues.Length < 11) return false; // Should be 11 in the string
			int iTens = Convert.ToInt32(sValues[5]);
			int iHundreds = Convert.ToInt32(sValues[6]);
			int iTenThousands = Convert.ToInt32(sValues[7]);
			int iMeg = Convert.ToInt32(sValues[8]);
			int iHundredMeg = Convert.ToInt32(sValues[9]);
			int iFrequency = iTens + iHundreds * 100 + iTenThousands * 10000 + iMeg * 1000000 + iHundredMeg * 100000000;
			double dFrequency = iFrequency / 1000000.0;
			sFrequency = dFrequency.ToString();
			oMem.Frequency = sFrequency;
			return true;
		}

		private string LastCommandReorder()
		{
			string sLastCommand = this.oComm.PeekResponse();
			// Reorder the sending and receiving addresses
			string[] sSplit = sLastCommand.Split(' ');
			// Reverse 3 and 4
			string sPreviousCommand = string.Empty;
			for (int i = 0; i < 2; i++)
			{
				sPreviousCommand += sSplit[i] + " ";
			}
			sPreviousCommand += sSplit[3] + " ";
			sPreviousCommand += sSplit[2] + " ";
			for (int i = 4; i < sSplit.Length; i++)
			{
				sPreviousCommand += sSplit[i] + " ";
			}
			return sPreviousCommand;
		}

		private string WaitResponse()
		{
			string sResponse = string.Empty;
			bool complete = false;
			while (!complete)
			{
				this.oComm.CreateResponses();
				if (this.oComm.ResponseCount() > 1)
				{
					sResponse = this.oComm.PopResponse(); // First is the command
					sResponse = this.oComm.PopResponse(); // Second is the response
					if (this.CurrentResponseType == ResponseType.FB)
					{
						if (sResponse.Contains("FB"))
						{
							return "FB";
						}
						else
						{
							return "FA";
						}
					}
					else
					{
						complete = true;
					}
				}
			}
			return sResponse;
		}

		private void btnSetup_Click(object sender, EventArgs e)
		{
			SerialSetup oSerialForm = new SerialSetup();
			oSerialForm.oComm = this.oComm;
			oSerialForm.oSerial = this.oSerialSettings;
			if (oSerialForm.ShowDialog() == DialogResult.OK)
			{
				this.oSerialSettings = oSerialForm.oSerial;
			}
		}

		private void btnReadFile_Click(object sender, EventArgs e)
		{
			List<string> oList = new List<string>();

			OpenFileDialog oFileDlg = new OpenFileDialog();
			oFileDlg.Filter = "XML File (*.xml)|*.xml;";
			if (oFileDlg.ShowDialog() == DialogResult.OK)
			{
				XmlSerializer Deserializer = new XmlSerializer(typeof(List<IcomMemory>));
				FileStream Filestream = new FileStream(oFileDlg.FileName, FileMode.Open);
				oMemories = (List<IcomMemory>)Deserializer.Deserialize(Filestream);
				Filestream.Close();
			}
		}

	}
}
