using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunSqlNew.Models
{
    public class ExcelDatas : ObservableObject
    {
        private string datum;
        private string ido;
        private string riport;
        private string xls_kvt;
        private string xls_nev;
        private string cimek;
        private string h_h_n_e;
        private string df;
        private string m_nap;
        private string eng;

        public string Dátum
        {
            get { return datum; }
            set { SetProperty(ref datum, value); }
        }

        public string Idő
        {
            get { return ido; }
            set { SetProperty(ref ido, value); }
        }

        public string Riport
        {
            get { return riport; }
            set { SetProperty(ref riport, value); }
        }

        public string XLS_KVT
        {
            get { return xls_kvt; }
            set { SetProperty(ref xls_kvt, value); }
        }

        public string XLS_NÉV
        {
            get { return xls_nev; }
            set { SetProperty(ref xls_nev, value); }
        }

        public string Címek
        {
            get { return cimek; }
            set { SetProperty(ref cimek, value); }
        }

        public string DF
        {
            get { return df; }
            set { SetProperty(ref df, value); }
        }

        public string M_nap
        {
            get { return m_nap; }
            set { SetProperty(ref m_nap, value); }
        }

        public string Eng
        {
            get { return eng; }
            set { SetProperty(ref eng, value); }
        }

        public ExcelDatas()
        {
            string defualt = "nincs adat";

            datum = defualt;
            ido = defualt;
            riport = defualt;
            xls_kvt = defualt;
            xls_nev = defualt;
            cimek = defualt;
            h_h_n_e = defualt;
            df = defualt;
            m_nap = defualt;
            eng = defualt;
        }

        public ExcelDatas(string datum = "nincs adat", string ido = "nincs adat", string riport = "nincs adat", 
            string xls_kvt = "nincs adat", string xls_nev = "nincs adat", string cimek = "nincs adat", 
            string h_h_n_e = "nincs adat", string df = "nincs adat", string m_nap = "nincs adat", string eng = "nincs adat")
        {
            this.datum = datum;
            this.ido = ido;
            this.riport = riport;
            this.xls_kvt = xls_kvt;
            this.xls_nev = xls_nev;
            this.cimek = cimek;
            this.h_h_n_e = h_h_n_e;
            this.df = df;
            this.m_nap = m_nap;
            this.eng = eng;
        }

        /*
        public ExcelDatas(string datum, string ido, string riport, string xls_kvt, string xls_nev,
            string cimek, string h_h_n_e, string df, string m_nap, string eng)
        {
            this.datum = datum;
            this.ido = ido;
            this.riport = riport;
            this.xls_kvt = xls_kvt;
            this.xls_nev = xls_nev;
            this.cimek = cimek;
            this.h_h_n_e = h_h_n_e;
            this.df = df;
            this.m_nap = m_nap;
            this.eng = eng;
        }
        */
    }
}
