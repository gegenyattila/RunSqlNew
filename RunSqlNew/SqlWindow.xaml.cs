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
            string sqlpath = textbox_sqlfilepath.Text;

            //if (File.Exists(sqlpath)) 
            //{
            //    //MessageBox.Show("fasza");
            //    //{
            //    if (sqlpath.Split('.').LastOrDefault() == ".sql")
            //    {
            //        this.Logic.SqlQuery = sqlpath;
            //        Close();
            //    }
            //    else
            //        MessageBox.Show("A megadott elérési út nem SQL fájlra mutat!");
            //}
            //else
            //    MessageBox.Show("Nem sikerült megtalálni az SQL fájlt.");

            #region új oledb próba

            //string networkpath = @"\\192.168.96.9\runSql\riportok\DrinkMix";
            //string username = "dradmin";
            //string password = "drinks96";

            // Provider = MySQLProv; Data Source = mydb; User Id = myUsername; Password = myPassword;
            //L:\runSql\riportok\DrinkMix\DrinkMix_rendeles_adatok.sql

            this.Logic.SqlQuery = sqlpath;

            string connectionString = "Provider=SQLOLEDB;Data Source=192.168.96.5;User ID=dradmin;Password=drinks96";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(this.Logic.SqlQuery);

                command.Connection = connection;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("fos");
                }
            }

            #endregion



            #region rossz próbálkozások

            #region WNetAddConnection2 próbálkozás
            /*
            string networkpath = @"\\192.168.96.9\runSql\riportok\DrinkMix";
            string username = "dradmin";
            string password = "drinks96";

            var netResource = new NETRESOURCE
            {
                dwType = 1,
                lpRemoteName = @"\\192.168.96.9\runSql"
            };

            int result = WNetAddConnection2(ref netResource, password, username, 0);

            if (result == 0)
            {
                if(Directory.Exists(networkpath))
                {
                    string folder = Directory.GetFiles(networkpath).ToString();
                }
                else
                {
                    MessageBox.Show("NOPE1");
                }

                WNetCancelConnection2(netResource.lpRemoteName, 0, true);
            }
            else
                MessageBox.Show("NOPE2");
            */
            #endregion

            //NetworkCredential credentials = new NetworkCredential(@"dradmin", "drinks96");
            //bool CanSeeDirectory = Directory.Exists(networkpath);

            //\runSql\riportok\DrinkMix

            //string networkpath = @"192.168.96.9";
            //string username = "dradmin";
            //string password = "drinks96";


            //SqlConnection conn = new SqlConnection();
            //string connString = "Server=192.168.96.9\\runSql\\riportok\\DrinkMix;Database=DrinkMix_rendeles_adatok.sql;User Id=dradmin;Password=drinks96";
            //string connString = @"Data Source=192.168.96.5;User ID=cdrunsql;Password=7BB569A26BB255BF5F";

            //conn.ConnectionString = connString;

            //conn.Open();

            #region OdbcConnectionStringBUILDER

            /*
            //string connstring = "Driver={ODBC Driver 17 for SQL Server};Server=192.168.96.9;Database=cyberjani;Uid=ab;Pwd=pass@word1";

            if (Directory.Exists("A:\\runSql"))
            {
                // ÍGY MEGTALÁLJA
                //MessageBox.Show("okay");
            }

            //ODBC connstring BUILDER próbálkozás
            
            OdbcConnectionStringBuilder builder =
            new OdbcConnectionStringBuilder();
            builder.Driver = "ODBC Driver 17 for SQL Server";

            //builder.Add("Server", "192.168.96.9");
            builder.Add("Dbq", "A:\\");
            builder.Add("Uid", "dradmin");
            builder.Add("Pwd", "drinks96");

            try
            {
                using (OdbcConnection connection = new OdbcConnection(builder.ConnectionString))
                {
                    connection.Open();
                    if (Directory.Exists(@"\\192.168.96.9\runSql"))
                    {
                        MessageBox.Show("okay");
                    }
                    string ok = connection.State.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */

            #endregion

            //string connectionString = $"DSN={networkpath};Uid={username};Pwd={password};";

            //string connectionString = $"DSN={networkpath};Database=IBMDA400;Uid={username};Pwd={password};";

            //string connectionString = "Provider=IBMDA400;Data source=cyberjani;User ID=dradmin;Password=drinks96;Force Translate=1250;";

            // ODBCConnection próbálkozás
            /*
            string connectionString = "Driver={ODBC Driver 17 for SQL Server};Server=cyberjani;Database=192.168.96.5;UID=cdrunsql;PWD=7BB569A26BB255BF5F";

            try
            {
                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    connection.Open();
                    if(Directory.Exists(@"\\192.168.96.9\runSql")) 
                    {
                        MessageBox.Show("okay");
                    }
                    string ok = connection.State.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */

            #region OleDbConnection ConStringBUILDER

            //OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            //builder.ConnectionString = @"Data Source=A:\cyberjani";


            #endregion

            //using (OleDbConnection connection = new OleDbConnection(networkpath, credentials))
            //{

            //}

            #endregion
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
