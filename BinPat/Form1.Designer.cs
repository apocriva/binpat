namespace BinPat
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			btnLoad = new Button();
			openFileDialog = new OpenFileDialog();
			saveFileDialog = new SaveFileDialog();
			textBoxFilename = new TextBox();
			textBoxOutput = new TextBox();
			groupBox1 = new GroupBox();
			textBoxBytesPerRow = new TextBox();
			label1 = new Label();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// btnLoad
			// 
			btnLoad.Location = new Point(12, 188);
			btnLoad.Name = "btnLoad";
			btnLoad.Size = new Size(141, 23);
			btnLoad.TabIndex = 0;
			btnLoad.Text = "Load";
			btnLoad.UseVisualStyleBackColor = true;
			btnLoad.Click += btnLoad_Click;
			// 
			// openFileDialog
			// 
			openFileDialog.FileName = "file.bin";
			openFileDialog.Title = "Select Input File...";
			// 
			// saveFileDialog
			// 
			saveFileDialog.FileName = "output.csv";
			saveFileDialog.Title = "Save Output...";
			// 
			// textBoxFilename
			// 
			textBoxFilename.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBoxFilename.Location = new Point(159, 188);
			textBoxFilename.Name = "textBoxFilename";
			textBoxFilename.ReadOnly = true;
			textBoxFilename.Size = new Size(513, 23);
			textBoxFilename.TabIndex = 1;
			// 
			// textBoxOutput
			// 
			textBoxOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			textBoxOutput.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
			textBoxOutput.Location = new Point(12, 217);
			textBoxOutput.Multiline = true;
			textBoxOutput.Name = "textBoxOutput";
			textBoxOutput.ReadOnly = true;
			textBoxOutput.ScrollBars = ScrollBars.Both;
			textBoxOutput.Size = new Size(660, 361);
			textBoxOutput.TabIndex = 2;
			// 
			// groupBox1
			// 
			groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			groupBox1.Controls.Add(textBoxBytesPerRow);
			groupBox1.Controls.Add(label1);
			groupBox1.Location = new Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(660, 170);
			groupBox1.TabIndex = 3;
			groupBox1.TabStop = false;
			groupBox1.Text = "Settings";
			// 
			// textBoxBytesPerRow
			// 
			textBoxBytesPerRow.Location = new Point(176, 19);
			textBoxBytesPerRow.Name = "textBoxBytesPerRow";
			textBoxBytesPerRow.Size = new Size(100, 23);
			textBoxBytesPerRow.TabIndex = 1;
			textBoxBytesPerRow.Text = "2";
			// 
			// label1
			// 
			label1.Location = new Point(6, 19);
			label1.Name = "label1";
			label1.Size = new Size(164, 23);
			label1.TabIndex = 0;
			label1.Text = "Bytes per Row";
			label1.TextAlign = ContentAlignment.MiddleRight;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(684, 590);
			Controls.Add(btnLoad);
			Controls.Add(textBoxFilename);
			Controls.Add(groupBox1);
			Controls.Add(textBoxOutput);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			Name = "Form1";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "BinPat";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnLoad;
		private OpenFileDialog openFileDialog;
		private SaveFileDialog saveFileDialog;
		private TextBox textBoxFilename;
		private TextBox textBoxOutput;
		private GroupBox groupBox1;
		private TextBox textBoxBytesPerRow;
		private Label label1;
	}
}