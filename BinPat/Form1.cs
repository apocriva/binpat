using System.IO;
using System.Security.Permissions;
using System.Text;

namespace BinPat
{
	public partial class Form1 : Form
	{
		public static readonly int BlockSize = 8;
		public static readonly int NumColumns = 4;

		public static readonly int[,] Sections = new int[,]
		{
			{	// Start	Section Size
				0x0000,		0,
				0x0010,		8,
			}
		};

		public Form1()
		{
			InitializeComponent();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			if(openFileDialog.ShowDialog(this) != DialogResult.OK)
			{
				textBoxFilename.Text = string.Empty;
				textBoxOutput.Text = string.Empty;
				return;
			}

			textBoxFilename.Text = openFileDialog.FileName;
			textBoxOutput.Text = string.Empty;

			StringBuilder sb = new();
			byte[] buffer;
			using(BinaryReader reader = new(File.Open(openFileDialog.FileName, FileMode.Open)))
			{
				buffer = reader.ReadBytes((int)reader.BaseStream.Length);
			}

			if(buffer == null)
			{
				textBoxOutput.Text = "Oops!";
				return;
			}

			// Identify the end of meaningful data.
			int curByte = 0;
			int lastDataByte = 0;
			while(curByte < buffer.Length)
			{
				byte b = buffer[curByte];
				if(b != 0)
				{
					lastDataByte = curByte;
				}
				curByte++;
			}

			// We take that and halve it because we're dropping what doesn't look good.
			int pageSize = (lastDataByte / 2 + 1) / NumColumns;
			byte[] patternData = new byte[pageSize * NumColumns];
			curByte = 0;
			while(curByte < patternData.Length)
			{
				patternData[curByte] = buffer[curByte * 2 + 1];
				curByte++;
			}

			// Output!
			curByte = 0;
			int curRow = 0;
			while(curByte < pageSize)
			{
				sb.Append("-,");
				for(int column = 0; column < NumColumns; column++)
				{
					byte b = patternData[curByte + pageSize * column];
					for(int i = 0; i < 8; i++)
					{
						sb.Append(b & 0x01);
						sb.Append(',');
						b >>= 1;
					}
					sb.Append("-,");
				}
				curByte++;
				sb.Append(Environment.NewLine);
				curRow++;
				if(curRow % BlockSize == 0)
				{
					for(int i = 0; i < NumColumns * 9 + 1; i++)
					{
						sb.Append("-,");
					}
					sb.Append(Environment.NewLine);
				}
			}

			textBoxOutput.Text = sb.ToString();
		}
	}
}