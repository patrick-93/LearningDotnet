using Serilog;

namespace DotnetClient
{
	public class Data
	{
		public long DataId { get; set; }
		public string Text { get; set; }

		public Data()
		{
			this.Text = "";
		}
	}
}