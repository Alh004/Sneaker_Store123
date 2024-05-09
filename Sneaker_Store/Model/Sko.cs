using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Sneaker_Store.Model
{
    public class Sko
    {
        //Instansfelter
        private int _skoid;
        private string _maerke;
        private string _model;
        private int _str;
        private double _pris;

        //Properties
        public int SkoId
        {
            get { return _skoid; }
            set { _skoid = value; }
        }

        public string Maerke
        {
            get { return _maerke; }
            set { _maerke = value; }
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public int Str
        {
            get { return _str; }
            set { _str = value; }
        }

        public double Pris
        {
            get { return _pris; }
            set { _pris = value; }
        }

        //Konstruktør
        public Sko(int skoid, string maerke, string model, int str, double pris)
        {
            _skoid = skoid;
            _maerke = maerke;
            _model = model;
            _str = str;
            _pris = pris;
        }

        public Sko() // default
        {
            _skoid = 0;
            _maerke = "";
            _model = "";
            _str = 0;
            _pris = 0;
        }


        //metode
        public override string ToString()
        {
            return $"{{{nameof(SkoId)}={SkoId.ToString()}, {nameof(Maerke)}={Maerke}, {nameof(Model)}={Model}, {nameof(Str)}={Str.ToString()}, {nameof(Pris)}={Pris.ToString()}}}";
        }

    }
}