using System.IO;
using System.Security.Permissions;
using System.Text;

namespace BinPat
{
	public partial class Form1 : Form
	{
		public static readonly int[,] Sections = new int[,]
		{
			//	Start		Section Size
			{   0x0000,     0   }, // Header
			{   0x0312,     16  }, // Character Sprites
			{   0x04F2,     0   }, // ???
			{   0x07A5,     8   }, // Tiles
			{   0x0845,     0   }, // ???
			{	0xFFFF,		0	}, // EOF
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
			byte[] patternData = new byte[lastDataByte / 2 + 1];
			curByte = 0;
			while(curByte < patternData.Length)
			{
				patternData[curByte] = buffer[curByte * 2 + 1];
				curByte++;
			}

			// Output!
			curByte = 0;
			int curRow = 0;
			int curSection = 0;
			int curSectionRow = 0;
			AppendDivider(sb);
			while(curByte < patternData.Length)
			{
				// Are we into the next section?
				if(curRow >= Sections[curSection + 1, 0])
				{
					curSection++;
					curSectionRow = 0;
					AppendDivider(sb);
				}
				sb.Append("-,");
				byte b = patternData[curByte];
				for(int i = 0; i < 8; i++)
				{
					sb.Append(b & 0x01);
					sb.Append(',');
					b >>= 1;
				}
				sb.Append("-,");
				if(patternData[curByte] >= 0x20 && patternData[curByte] <= 0x7D)
				{
					sb.Append((char)patternData[curByte]);
					sb.Append(',');
				}
				else
				{
					sb.Append(".,");
				}
				sb.AppendFormat("0x{0:X4}", curRow);
				curByte++;
				sb.Append(Environment.NewLine);
				curRow++;
				curSectionRow++;
				if(Sections[curSection, 1] > 0 && curSectionRow % Sections[curSection, 1] == 0)
				{
					AppendDivider(sb);
				}
			}
			AppendDivider(sb);

			textBoxOutput.Text = sb.ToString();
		}

		private void AppendDivider(StringBuilder sb)
		{
			for(int i = 0; i < 10; i++)
			{
				sb.Append("-,");
			}
			sb.Append(Environment.NewLine);
		}
	}
}