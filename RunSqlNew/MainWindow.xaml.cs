﻿using System;
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
        public IRunSqlLogic Logic { get; set; }

        // Main metódus, csak a megjelenítésért felelős
        public MainWindow()
        {
            InitializeComponent();
            Logic = new RunSqlLogic();
            DataContext = new MainWindowViewModel(Logic);
        }

        // Ablak betöltődésekot mi történjen, Logic példányosítás
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Logic példányosítása
                //Logic = new RunSqlLogic();
                //this.DataContext = Logic;

                // Thread létrehozás az időzített lekérdezések kezelésére (jó helyen van?)
                //Thread t = new Thread(this.TimeTest);
                //t.Start();
                // Thread teszt
                //Thread t = new Thread(this.TimeTest);
                //t.Start();
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

        // Thread teszt, amíg az alkalmazás fut, folyamatosan figyeli, hogy az aktuális idő megegyezik e a megadottal,
        // ha igen, megpróbálja bezárni az alkalmazást (hibára fog futni, de ki tudja értékelni az if-et)
        private void TimeTest()
        {
            DateTime minta = new DateTime(2025, 3, 4, 11, 56, 0);
            while (true)
            {
                if (DateTime.Equals((DateTime.Now).ToString(), minta.ToString()))
                {
                    Console.WriteLine("fasza");
                    //Application.Current.Shutdown();
                    break;
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

        // 1-1 elem kijelölésekor mi történjen, betölti a szerkeszthető mezőkbe a kijelölt elem adatait
        // ÖTLET: visszaállítás gomb, program tárolja a kijelölt elem eredeti változatát (kijelöléskor aktuális adatokat)
        //        és ahelyett, hogy újra ki kéne jelölgetni, ha vissza akarjuk kapni az eredeti adatokat, elég lenne csak
        //        egy gombot megnyomni
        private void DataGrid_Selected(object sender, RoutedEventArgs e)
        {
            // Kijelölt elem indexének lekérése
            int selectedIndex = DatasInWindow.SelectedIndex;

            // Index helyességenek ellenőrzése
            if (DatasInWindow.SelectedIndex >= Logic.Riports.Count)
                Logic.selectedRow = -1;
            else
                Logic.selectedRow = DatasInWindow.SelectedIndex;

            // Segédváltozók
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

            // Segédváltozóknak értékadás
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

            // Kijelölt elem dátumának szerkeszthető mezőbe illesztése
            textbox_Date.Text = stForDatum;

            // Kijelölt elem idejének szerkeszthető mezőbe illesztése
            textbox_Time.Text = stForIdo.Split(" ").LastOrDefault();

            // Kijelölt elem Havi-heti-napi-egyszeri futtatást megadó változó radio button-be illesztése
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

            // Kijelölt elem futtatandó napjainak segédfüggvénye
            NapHelper(stForM_nap);

            // Kijelölt elem egyszerű string tulajdonságinak szerkeszthető mezőbe illesztése
            textbox_Riport.Text = stForRiport;
            textbox_XLSKVT.Text = stForXls_kvt;
            textbox_XLSnev.Text = stForXls_nev;
            textbox_Email.Text = stForCimek;

            // Kijelölt elem engedélyezve tulajdonságának checkboxba illesztése
            if (stForEng == "1")
                CB_engedelyezve.IsChecked = true;
            else
                CB_engedelyezve.IsChecked = false;
        }

        // Kijelölt elem futtatandó napjainak megjelenítése checkboxokban
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
            cb_MN.IsChecked = false;

            // üres sor esetén nem fut végig a függvény
            if (stForM_nap == "")
                return;

            // A specifikált karaktersorozat esetén minden napot szükséges lenne bejelölni, de elég a Minden Nap checkbox-ot true-ra állítani
            if(stForM_nap == "01111111")
            {
                //cb_H.IsChecked = true;
                //cb_K.IsChecked = true;
                //cb_SZE.IsChecked = true;
                //cb_CS.IsChecked = true;
                //cb_P.IsChecked = true;
                //cb_SZO.IsChecked = true;
                //cb_V.IsChecked = true;
                cb_MN.IsChecked = true;
                return;
            }

            if (stForM_nap[0].Equals('1'))
                cb_M.IsChecked = true;
            if (stForM_nap[1].Equals('1'))
                cb_H.IsChecked = true;
            if (stForM_nap[2].Equals('1'))
                cb_K.IsChecked = true;
            if (stForM_nap[3].Equals('1'))
                cb_SZE.IsChecked = true;
            if (stForM_nap[4].Equals('1'))
                cb_CS.IsChecked = true;
            if (stForM_nap[5].Equals('1'))
                cb_P.IsChecked = true;
            if (stForM_nap[6].Equals('1'))
                cb_SZO.IsChecked = true;
            if (stForM_nap[7].Equals('1'))
                cb_V.IsChecked = true;
        }

        // "Minden nap" checkbox bepipálása event, az összes többi checkboxből eltüntethető a pipa
        private void cb_MN_Checked(object sender, RoutedEventArgs e)
        {
            cb_M.IsChecked = false;
            cb_H.IsChecked = false;
            cb_K.IsChecked = false;
            cb_SZE.IsChecked = false;
            cb_CS.IsChecked = false;
            cb_P.IsChecked = false;
            cb_SZO.IsChecked = false;
            cb_V.IsChecked = false;
            cb_MN.IsChecked = true;
        }

        // "Minden nap" checkboxon kívül bármelyik másik nap checkbox bepipálása event, kényelmi funkcióként a "Minden nap" checkbox-ból eltűnik a pipa
        private void NOT_cb_MN_Checked(object sender, RoutedEventArgs e)
        {
            cb_MN.IsChecked = false;
        }

        // Enter lenyomása event, ha a kurzor a "Riport" szerkeszthető mezőben van:
        // szerkeszti a kijelölt elem riport tulajdonságát, hogy a szerkeszthető mezőkben lévővel megegyezzen
        private void textbox_Riport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Riports[selectedIndex].Riport = textbox_Riport.Text;
            }
        }

        // Mentés gomb megnyomása event
        private void button_Mentes_Click(object sender, RoutedEventArgs e)
        {
            //Logic.SaveExcel();
        }

        // Enter lenyomása event, ha a kurzor a "Dátum" szerkeszthető mezőben van:
        // szerkeszti a kijelölt elem dátum tulajdonságát, hogy a szerkeszthető mezőkben lévővel megegyezzen
        private void textbox_Date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Riports[selectedIndex].Dátum = textbox_Date.Text;
            }
        }

        // Enter lenyomása event, ha a kurzor az "Idő" szerkeszthető mezőben van:
        // szerkeszti a kijelölt elem idő tulajdonságát, hogy a szerkeszthető mezőkben lévővel megegyezzen
        private void textbox_Ido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Riports[selectedIndex].Idő = textbox_Time.Text;
            }
        }

        // Enter lenyomása event, ha a kurzor az "XLS kvt" szerkeszthető mezőben van:
        // szerkeszti a kijelölt elem XLSKVT tulajdonságát, hogy a szerkeszthető mezőkben lévővel megegyezzen
        private void textbox_XLSkvt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Riports[selectedIndex].XLS_KVT = textbox_XLSKVT.Text;
            }
        }

        // Enter lenyomása event, ha a kurzor az "XLS név" szerkeszthető mezőben van:
        // szerkeszti a kijelölt elem XLSNEV tulajdonságát, hogy a szerkeszthető mezőkben lévővel megegyezzen
        private void textbox_XLSnev_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Riports[selectedIndex].XLS_NÉV = textbox_XLSnev.Text;
            }
        }

        // Enter lenyomása event, ha a kurzor az "Email" szerkeszthető mezőben van:
        // szerkeszti a kijelölt elem Email tulajdonságát, hogy a szerkeszthető mezőkben lévővel megegyezzen
        private void textbox_Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int selectedIndex = DatasInWindow.SelectedIndex;
                Logic.Riports[selectedIndex].Címek = textbox_Email.Text;
            }
        }

        // "SQL betöltése" gomb lenyomása event
        // megnyit egy új ablakot, ahova be lehet írni, melyik sql lekérdezést szeretnénk használni
        // (lehet kell bele hibakezelés még, ha pl becsukódik az ablak a munkamenet közben, stb...)
        private void button_SQL_Click(object sender, RoutedEventArgs e)
        {
            // csak 1 példány lehet megnyitva ebből az ablakból
            if (!Application.Current.Windows.OfType<SqlWindow>().Any(w => w.GetType().Equals(typeof(SqlWindow))))
            {
                SqlWindow sqlwindow = new SqlWindow();
                sqlwindow.SettingLogic(/*ref */this.Logic);
                sqlwindow.Show();
                sqlwindow.Focus();
            }
        }

        // Adatok kiszedése elérési úttal megadott Excel fájlból
        // ( lehet nem excel fileból kell majd kiszedni a végén )
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


        // Engedélyezve checkbox
        private void CB_engedelyezve_Checked(object sender, RoutedEventArgs e)
        {
            //9. oszlop
            if (CB_engedelyezve.IsChecked == true)
            {

            }

            //CB_engedelyezve;
        }

        // Szerkeszt gomb lenyomására adott reakció
        // Ha minden mezőben (elsősorban a dátumra vonatkozókban) valid formátumú adat van megadva, fölülírja a korábbi adatokat az újonnan megadottakkal
        // Különben hibát dob és eredmény nélkül visszatér
        // ( A sorrend lehet nem ideális !!! )
        private void button_Szerkeszt_Click(object sender, RoutedEventArgs e)
        {
            // Dátum validitásának ellenőrzése
            // FORMÁTUMRA KELL IGAZÍTANI
            /*
            if (!DateCheck(textbox_Date.Text) || textbox_Date.Text == "")
            {
                MessageBox.Show("Hibás dátum!");
                return;
            }
            */

            // Idő validitásának ellenőrzése
            if (!TimeCheck(textbox_Time.Text) || textbox_Time.Text == "")
            {
                MessageBox.Show("Hibás idő!");
                return;
            }

            // Ellenőrzi, hogy van-e kijelölt sor ( LEHET A 2 DÁTUM ELLENŐRZÉST IS EBBEN KELLENE MEGEJTENI !!!!!!!!!!!!!!!!!!!!!!!!!!! )
            if (Logic.selectedRow != -1)
            {
                //DatasInWindow.AllowEditing = true;
                //DataGridCellInfo dataGridCellInfo = new DataGridCellInfo(textbox_Date.Text, new DataGridColumn());
                //dataGridCellInfo.Item = textbox_Date.Text;
                //DatasInWindow.SelectedCells[0].Item = textbox_Date.Text;
                Logic.Riports[Logic.selectedRow].Dátum = textbox_Date.Text;
                Logic.Riports[Logic.selectedRow].Idő = textbox_Time.Text;
                Logic.Riports[Logic.selectedRow].Riport = textbox_Riport.Text;
                Logic.Riports[Logic.selectedRow].XLS_KVT = textbox_XLSKVT.Text;
                Logic.Riports[Logic.selectedRow].XLS_NÉV = textbox_XLSnev.Text;
                Logic.Riports[Logic.selectedRow].Címek = textbox_Email.Text;      // Az eredeti programban ennek a kitöltése nem kötelező !!!!!!!!!!!!!!!!!!!!!!!!

                // Gyakoriság formátum ellenőrzés
                // feltehetően nem kell majd a hibaüzenet, ha nincs kijelölve semmi!
                if (rb_RepFreqHavi.IsChecked == true)
                    Logic.Riports[Logic.selectedRow].H_H_N_E = "0";
                else if (rb_RepFreqHeti.IsChecked == true)
                    Logic.Riports[Logic.selectedRow].H_H_N_E = "1";
                else if (rb_RepFreqNapi.IsChecked == true)
                    Logic.Riports[Logic.selectedRow].H_H_N_E = "2";
                else if (rb_RepFreqEgyszer.IsChecked == true)
                    Logic.Riports[Logic.selectedRow].H_H_N_E = "3";
                else
                {
                    MessageBox.Show("Nincs kijelölve gyakoriság! (H_H_N_E)");
                    return;
                }

                // Napok checkboxok végigellenőrzése
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

                // Megadott napok betöltése a Logic megfelelő Riports példányába (átdolgozásra szorul)
                Logic.Riports[Logic.selectedRow].M_nap = MnapHelper;

                // Engedélyezve checkbox ellenőrzése, és a megfelelő adat betöltése a Logic megfelelő Riports példányába
                if (CB_engedelyezve.IsChecked == true)
                    Logic.Riports[Logic.selectedRow].Eng = "1";
                else Logic.Riports[Logic.selectedRow].Eng = "0";

                // UI frissítése
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

        // Idő valid formátumának ellenőrzése
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

        // Hozzáad gomb lenyomása event
        // KELL MÉG FORMÁTUM ELLENŐRZÉS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private void button_Hozzaad_Click(object sender, RoutedEventArgs e)
        {
            // Havi/Heti/Napi/Egyszeri gyakoriság kinyerése
            string RiportFrequencytoAdd = "3";
            if ((bool)rb_RepFreqHavi.IsChecked)
                RiportFrequencytoAdd = "0";
            if ((bool)rb_RepFreqHeti.IsChecked)
                RiportFrequencytoAdd = "1";
            if ((bool)rb_RepFreqNapi.IsChecked)
                RiportFrequencytoAdd = "2";

            // Dátum jelző kinyerése
            string DatumJelzotoAdd = "0";
            if ((bool)CB_DatumJelzo.IsChecked)
                DatumJelzotoAdd = "1";

            // Napok kinyerése
            // while ciklus azért kell, hogy akkor, ha olyan eset fordul elő, amikor nem szükséges a hátralévő checkboxok ellenőrzése,
            // könnyen átugorhatók legyenek a fölösleges kiértékelések break; paranccsal, goto nélkül
            bool onlyOnce = true;
            string NapoktoAdd = "";
            while (onlyOnce == true)
            {
                onlyOnce = false;

                if ((bool)cb_MN.IsChecked)
                {
                    NapoktoAdd = "01111111";
                    break;
                }    
                    
                if ((bool)cb_M.IsChecked)
                {
                    NapoktoAdd = "10000000";
                    break;
                }
                else
                    NapoktoAdd = NapoktoAdd + "0";

                if ((bool)cb_H.IsChecked)
                    NapoktoAdd = NapoktoAdd + "1";
                else
                    NapoktoAdd = NapoktoAdd + "0";

                if ((bool)cb_K.IsChecked)
                    NapoktoAdd = NapoktoAdd + "1";
                else
                    NapoktoAdd = NapoktoAdd + "0";

                if ((bool)cb_SZE.IsChecked)
                    NapoktoAdd = NapoktoAdd + "1";
                else
                    NapoktoAdd = NapoktoAdd + "0";

                if ((bool)cb_CS.IsChecked)
                    NapoktoAdd = NapoktoAdd + "1";
                else
                    NapoktoAdd = NapoktoAdd + "0";

                if ((bool)cb_P.IsChecked)
                    NapoktoAdd = NapoktoAdd + "1";
                else
                    NapoktoAdd = NapoktoAdd + "0";

                if ((bool)cb_SZO.IsChecked)
                    NapoktoAdd = NapoktoAdd + "1";
                else
                    NapoktoAdd = NapoktoAdd + "0";

                if ((bool)cb_V.IsChecked)
                    NapoktoAdd = NapoktoAdd + "1";
                else
                    NapoktoAdd = NapoktoAdd + "0";
            }

            // Engedélyezve checkbox kinyerése
            string EngtoAdd = "1";
            if (!(bool)CB_engedelyezve.IsChecked)
                EngtoAdd = "0";

            Riports toAdd = new Riports(textbox_Date.Text, textbox_Time.Text, textbox_Riport.Text, textbox_XLSKVT.Text, textbox_XLSnev.Text, 
                                        textbox_Email.Text, RiportFrequencytoAdd, DatumJelzotoAdd, NapoktoAdd, EngtoAdd);
            Logic.addRiport(toAdd);
            //DatasInWindow.Items.Refresh();
            //InitializeComponent();
        }

        // Mégsem gomb lenyomása event
        // Törli az riport kijelölést
        private void button_Megsem_Click(object sender, RoutedEventArgs e)
        {
            DatasInWindow.UnselectAll();
            Logic.selectedRow = -1;     // ez kell???
        }

        // Törlés gomb lenyomása event
        // Törli az éppen kijelölt riportot
        private void button_Torol_Click(object sender, RoutedEventArgs e)
        {
            Logic.Riports.Remove(Logic.Riports[Logic.selectedRow]);
            DatasInWindow.Items.Refresh();
        }

        // Most gomb lenyomása event
        // a kijelölt riport példány idejét az aktuális időre állítja
        private void button_Most_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
