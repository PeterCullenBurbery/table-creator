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

        private void SubmitFinalIdea_Click(object sender, RoutedEventArgs e)
        {
            // Get the text from the final idea box and process it.
            string finalIdea = FinalIdeaTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(finalIdea))
            {
                MessageBox.Show("Please enter a final idea before transforming.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Transform the final idea: Replace spaces with underscores and convert to uppercase.
            string transformedName = finalIdea.Replace(" ", "_").ToUpper();
            TransformedNameTextBox.Text = transformedName;

            // Do not modify the GeneratedSQLTextBox content here.
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string transformedName = TransformedNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(transformedName))
            {
                MessageBox.Show("Please transform a name before confirming.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Generate SQL for the transformed name.
            string sqlTemplate = GenerateSQL(transformedName);
            GeneratedSQLTextBox.Text = sqlTemplate;

            
        }

        private string GenerateSQL(string tableName)
        {
            // Template for creating the table.
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
/";
            return sqlTemplate;
        }


    }
}
