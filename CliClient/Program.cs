using CommandLine;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace CliClient
{
    class Program
    {
        public class Options
        {
            //[Option('a', "amount", HelpText = "Die Anzahl der Wiederholungen der Erstellen von Testbögen", Min = 1, Default = 1)]
            [Option('a', "amount", Default = 1)]
            public int amount { get; set; }
            [Option('x', "excel", Required = true)]
            public string excelPath { get; set; }
            [Option('o', "out", Required = true)]
            public string outputPath { get; set; }
            [Option('m', "merge", Default = false)]
            public bool merge { get; set; }
        }
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {

                Console.WriteLine("Öffne Liste der Personen");
                var persons = Core.ExcelParser.Parse(o.excelPath);
                int count = persons.Count;
                Console.WriteLine("Liste der Personen geöffnet");
                for (int j = 1; j <= count; j++)
                {
                    var person = persons[j - 1];
                    Console.WriteLine("Person {0} von {1}: {2} {3}", j, count, person.Surname, person.Forename);
                    for (int i = 1; i <= o.amount; i++)
                    {
                        Console.WriteLine("Generiere {0} von {1} für {2} {3}", i, o.amount, person.Surname, person.Forename);
                        var entry = new Core.Entry(person, i);
                        Console.WriteLine("Hole ID aus dem Internet");
                        entry.GetID();
                        Console.WriteLine("ID erhalten ({0})", entry.ID);
                        Console.WriteLine("Erstelle PDF");
                        entry.CreatePdf(o.outputPath);
                        Console.WriteLine("PDF erstellt");
                    }
                }
                if (o.merge)
                {
                    Console.WriteLine("PDFs zusammenfügen");
                    string[] f = Directory.GetFiles(o.outputPath);
                    List<string> files = new List<string>(f);
                    var documents = new List<PdfDocument>();
                    Console.WriteLine($"Zusammenführen aller PDFs");
                    foreach (var t in files)
                    {
                        documents.Add(new PdfDocument(new PdfReader(t)));

                    }
                    var merged = new PdfDocument(new PdfWriter(Path.Combine(o.outputPath, $"__Alle_Pdfs.pdf")));

                    PdfMerger merger = new PdfMerger(merged);
                    foreach (var document in documents)
                    {
                        merger.Merge(document, 1, document.GetNumberOfPages());
                        document.Close();
                    }
                    merged.Close();
                    Console.WriteLine($"PDFs zusammengeführt");
                }
                Console.WriteLine("Vorgang abgeschlossen");
            });
        }
    }
}
