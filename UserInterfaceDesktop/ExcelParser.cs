using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UserInterfaceDesktop
{
    public class ExcelParser
    {
        public static string[] AllowedCountries = { "DE", "AF", "EG", "AX", "AL", "DZ", "AS", "AD", "AO", "AI", "AQ", "AG", "GQ", "AR", "AM", "AW", "AZ", "ET", "AU", "BS", "BH", "BD", "BB", "BY", "BE", "BZ", "BJ", "BM", "BT", "BO", "BQ", "BA", "BW", "BV", "BR", "IO", "BN", "BG", "BF", "BI", "CL", "CN", "CK", "CR", "CW", "DK", "CD", "DE", "DM", "DO", "DJ", "EC", "SV", "CI", "ER", "EE", "SZ", "FK", "FO", "FJ", "FI", "FR", "GF", "PF", "TF", "GA", "GM", "GE", "GH", "GI", "GD", "GR", "GL", "GP", "GU", "GT", "GG", "GN", "GW", "GY", "HT", "HM", "HN", "HK", "IN", "ID", "IM", "IQ", "IR", "IE", "IS", "IL", "IT", "JM", "JP", "YE", "JE", "JO", "VG", "VI", "KY", "KH", "CM", "CA", "CV", "KZ", "QA", "KE", "KG", "KI", "CC", "CO", "KM", "XK", "HR", "CU", "KW", "LA", "LS", "LV", "LB", "LR", "LY", "LI", "LT", "LU", "MO", "MG", "MW", "MY", "MV", "ML", "MT", "MA", "MH", "MQ", "MR", "MU", "YT", "MX", "FM", "MD", "MC", "MN", "ME", "MS", "MZ", "MM", "NA", "NR", "NP", "NC", "NZ", "NI", "NL", "NE", "NG", "NU", "KP", "MP", "MK", "NF", "NO", "OM", "AT", "TL", "PK", "PS", "PW", "PA", "PG", "PY", "PE", "PH", "PN", "PL", "PT", "PR", "CG", "RE", "RW", "RO", "RU", "MF", "SB", "ZM", "WS", "SM", "BL", "ST", "SA", "SE", "CH", "SN", "RS", "SC", "SL", "ZW", "SG", "SX", "SK", "SI", "SO", "ES", "LK", "SH", "KN", "LC", "PM", "VC", "ZA", "SD", "GS", "KR", "SS", "SR", "SJ", "SY", "TJ", "TW", "TZ", "TH", "TG", "TK", "TO", "TT", "TD", "CZ", "TN", "TR", "TM", "TC", "TV", "UG", "UA", "HU", "UY", "UZ", "VU", "VA", "VE", "AE", "US", "GB", "VN", "WF", "CX", "EH", "CF", "CY", };

        public static string[] AllowedGenders = { "M", "W" };

        public static List<Testee> Parse(string path)
        {
            var fi = new FileInfo(path);
            var p = new ExcelPackage(fi);
            var ws = p.Workbook.Worksheets["Personen"];
            List<Testee> persons = new List<Testee>();
            for (int i = ws.Dimension.Start.Row + 1; i <= ws.Dimension.End.Row; i++)
            {
                string gender = ws.Cells[i, 3].Text;
                if (!Array.Exists(AllowedGenders, e => String.Equals(gender, e, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Fehler in Zeile {i}: Das Geschlecht wurde nicht erkannt. Es wird nur M und W unterstützt.");
                }
                string country = ws.Cells[i, 10].Text;
                if (!Array.Exists(AllowedCountries, e => String.Equals(country, e, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Fehler in Zeile {i}: Das Herkunftsland wurde nicht erkannt. Bitte benutze eines der folgenden Kürzel: {String.Join(", ", AllowedCountries)}");
                }
                string gsa = ws.Cells[i, 11].Text;
                if (!Array.Exists(new string[] { "0", "1" }, e => String.Equals(gsa, e, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Fehler in Zeile {i}: Die Übermittelung an das Gesundheitsamt muss entweder 0 oder 1 sein.");
                }
                string first = ws.Cells[i, 12].Text;
                if (!Array.Exists(new string[] { "0", "1" }, e => String.Equals(first, e, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Fehler in Zeile {i}: Die Angabe ob Ersttestung muss entweder 0 oder 1 sein.");
                }
                string symptoms = ws.Cells[i, 13].Text;
                if (!Array.Exists(new string[] { "0", "1" }, e => String.Equals(symptoms, e, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Fehler in Zeile {i}: Die Angabe ob Symptome muss entweder 0 oder 1 sein.");
                }
                string surname = ws.Cells[i, 1].Text;
                if (String.IsNullOrWhiteSpace(surname))
                {
                    throw new Exception($"Fehler in Zeile {i}: Nachname nicht gesetzt");
                }
                string forename = ws.Cells[i, 2].Text;
                if (String.IsNullOrWhiteSpace(forename))
                {
                    throw new Exception($"Fehler in Zeile {i}: Vorname nicht gesetzt");
                }
                DateTime birthday = DateTime.Parse(ws.Cells[i, 4].Text);
                if (birthday > DateTime.Now || birthday < DateTime.Now.AddYears(-150))
                {
                    throw new Exception($"Fehler in Zeile {i}: Bitte überprüfe den Geburtstag");
                }
                persons.Add(new Testee
                {
                    Surname = surname,
                    Forename = forename,
                    Gender = gender,
                    Birthday = DateTime.Parse(ws.Cells[i, 4].Text ?? throw new Exception($"Fehler in Zeile {i}: Geburtstag nicht gesetzt")),
                    Address = ws.Cells[i, 5].Text,
                    ZIPCode = ws.Cells[i, 6].Text,
                    City = ws.Cells[i, 7].Text,
                    Email = ws.Cells[i, 8].Text ?? throw new Exception($"Fehler in Zeile {i}: Email nicht gesetzt"),
                    Phone = ws.Cells[i, 9].Text ?? throw new Exception($"Fehler in Zeile {i}: Telefonnummer nicht gesetzt"),
                    Country = country,
                    GSA = gsa,
                    Symptoms = symptoms,
                    FirstTest = first,
                    Line = i,
                });
            }
            return persons;
        }
    }
}
