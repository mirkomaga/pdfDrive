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
            IDictionary<string, string> res = new Dictionary<string, string>();

            FilesResource.ListRequest listRequest = GoogleDrive.service.Files.List();
            listRequest.Fields = "nextPageToken, files(id, name)";
            listRequest.Q = "mimeType='application/vnd.google-apps.folder'";

            IList<Google.Apis.Drive.v3.Data.File> fldrs = listRequest.Execute()
                .Files;

            return fldrs;
        }
        public static void uploadOnDrive(IDictionary<string, string> listaPdf)
        {
            IList<Google.Apis.Drive.v3.Data.File> fldrs = GoogleDrive.listFolderDrive();

            foreach (var fld in fldrs)
            {
                string name = fld.Name;
                string tag = fld.Id;

                if (listaPdf.ContainsKey(name.ToLower()))
                {
                    //File body = new File();
                    //body.Title = System.IO.Path.GetFileName(_uploadFile);
                    //body.Description = _descrp;
                    //body.MimeType = GetMimeType(_uploadFile);
                    //body.Parents = new List<ParentReference>() { new ParentReference() { Id = _parent } };

                    ////carico su questo tag
                    //Google.Apis.Drive.v3.Data.File fileMetadata = new Google.Apis.Drive.v3.Data.File();
                    //fileMetadata.SetParents
                    //Google.Apis.Drive.v3.Data.File file = GoogleDrive.service.Files.Create(test).Fields("id");
                }
            }
        }
    }
}
