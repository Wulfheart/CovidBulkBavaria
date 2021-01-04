using iText.Kernel.Pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserInterfaceDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected string output {get; set;}
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Select_Excel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                Excel_File.Text = dlg.FileName;
            }

        }


        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            // Use SelectionStart property to find the caret position.
            // Insert the previewed text into the existing text in the textbox.
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

            double val;
            // If parsing is successful, set Handled to false
            e.Handled = !double.TryParse(fullText,
                                         System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign,
                                         CultureInfo.InvariantCulture,
                                         out val);
        }

        private async void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DisplayLog.Document.Blocks.Clear();
                Progress.IsIndeterminate = true;
                string excelPath = Excel_File.Text;
                int amount = int.Parse(Amount.Text);
                if (amount <= 0)
                {
                    throw new Exception("Die Anzahl der Testungen muss größer als 0 sein.");
                }
                if (String.IsNullOrWhiteSpace(excelPath))
                {
                    throw new Exception("Es sieht so aus, als wäre kein ExcelFile angegeben worden. Bitte hole das nach.");
                }
                SaveFileDialog dlg = new SaveFileDialog();

                dlg.FileName = $"Reihentestung_generiert_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm")}"; // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string outPutPath = dlg.FileName;
                    output = outPutPath;
                    AddToLog($"Einlesen der Exceldatei von {excelPath}");
                    List<Testee> persons = ExcelParser.Parse(excelPath);
                    AddToLog("Exceldatei erfolgreich eingelesen");
                    Progress.Maximum = persons.Count * amount + 1;
                    Progress.IsIndeterminate = false;
                    AdvanceProgressValue();
                    PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream(outPutPath, FileMode.Create, FileAccess.Write)));
                    iText.Layout.Document document = new iText.Layout.Document(pdfDocument);
                    int count = persons.Count;
                    for (var j = 1; j <= count; j++)
                    {
                        var person = persons[j - 1];
                        AddToLog($"Person {j} von {count}: {person.Surname} {person.Forename}");
                       
                        for (int i = 0; i < amount; i++)
                        {
                            AddToLog(String.Format("Generiere {0} von {1} für {2} {3}", i + 1, amount, person.Surname, person.Forename));
                            var entry = new Entry(person, i);
                            AddToLog("Hole ID aus dem Internet");
                            await entry.GetID();
                            //entry.ID = "100";
                            //await Task.Delay(500);
                            AddToLog(String.Format("ID erhalten ({0})", entry.ID));
                            AddToLog("Füge an PDF an");
                            entry.CreatePdf(pdfDocument, document, j == count && i == amount - 1);
                            AddToLog("An PDF angefügt");
                            AdvanceProgressValue();
                            
                            
                        }
                    }
                    document.Close();
                    AddToLog($"PDF erstellt unter {outPutPath}");
                    Progress.Value = 0;

                }
            }
            catch (Exception ex)
            {
                Progress.Value = 0;
                Progress.IsIndeterminate = false;
                AddToLog(ex.Message);
                AddToLog($"Löschen der beschädigten PDF-Datei unter {output}");
                File.Delete(output);
                MessageBox.Show(ex.Message + "\r");
            }
        }

        private void AddToLog(string message)
        {
            DisplayLog.Document.Blocks.Add(new Paragraph(new Run(message)));
            DisplayLog.ScrollToEnd();
            
        }

        private void AdvanceProgressValue()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.To = Progress.Value + 1;
            da.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 200));
            Progress.BeginAnimation(System.Windows.Controls.ProgressBar.ValueProperty, da);
        }

        
        
    }
}
