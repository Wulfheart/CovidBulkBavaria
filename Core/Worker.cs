using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Worker
    {
        public static void Run(string excel, string outputdir, int amount)
        {
            var data = ExcelParser.Parse(excel);
            _ = data;
        }
    }
}
