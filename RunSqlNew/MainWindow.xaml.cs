using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using OfficeOpenXml.Drawing.Slicer.Style;
using RunSqlNew.Models;
using RunSqlNew.ViewModels;

namespace RunSqlNew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // nem kell?
        //enum MonthDayCount
        //{
        //    januar = 31, februarYesLeap = 29, februarNoLeap = 28, marcius = 31, aprilis = 30, majus = 31, junius = 30, julius = 31,
        //    augusztus = 31, szeptemeber = 30, oktober = 31, november = 30, december = 31
        //};

        // Logic osztály példány
        public IRunSqlLogic Logic;

        // Main metódus, csak a megjelenítésért felelős
        public MainWindow()
        {
            InitializeComponent();
        }

        // *ablak betöltődik, mi történjen*, Logic példányosítás
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Logic = new RunSqlLogic();
                this.DataContext = Logic;

                Thread t = new Thread(this.TimeTest);
                t.Start();
            }
            catch (FileNotFoundException fe)
            {
                MessageBox.Show(fe.ToString());
            }
            catch (InvalidOperationException ie)
            {
                MessageBox.Show(ie.ToString(), "File not found!");
            }
        }

        private void TimeTest()
        {
            while (true)
            {
                DateTime minta = new DateTime(2024, 7, 5, 13, 33, 0);
                if (DateTime.Equals((DateTime.Now).ToString(), minta.ToString()))
                {
                    
                }
            }
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

        // 1-1 elem kijelölésekor mi történjen
        private void DataGrid_Selected(object sender, RoutedEventArgs e)
        {
            int selectedIndex = DatasInWindow.SelectedIndex;

            if (DatasInWindow.SelectedIndex >= Logic.Datas.Count)
                Logic.selectedRow = -1;
            else
                Logic.selectedRow = DatasInWindow.SelectedIndex;

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
            textbox_Time.Text = stForIdo.Split(" ").LastOrDefault();
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

            if (stForEng == "1")
                CB_engedelyezve.IsChecked = true;
            else
                CB_engedelyezve.IsChecked = false;
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

            // üres sor
            if (stForM_nap == "")
                return;

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

        private void textbox_Riport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Datas[selectedIndex].Riport = textbox_Riport.Text;
            }
        }

        private void button_Mentes_Click(object sender, RoutedEventArgs e)
        {
            //Logic.SaveExcel();
        }

        private void textbox_Date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Datas[selectedIndex].Dátum = textbox_Date.Text;
            }
        }

        private void textbox_Ido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Datas[selectedIndex].Idő = textbox_Time.Text;
            }
        }

        private void textbox_XLSkvt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Datas[selectedIndex].XLS_KVT = textbox_XLSKVT.Text;
            }
        }
        private void textbox_XLSnev_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Datas[selectedIndex].XLS_NÉV = textbox_XLSnev.Text;
            }
        }

        private void textbox_Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Datas[selectedIndex].Címek = textbox_Email.Text;
            }
        }

        private void button_SQL_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<SqlWindow>().Any(w => w.GetType().Equals(typeof(SqlWindow))))
            {
                SqlWindow sqlwindow = new SqlWindow();
                sqlwindow.SettingLogic(ref this.Logic);
                sqlwindow.Show();
                sqlwindow.Focus();
            }
        }

        public void SetupNewDatas(string path)
        {
            try
            {
                Logic.DatasSetup(path);
            }
            catch (FileNotFoundException fe)
            {
                MessageBox.Show(fe.ToString());
            }
            catch (InvalidOperationException ie)
            {
                MessageBox.Show(ie.ToString(), "File not found!");
            }
        }

        public void testMethod()
        {

        }

        // Engedélyezve checkbox
        private void CB_engedelyezve_Checked(object sender, RoutedEventArgs e)
        {
            //9. oszlop
            if (CB_engedelyezve.IsChecked == true)
            {

            }

            //CB_engedelyezve;
        }

        // Ha minden mezőben (elsősorban a dátumra vonatkozókban) valid formátumú adat van megadva, fölülírja a korábbi adatokat az újonnan megadottakkal
        // Különben hibát dob és eredmény nélkül visszatér
        // ( A SORREND LEHET NEM IDEÁLIS !!!!!!!!!!!!!!!! )
        private void button_Szerkeszt_Click(object sender, RoutedEventArgs e)
        {
            // Dátum validitásának ellenőrzése
            if (!DateCheck(textbox_Date.Text) || textbox_Date.Text == "")
            {
                MessageBox.Show("Hibás dátum!");
                return;
            }

            // Idő validitásának ellenőrzése
            if (!TimeCheck(textbox_Time.Text) || textbox_Time.Text == "")
            {
                MessageBox.Show("Hibás idő!");
                return;
            }

            // Ellenőrzi, hogy van a kijelölt sor ( LEHET A 2 DÁTUM ELLENŐRZÉST IS EBBEN KELLENE MEGEJTENI !!!!!!!!!!!!!!!!!!!!!!!!!!! )
            if (Logic.selectedRow != -1)
            {
                //DatasInWindow.AllowEditing = true;
                //DataGridCellInfo dataGridCellInfo = new DataGridCellInfo(textbox_Date.Text, new DataGridColumn());
                //dataGridCellInfo.Item = textbox_Date.Text;
                //DatasInWindow.SelectedCells[0].Item = textbox_Date.Text;
                Logic.Datas[Logic.selectedRow].Dátum = textbox_Date.Text;
                Logic.Datas[Logic.selectedRow].Idő = textbox_Time.Text;
                Logic.Datas[Logic.selectedRow].Riport = textbox_Riport.Text;
                Logic.Datas[Logic.selectedRow].XLS_KVT = textbox_XLSKVT.Text;
                Logic.Datas[Logic.selectedRow].XLS_NÉV = textbox_XLSnev.Text;
                Logic.Datas[Logic.selectedRow].Címek = textbox_Email.Text;      // Az eredeti programban ennek a kitöltése nem kötelező !!!!!!!!!!!!!!!!!!!!!!!!

                // GYAKORISÁG FORMÁTUM ELLENŐRZÉS
                if (rb_RepFreqHavi.IsChecked == true)
                    Logic.Datas[Logic.selectedRow].H_H_N_E = "0";
                else if (rb_RepFreqHeti.IsChecked == true)
                    Logic.Datas[Logic.selectedRow].H_H_N_E = "1";
                else if (rb_RepFreqNapi.IsChecked == true)
                    Logic.Datas[Logic.selectedRow].H_H_N_E = "2";
                else if (rb_RepFreqEgyszer.IsChecked == true)
                    Logic.Datas[Logic.selectedRow].H_H_N_E = "3";
                else
                {
                    MessageBox.Show("Nincs kijelölve gyakoriság! (H_H_N_E)");
                    return;
                }

                string MnapHelper = "";
                if (cb_H.IsChecked == true) MnapHelper += "1";
                else MnapHelper += "0";
                if (cb_K.IsChecked == true) MnapHelper += "1";
                else MnapHelper += "0";
                if (cb_SZE.IsChecked == true) MnapHelper += "1";
                else MnapHelper += "0";
                if (cb_CS.IsChecked == true) MnapHelper += "1";
                else MnapHelper += "0";
                if (cb_P.IsChecked == true) MnapHelper += "1";
                else MnapHelper += "0";
                if (cb_SZO.IsChecked == true) MnapHelper += "1";
                else MnapHelper += "0";
                if (cb_V.IsChecked == true) MnapHelper += "1";
                else MnapHelper += "0";

                Logic.Datas[Logic.selectedRow].M_nap = MnapHelper;

                // NEM LENNE JOBB, HA SZERKESZTÉS MEGNYOMÁSA NÉLKÜL LEHETNE ÁLLÍTANI???
                if (CB_engedelyezve.IsChecked == true)
                    Logic.Datas[Logic.selectedRow].Eng = "1";
                else Logic.Datas[Logic.selectedRow].Eng = "0";

                DatasInWindow.Items.Refresh();
            }
        }

        // Dátumok valid formátumának ellenőrzése
        private bool DateCheck(string date)
        {
            //int ok = Int32.Parse(date.Split("/")[0]);

            if (date.Split("/")[2].Count() != 4)
                return false;
            if (date.Split("/")[1].Count() != 2 || Int32.Parse(date.Split("/")[1]) < 0)
                return false;
            if (date.Split("/")[0].Count() != 2 || Int32.Parse(date.Split("/")[0]) < 0)
                return false;

            if (date.Split("/")[0] == "01" && Int32.Parse(date.Split("/")[1]) > 31)
                return false;
            if (date.Split("/")[0] == "02")
            {
                if (Int32.Parse(date.Split("/")[2]) % 4 == 0 && Int32.Parse(date.Split("/")[1]) > 28)
                    return false;
                else if (Int32.Parse(date.Split("/")[2]) % 4 != 0 && Int32.Parse(date.Split("/")[1]) > 29)
                    return false;
            }
            if (date.Split("/")[0] == "03" && Int32.Parse(date.Split("/")[1]) > 31)
                return false;
            if (date.Split("/")[0] == "04" && Int32.Parse(date.Split("/")[1]) > 30)
                return false;
            if (date.Split("/")[0] == "05" && Int32.Parse(date.Split("/")[1]) > 31)
                return false;
            if (date.Split("/")[0] == "06" && Int32.Parse(date.Split("/")[1]) > 30)
                return false;
            if (date.Split("/")[0] == "07" && Int32.Parse(date.Split("/")[1]) > 31)
                return false;
            if (date.Split("/")[0] == "08" && Int32.Parse(date.Split("/")[1]) > 31)
                return false;
            if (date.Split("/")[0] == "09" && Int32.Parse(date.Split("/")[1]) > 30)
                return false;
            if (date.Split("/")[0] == "10" && Int32.Parse(date.Split("/")[1]) > 31)
                return false;
            if (date.Split("/")[0] == "11" && Int32.Parse(date.Split("/")[1]) > 30)
                return false;
            if (date.Split("/")[0] == "12" && Int32.Parse(date.Split("/")[1]) > 31)
                return false;
            return true;
        }

        private bool TimeCheck(string time)
        {
            if (time.Split(":")[0].Count() != 2)
                return false;
            if (time.Split(":")[1].Count() != 2)
                return false;
            if (time.Split(":")[2].Count() != 2)
                return false;

            if (Int32.Parse(time.Split(":")[0]) < 0 || Int32.Parse(time.Split(":")[0]) > 24)
                return false;

            return true;
        }

        private void button_Hozzaad_Click(object sender, RoutedEventArgs e)
        {

        }

        // MÉGSEM GOMB: ELTÜNTETI A KIJELÖLÉSEKET
        private void button_Megsem_Click(object sender, RoutedEventArgs e)
        {
            DatasInWindow.UnselectAll();
            Logic.selectedRow = -1;     // ez kell ????????????????????????
        }

        // TÖRLÉS GOMB: TÖRLI AZ ÉPPEN KIJELÖLT SORT
        private void button_Torol_Click(object sender, RoutedEventArgs e)
        {
            Logic.Datas.Remove(Logic.Datas[Logic.selectedRow]);
            DatasInWindow.Items.Refresh();
        }

        private void button_Most_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void DataGrid_Selected(object sender, SelectionChangedEventArgs e)
        //{
        //    //if(DatasInWindow.SelectedIndex == 0)
        //    //{

        //    //}

        //}
    }
}
