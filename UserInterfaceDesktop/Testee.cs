using OfficeOpenXml.FormulaParsing.Excel.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserInterfaceDesktop
{
    public class Testee
    {
        public DateTime Birthday { get; set; }
        public GenderEnum Gender { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ZIPCode { get; set; }
        public string City { get; set; }
        public string Country { get; } = "DE";
        public string GSA { get; } = "0";
        public string CoronaApp { get; } = "0";
        public string Symptoms { get; } = "0";
        public string FirstTest { get; } = "0";
        public string TestReason { get; } = "0";
        public string VAufendhalt3 { get; } = "2021";
        public string BAufendhalt3 { get; } = "2021";
        public int Line { get; set; }
       
    }
}
