using DocumentFormat.OpenXml.Packaging;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace ResumeAnalyzer.Services
{
    public class TextExtractorService
    {
        public async Task<string> ExtractTextAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            var extension = Path.GetExtension(file.FileName).ToLower();
            switch (extension)
            {
                case ".pdf":
                    return await ExtractTextFromPdfAsync(file);
                case ".docx":
                    return await ExtractTextFromDocxAsync(file);
                default:
                    throw new NotSupportedException("Only PDF and DOCX files are supported.");
            }
        }

        private async Task<string> ExtractTextFromPdfAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var reader = new PdfReader(new MemoryStream(memoryStream.ToArray()));
            var pdfDoc = new PdfDocument(reader);

            var text = "";
            for (var i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                text += PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i));
            }

            pdfDoc.Close();
            reader.Close();

            return text;
        }

        private async Task<string> ExtractTextFromDocxAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            using var doc = WordprocessingDocument.Open(memoryStream, false);

            return doc.MainDocumentPart.Document.Body.InnerText;
        }
    }
}
