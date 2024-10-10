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
        public StarterTablePage()
        {
            InitializeComponent();
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the HomePage
            NavigationService.Navigate(new HomePage());
        }

        private void LoadFinalIdea_Click(object sender, RoutedEventArgs e)
        {
            // Get the final idea text
            string finalIdea = FinalIdeaTextBox.Text.Trim();

            // If the final idea is empty, show an error message
            if (string.IsNullOrWhiteSpace(finalIdea))
            {
                MessageBox.Show("Please enter a final idea before loading.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Transformations
            string suggestedName = finalIdea.Replace(" ", "_");
            string upperCaseName = suggestedName.ToUpper();
            string lowerCaseName = suggestedName.ToLower();
            int characterLength = suggestedName.Length;
            int byteLength = Encoding.UTF8.GetByteCount(suggestedName);

            // Split the suggested name by underscores to get the list of words
            string[] words = suggestedName.Split('_');
            int wordCount = words.Length;

            // Display the results in the appropriate text boxes
            SuggestedNameTextBox.Text = suggestedName;
            UpperCaseNameTextBox.Text = upperCaseName;
            LowerCaseNameTextBox.Text = lowerCaseName;
            CharacterLengthTextBox.Text = characterLength.ToString();
            ByteLengthTextBox.Text = byteLength.ToString();
            WordsListTextBox.Text = "{" + string.Join(", ", words.Select(w => $"\"{w}\"")) + "}";
            WordCountTextBox.Text = wordCount.ToString();
        }
    }
}
