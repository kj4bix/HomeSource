using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMemory
{
	public class IcomMemory
	{
		private string sBank;
		private string sMemChannel;
		private string sFrequency;
		private string sMode;
		private string sFilter;
		private string sOffset;
		private string sTone;

		public string Bank
		{
			get { return sBank; }
			set { sBank = value; }
		}
		public string MemChannel
		{
			get { return sMemChannel; }
			set { sMemChannel = value; }
		}
		public string Frequency
		{
			get { return sFrequency; }
			set { sFrequency = value; }
		}
		public string Mode
		{
			get { return sMode; }
			set { sMode = value; }
		}
		public string Filter
		{
			get { return sFilter; }
			set { sFilter = value; }
		}

		public string Offset
		{
			get { return sOffset; }
			set { sOffset = value; }
		}

		public string Tone
		{
			get { return sTone; }
			set { sTone = value; }
		}

	

	}
}
