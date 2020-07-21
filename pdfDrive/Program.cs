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
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Google.Apis.Analytics.v3;
using Newtonsoft.Json;

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
            string credPath = @path;
            var json = File.ReadAllText(credPath);
            var cr = JsonConvert.DeserializeObject<PersonalServiceAccountCred>(json); // "personal" service account credential
            var xCred = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(cr.ClientEmail)
            {
                Scopes = new[] {
                    AnalyticsService.Scope.AnalyticsManageUsersReadonly,
                    AnalyticsService.Scope.AnalyticsReadonly
                }
            }.FromPrivateKey(cr.PrivateKey));

            AnalyticsService service = new AnalyticsService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = xCred,
                }
            );
            var act1 = service.Management.Accounts.List().Execute();

            var actSum = service.Management.AccountSummaries.List().Execute();

            var resp1 = service.Management.Profiles.List(actSum.Items[0].Id, actSum.Items[0].WebProperties[0].Id).Execute();
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
    public class PersonalServiceAccountCred
    {
        public string type { get; set; }
        public string project_id { get; set; }
        public string private_key_id { get; set; }
        public string private_key { get; set; }
        public string client_email { get; set; }
        public string client_id { get; set; }
        public string auth_uri { get; set; }
        public string token_uri { get; set; }
        public string auth_provider_x509_cert_url { get; set; }
        public string client_x509_cert_url { get; set; }
        public string ClientEmail { get; internal set; }
        public string PrivateKey { get; internal set; }
    }
}
