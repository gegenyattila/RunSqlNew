using RunSqlNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IRunSqlLogic
    {
        //public ExcelDatas CurrentlySelected { get; set; }
        public int selectedRow { get; set; }
        public string ReturnDatas(int rowIndex, int colIndex);
        public ObservableCollection<Riports> Riports { get; set; }
        public string DateAppend(string s, string date);
        public void DatasSetup(string path);
        public void OdbcConnectionSetup(string sqlpath);
        public void ExcelAdapterAndSaver(DataTable dataTable);
        public string SqlQuery { get; set; }
        public void addRiport(Riports newRiport);
        //public void SaveExcel();
    }
}
