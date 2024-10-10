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
    /// Interaction logic for ManyToManyTablePage.xaml
    /// </summary>
    public partial class ManyToManyTablePage : Page
    {
        public ManyToManyTablePage()
        {
            InitializeComponent();
        }
        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the HomePage
            NavigationService.Navigate(new HomePage());
        }
    }
}
