using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RunSqlNew.Models;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace RunSqlNew.ViewModels
{
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
    public class MainWindowViewModel : ObservableRecipient
    {
        public ObservableCollection<ExcelDatas> Datas { get; set; }

        public MainWindowViewModel()
        {
            //Excel.Application xlapp = new Excel.Application();
            //Excel.Workbook xlWorkbook = xlapp.Workbooks.Open("C:\\Users\\GégényAttilaGábor\\Documents\\runsqltest.xlsx");
            //Excel._Worksheet xlwoorksheet = xlWorkbook.Sheets[1];
            //Excel.Range xlRange = xlwoorksheet.UsedRange;

            FileInfo file = new FileInfo("C:\\Users\\GégényAttilaGábor\\Documents\\runsqltest.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            Datas = new ObservableCollection<ExcelDatas>();

            using (var package = new ExcelPackage(file))
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


                    for (int j = 1; j <= colCount; j++)
                    {
                        //new line
                        if (j == 1)
                            Console.Write("\r\n");

                        //write the value to the console
                        if (worksheet.Cells[i, j] != null && worksheet.Cells[i, j].Value != null)
                        {
                            switch (j)
                            {
                                case 1:
                                    {
                                        //DateTime date = DateTime.FromOADate(DateTime.Parse(worksheet.Cells[i, j].Value));
                                        datum = worksheet.Cells[i, j].Value.ToString(); //= date.ToShortDateString();
                                        break;
                                    }
                                case 2:
                                    {
                                        //DateTime date = DateTime.FromOADate(worksheet.Cells[i, j].Value);//.ToString();
                                        ido = worksheet.Cells[i, j].Value.ToString();//date.Hour.ToString() + ":" + date.Minute.ToString();
                                        //if (date.Minute.ToString() == "0")
                                        //{
                                        //    ido += "0";
                                        //}
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


            //int rowCount = xlRange.Rows.Count;
            //int colCount = xlRange.Columns.Count;

            
        }
    }
}
