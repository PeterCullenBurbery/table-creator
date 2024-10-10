using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonCreateStarterTable_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the StarterTablePage
            MainFrame.Navigate(new StarterTablePage());
        }

        private void ButtonCreateManyToMany_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the ManyToManyTablePage
            MainFrame.Navigate(new ManyToManyTablePage());
        }
    }
}