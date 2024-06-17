using OfficeOpenXml;
using RunSqlNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class RunSqlLogic : IRunSqlLogic
    {
        //public ExcelDatas CurrentlySelected { get; set; }
        public int selectedRow { get; set; }
        public ObservableCollection<ExcelDatas> Datas { get; set; }

        private ExcelPackage package;

        public RunSqlLogic()
        {

            // testpath:
            // "C:\\Users\\GégényAttilaGábor\\Documents\\runsqltest.xlsx"
            selectedRow = -1;

            try { 
            DatasSetup("C:\\Users\\GégényAttilaGábor\\Documents\\runsqltest.xlsx");
            }
            catch(FileNotFoundException e)
            {
                throw e;
            }
            catch(InvalidOperationException e)
            {
                throw e;
            }
        }

        public void DatasSetup(string path)
        {
            // valamiért átmegy az ellenőrzés
            //if (PathExistance(path))
            //    throw new FileNotFoundException();

            FileInfo file = new FileInfo(path);
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            Datas = new ObservableCollection<ExcelDatas>();

            using (package = new ExcelPackage(file))
            {
                ExcelWorkbook workbook = package.Workbook;
                ExcelWorksheet worksheet = workbook.Worksheets.First(); //SingleOrDefault(w => w.Name == "sheet1");

                int rowCount = worksheet.Dimension.End.Row;
                int colCount = worksheet.Dimension.End.Column;

                for (int i = 1; i <= rowCount; i++)
                {
                    string? datum = "";
                    string? ido = "";
                    string? riport = "";
                    string? xls_kvt = "";
                    string? xls_nev = "";
                    string? cimek = "";
                    string? h_h_n_e = "";
                    string? df = "";
                    string? m_nap = "";
                    string? eng = "";

                    if (i == 1)
                    {
                        //CurrentlySelected = new ExcelDatas(datum, ido, riport, xls_kvt, xls_nev, cimek, h_h_n_e, df, m_nap, eng);
                    }

                    for (int j = 1; j <= colCount; j++)
                    {
                        if (worksheet.Cells[i, j] != null && worksheet.Cells[i, j].Value != null)
                        {
                            switch (j)
                            {
                                case 1:
                                    {
                                        datum = worksheet.Cells[i, j].Value.ToString().Split(" ").FirstOrDefault();
                                        break;
                                    }
                                case 2:
                                    {
                                        // ITT SPACE SZERINT SPLIT-EL, A MINTA ADATOK MIATT KELL, HOGY RENDESEN JELENJEN MEG, LEHET VÉGÜL NEM FOG KELLENI
                                        ido = worksheet.Cells[i, j].Value.ToString().Split(" ").LastOrDefault();
                                        break;
                                    }
                                case 3:
                                    {
                                        riport = worksheet.Cells[i, j].Value.ToString();
                                        break;
                                    }
                                case 4:
                                    {
                                        xls_kvt = worksheet.Cells[i, j].Value.ToString();
                                        break;
                                    }
                                case 5:
                                    {
                                        xls_nev = worksheet.Cells[i, j].Value.ToString();
                                        break;
                                    }
                                case 6:
                                    {
                                        cimek = worksheet.Cells[i, j].Value.ToString();
                                        break;
                                    }
                                case 7:
                                    {
                                        h_h_n_e = worksheet.Cells[i, j].Value.ToString();
                                        break;
                                    }
                                case 8:
                                    {
                                        df = worksheet.Cells[i, j].Value.ToString();
                                        break;
                                    }
                                case 9:
                                    {
                                        m_nap = worksheet.Cells[i, j].Value.ToString();
                                        break;
                                    }
                                case 10:
                                    {
                                        eng = worksheet.Cells[i, j].Value.ToString();
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                    }
                    Datas.Add(new ExcelDatas(datum, ido, riport, xls_kvt, xls_nev, cimek, h_h_n_e, df, m_nap, eng));
                }
            }
        }

        // commandSql: SQL statement
        public void DatasSetup_SqlConnection(string connectionString, string commandSql)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(commandSql);

                command.Connection = connection;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        private bool PathExistance(string path)
        {
            return Directory.Exists(path);
        }

        //public void SaveExcel()
        //{
        //    package.SaveAs();
        //}

        ~RunSqlLogic()
        {
            //SaveExcel();
        }

        public string ReturnDatas(int rowIndex, int colIndex)
        {
            // üres sor
            if (rowIndex < 0 || rowIndex >= Datas.Count)
                return "";
            //if (Datas[rowIndex].Dátum == null)
            //    return "";

            switch (colIndex)
            {
                case 1:
                    {
                        return Datas[rowIndex].Dátum;
                    }
                case 2:
                    {
                        return Datas[rowIndex].Idő;
                    }
                case 3:
                    {
                        return Datas[rowIndex].Riport;
                    }
                case 4:
                    {
                        return Datas[rowIndex].XLS_KVT;
                    }
                case 5:
                    {
                        return Datas[rowIndex].XLS_NÉV;
                    }
                case 6:
                    {
                        return Datas[rowIndex].Címek;
                    }
                case 7:
                    {
                        return Datas[rowIndex].H_H_N_E;
                    }
                case 8:
                    {
                        return Datas[rowIndex].DF;
                    }
                case 9:
                    {
                        return Datas[rowIndex].M_nap;
                    }
                case 10:
                    {
                        return Datas[rowIndex].Eng;
                    }
                default:
                    return null;
            }
        }

        public string DateAppend(string s, string date)
        {
            string[] strings = s.Split('.');
            strings[0] += date;
            return strings[0] + strings[1];
        }

        /*public bool CorrectDate(string date)
        {
            try
            {
                int month;

                if (month == 1)
                    return m_iDay;
                else
                {
                    forret = MonthDayCount::januar;

                    switch (month)
                    {
                        case 2:
                            forret += m_iDay;
                            break;
                        case 3:
                            forret += MonthDayCount::februarNoLeap + m_iDay;
                            break;
                        case 4:
                            forret += MonthDayCount::februarNoLeap + MonthDayCount::marcius +
                                +m_iDay;
                            break;
                        case 5:
                            forret += MonthDayCount::februarNoLeap + MonthDayCount::marcius +
                                MonthDayCount::aprilis + m_iDay;
                            break;
                        case 6:
                            forret += MonthDayCount::februarNoLeap + MonthDayCount::marcius +
                                MonthDayCount::aprilis + MonthDayCount::majus + m_iDay;
                            break;
                        case 7:
                            forret += MonthDayCount::februarNoLeap + MonthDayCount::marcius +
                                MonthDayCount::aprilis + MonthDayCount::majus + MonthDayCount::junius +
                                +m_iDay;
                            break;
                        case 8:
                            forret += MonthDayCount::februarNoLeap + MonthDayCount::marcius +
                                MonthDayCount::aprilis + MonthDayCount::majus + MonthDayCount::junius +
                                MonthDayCount::julius + m_iDay;
                            break;
                        case 9:
                            forret += MonthDayCount::februarNoLeap + MonthDayCount::marcius +
                                MonthDayCount::aprilis + MonthDayCount::majus + MonthDayCount::junius +
                                MonthDayCount::julius + MonthDayCount::augusztus + m_iDay;
                            break;
                        case 10:
                            forret += MonthDayCount::februarNoLeap + MonthDayCount::marcius +
                                MonthDayCount::aprilis + MonthDayCount::majus + MonthDayCount::junius +
                                MonthDayCount::julius + MonthDayCount::augusztus + MonthDayCount::szeptemeber
                                + m_iDay;
                            break;
                        case 11:
                            forret += MonthDayCount::februarNoLeap + MonthDayCount::marcius +
                                MonthDayCount::aprilis + MonthDayCount::majus + MonthDayCount::junius +
                                MonthDayCount::julius + MonthDayCount::augusztus + MonthDayCount::szeptemeber +
                                MonthDayCount::oktober + m_iDay;
                            break;
                        case 12:
                            forret += MonthDayCount::februarNoLeap + MonthDayCount::marcius +
                                MonthDayCount::aprilis + MonthDayCount::majus + MonthDayCount::junius +
                                MonthDayCount::julius + MonthDayCount::augusztus + MonthDayCount::szeptemeber +
                                MonthDayCount::oktober + MonthDayCount::november + m_iDay;
                            break;
                    }
                }
            }
        */
    }
}
