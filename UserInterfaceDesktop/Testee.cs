using OfficeOpenXml.FormulaParsing.Excel.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserInterfaceDesktop
{
    public class Testee
    {
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ZIPCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string GSA { get; set; }
        public string CoronaApp { get; } = "0";
        public string Symptoms { get; set; }
        public string FirstTest { get; set; }
        public string TestReason { get; } = "14";
        public string VAufendhalt3 { get; } = "2021";
        public string BAufendhalt3 { get; } = "2021";
        public int Line { get; set; }
       
    }
}
