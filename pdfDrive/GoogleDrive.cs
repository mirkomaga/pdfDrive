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
using System.Reflection;
using Google.Apis.Auth.OAuth2.Requests;

namespace pdfDrive
{
    class GoogleDrive
    {
        static string[] Scopes = { DriveService.Scope.DriveFile, DriveService.Scope.DriveReadonly };

        private static DriveService service;

        public static void login(string @pathCredential, bool save)
        {
            UserCredential credential;

            if (save) 
            {
                bool exists = System.IO.Directory.Exists(@"C:\\token.json");

                if (!exists)
                    System.IO.Directory.CreateDirectory(@"C:\\token.json");

                System.IO.File.Copy(@pathCredential, @"C:\\token.json\\cred.json", true);
            }


            using (var stream =new FileStream(pathCredential, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = @"C:\\token.json";
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

        public static bool statusToken()
        {
            if (System.IO.Directory.Exists(@"C:\\token.json"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string GetMimeType(string fileName) { string mimeType = "application/unknown"; string ext = System.IO.Path.GetExtension(fileName).ToLower(); Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext); if (regKey != null && regKey.GetValue("Content Type") != null) mimeType = regKey.GetValue("Content Type").ToString(); System.Diagnostics.Debug.WriteLine(mimeType); return mimeType; 
        }

        public static IList<Google.Apis.Drive.v3.Data.File> listFolderDrive()
        {
            GoogleDrive.login("C:\\token.json\\cred.json", false);

            IDictionary<string, string> res = new Dictionary<string, string>();

            FilesResource.ListRequest listRequest = GoogleDrive.service.Files.List();
            listRequest.Fields = "nextPageToken, files(id, name)";
            listRequest.Q = "mimeType='application/vnd.google-apps.folder' and trashed=false";

            IList<Google.Apis.Drive.v3.Data.File> fldrs = listRequest.Execute()
                .Files;

            return fldrs;
        }

        private static IList<Google.Apis.Drive.v3.Data.File> creaCartelleInesistenti(IList<Google.Apis.Drive.v3.Data.File> fldrs, IDictionary<string, string> listaPdf, ListView lv) 
        {

            List<string> nomiCartella = new List<string>();

            foreach (var fld in fldrs)
            {
                nomiCartella.Add(fld.Name.ToLower());
            }

            foreach (var val in listaPdf)
            {
                if(!nomiCartella.Contains(val.Key.ToLower()))
                {
                    DriveService service = new DriveService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = GoogleDrive.service.HttpClientInitializer,
                        ApplicationName = "TheAppName"
                    });

                    //then you can upload the file:

                    File body = new File();
                    body.Name = Program.ToTitleCase(val.Key.ToLower());
                    body.MimeType = "application/vnd.google-apps.folder";

                    File folder = GoogleDrive.service.Files.Create(body).Execute();


                    ListViewItem item1 = new ListViewItem("3");
                    item1.SubItems.Add(val.Key.ToLower() + ": Cartella creata");
                    
                    item1.SubItems.Add("1");
                    
                    lv.Items.Add(item1);
                }
            }
            return listFolderDrive();
        }

        public static Google.Apis.Drive.v3.Data.File uploadOnDrive(IDictionary<string, string> listaPdf, ListView lv, bool createFolder, ToolStripProgressBar pb)
        {
            IList<Google.Apis.Drive.v3.Data.File> fldrs = GoogleDrive.listFolderDrive();

            
            if (createFolder)
            {
                fldrs = creaCartelleInesistenti(fldrs, listaPdf, lv);
            }

            List<string> tmpPdfAggiunti = new List<string>();

            List<ListViewItem> lwi = new List<ListViewItem>();

            pb.Value = 0;

            pb.Maximum = listaPdf.Count;

            foreach (var fld in fldrs)
            {
                string name = fld.Name.ToLower();
                string tag = fld.Id;

                if (listaPdf.ContainsKey(name.ToLower()))
                {
                    string path = listaPdf[name];

                    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();

                    body.Name = mainI.data + "_" + Program.gestiscoNome(System.IO.Path.GetFileName(@path).ToUpper())+".pdf";

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

                        lwi.Add(item1);

                        tmpPdfAggiunti.Add(name);

                        pb.Value = pb.Value + 1;
                    }
                    catch (Exception e)
                    {
                        ListViewItem item1 = new ListViewItem("3");
                        item1.SubItems.Add(name);
                        item1.SubItems.Add("0");

                        lv.Items.AddRange(new ListViewItem[] { item1 });

                        pb.Value = pb.Value + 1;
                    }
                }
            }

            foreach (var namePDF in listaPdf)
            {
                if (!tmpPdfAggiunti.Contains(namePDF.Key))
                {

                    ListViewItem i = new ListViewItem("3");

                    i.SubItems.Add(namePDF.Key);
                    i.SubItems.Add("0");

                    lwi.Add(i);

                }
            }

            foreach (ListViewItem lw in lwi)
            {
                lv.Items.Add(lw);
                lv.EnsureVisible(lv.Items.Count - 1);
            }

            return null;
        }
    }
}

