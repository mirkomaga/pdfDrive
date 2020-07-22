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
using System.Windows.Forms;
using System.IO;
using sun.tools.tree;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;
using iTextSharp.text;

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

            this.Refresh();
            this.Update();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
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
                Program.googleLogin(file);
            }
        }
        private void cbDrive_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbDrive.Checked == true)
            {
                this.btnJson.Visible = true;
                this.lblJson.Visible = true;
                this.Refresh();
                this.Update();
            }else
            {
                this.btnJson.Visible = false;
                this.lblJson.Visible = false;
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
                openFileDialog.InitialDirectory = "c:\\";
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
            if (!string.IsNullOrEmpty(mainI.pdfPath) && !string.IsNullOrEmpty(mainI.folderDestination))
            {
                this.gestiscoPDF();
            }
            else
            {
                // TODO seleziona uno dei due
            }
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

            ListViewItem item1 = new ListViewItem("item1", 0);
            item1.SubItems.Add("Avvio");
            item1.SubItems.Add("Inizializzato correttamente");
            item1.SubItems.Add("Ok");
            
            this.lv1.Columns.Add("Operazione");
            this.lv1.Columns.Add("Messaggio");
            this.lv1.Columns.Add("Stato");

            this.lv1.Items.AddRange(new ListViewItem[] { item1});

            this.lv1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.lv1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void lv1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void gestiscoPDF()
        {
            //var text = new TikaOnDotNet.TextExtraction.TextExtractor().Extract(path).Text.Trim();

            PdfReader reader = new PdfReader(mainI.pdfPath);

            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                //try
                //{
                    PdfTextExtractor.GetTextFromPage(reader, page, its);
                    string strPage = its.GetResultantText();

                    //ListViewItem itm = new ListViewItem("Pdf", 0);
                    //itm.SubItems.Add("Processo pagina: "+ page);
                    //itm.SubItems.Add("ok");
                    //this.lv1.Items.AddRange(new ListViewItem[] { itm });

                    IDictionary<string, string> res = this.findDataPdf(strPage);

                    if (!string.IsNullOrEmpty((string)res["nameCognome"]) && !string.IsNullOrEmpty((string)res["data"]))
                    {
                        //Salvo nome e cognome + pdf

                        string nameCognome = (string)res["nameCognome"];
                        string data = (string)res["data"];

                        if (!mainI.result.ContainsKey(nameCognome))
                        {
                            mainI.result[nameCognome] = strPage;

                            List<string> a = new List<string>();
                            this.addToLV(a);
                        }
                        else
                        {
                            // PDF DUPLICATO
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
                //}
                //    catch
                //{
                //    ListViewItem itm = new ListViewItem("Pdf", 0);
                //    itm.SubItems.Add("Impossibile leggere pdf.");
                //    itm.SubItems.Add("KO");

                //    this.lv1.Items.AddRange(new ListViewItem[] { itm });
                //}
            }
            reader.Close();

            this.writeNewPdf();
            // EVENTUALMENTE DRIVE
        }
        private void writeNewPdf()
        {
            if (!string.IsNullOrEmpty(mainI.folderDestination))
            {
                foreach(var sing in mainI.result)
                {
                    //SCRIVO NUOVI PDF IN CARTELLA DI DESTINAZIONE

                    using (MemoryStream myMemoryStream = new MemoryStream())
                    {
                        iTextSharp.text.Document myDocument = new iTextSharp.text.Document();
                        PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);

                        myDocument.Open();

                        myDocument.Add(new Paragraph(sing.Value));

                        myDocument.Close();

                        byte[] content = myMemoryStream.ToArray();

                        // Write out PDF from memory stream.
                        using (FileStream fs = File.Create(mainI.folderDestination + "\\" + sing.Key + ".pdf"))
                        {
                            fs.Write(content, 0, (int)content.Length);
                        }
                    }
                }
            }
        }
        private IDictionary<string,string> findDataPdf(string str)
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
        private string cercaNome(string toFind)
        {
            string rx = @"SESSO([^\n]*\n+)[0-9]{2} ([^>]+) [0-9]{6}[^a-z]{1}([^\n]*\n+)DES";
            MatchCollection res = this.findRegex(toFind, rx);

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
        private void btnDestF_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    if (files.Length > 0)
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult flrCheck = MessageBox.Show("Cartella non vuota, i file duplicati verranno sostituiti", "Attenzione", buttons);
                        if (flrCheck == DialogResult.Yes)
                        {
                            //this.Close();
                        }
                        else
                        {
                            //simulo il click
                            this.btnDestF_Click(sender, e);
                        }
                    }
                    mainI.folderDestination = fbd.SelectedPath;
                    this.lblFoldDest.Text = "Nome cartella: " + System.IO.Path.GetFileName(fbd.SelectedPath);
                }
            }
        }
        private void addToLV(List<string> msg)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.SubItems.Add("SubItem");
            lvi.SubItems.Add("SubItem");
            lvi.SubItems.Add("SubItem");
            lv1.Items.Add(lvi);
        }
    }
}
