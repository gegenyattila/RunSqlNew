using OfficeOpenXml;
using RunSqlNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class RunSqlLogic : IRunSqlLogic
    {
        public ExcelDatas CurrentlySelected { get; set; }
        public ObservableCollection<ExcelDatas> Datas { get; set; }

        public RunSqlLogic()
        {
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

                    if (i == 1)
                    {
                        CurrentlySelected = new ExcelDatas(datum, ido, riport, xls_kvt, xls_nev, cimek, h_h_n_e, df, m_nap, eng);
                    }

                    for (int j = 1; j <= colCount; j++)
                    {
                        if (worksheet.Cells[i, j] != null && worksheet.Cells[i, j].Value != null)
                        {
                            switch (j)
                            {
                                case 1:
                                    {
                                        datum = worksheet.Cells[i, j].Value.ToString();
                                        break;
                                    }
                                case 2:
                                    {
                                        ido = worksheet.Cells[i, j].Value.ToString();
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

        public string ReturnDatas(int rowIndex, int colIndex)
        {
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
    }
}
