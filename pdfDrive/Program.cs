using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using com.sun.tools.javac;
using sun.tools.tree;

namespace pdfDrive
{
    static class Program
    {
        public static Form globalForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            globalForm = new mainI();
            Application.Run(globalForm);
        }
        public static void process()
        {

        }

        public static bool googleLogin(string path)
        {
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var credentials = GoogleCredential.FromStream(stream);

            //using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            //{
            //    var credentials = GoogleCredential.FromStream(stream);
            //    if (credentials.IsCreateScopedRequired)
            //    {
            //        credentials = credentials.CreateScoped(new string[] { DriveService.Scope.Drive });
            //    }

            //    var service = new DriveService(new BaseClientService.Initializer()
            //    {
            //        HttpClientInitializer = credentials,
            //        ApplicationName = "Pdf Splitter",
            //    });

            //    FilesResource.ListRequest listRequest = service.Files.List();
            //    listRequest.Fields = "nextPageToken, files(id, name)";

            //    IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            //}

            return false;
        }
    }
}
