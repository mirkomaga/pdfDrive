using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace pdfDrive
{
    class Test
    {
        public static void importPage(PdfReader reader, int nPage)
        {
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage = null;

            sourceDocument = new Document(reader.GetPageSizeWithRotation(1));
            pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(@"adsdsdasdsa.pdf", System.IO.FileMode.Create));

            sourceDocument.Open();

            importedPage = pdfCopyProvider.GetImportedPage(reader, nPage);
            pdfCopyProvider.AddPage(importedPage);

            sourceDocument.Close();
        }
    }
}
