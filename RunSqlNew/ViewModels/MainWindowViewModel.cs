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

namespace RunSqlNew.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public ObservableCollection<ExcelDatas> Datas { get; set; }

        public MainWindowViewModel()
        {
            Datas = new ObservableCollection<ExcelDatas>();

            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());

            Datas.Add(new ExcelDatas());

            Datas.Add(new ExcelDatas());

            Datas.Add(new ExcelDatas());

            Datas.Add(new ExcelDatas());

            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());

            Datas.Add(new ExcelDatas());

            Datas.Add(new ExcelDatas());

            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());
            Datas.Add(new ExcelDatas());


        }
    }
}
