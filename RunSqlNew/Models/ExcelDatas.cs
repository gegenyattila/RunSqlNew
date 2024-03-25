﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
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
    }
}