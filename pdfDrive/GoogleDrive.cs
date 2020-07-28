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
        static string[] Scopes = { DriveService.Scope.DriveFile, DriveService.Scope.DriveReadonly };
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

        public static bool statusToken()
        {

            if (System.IO.Directory.Exists("token.json"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static string GetMimeType(string fileName) { string mimeType = "application/unknown"; string ext = System.IO.Path.GetExtension(fileName).ToLower(); Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext); if (regKey != null && regKey.GetValue("Content Type") != null) mimeType = regKey.GetValue("Content Type").ToString(); System.Diagnostics.Debug.WriteLine(mimeType); return mimeType; }
        public static Google.Apis.Drive.v3.Data.File uploadOnDrive(IDictionary<string, string> listaPdf, ListView lv)
        {
            IList<Google.Apis.Drive.v3.Data.File> fldrs = GoogleDrive.listFolderDrive();

            foreach (var fld in fldrs)
            {
                string name = fld.Name;
                string tag = fld.Id;

                if (listaPdf.ContainsKey(name.ToLower()))
                {
                    string path = listaPdf[name];

                    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
                    body.Name = System.IO.Path.GetFileName(@path);
                    body.MimeType = GetMimeType(@path);
                    body.Parents = new List<string> { tag };
                    byte[] byteArray = System.IO.File.ReadAllBytes(@path);
                    System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                    try
                    {
                        FilesResource.CreateMediaUpload request = GoogleDrive.service.Files.Create(body, stream, body.MimeType);
                        request.SupportsTeamDrives = true;
                        request.Upload();



                        ListViewItem item1 = new ListViewItem("3", 0);
                        item1.SubItems.Add(name);
                        item1.SubItems.Add("1");

                        lv.Items.AddRange(new ListViewItem[] { item1 });
                    }
                    catch (Exception e)
                    {
                        ListViewItem item1 = new ListViewItem("Drive", 0);
                        item1.SubItems.Add(name);
                        item1.SubItems.Add("0");

                        lv.Items.AddRange(new ListViewItem[] { item1 });
                    }
                }
                else
                {
                    var dioboia = name;
                }
            }
            return null;
        }
    }
}
