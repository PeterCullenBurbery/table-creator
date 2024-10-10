using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for StarterTablePage.xaml
    /// </summary>
    public partial class StarterTablePage : Page
    {
        private readonly string connectionString;
        public StarterTablePage()
        {
            InitializeComponent();
            connectionString = "User Id=sys;Password=1234;Data Source=localhost:1521/orcl.localdomain;DBA Privilege=SYSDBA;";

        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the HomePage
            NavigationService.Navigate(new HomePage());
        }

        private void LoadFinalIdea_Click(object sender, RoutedEventArgs e)
        {
            string finalIdea = FinalIdeaTextBox.Text.Trim();
            if (string.IsNullOrEmpty(finalIdea))
            {
                MessageBox.Show("Please enter a final idea.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Generate transformations
            string suggestedName = finalIdea.Replace(" ", "_");
            string upperCaseName = suggestedName.ToUpper();
            string lowerCaseName = suggestedName.ToLower();
            int charLength = finalIdea.Length;
            int byteLength = Encoding.UTF8.GetByteCount(finalIdea);
            var words = suggestedName.Split('_');
            int wordCount = words.Length;

            // Set the transformed values
            SuggestedNameTextBox.Text = suggestedName;
            UpperCaseNameTextBox.Text = upperCaseName;
            LowerCaseNameTextBox.Text = lowerCaseName;
            CharacterLengthTextBox.Text = charLength.ToString();
            ByteLengthTextBox.Text = byteLength.ToString();
            WordsListTextBox.Text = "{" + string.Join(", ", words.Select(w => $"\"{w}\"")) + "}";
            WordCountTextBox.Text = wordCount.ToString();

            // Handle truncation to 128 bytes
            string truncatedName = TruncateToBytes(suggestedName, 128);
            TruncatedTextBox.Text = truncatedName;

            // Calculate and display the overflowed part
            if (byteLength > 128)
            {
                int overflowStartIndex = Encoding.UTF8.GetByteCount(truncatedName);
                string overflowedText = finalIdea.Substring(overflowStartIndex);
                int bytesOver = byteLength - 128;
                int charsOver = charLength - truncatedName.Length;

                OverflowedTextBox.Text = overflowedText;
                BytesOverTextBox.Text = bytesOver.ToString();
                CharactersOverTextBox.Text = charsOver > 0 ? charsOver.ToString() : "0";
            }
            else
            {
                OverflowedTextBox.Text = string.Empty;
                BytesOverTextBox.Text = "0";
                CharactersOverTextBox.Text = "0";
            }
        }

        // Helper method to truncate a string to a given byte length
        private string TruncateToBytes(string input, int maxBytes)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            if (bytes.Length <= maxBytes) return input;

            // Get the maximum length that fits within the byte limit
            string truncated = Encoding.UTF8.GetString(bytes, 0, maxBytes);

            // Check if truncation ended in the middle of a multi-byte character and adjust if needed
            while (Encoding.UTF8.GetByteCount(truncated) > maxBytes)
            {
                truncated = truncated.Substring(0, truncated.Length - 1);
            }

            return truncated;
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string tableName = UpperCaseNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Please load a valid idea first.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Generate the SQL template
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

            // Display the generated SQL in the TextBox
            GeneratedSqlTextBox.Text = sqlTemplate;

            // Optionally, you can directly execute the SQL here if needed
            // ExecuteSql(sqlTemplate);
        }

        private void ExecuteSql(string sql)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Table created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating table: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
