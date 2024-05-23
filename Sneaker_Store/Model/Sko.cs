using System;

namespace Sneaker_Store.Model
{
    public class Sko
    {
        // Instance fields
        private int _skoid;
        private string _maerke;
        private string _model;
        private int _str;
        private double _pris;
        private string _imageUrl; // Add this line

        // Properties
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

        public string ImageUrl // Add this property
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }

        // Constructor
        public Sko(int skoid, string maerke, string model, int str, double pris, string imageUrl) // Update constructor
        {
            _skoid = skoid;
            _maerke = maerke;
            _model = model;
            _str = str;
            _pris = pris;
            _imageUrl = imageUrl;
        }

        public Sko() // default
        {
            _skoid = 0;
            _maerke = "";
            _model = "";
            _str = 0;
            _pris = 0;
            _imageUrl = ""; // Add this line
        }

        // Method
        public override string ToString()
        {
            return $"{{{nameof(SkoId)}={SkoId.ToString()}, {nameof(Maerke)}={Maerke}, {nameof(Model)}={Model}, {nameof(Str)}={Str.ToString()}, {nameof(Pris)}={Pris.ToString()}, {nameof(ImageUrl)}={ImageUrl}}}";
        }
    }
}