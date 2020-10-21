using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

using sun.tools.tree;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;
using iTextSharp.text;
using org.apache.tika.metadata;
using PdfSharp.Drawing;
using SelectPdf;
using org.omg.CosNaming.NamingContextExtPackage;
using thredds.filesystem;

namespace pdfDrive
{
    public partial class mainI : Form
    {
        public static IDictionary<string, string> result = new Dictionary<string, string>();
        public static string data = null;
        public static string folderDestination = null;
        public static string pdfPath = null;
        public mainI()
        {
            InitializeComponent();

            this.btnJson.Visible = false;
            this.lblJson.Visible = false;
            this.cleanToken.Visible = false;
            this.checkBoxCreateFolder.Visible = false;

            this.Refresh();
            this.Update();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lv1.View = View.Details;
            lv1.AllowColumnReorder = true;
            lv1.FullRowSelect = true;
            lv1.GridLines = true;
            lv1.OwnerDraw = true;
            lv1.LabelEdit = false;
            this.writeLwIntestatura();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        { 
            string file = this.choosePath("Credenziali (*.json)|*.json");


            if (!string.IsNullOrEmpty(file))
            {
                this.lblJson.Text = System.IO.Path.GetFileName(file);
                GoogleDrive.login(file, true);
            }

            refreshTokenInput();
        }
        private void cbDrive_CheckedChanged(object sender, EventArgs e)
        {
            refreshTokenInput();
        }
        public void refreshTokenInput()
        {
            if (this.cbDrive.Checked == true)
            {
                if (GoogleDrive.statusToken())
                {
                    this.lblJson.Visible = true;
                    this.btnJson.Visible = true;
                    this.btnJson.Enabled = false;
                    this.cleanToken.Visible = true;
                    this.cleanToken.Enabled = true;
                    this.checkBoxCreateFolder.Enabled = true;
                    this.checkBoxCreateFolder.Visible = true;

                    this.lblJson.Text = "Token già creato";
                }
                else
                {
                    this.btnJson.Visible = true;
                    this.lblJson.Visible = true;
                    this.cleanToken.Visible = true;
                    this.btnJson.Enabled = true;

                    this.cleanToken.Enabled = false;
                    this.checkBoxCreateFolder.Visible = true;
                    this.checkBoxCreateFolder.Enabled = false;

                    this.lblJson.Text = "Nessun file selezionato";
                }
                this.Refresh();
                this.Update();

            }
            else
            {
                this.btnJson.Visible = false;
                this.lblJson.Visible = false;
                this.cleanToken.Visible = false;
                this.checkBoxCreateFolder.Visible = false;
                this.Refresh();
                this.Update();
            }
        }
        private string choosePath(string filter)
        {
            string path = "";
            
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "Desktop";
                openFileDialog.Filter = filter;
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            return filePath;
        }
        private void btnPdf_Click(object sender, EventArgs e)
        {
            string file = this.choosePath("Pdf(*.pdf) | *.pdf");

            if (!string.IsNullOrEmpty(file))
            {
                this.lblPdf.Text = "Nome file: " + System.IO.Path.GetFileName(file);
                mainI.pdfPath = file;
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {

            mainI.folderDestination = tmpFolder();

            if (!string.IsNullOrEmpty(mainI.pdfPath) && !string.IsNullOrEmpty(mainI.folderDestination))
            {
                this.gestiscoPDF();
            }
            else
            {
                // TODO seleziona uno dei due
            }
            tmpFolder();
        }
        private string tmpFolder()
        {
            string path = @"C:\\TmpPdf";

            try
            {
                // Creo cartella
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
                else
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(path);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Impossibile creare folder temporanea", "Folder temporanea");
            }

            return path;
        }
        private void writeLwIntestatura()
        {
            this.lv1.View = View.Details;
            this.lv1.LabelEdit = true;
            this.lv1.AllowColumnReorder = true;
            this.lv1.CheckBoxes = false;
            this.lv1.FullRowSelect = true;
            this.lv1.GridLines = true;
            this.lv1.Sorting = SortOrder.Ascending;

            //this.lv1.Columns.Add("Operazione");
            //this.lv1.Columns.Add("Messaggio");
            //this.lv1.Columns.Add("Stato");

            //this.lv1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //this.lv1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        public void gestiscoPDF()
        {
            //var text = new TikaOnDotNet.TextExtraction.TextExtractor().Extract(path).Text.Trim();

            PdfReader reader = new PdfReader(mainI.pdfPath);

            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                PdfTextExtractor.GetTextFromPage(reader, page, its);
                string strPage = its.GetResultantText();
                IDictionary<string, string> res = this.findDataPdf(strPage);

                if (!string.IsNullOrEmpty((string)res["nameCognome"]) && !string.IsNullOrEmpty((string)res["data"]))
                {
                    //Salvo nome e cognome + pdf

                    string nameCognome = (string)res["nameCognome"].ToLower();
                    string data = (string)res["data"];

                    if (!mainI.result.ContainsKey(nameCognome))
                    {
                        mainI.result[nameCognome] = importPage(reader, page, nameCognome);

                        ListViewItem item1 = new ListViewItem("2", 0);
                        item1.SubItems.Add(nameCognome);
                        item1.SubItems.Add("1");

                        lv1.Items.AddRange(new ListViewItem[] { item1 });

                        lv1.EnsureVisible(lv1.Items.Count - 1);
                    }
                    else
                    {
                        // PDF DUPLICATO
                        ListViewItem item1 = new ListViewItem("2", 0);
                        item1.SubItems.Add(nameCognome);
                        item1.SubItems.Add("0");

                        lv1.Items.AddRange(new ListViewItem[] { item1 });

                        lv1.EnsureVisible(lv1.Items.Count - 1);
                    }

                    if (string.IsNullOrEmpty(mainI.data))
                    {
                        mainI.data = data;
                    }

                    if (mainI.data != data)
                    {
                        // DATE DIVERSE NEL PDF
                    }
                }
            }
            reader.Close();


            // EVENTUALMENTE DRIVE
            if (this.cbDrive.Checked)
            {
                var a = GoogleDrive.uploadOnDrive(mainI.result, lv1, checkBoxCreateFolder.Checked);
            }
        }
        public static string importPage(PdfReader reader, int nPage, string name)
        {
            string path = folderDestination+"\\"+name+".pdf";

            if (!string.IsNullOrEmpty(path))
            {
                Document sourceDocument = null;
                PdfCopy pdfCopyProvider = null;
                PdfImportedPage importedPage = null;

                sourceDocument = new Document(reader.GetPageSizeWithRotation(1));
                pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(@path, System.IO.FileMode.Create));

                sourceDocument.Open();

                importedPage = pdfCopyProvider.GetImportedPage(reader, nPage);
                pdfCopyProvider.AddPage(importedPage);

                sourceDocument.Close();
            }

            return path;
        }
        private string cercaNome(string toFind)
        {
            string rx = @"SESSO([^\n]*\n+)[0-9]{2} ([^>]+) [0-9]{6}[^a-z]{1}([^\n]*\n+)DES";
            MatchCollection res = this.findRegex(toFind, rx);

            if (res.Count == 0)
            {
                rx = @"SESSO([^\n]*\n+)[0-9]{1} ([^>]+) [0-9]{6}[^a-z]{1}([^\n]*\n+)DES";
                res = this.findRegex(toFind, rx);
            }

            foreach (Match match in res)
            {
                GroupCollection groups = match.Groups;

                string nomeCognome = groups[2].ToString();

                return nomeCognome;
            }

            return "";
        }
        private string cercaData(string toFind)
        {
            string rx = @"[0-9]{1,2}\/[0-9]{1,2}\/[0-9]{2,4}([^\n]*\n+)DATA SCATTI";
            MatchCollection res = this.findRegex(toFind, rx);

            foreach (Match match in res)
            {
                GroupCollection groups = match.Groups;
                string data = groups[1].ToString();
                string[] allDate = data.Split(" ".ToCharArray());

                string[] result = allDate[allDate.Length - 1].Split("/".ToCharArray());

                try
                {
                    return result[2] + "_" + result[1];
                }
                catch
                {
                    return "";
                }
            }

            return "";
        }
        
        private void addToLV(List<dynamic> msg)
        {
            ListViewItem lvi = new ListViewItem(msg[0], 0);
            lvi.SubItems.Add(msg[1]);
            lvi.SubItems.Add(msg[2]);
            lv1.Items.Add(lvi);
        }
        private void lv1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(imageList1.Images[int.Parse(e.SubItem.Text)], e.SubItem.Bounds.X, e.SubItem.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
            }
            catch
            {
                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, Brushes.Black, e.Bounds);
            }

        }
        private void lv1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }
        private IDictionary<string, string> findDataPdf(string str)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            results["nameCognome"] = this.cercaNome(str);
            results["data"] = this.cercaData(str);

            return results;
        }
        private MatchCollection findRegex(string toFind, string regex)
        {
            Regex rx = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(toFind);
            return matches;
        }

        private void cleanToken_Click(object sender, EventArgs e)
        {

            try
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(@"C:\\token.json");

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                Directory.Delete(@"C:\\token.json");
                MessageBox.Show("Eliminato correttamente", "Delete Cache");
            }
            catch
            {
                MessageBox.Show("Non è stato possibile eliminare la cartella", "Delete Cache");
            }
            refreshTokenInput();
        }

        private void checkBoxCreateFolder_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
    public class MyListView : ListView
    {
        protected override void OnDrawSubItem(System.Windows.Forms.DrawListViewSubItemEventArgs e)
        {
            base.OnDrawSubItem(e);
        }
    }
}
