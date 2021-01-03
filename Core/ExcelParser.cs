using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core
{
    public class ExcelParser
    {
        public static List<Testee> Parse(string path)
        {
            var fi = new FileInfo(path);
            var p = new ExcelPackage(fi);
            var ws = p.Workbook.Worksheets["Personen"];
            List<Testee> persons = new List<Testee>();
            for(int i = ws.Dimension.Start.Row + 1; i <= ws.Dimension.End.Row; i++)
            {
                persons.Add(new Testee
                {
                    Surname = ws.Cells[i, 1].Text,
                    Forename = ws.Cells[i, 2].Text,
                    Gender = ws.Cells[i, 3].Text == "M" ? GenderEnum.MALE : GenderEnum.FEMALE,
                    Birthday = DateTime.Parse(ws.Cells[i, 4].Text),
                    Address = ws.Cells[i, 5].Text,
                    ZIPCode = ws.Cells[i, 6].Text,
                    City = ws.Cells[i, 7].Text,
                    Email = ws.Cells[i, 8].Text,
                    Phone = ws.Cells[i, 9].Text,
                    Line = i,
                });
            }
            return persons;
        }
    }
}
