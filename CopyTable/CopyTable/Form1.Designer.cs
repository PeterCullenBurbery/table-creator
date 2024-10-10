namespace CopyTable
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
            if (disposing && (components != null))
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
            textBox1 = new TextBox();
            button1 = new Button();
            textBox2 = new TextBox();
            label1 = new Label();
            button2 = new Button();
            label2 = new Label();
            textBoxFolderPath = new TextBox();
            button3 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            ServiceNameLabel = new Label();
            ServiceNameTextBox = new TextBox();
            label3 = new Label();
            SchemaUserTextBox = new TextBox();
            OverwriteFileGroupBox = new GroupBox();
            DoOverwriteFileRadioButton = new RadioButton();
            DoNotOverwriteFileRadioButton = new RadioButton();
            AbsoluteFilePathTextBox = new TextBox();
            label4 = new Label();
            CopyAbsoluteFilePathButton = new Button();
            OverwriteFileGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(74, 51);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(769, 23);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(74, 110);
            button1.Name = "button1";
            button1.Size = new Size(134, 40);
            button1.TabIndex = 1;
            button1.Text = "Generate table";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(865, 60);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(521, 386);
            textBox2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(100, 15);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 3;
            label1.Text = "Enter table name.";
            // 
            // button2
            // 
            button2.Location = new Point(1242, 469);
            button2.Name = "button2";
            button2.Size = new Size(144, 41);
            button2.TabIndex = 4;
            button2.Text = "Copy DDL";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(84, 177);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 5;
            label2.Text = "Folder for file";
            // 
            // textBoxFolderPath
            // 
            textBoxFolderPath.Location = new Point(74, 210);
            textBoxFolderPath.Name = "textBoxFolderPath";
            textBoxFolderPath.Size = new Size(769, 23);
            textBoxFolderPath.TabIndex = 6;
            // 
            // button3
            // 
            button3.Location = new Point(74, 265);
            button3.Name = "button3";
            button3.Size = new Size(124, 39);
            button3.TabIndex = 7;
            button3.Text = "Folder for File";
            button3.UseVisualStyleBackColor = true;
            button3.Click += buttonBrowseFolder_Click;
            // 
            // ServiceNameLabel
            // 
            ServiceNameLabel.AutoSize = true;
            ServiceNameLabel.Location = new Point(74, 664);
            ServiceNameLabel.Name = "ServiceNameLabel";
            ServiceNameLabel.Size = new Size(77, 15);
            ServiceNameLabel.TabIndex = 8;
            ServiceNameLabel.Text = "Service name";
            // 
            // ServiceNameTextBox
            // 
            ServiceNameTextBox.Location = new Point(74, 711);
            ServiceNameTextBox.Name = "ServiceNameTextBox";
            ServiceNameTextBox.Size = new Size(413, 23);
            ServiceNameTextBox.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(74, 563);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 10;
            label3.Text = "Schema/user";
            // 
            // SchemaUserTextBox
            // 
            SchemaUserTextBox.Location = new Point(74, 612);
            SchemaUserTextBox.Name = "SchemaUserTextBox";
            SchemaUserTextBox.Size = new Size(413, 23);
            SchemaUserTextBox.TabIndex = 11;
            // 
            // OverwriteFileGroupBox
            // 
            OverwriteFileGroupBox.Controls.Add(DoOverwriteFileRadioButton);
            OverwriteFileGroupBox.Controls.Add(DoNotOverwriteFileRadioButton);
            OverwriteFileGroupBox.Location = new Point(74, 411);
            OverwriteFileGroupBox.Name = "OverwriteFileGroupBox";
            OverwriteFileGroupBox.Size = new Size(134, 131);
            OverwriteFileGroupBox.TabIndex = 12;
            OverwriteFileGroupBox.TabStop = false;
            OverwriteFileGroupBox.Text = "Overwrite file";
            // 
            // DoOverwriteFileRadioButton
            // 
            DoOverwriteFileRadioButton.AutoSize = true;
            DoOverwriteFileRadioButton.Location = new Point(17, 80);
            DoOverwriteFileRadioButton.Name = "DoOverwriteFileRadioButton";
            DoOverwriteFileRadioButton.Size = new Size(47, 19);
            DoOverwriteFileRadioButton.TabIndex = 1;
            DoOverwriteFileRadioButton.Text = "True";
            DoOverwriteFileRadioButton.UseVisualStyleBackColor = true;
            // 
            // DoNotOverwriteFileRadioButton
            // 
            DoNotOverwriteFileRadioButton.AutoSize = true;
            DoNotOverwriteFileRadioButton.Checked = true;
            DoNotOverwriteFileRadioButton.Location = new Point(17, 41);
            DoNotOverwriteFileRadioButton.Name = "DoNotOverwriteFileRadioButton";
            DoNotOverwriteFileRadioButton.Size = new Size(51, 19);
            DoNotOverwriteFileRadioButton.TabIndex = 0;
            DoNotOverwriteFileRadioButton.TabStop = true;
            DoNotOverwriteFileRadioButton.Text = "False";
            DoNotOverwriteFileRadioButton.UseVisualStyleBackColor = true;
            // 
            // AbsoluteFilePathTextBox
            // 
            AbsoluteFilePathTextBox.Location = new Point(74, 363);
            AbsoluteFilePathTextBox.Name = "AbsoluteFilePathTextBox";
            AbsoluteFilePathTextBox.ReadOnly = true;
            AbsoluteFilePathTextBox.Size = new Size(769, 23);
            AbsoluteFilePathTextBox.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(74, 327);
            label4.Name = "label4";
            label4.Size = new Size(102, 15);
            label4.TabIndex = 14;
            label4.Text = "Absolute File Path";
            // 
            // CopyAbsoluteFilePathButton
            // 
            CopyAbsoluteFilePathButton.Location = new Point(251, 320);
            CopyAbsoluteFilePathButton.Name = "CopyAbsoluteFilePathButton";
            CopyAbsoluteFilePathButton.Size = new Size(142, 28);
            CopyAbsoluteFilePathButton.TabIndex = 15;
            CopyAbsoluteFilePathButton.Text = "Copy absolute file path";
            CopyAbsoluteFilePathButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1414, 802);
            Controls.Add(CopyAbsoluteFilePathButton);
            Controls.Add(label4);
            Controls.Add(AbsoluteFilePathTextBox);
            Controls.Add(OverwriteFileGroupBox);
            Controls.Add(SchemaUserTextBox);
            Controls.Add(label3);
            Controls.Add(ServiceNameTextBox);
            Controls.Add(ServiceNameLabel);
            Controls.Add(button3);
            Controls.Add(textBoxFolderPath);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            OverwriteFileGroupBox.ResumeLayout(false);
            OverwriteFileGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private TextBox textBox2;
        private Label label1;
        private Button button2;
        private Label label2;
        private TextBox textBoxFolderPath;
        private Button button3;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label ServiceNameLabel;
        private TextBox ServiceNameTextBox;
        private Label label3;
        private TextBox SchemaUserTextBox;
        private GroupBox OverwriteFileGroupBox;
        private RadioButton DoOverwriteFileRadioButton;
        private RadioButton DoNotOverwriteFileRadioButton;
        private TextBox AbsoluteFilePathTextBox;
        private Label label4;
        private Button CopyAbsoluteFilePathButton;
    }
}
