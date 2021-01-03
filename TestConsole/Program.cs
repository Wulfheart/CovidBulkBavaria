using System;
using System.IO;
using iText.Barcodes;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            //b.IncludeLabel = true;
            //System.Drawing.Image img = b.Encode(BarcodeLib.TYPE.CODE128, "101554573");
            //img.Save(@"C:\Users\Alex\Documents\temp\img\test.jpg");

            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream(@"C:\Users\Alex\Documents\temp\img\test.pdf", FileMode.Create, FileAccess.Write)));
            Document document = new Document(pdfDocument);

            //String line = "Hello! Welcome to iTextPdf";
            //document.Add(new Paragraph(line));
            Barcode128 b = new Barcode128(pdfDocument);
            b.SetCodeType(Barcode128.CODE128);
            b.SetCode("101554573");

            PdfFormXObject bobject = b.CreateFormXObject(pdfDocument);
            BarcodeQRCode qr = new BarcodeQRCode("101554573");
            document.Add(new Paragraph("VORNAME NACHNAME 12-09-1992").SetFontSize(25).SetPaddingBottom(10));
            document.Add(new Paragraph("ID:"));
            document.Add(new Paragraph("101554573").SetFontSize(40));
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
            //document.Add(new Image(qr.CreateFormXObject(pdfDocument)).SetAutoScale(true));
            document.Close();
            Console.WriteLine("Awesome PDF just got created.");
        }
    }
}
