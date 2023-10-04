using System.IO;
using System.Text;

namespace BinPat
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			int bytesPerRow = int.Parse(textBoxBytesPerRow.Text);
			if(bytesPerRow < 1)
			{
				bytesPerRow = 1;
			}

			if(openFileDialog.ShowDialog(this) != DialogResult.OK)
			{
				textBoxFilename.Text = string.Empty;
				textBoxOutput.Text = string.Empty;
				return;
			}

			textBoxFilename.Text = openFileDialog.FileName;
			textBoxOutput.Text = string.Empty;
			try
			{
				StringBuilder sb = new StringBuilder();
				byte[] rowBytes = new byte[bytesPerRow];
				using(BinaryReader reader = new BinaryReader(File.Open(openFileDialog.FileName, FileMode.Open)))
				{
					int curRowByte = 0;
					while(reader.BaseStream.Position != reader.BaseStream.Length)
					{
						byte b = reader.ReadByte();
						rowBytes[curRowByte] = b;
						if(ShouldIncludeByte(curRowByte))
						{
							for(int i = 0; i < 8; i++)
							{
								int bit = b & 0x01;
								b >>= 1;
								sb.Append(bit);
								sb.Append(',');
							}
						}

						curRowByte++;
						if(curRowByte == bytesPerRow)
						{
							for(int i = 0; i < bytesPerRow; i++)
							{
								char byteChar = '.';
								if(rowBytes[i] >= 32 && rowBytes[i] <= 127)
								{
									byteChar = (char)rowBytes[i];
								}
								if(ShouldIncludeByte(i))
								{
									sb.Append(byteChar);
									sb.Append(',');
								}
							}
							sb.Append(Environment.NewLine);
							curRowByte = 0;
						}
					}
				}

				textBoxOutput.Text = sb.ToString();
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private bool ShouldIncludeByte(int curRowByte)
		{
			return curRowByte != 0;
		}
	}
}