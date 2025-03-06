using OfficeOpenXml;
using RunSqlNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.Data.Odbc;
using DocumentFormat.OpenXml.Vml;
using System.IO;
using DocumentFormat.OpenXml.Bibliography;
using System.ComponentModel;

namespace Logic
{
    public class RunSqlLogic : IRunSqlLogic
    {
        //public Riports CurrentlySelected { get; set; }

        // Éppen kijelölt riport sorának a száma
        public int selectedRow { get; set; }

        // Riportok adatainak tárolására szolgáló reaktív lista
        // Ideálisan a példányokban tárolt adatokat egy külön fájlban kellene lementeni
        public ObservableCollection<Riports> Riports { get; set; }

        // SQL lekérdezést eltároló string példány
        public string SqlQuery { get; set; }

        // Erre nincs szökség (???)
        private ExcelPackage package;

        // Logic konstruktor
        public RunSqlLogic()
        {

            // testpath:
            // "C:\\Users\\GégényAttilaGábor\\Documents\\runsqltest.xlsx"
            selectedRow = -1;

            Riports = new ObservableCollection<Riports>();

            this.RiportsBuilder();

            //RiportsBuilder();

            //SAJÁT GÉPEN TESZT
            //try { 
            //DatasSetup("C:\\Users\\GégényAttilaGábor\\Documents\\runsqltest.xlsx");
            //}
            //catch(FileNotFoundException e)
            //{
            //    throw e;
            //}
            //catch(InvalidOperationException e)
            //{
            //    throw e;
            //}
        }

        // Adatok kiszedése az elérési úttal megadott excel file-ból
        // HASZNÁLATON KÍVÜL!!!!!!!!!!!!!!!!!!!
        public void DatasSetup(string path)
        {
            // valamiért átmegy az ellenőrzés
            //if (PathExistance(path))
            //    throw new FileNotFoundException();

            #region régi teszt beolvasás
            FileInfo file = new FileInfo(path);
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            Riports = new ObservableCollection<Riports>();

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

                    //if (i == 1)
                    //{
                    //    CurrentlySelected = new ExcelDatas(datum, ido, riport, xls_kvt, xls_nev, cimek, h_h_n_e, df, m_nap, eng);
                    //}

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
                    Riports.Add(new Riports(datum, ido, riport, xls_kvt, xls_nev, cimek, h_h_n_e, df, m_nap, eng));
                }
            }
            #endregion
        }

        /*
        public void addRiport()
        {
            string riportsPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Riportok.ini");

            const Int32 BufferSize = 1024;
            using (var fileStream = File.OpenWrite(riportsPath))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                int riportsHelperListCount = 0;

                string line;

                // .ini fájlon végigiterálás
                // (Ez a while csak riportok számát keresi ki)
                while ((line = streamReader.ReadLine()) != null)
                {
                    // CSAK AKKOR HOZZA LÉTRE A MEGFELELŐ SZÁMÚ (üres) "Riports" PÉLDÁNYOKBÓL ÁLLÓ LISTÁT,
                    // HA BENNE VAN A "RiportNR" sor, ami megmondja, hogy hány különböző riport van
                    if (line.Contains("RiportNR"))
                    {
                        string[] splitHelper = line.Split('=');
                        riportsHelperListCount = Int32.Parse(splitHelper[1]);
                        splitHelper[1].Replace(int.Parse(splitHelper[1].ToString()))

                        break;
                    }
                }

                byte[] info = new UTF8Encoding(true).GetBytes(value);
                fileStream.Write(info, 0, info.Length);
            }
        }
        */

        // .ini fájl elején lévő RiportNR változót növeli
        // (használható lenne az "Hozzáad" gomb új elemének beírásához is???)
        public void addRiport(Riports newRiport)
        {
            string filePath = "Riportok.ini";
            string tempFile = "riportok_temphelp.ini";
            string searchText = "RiportNR";  // The line that contains this text will be modified
            string newLine = ""; // The new line content

            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(tempFile, false, Encoding.UTF8))
            {
                string line;
                int riportNum = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(searchText))  // Modify only the matching line
                    {
                        string[] splitHelper = line.Split('=');
                        int temp = int.Parse(splitHelper[1]);
                        riportNum = temp + 1;
                        newLine = "RiportNR=" + riportNum.ToString();
                        writer.WriteLine(newLine);
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }

                riportNum--;

                // Új riport attribútumainak egyenkénti beírása
                writer.WriteLine("Datum" + riportNum + '=' + newRiport.Dátum);
                writer.WriteLine("Ido" + riportNum + '=' + newRiport.Idő);
                writer.WriteLine("Riport" + riportNum + '=' + newRiport.Riport);
                writer.WriteLine("XLSDir" + riportNum + '=' + newRiport.XLS_KVT);
                writer.WriteLine("XLS" + riportNum + '=' + newRiport.XLS_NÉV);
                writer.WriteLine("Email" + riportNum + '=' + newRiport.Címek);
                writer.WriteLine("HaviHetiNapi" + riportNum + '=' + newRiport.H_H_N_E);
                writer.WriteLine("DatumFlag" + riportNum + '=' + newRiport.DF);
                writer.WriteLine("Munkanap" + riportNum + '=' + newRiport.M_nap);
                writer.WriteLine("Engedely" + riportNum + '=' + newRiport.Eng);
            }

            // Új riport adatainak felvitele

            // Replace the original file with the modified one
            File.Delete(filePath);
            File.Move(tempFile, filePath);

            //Console.WriteLine("File updated successfully!");
        }

        // Riportokat feldolgozó metódus
        // Átdolgozandó !!!
        private void RiportsBuilder()
        {
            #region .ini file kezelő osztály próbálkozás
            /*
            // Creates or loads an INI file in the same directory as your executable
            // named EXE.ini (where EXE is the name of your executable)
            // var MyIni = new IniFile();

            // Or specify a specific name in the current dir
            var MyIni = new IniFile("Riportok.ini");

            if (MyIni.KeyExists("RiportNR"))
                ;
            else
                ;

            // Or specify a specific name in a specific dir
            //var MyIni = new IniFile(@"C:\Settings.ini");
            */
            #endregion

            // .ini fájl elérési útjának kinyerése
            // C:\Users\gegeny.gabor\source\repos\gegenyattila\RunSqlNew\RunSqlNew\bin\Debug\net6.0-windows
            string riportsPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Riportok.ini");

            // Elérési út helyességének ellenőrzése
            bool ok1 = PathExistance(riportsPath);

            // Fájl megnyitása és feldolgozása
            const Int32 BufferSize = 1024;
            using (var fileStream = File.OpenRead(riportsPath))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                // Egyes sorokat kezelő és az elemeket tároló (lista) segédváltozók
                string line;
                ObservableCollection<Riports> riportsHelperList = new ObservableCollection<Riports>();

                int riportsHelperListCount = 0;

                // .ini fájlon végigiterálás
                // (Ez a while csak riportok számát keresi ki)
                while ((line = streamReader.ReadLine()) != null)
                {
                    // CSAK AKKOR HOZZA LÉTRE A MEGFELELŐ SZÁMÚ (üres) "Riports" PÉLDÁNYOKBÓL ÁLLÓ LISTÁT,
                    // HA BENNE VAN A "RiportNR" sor, ami megmondja, hogy hány különböző riport van
                    if (line.Contains("RiportNR"))
                    {
                        string[] splitHelper = line.Split('=');
                        riportsHelperListCount = Int32.Parse(splitHelper[1]);

                        for (int i = 0; i < riportsHelperListCount; i++)
                        {
                            riportsHelperList.Add(new Riports());
                        }
                        break;
                    }
                }

                // Lehetne szebb kivételkezelés !!!
                if(riportsHelperList.Count == 0 || riportsHelperListCount == 0)
                    throw new ArgumentException("A Riportok.ini fájl hibás, vagy üres!");

                // Elemek felodlgozásához segédváltozók:
                // Hány riportnál tartunk
                int riportsCountHelper = 0;
                // Riporton belül hány attributumnál tatunk
                int attributeCountHelper = 0;
                // Attributumok száma (ha változik a struktúra, átírandó!)
                int attributeCount = 10;

                // .ini fájlon végigiterálás
                // Hard coded ciklus, csak az attributeCount változóban megadótt darabszámú tulajdonsággal működik rendeltetésszerűen!
                // Akkor fut le, ha: -még nem értük el a fájl végét, -nem futunk üres sorra, -a riport számláló kisebb, mint a .ini fájlból kinyert RiportNR szám
                while ((line = streamReader.ReadLine()) != null && line != "" && riportsCountHelper < riportsHelperListCount)
                {
                    // Ellenőrzi, hogy az adott riporton belül hány attribútumnál tartunk. Ha a számláló elérte a megadott számot (=10),
                    // akkor visszaállítja 0-ra az attribútum számot és megnöveli a riport számlálót
                    if (attributeCountHelper == attributeCount)
                    {
                        attributeCountHelper = 0;
                        riportsCountHelper++;
                    }
                    
                    // Mielőtt megkezdi a sor feldolgozását, ellenőrzi, hogy biztosan attribútumot tartalmaz-e
                    if (line.Contains('=') && !line.Contains("RiportNR") && !line.Contains("[Riport]"))
                    {
                        // Aktuális sor szétválasztása '='-nél
                        string[] splitHelper = line.Split('=');
                        
                        /*
                        if (int.Parse(splitHelper[0].LastOrDefault().ToString()) != riportsCountHelper)
                        {
                            attributeCountHelper = 0;
                            riportsCountHelper++;
                        }
                        */

                        // Az attribútum számláló alapján meghatározza, hogy hanyadik attribútum-ot kell beállítania
                        switch (attributeCountHelper)
                        {
                            case 0:
                                {
                                    riportsHelperList[riportsCountHelper].Dátum = splitHelper[1];
                                    break;
                                }
                            case 1:
                                {
                                    riportsHelperList[riportsCountHelper].Idő = splitHelper[1];
                                    break;
                                }
                            case 2:
                                {
                                    riportsHelperList[riportsCountHelper].Riport = splitHelper[1];
                                    break;
                                }
                            case 3:
                                {
                                    riportsHelperList[riportsCountHelper].XLS_KVT = splitHelper[1];
                                    break;
                                }
                            case 4:
                                {
                                    riportsHelperList[riportsCountHelper].XLS_NÉV = splitHelper[1];
                                    break;
                                }
                            case 5:
                                {
                                    riportsHelperList[riportsCountHelper].Címek = splitHelper[1];
                                    break;
                                }
                            case 6:
                                {
                                    riportsHelperList[riportsCountHelper].H_H_N_E = splitHelper[1];
                                    break;
                                }
                            case 7:
                                {
                                    riportsHelperList[riportsCountHelper].DF = splitHelper[1];
                                    break;
                                }
                            case 8:
                                {
                                    riportsHelperList[riportsCountHelper].M_nap = splitHelper[1];
                                    break;
                                }
                            case 9:
                                {
                                    riportsHelperList[riportsCountHelper].Eng = splitHelper[1];
                                    break;
                                }
                            default:
                                break;
                        }
                    }

                    // Attribútum számláló növelése
                    attributeCountHelper++;
                }

                // Teljes segédlista betöltése a Logic osztály saját, végleges listájába
                Riports = riportsHelperList;
            }
        }

        // Elérési út helyességének ellenőrzése
        private bool PathExistance(string path)
        {
            int i = 0;
            string cleanPath = "";
            while(i < path.Length)
            {
                if (path[i] != '\\')
                {
                    cleanPath += path[i];
                    i++;
                }
                else if (path[i] == '\\')
                {
                    cleanPath += path[i];
                    i += 2;
                }
            }

            return Directory.Exists(cleanPath);
        }

        ~RunSqlLogic()
        {
            //SaveExcel();
        }

        // Indexekkel megadott adat visszaadása
        public string ReturnDatas(int rowIndex, int colIndex)
        {
            // üres sor
            if (rowIndex < 0 || rowIndex >= Riports.Count)
                return "";

            // Megfelelő oszlop kiválasztása
            switch (colIndex)
            {
                case 1:
                    {
                        return Riports[rowIndex].Dátum;
                    }
                case 2:
                    {
                        return Riports[rowIndex].Idő;
                    }
                case 3:
                    {
                        return Riports[rowIndex].Riport;
                    }
                case 4:
                    {
                        return Riports[rowIndex].XLS_KVT;
                    }
                case 5:
                    {
                        return Riports[rowIndex].XLS_NÉV;
                    }
                case 6:
                    {
                        return Riports[rowIndex].Címek;
                    }
                case 7:
                    {
                        return Riports[rowIndex].H_H_N_E;
                    }
                case 8:
                    {
                        return Riports[rowIndex].DF;
                    }
                case 9:
                    {
                        return Riports[rowIndex].M_nap;
                    }
                case 10:
                    {
                        return Riports[rowIndex].Eng;
                    }
                default:
                    return null;
            }
        }

        // Dátum formázáshoz segédfüggvény
        public string DateAppend(string s, string date)
        {
            string[] strings = s.Split('.');
            strings[0] += date;
            return strings[0] + strings[1];
        }

        // Létrehoz egy OdbcConnection példányt, amivel képes lefuttatni az inputban megadott SQL fájlt
        // A lehető legtöbb paramétert érdemes lenne kiszervezni egy .ini fájlba, ahol könnyen lehet szerkeszteni őket
        public void OdbcConnectionSetup(string sqlpath)
        {
            //string networkpath = @"\\192.168.96.9\runSql\riportok\DrinkMix";
            //string username = "dradmin";
            //string password = "drinks96";
            //L:\runSql\riportok\DrinkMix\DrinkMix_rendeles_adatok.sql

            this.SqlQuery = File.ReadAllText(sqlpath);

            // ConnectionString
            string connectionString = "Driver={iseries Access ODBC Driver};System=192.168.96.5;Uid=cdrunsql;Pwd=cdrunsql;";

            // A Connection létrehozása a connstring-el
            using (OdbcConnection odbcConnection = new OdbcConnection(connectionString))
            {
                //adapter létrehozása az adatok kinyeréséhez
                OdbcDataAdapter adapter = new OdbcDataAdapter(this.SqlQuery, odbcConnection);

                odbcConnection.Open();

                DataSet dataset = new DataSet();

                //dataset feltöltése az adapter lekérdezésből származó tartalmával
                adapter.Fill(dataset);

                //datatable létrehozása az excelbe való mentéshez
                System.Data.DataTable datatable = new System.Data.DataTable();

                //datatable feltöltése a kinyert adatokkal
                datatable = dataset.Tables[0];

                // Excel-t létrehozó és elmentő metódus meghívása
                this.ExcelAdapterAndSaver(datatable);
            }
        }

        //excel létrehozása és mentése megadott helyre !!! RÉGI, JELENLEG BIZTOS, HOGY ROSSZUL MŰKÖDIK !!!
        public void ExcelAdapterAndSaver(DataTable datatable)
        {
            return;
            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(datatable, "exceltest");
            wb.SaveAs("C:\\Users\\3dkruppsystem\\Downloads\\runsqlexceltest.xlsx");
        }

        #region Használaton kívüli dátum helyességet ellenőrző metódus
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
        #endregion
    }
}
