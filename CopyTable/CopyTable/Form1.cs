using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CopyTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Event handler for the folder browse button (button3)
        private void buttonBrowseFolder_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // Set the selected folder path to textBoxFolderPath
                textBoxFolderPath.Text = folderBrowserDialog1.SelectedPath;

                // Generate the absolute file path using the folder and table name
                UpdateAbsoluteFilePath();
            }
        }
        private void UpdateAbsoluteFilePath()
        {
            string tableName = textBox1.Text.Trim();
            string folderPath = textBoxFolderPath.Text.Trim();

            // Check if both table name and folder path are provided
            if (!string.IsNullOrWhiteSpace(tableName) && !string.IsNullOrWhiteSpace(folderPath))
            {
                // Generate the file name by replacing underscores with dashes
                string fileName = tableName.Replace("_", "-") + ".sql";
                string filePath = Path.Combine(folderPath, fileName);

                // Display the generated file path in AbsoluteFilePathTextBox
                AbsoluteFilePathTextBox.Text = filePath;
            }
            else
            {
                AbsoluteFilePathTextBox.Text = string.Empty; // Clear the text box if inputs are missing
            }
        }


        // Event handler for the generate button (button1)
        private void button1_Click(object sender, EventArgs e)
        {

            // Update the absolute file path based on the current inputs
            UpdateAbsoluteFilePath();

            // Get the table name, folder path, service name, and schema user from the text boxes
            string tableName = textBox1.Text.Trim();
            string folderPath = textBoxFolderPath.Text.Trim();
            string serviceName = ServiceNameTextBox.Text.Trim();
            string schemaUser = SchemaUserTextBox.Text.Trim();

            // Validate the inputs
            if (string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show("Please enter a table name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                MessageBox.Show("Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                MessageBox.Show("Please enter a service name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(schemaUser))
            {
                MessageBox.Show("Please enter a schema user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ensure service name ends with ".localdomain"
            if (!serviceName.EndsWith(".localdomain"))
            {
                serviceName += ".localdomain";
            }

            // Generate the SQL script
            string sqlTemplate =
$@"CREATE TABLE {tableName} (
    {tableName}_id         RAW(16) DEFAULT sys_guid() PRIMARY KEY,
    {tableName}            VARCHAR2(4000),
    -- Additional columns for note and dates
    note                   VARCHAR2(4000),  -- General-purpose note field
    date_created           TIMESTAMP(9) WITH TIME ZONE DEFAULT systimestamp(9) NOT NULL,
    date_updated           TIMESTAMP(9) WITH TIME ZONE,
    date_created_or_updated TIMESTAMP(9) WITH TIME ZONE GENERATED ALWAYS AS ( coalesce(date_updated, date_created) ) VIRTUAL
);

-- Trigger to update date_updated for {tableName}
CREATE OR REPLACE TRIGGER set_date_updated_{tableName}
    BEFORE UPDATE ON {tableName}
    FOR EACH ROW
BEGIN
    :new.date_updated := systimestamp;
END;
/
";

            // Set the generated SQL in textBox2
            textBox2.Text = sqlTemplate;

            // Generate the file name by replacing underscores with dashes
            string fileName = tableName.Replace("_", "-") + ".sql";
            string filePath = Path.Combine(folderPath, fileName);

            try
            {
                // Determine overwrite behavior based on the selected radio button
                if (DoOverwriteFileRadioButton.Checked)
                {
                    // Overwrite is enabled, write the file even if it exists
                    File.WriteAllText(filePath, sqlTemplate);
                }
                else if (DoNotOverwriteFileRadioButton.Checked)
                {
                    // Overwrite is disabled, only write the file if it does not exist
                    if (File.Exists(filePath))
                    {
                        MessageBox.Show("The file already exists and overwrite is disabled.", "File Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Exit the method early if the file should not be overwritten
                    }
                    else
                    {
                        File.WriteAllText(filePath, sqlTemplate);
                    }
                }

                // Copy the SQL to the clipboard
                Clipboard.SetText(sqlTemplate);

                // Start a process to run cmd.exe
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = processStartInfo })
                {
                    process.Start();

                    // Write commands to SQL*Plus through StandardInput
                    using (StreamWriter sw = process.StandardInput)
                    {
                        if (sw.BaseStream.CanWrite)
                        {
                            // Connect to SQL*Plus
                            string connectCommand = $"sqlplus {schemaUser}/1234@localhost:1521/{serviceName}";
                            sw.WriteLine(connectCommand);

                            // Execute the SQL script
                            sw.WriteLine($"@\"{filePath}\"");

                            // Exit SQL*Plus
                            sw.WriteLine("exit");
                        }
                    }

                    // Read the output (optional: you can also read the error output)
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    // Show output or errors if there are any
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show($"Error executing SQLPlus:\n{error}", "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"SQL script generated, copied to clipboard, and executed successfully.\n\nOutput:\n{output}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Event handler for the copy button (button2)
        private void button2_Click(object sender, EventArgs e)
        {
            // Copy the content of textBox2 to the clipboard
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                Clipboard.SetText(textBox2.Text);
                MessageBox.Show("SQL script copied to clipboard.", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("There is no SQL script to copy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
