using iText.Barcodes;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core
{
    public class Entry
    {
        public Testee Testee { get; set; }
        public string ID { get; set; }
        public int RunNumber { get; set; }

        public Entry(Testee T, int run)
        {
            Testee = T;
            RunNumber = run;
        }

        public async Task GetID()
        {
            var client = new HttpClient();
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(Testee.Symptoms), "symptome");
            form.Add(new StringContent(Testee.FirstTest), "firsttest");
            form.Add(new StringContent(Testee.TestReason), "testgrund");
            form.Add(new StringContent(Testee.VAufendhalt3), "VAufendhalt3");
            form.Add(new StringContent(Testee.BAufendhalt3), "BAufendhalt3");
            form.Add(new StringContent(Testee.Birthday.Day.ToString()), "birstday1");
            form.Add(new StringContent(Testee.Birthday.Month.ToString()), "birstday2");
            form.Add(new StringContent(Testee.Birthday.Year.ToString()), "birstday3");
            form.Add(new StringContent(Testee.Gender == GenderEnum.MALE ? "M" : "W"), "geschlecht");
            form.Add(new StringContent(Testee.Forename), "vorname");
            form.Add(new StringContent(Testee.Surname), "nachname");
            form.Add(new StringContent(Testee.Email), "email1");
            form.Add(new StringContent(Testee.Phone), "telefon");
            form.Add(new StringContent(Testee.Address), "strasse1");
            form.Add(new StringContent(Testee.ZIPCode), "plz1");
            form.Add(new StringContent(Testee.City), "stadt1");
            form.Add(new StringContent(Testee.Country), "land1");
            form.Add(new StringContent(Testee.GSA), "gsa");
            form.Add(new StringContent(Testee.CoronaApp), "coronaapp1");
            var response = client.PostAsync(@"https://covidtestbayern.sampletracker.eu/gateway.php", form).Result;
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            ID = Regex.Replace(content, @"\s+", "");
        }

        public void CreatePdf(string outputFolder)
        {
            string[] paths = { outputFolder, String.Format("{0}_{1}_{2}_{3}.pdf", RunNumber, Testee.Surname, Testee.Forename, ID),};
            string fullPath = Path.Combine(paths);
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream(fullPath, FileMode.Create, FileAccess.Write)));
            Document document = new Document(pdfDocument);

            //String line = "Hello! Welcome to iTextPdf";
            //document.Add(new Paragraph(line));
            Barcode128 b = new Barcode128(pdfDocument);
            b.SetCodeType(Barcode128.CODE128);
            b.SetCode(ID);

            PdfFormXObject bobject = b.CreateFormXObject(pdfDocument);
            BarcodeQRCode qr = new BarcodeQRCode(ID);
            document.Add(new Paragraph(String.Format("{0} {1} {2}", Testee.Surname, Testee.Forename, Testee.Birthday.ToString("yyyy-MM-dd"))).SetFontSize(25).SetPaddingBottom(10));
            document.Add(new Paragraph("ID:"));
            document.Add(new Paragraph(ID).SetFontSize(40));
            document.Add(new Paragraph("ID NICHT DOPPELT SCANNEN").SetFontColor(ColorConstants.RED).SetBold().SetPaddingBottom(20));
            //document.Add(new Image(bobject).SetAutoScale(true));
            Div d = new Div();
            d.SetWidth(UnitValue.CreatePercentValue(33));
            d.Add(new Image(bobject).SetAutoScale(true));
            d.SetMarginBottom(20);
            document.Add(d);
            Div q = new Div();
            q.SetWidth(UnitValue.CreatePercentValue(50));
            q.Add(new Image(qr.CreateFormXObject(pdfDocument)).SetAutoScale(true));
            document.Add(q);
            document.Close();
        }
    }
}
