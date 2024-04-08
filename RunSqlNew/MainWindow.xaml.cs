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
using Logic;
using RunSqlNew.ViewModels;

namespace RunSqlNew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IRunSqlLogic Logic;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Logic = new RunSqlLogic();
        }

        private void DataGrid_Selected(object sender, RoutedEventArgs e)
        {
            int selectedInex = DatasInWindow.SelectedIndex;

            //ViewModel.CurrentlySelected
            //textbox_Date.Text = DatasInWindow.SelectedIndex.ToString();
        }
    }
}
