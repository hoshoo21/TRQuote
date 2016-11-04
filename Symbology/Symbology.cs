using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRQuoteCore.Symbology
{
    public enum DetectedSymbology
    {
        Equity,
        EquityOptions,
        Futures,
        Index,
        PinkSheets,

        ASXEquity,
        OSLOEquity,
        LondonEquity,
        AmsterdamEquity,
        LondonLIFFE,
        BruselsEquity,
        ParisEquity,
        LisbonEquity,
        OsakaEquity,
        ChiXEuropeEquity,
        Canadian

    }
    class Symbology
    {
        public  static string FormatOptionRIC(string Symbol)
        {
            if (!Symbol.StartsWith("."))
            {
                return Symbol;
            }
            int rootlenght = 0;
            string optionside = Symbol.Substring(13, 1);

            bool isPut = true;
            string FormattedRIC = "";
            string CallTypeIndicator = "";
            string strikeprice = "00000000";
            if (optionside.ToLower() == "p")
            {
                isPut = true;
            }

            FormattedRIC = Symbol.Substring(0, 6).Trim();
            FormattedRIC = FormattedRIC.Replace(".", "");
            rootlenght = FormattedRIC.Length;

            string MonthIndicator = Symbol.Substring(9, 2);

            switch (MonthIndicator)
            {
                case "01":
                    if (!isPut)
                    {
                        FormattedRIC = FormattedRIC + "M";

                    }
                    else
                    {
                        FormattedRIC = FormattedRIC + "A";

                    }
                    break;
                case "02":
                    if (!isPut)
                    {
                        FormattedRIC = FormattedRIC + "N";

                    }
                    else
                    {
                        FormattedRIC = FormattedRIC + "B";

                    }
                    break;
                case "03":
                    if (!isPut)
                    {
                        FormattedRIC = FormattedRIC + "O";

                    }
                    else
                    {
                        FormattedRIC = FormattedRIC + "C";

                    }
                    break;
                case "04":
                    if (!isPut)
                    {
                        FormattedRIC = FormattedRIC + "P";
                    }
                    else
                    {
                        FormattedRIC = FormattedRIC + "D";

                    }
                    break;
                case "05":
                    if (!isPut)
                    {
                        FormattedRIC = FormattedRIC + "Q";

                    }
                    else
                    {
                        FormattedRIC = FormattedRIC + "E";

                    }
                    break;
                case "06":
                    if (!isPut)
                    {
                        FormattedRIC = FormattedRIC + "R";

                    }
                    else
                    {
                        FormattedRIC = FormattedRIC + "F";

                    }
                    break;
                case "07":
                    if (!isPut)
                    {
                        FormattedRIC = FormattedRIC + "S";
                    }
                    else
                    {

                        FormattedRIC = FormattedRIC + "G";

                    }
                    break;
                case "08":
                    if (!isPut)
                    {
                        FormattedRIC = FormattedRIC + "T";
                    }
                    else
                    {
                        FormattedRIC = FormattedRIC + "H";
                    }
                    break;
                case "09":
                    if (!isPut)
                    {
                        FormattedRIC = FormattedRIC + "U";

                    }
                    else
                    {
                        FormattedRIC = FormattedRIC + "I";

                    }
                    break;
                case "10":
                    if (!isPut)
                    {

                        FormattedRIC = FormattedRIC + "V";

                    }
                    else
                    {
                        FormattedRIC = FormattedRIC + "J";
                    }
                    break;
                case "11":
                    if (!isPut)
                    {
                        FormattedRIC = FormattedRIC + "W";

                    }
                    else
                    {
                        FormattedRIC = FormattedRIC + "K";

                    }
                    break;
                case "12":
                    if (!isPut)
                    {
                        FormattedRIC = FormattedRIC + "X";
                    }
                    else
                    {

                        FormattedRIC = FormattedRIC + "L";

                    }
                    break;
            }

            FormattedRIC += Symbol.Substring(11, 2);
            FormattedRIC += Symbol.Substring(7, 2);
            strikeprice = Symbol.Substring(14);
            bool riccomplete = false;
            if (Symbol.Substring(14).TrimStart('0').Length == 5 && (int.Parse(Symbol.Substring(14, 6)) % 10) == 0)
            {
                FormattedRIC += "0" + (int.Parse(Symbol.Substring(14)) / 1000).ToString();

                FormattedRIC = FormattedRIC.PadRight(FormattedRIC.Length + 2, '0');
                riccomplete = true;
            }
            else if (Symbol.Substring(14).TrimStart('0').Length == 5 && (int.Parse(Symbol.Substring(14, 6)) % 10) > 0)
            {
                FormattedRIC += "0" + (int.Parse(Symbol.Substring(14)) / 1000).ToString();
                FormattedRIC += (int.Parse(Symbol.Substring(14, 6)) % 10).ToString();
                FormattedRIC = FormattedRIC.PadRight(FormattedRIC.Length + 1, '0');
                riccomplete = true;
            }
            if (Symbol.Substring(14).TrimStart('0').Length == 6 && (int.Parse(Symbol.Substring(14, 6)) % 10) == 0)
            {
                FormattedRIC += (int.Parse(Symbol.Substring(14)) / 1000).ToString();
                FormattedRIC = FormattedRIC.PadRight(FormattedRIC.Length + 2, '0');

            }
            else
            {
                if (!riccomplete)
                {
                    FormattedRIC += (int.Parse(Symbol.Substring(14)) / 1000).ToString();
                    FormattedRIC += (int.Parse(Symbol.Substring(14, 6)) % 10).ToString();
                    FormattedRIC = FormattedRIC.PadRight(FormattedRIC.Length + 1, '0');

                }
            }


            return FormattedRIC + ".U";
        }
        private static string FormatFutures(string Symbol){

            string rootsymbol =  Symbol.Substring(0,Symbol.IndexOf("/"));
            int year = int.Parse(Symbol.Substring(Symbol.IndexOf("/") + 2, 1));
            string month = Symbol.Substring(Symbol.IndexOf("/") + 3,1);

            return rootsymbol+month+year;
        }


        public static DetectedSymbology DetectSymbology(string Symbol)
        {
            if (Symbol.StartsWith(".") && Symbol.Length >= 20)
            {
                return DetectedSymbology.EquityOptions;
            }
            if (Symbol.StartsWith("="))
                return DetectedSymbology.Index;
            if (Symbol.EndsWith(".ASX"))
                return DetectedSymbology.ASXEquity;
            if (Symbol.EndsWith(".OL"))
                return DetectedSymbology.OSLOEquity;
            if (Symbol.EndsWith(".L"))
                return DetectedSymbology.LondonEquity;
            if (Symbol.EndsWith(".OS"))
                return DetectedSymbology.OsakaEquity;

            if (Symbol.EndsWith(".PS"))
                return DetectedSymbology.PinkSheets;
            if (Symbol.EndsWith(".QX"))
                return DetectedSymbology.PinkSheets;
            if (Symbol.EndsWith(".QB"))
                return DetectedSymbology.PinkSheets;
            if (Symbol.EndsWith(".QO"))
                return DetectedSymbology.PinkSheets;
            if (Symbol.EndsWith(".OB"))
                return DetectedSymbology.PinkSheets;
            if (Symbol.EndsWith(".CHIX"))
                return DetectedSymbology.ChiXEuropeEquity;
            if (Symbol.EndsWith(".CN"))
                return DetectedSymbology.Canadian;
            if (Symbol.Length > Symbol.IndexOf("/") + 2)
            {
                if (char.IsNumber(Symbol[Symbol.IndexOf("/") + 1]) && //so this is future
                    char.IsNumber(Symbol[Symbol.IndexOf("/") + 2]))
                {
                    return DetectedSymbology.Futures;
                }
            }
            return DetectedSymbology.Equity;
        }
       
        
        
        public static string FormatExchangeCode(string PrimaryExchange) {
            string excahngecode = "";
            switch (PrimaryExchange) { 
                case "UW":
                case "UV":
                case "UQ":
                    excahngecode = ".O";
                    break;
                case "UN":
                    excahngecode = ".N";
                    break;
                case "CV":
                    excahngecode = ".P";
                    break;
                case "UA":
                    excahngecode = ".A";
                    break;

            }

            return excahngecode;
        } 
    }
}
