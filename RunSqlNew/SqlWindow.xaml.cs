using System;
using System.Data;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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
using Microsoft.Office.Interop.Excel;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using Logic;
using System.Windows.Markup;
using OfficeOpenXml;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace RunSqlNew
{
    /// <summary>
    /// Interaction logic for SqlWindow.xaml
    /// </summary>
    public partial class SqlWindow : System.Windows.Window
    {
        public IRunSqlLogic Logic;

        public SqlWindow()
        {
            InitializeComponent();
        }

        // Logic példány fogadása a főablaktól
        // Jelenleg ezt a metódust kívülről kell meghívni. Nem lehet máshogy átadni a Logic példányt,
        // hogy ne a MainWindow.xaml.cs-ből kelljen meghívni ???
        public void SettingLogic(/*ref*/ IRunSqlLogic logic)
        {
            this.Logic = logic;
        }

        // Ablak betöltődésére adott reakció
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Activate();

            // Az lenne az ideális, ha betöltődés után a kurzort egyből "elfogná" az ablakban lévő szövegmező
            // Jelenleg ezekkel a parancsokkal nem kapja el !!!
            CaptureMouse();
            textbox_sqlfilepath.CaptureMouse();
        }

        // OK gomb lenyomására adott reakció
        // Meghívja a megfelelő Logic metódust
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Megadott SQL elérési út kinyerése az input szövegmezőből
            // Az elérési út ellenörzésére még szükség van !!!
            string sqlpath = textbox_sqlfilepath.Text;

            // Megfelelő Logic metódus meghívása
            this.Logic.OdbcConnectionSetup(sqlpath);
        }

        // Szövegmező reakció enter lenyomásra (minden gomblenyomásra meghívódik, nemcsak enterre, optimalizálható?)
        private void textbox_sqlfilepath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }
    }
}
