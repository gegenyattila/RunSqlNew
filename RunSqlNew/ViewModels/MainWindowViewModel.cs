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
using Logic;

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
        public IRunSqlLogic Logic;

        public MainWindowViewModel()
        {

        }
    }
}
