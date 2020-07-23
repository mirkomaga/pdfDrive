using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using com.google.common.util.concurrent;
using sun.tools.tree;
using org.apache.poi.hwpf.model.types;
using sun.swing.plaf.windows;
using File = Google.Apis.Drive.v3.Data.File;
using System.Windows.Forms;

namespace pdfDrive
{
    class GoogleDrive
    {
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        private static DriveService service;
        public static void login(string pathCredential)
        {
            UserCredential credential;

            using (var stream =new FileStream(@"C:\\cred.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                    //return true;
            }

            GoogleDrive.service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

        }

        public static IList<Google.Apis.Drive.v3.Data.File> listFolderDrive()
        {
            GoogleDrive.login(@"C:\\cred.json");

            IDictionary<string, string> res = new Dictionary<string, string>();

            FilesResource.ListRequest listRequest = GoogleDrive.service.Files.List();
            listRequest.Fields = "nextPageToken, files(id, name)";
            listRequest.Q = "mimeType='application/vnd.google-apps.folder'";

            IList<Google.Apis.Drive.v3.Data.File> fldrs = listRequest.Execute()
                .Files;

            return fldrs;
        }

        private static string GetMimeType(string fileName) { string mimeType = "application/unknown"; string ext = System.IO.Path.GetExtension(fileName).ToLower(); Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext); if (regKey != null && regKey.GetValue("Content Type") != null) mimeType = regKey.GetValue("Content Type").ToString(); System.Diagnostics.Debug.WriteLine(mimeType); return mimeType; }
        public static Google.Apis.Drive.v3.Data.File uploadOnDrive(IDictionary<string, string> listaPdf)
        {
            IList<Google.Apis.Drive.v3.Data.File> fldrs = GoogleDrive.listFolderDrive();

            foreach (var fld in fldrs)
            {
                string name = fld.Name;
                string tag = fld.Id;

                if (listaPdf.ContainsKey(name.ToLower()))
                {
                    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
                    body.Name = System.IO.Path.GetFileName("C:\\2020_06_piana alberto.pdf");
                    //body.Description = "boh";
                    //body.MimeType = GetMimeType(_uploadFile);
                    body.Parents = new List<string> { tag };
                    byte[] byteArray = System.IO.File.ReadAllBytes("C:\\2020_06_piana alberto.pdf");
                    System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                    try
                    {
                        FilesResource.CreateMediaUpload request = GoogleDrive.service.Files.Create(body, stream, GetMimeType("C:\\2020_06_piana alberto.pdf"));
                        request.SupportsTeamDrives = true;
                        // You can bind event handler with progress changed event and response recieved(completed event)
                        //request.ProgressChanged += Request_ProgressChanged;
                        //request.ResponseReceived += Request_ResponseReceived;
                        request.Upload();
                        return request.ResponseBody;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error Occured");
                        return null;
                    }
                }
            }
            return null;
        }

        //private void Request_ProgressChanged(Google.Apis.Upload.IUploadProgress obj)
        //{
        //    textBox2.Text += obj.Status + " " + obj.BytesSent;
        //}

        //private void Request_ResponseReceived(Google.Apis.Drive.v3.Data.File obj)
        //{
        //    if (obj != null)
        //    {
        //        MessageBox.Show("File was uploaded sucessfully--" + obj.Id);
        //    }
        //}

    }
}
