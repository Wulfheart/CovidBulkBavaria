using CommandLine;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace CliClient
{
    class Program
    {
        public class Options
        {
            [Option('a', "amount", Required = false, HelpText = "Die Anzahl der Wiederholungen der Erstellen von Testbögen", Min = 1)]
            public int amount { get; set; } = 1;
            [Option('x', "excel", Required = true, SetName = "x")]
            public string excelPath { get; set; }
            [Option('o', "out", Required = true, SetName = "o")]
            public string outputPath { get; set; }
        }
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {

                //Console.WriteLine("Öffne Liste der Personen");
                //var persons = Core.ExcelParser.Parse(o.excelPath);
                //int count = persons.Count;
                //Console.WriteLine("Liste der Personen geöffnet");
                //for (int j = 1; j <= count; j++)
                //{
                //    var person = persons[j];
                //    Console.WriteLine("Person {0} von {1}: {2} {3}", j, count, person.Surname, person.Forename);
                //    for (int i = 1; i <= o.amount; i++)
                //    {
                //        Console.WriteLine("Generiere {0} von {1} für {2} {3}", i, o.amount, person.Surname, person.Forename);
                //        var entry = new Core.Entry(person, i);
                //        Console.WriteLine("Hole ID aus dem Internet");
                //        entry.GetID().RunSynchronously();
                //        Console.WriteLine("ID erhalten ({0})", entry.ID);
                //        Console.WriteLine("Erstelle PDF");
                //        entry.CreatePdf(o.outputPath);
                //        Console.WriteLine("PDF erstellt");
                //    }
                //}
            });
        }
    }
}
