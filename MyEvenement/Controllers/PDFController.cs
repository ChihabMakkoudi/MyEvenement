using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;

namespace MyEvenement.Controllers
{
    [Controller]
    [Route("PDF")]
    public class PDFController : Controller
    {
        [HttpGet]
        [Route("CreatePDFDocument")]
        public IActionResult CreatePDFDocument()
        {
            //Create a new PDF document
            PdfDocument document = new PdfDocument();

            //Add a page to the document
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;

            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            PdfFont font2 = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            //Draw the text Title
            graphics.DrawString("PDF Sample", font, PdfBrushes.Black, new PointF(160, 0));

            //Draw the text Title
            graphics.DrawString("this is some testing text for pdf generation" +
                                "\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla ornare est eget risus " +
                                "\nmaximus sagittis. Nulla blandit scelerisque mauris et pretium. Donec vel eleifend mi. " +
                                "\nSed id nunc ut felis cursus accumsan. Morbi nisi orci, consectetur ut sagittis imperdiet," +
                                "\npellentesque ac risus. Donec dignissim nec nisi non mattis.Proin facilisis, lorem quis " +
                                "\ndapibus varius, nibh sapien sagittis odio, at ornare justo nisl sed ante.Phasellus gravida " +
                                "\ndiam eu luctus fringilla. Etiam cursus tellus et eros feugiat pharetra sed nec lacus.",
                
                
                font2, PdfBrushes.Black, new PointF(15, 50));

            //Load the image as stream.
            FileStream imageStream = new FileStream("wwwroot/img/lang.png", FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);
            //Draw the image
            graphics.DrawImage(image, 0, 200);

            //Saving the PDF to the MemoryStream
            MemoryStream stream = new MemoryStream();

            document.Save(stream);

            //Set the position as '0'.
            stream.Position = 0;

            //Download the PDF document in the browser
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

            fileStreamResult.FileDownloadName = "Sample.pdf";

            return fileStreamResult;
        }
    }
}
