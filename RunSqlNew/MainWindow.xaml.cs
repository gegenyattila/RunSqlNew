using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using RunSqlNew.Models;
using RunSqlNew.ViewModels;

namespace RunSqlNew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IRunSqlLogic Logic;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Logic = new RunSqlLogic();
            this.DataContext = Logic;
        }

        //string datum;
        //string ido;
        //string riport;
        //string xls_kvt;
        //string xls_nev;
        //string cimek;
        //string h_h_n_e;
        //string df;
        //string m_nap;
        //string eng;

        private void DataGrid_Selected(object sender, RoutedEventArgs e)
        {
            int selectedIndex = DatasInWindow.SelectedIndex;

            string stForDatum = "";
            string stForIdo = "";
            string stForRiport = "";
            string stForXls_kvt = "";
            string stForXls_nev = "";
            string stForCimek = "";
            string stForH_h_n_e = "";
            string stForDf = "";
            string stForM_nap = "";
            string stForEng = "";

            if (this.Logic != null)
            {
                stForDatum = Logic.ReturnDatas(selectedIndex, 1);
                stForIdo = Logic.ReturnDatas(selectedIndex, 2);
                stForRiport = Logic.ReturnDatas(selectedIndex, 3);
                stForXls_kvt = Logic.ReturnDatas(selectedIndex, 4);
                stForXls_nev = Logic.ReturnDatas(selectedIndex, 5);
                stForCimek = Logic.ReturnDatas(selectedIndex, 6);
                stForH_h_n_e = Logic.ReturnDatas(selectedIndex, 7);
                stForDf = Logic.ReturnDatas(selectedIndex, 8);
                stForM_nap = Logic.ReturnDatas(selectedIndex, 9);
                stForEng = Logic.ReturnDatas(selectedIndex, 10);

            }

            textbox_Date.Text = stForDatum;
            textbox_Time.Text = stForIdo;
            switch (stForH_h_n_e)
            {
                case "0":
                    rb_RepFreqHavi.IsChecked = true;
                    break;
                case "1":
                    rb_RepFreqHeti.IsChecked = true;
                    break;
                case "2":
                    rb_RepFreqNapi.IsChecked = true;
                    break;
                case "3":
                    rb_RepFreqEgyszer.IsChecked = true;
                    break;
                default:
                    rb_RepFreqHavi.IsChecked = false;
                    rb_RepFreqHeti.IsChecked = false;
                    rb_RepFreqNapi.IsChecked = false;
                    rb_RepFreqEgyszer.IsChecked = false;
                    break;
            }
            NapHelper(stForM_nap);
            textbox_Riport.Text = stForRiport;
            textbox_XLSKVT.Text = stForXls_nev;
            textbox_XLSnev.Text = stForXls_nev;
            textbox_Email.Text = stForCimek;
        }

        private void NapHelper(string stForM_nap)
        {
            cb_M.IsChecked = false;
            cb_H.IsChecked = false;
            cb_K.IsChecked = false;
            cb_SZE.IsChecked = false;
            cb_CS.IsChecked = false;
            cb_P.IsChecked = false;
            cb_SZO.IsChecked = false;
            cb_V.IsChecked = false;

            if (stForM_nap[0].Equals('1'))
            {
                cb_M.IsChecked = true;
            }
            if (stForM_nap[1].Equals('1'))
            {
                cb_H.IsChecked = true;
            }
            if (stForM_nap[2].Equals('1'))
            {
                cb_K.IsChecked = true;
            }
            if (stForM_nap[3].Equals('1'))
            {
                cb_SZE.IsChecked = true;
            }
            if (stForM_nap[4].Equals('1'))
            {
                cb_CS.IsChecked = true;
            }
            if (stForM_nap[5].Equals('1'))
            {
                cb_P.IsChecked = true;
            }
            if (stForM_nap[6].Equals('1'))
            {
                cb_SZO.IsChecked = true;
            }
            if (stForM_nap[7].Equals('1'))
            {
                cb_V.IsChecked = true;
            }
        }
    }
}
