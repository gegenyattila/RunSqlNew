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
            Excel.Application xlapp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlapp.Workbooks.Open("C:\\Users\\GégényAttilaGábor\\Documents\\runsqltest.xlsx");
            Excel._Worksheet xlwoorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlwoorksheet.UsedRange;

            for (int i = 1; i <= 5; i++)
            {
                string datum = "";
                string ido = "";
                string riport = "";
                string xls_kvt = "";
                string xls_nev = "";
                string cimek = "";
                string h_h_n_e = "";
                string df = "";
                string m_nap = "";
                string eng = "";

                Datas = new ObservableCollection<ExcelDatas>();

                for (int j = 1; j <= 5; j++)
                {
                    //new line
                    if (j == 1)
                        Console.Write("\r\n");

                    //write the value to the console
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        switch (j)
                        {
                            case 1:
                                {
                                    DateTime test = DateTime.FromOADate(xlRange.Cells[i, j].Value2);//.ToString();
                                    datum = test.ToShortDateString();
                                    break;
                                }
                            case 2:
                                {
                                    DateTime test = DateTime.FromOADate(xlRange.Cells[i, j].Value2);//.ToString();
                                    ido = test.Hour.ToString() +":"+ test.Minute.ToString();
                                    if(test.Minute.ToString() == "0")
                                    {
                                        ido += "0";
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    riport = xlRange.Cells[i, j].Value2.ToString();
                                    break;
                                }
                            case 4:
                                {
                                    xls_kvt = xlRange.Cells[i, j].Value2.ToString();
                                    break;
                                }
                            case 5:
                                {
                                    xls_nev = xlRange.Cells[i, j].Value2.ToString();
                                    break;
                                }
                            case 6:
                                {
                                    cimek = xlRange.Cells[i, j].Value2.ToString();
                                    break;
                                }
                            case 7:
                                {
                                    h_h_n_e = xlRange.Cells[i, j].Value2.ToString();
                                    break;
                                }
                            case 8:
                                {
                                    df = xlRange.Cells[i, j].Value2.ToString();
                                    break;
                                }
                            case 9:
                                {
                                    m_nap = xlRange.Cells[i, j].Value2.ToString();
                                    break;
                                }
                            case 10:
                                {
                                    eng = xlRange.Cells[i, j].Value2.ToString();
                                    break;
                                }
                            default:
                                break;
                        }
                        Datas.Add(new ExcelDatas(datum, ido, riport, xls_kvt, xls_nev, cimek, h_h_n_e, df, m_nap, eng));
                    }
                    //string test = xlRange.Cells[i, j].Value2.ToString() + "\t";

                    //add useful things here!   
                }
            }

            

            //Datas.Add(new ExcelDatas());
            //Datas.Add(new ExcelDatas());
            //Datas.Add(new ExcelDatas());


        }
    }
}
