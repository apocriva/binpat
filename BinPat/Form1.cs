using System.IO;
using System.Text;

namespace BinPat
{
	public partial class Form1 : Form
	{
		private const int BytesPerRow = 1;
		private const int BlockLength = 8;
		private const int BlockOffset = 2;
		private const bool SkipByte = true;

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
			try
			{
				StringBuilder sb = new StringBuilder();
				byte[] rowBytes = new byte[BytesPerRow];
				using(BinaryReader reader = new BinaryReader(File.Open(openFileDialog.FileName, FileMode.Open)))
				{
					int curRow = -BlockOffset;
					int curRowByte = 0;
					while(reader.BaseStream.Position != reader.BaseStream.Length)
					{
						byte b = reader.ReadByte();
						if(SkipByte)
						{
							b = reader.ReadByte();
						}
						rowBytes[curRowByte] = b;

						for(int i = 0; i < 8; i++)
						{
							int bit = b & 0x01;
							b >>= 1;
							sb.Append(bit);
							sb.Append(',');
						}

						curRowByte++;
						if(curRowByte == BytesPerRow)
						{
							for(int i = 0; i < BytesPerRow; i++)
							{
								char byteChar = '.';
								if(rowBytes[i] >= 32 && rowBytes[i] <= 127)
								{
									byteChar = (char)rowBytes[i];
								}
								sb.Append(byteChar);
								sb.Append(',');
							}
							sb.Append(Environment.NewLine);
							curRowByte = 0;

							curRow++;
							if(BlockLength > 0 && curRow > 0)
							{
								if(curRow % BlockLength == 0)
								{
									for(int i = 0; i < BytesPerRow; i++)
									{
										for(int j = 0; j < 8; j++)
										{
											sb.Append("-,");
										}
									}
									for(int i = 0; i < BytesPerRow; i++)
									{
										sb.Append("-,");
									}
									sb.Append(Environment.NewLine);
								}
							}
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
	}
}