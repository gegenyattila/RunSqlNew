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
using System.Windows.Shapes;

namespace RunSqlNew
{
    /// <summary>
    /// Interaction logic for SqlWindow.xaml
    /// </summary>
    public partial class SqlWindow : Window
    {
        public SqlWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // privát tagokat is eléri
            //((MainWindow)Application.Current.MainWindow).burtt;
            if (ok_button.Content.ToString() != null)
            {
                ((MainWindow)Application.Current.MainWindow).SetupNewDatas(ok_button.Content.ToString());
            }
            this.Close();
        }
    }
}
