using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace pdfDrive
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainI());
        }
        public static void process()
        {

        }

        public static bool googleLogin(string path)
        {

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var credentials = GoogleCredential.FromStream(stream);
                if (credentials.IsCreateScopedRequired)
                {
                    credentials = credentials.CreateScoped(new string[] { DriveService.Scope.Drive });
                }

                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credentials,
                    ApplicationName = "test",
                });

                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.Fields = "nextPageToken, files(id, name)";

                IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            }

            return false;
        }

        public static void gestiscoPDF(string path)
        {
            //var text = new TikaOnDotNet.TextExtraction.TextExtractor().Extract(path).Text.Trim();

            PdfReader reader = new PdfReader(path);
            
            List<String> pdfText = new List<string>();
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                try
                {
                    PdfTextExtractor.GetTextFromPage(reader, page, its);
                    string strPage = its.GetResultantText();
                    pdfText.Add(strPage);
                }
                catch
                {
                    mainI.writeLineLv(new List<string>() { "Pdf" , "Impossibile leggere pdf.", "KO"});
                }
            }

            reader.Close();
            //return text;
        }
    }
}
