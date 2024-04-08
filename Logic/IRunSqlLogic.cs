using RunSqlNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IRunSqlLogic
    {
        public ExcelDatas CurrentlySelected { get; set; }
        public string ReturnDatas(int rowIndex, int colIndex);
        public ObservableCollection<ExcelDatas> Datas { get; set; }
    }
}
