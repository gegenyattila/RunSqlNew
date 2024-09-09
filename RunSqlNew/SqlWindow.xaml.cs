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

        public void SettingLogic(ref IRunSqlLogic logic)
        {
            this.Logic = logic;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Activate();
            CaptureMouse();
            textbox_sqlfilepath.CaptureMouse();

            //textbox_sqlfilepath.Select(0, 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //string networkpath = @"\\192.168.96.9\runSql\riportok\DrinkMix";
            //string username = "dradmin";
            //string password = "drinks96";
            //L:\runSql\riportok\DrinkMix\DrinkMix_rendeles_adatok.sql

            string sqlpath = textbox_sqlfilepath.Text;
            this.Logic.SqlQuery = File.ReadAllText(sqlpath);//sqlpath;
            string connectionString = "Driver={iseries Access ODBC Driver};System=192.168.96.5;Uid=cdrunsql;Pwd=cdrunsql;";

            using (OdbcConnection odbcConnection = new OdbcConnection(connectionString))
            {
                OdbcDataAdapter adapter = new OdbcDataAdapter(this.Logic.SqlQuery, odbcConnection);

                odbcConnection.Open();

                DataSet dataset = new DataSet();

                adapter.Fill(dataset);

                System.Data.DataTable datatable = new System.Data.DataTable();

                datatable = dataset.Tables[0];

                XLWorkbook wb = new XLWorkbook();
                wb.Worksheets.Add(datatable, "exceltest");
                wb.SaveAs("C:\\Users\\3dkruppsystem\\Downloads\\runsqlexceltest.xlsx");
            }
        }

    private void textbox_sqlfilepath_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }
    }
}
