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

namespace pdfDrive
{
    public partial class mainI : Form
    {
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
                this.lblPdf.Text = System.IO.Path.GetFileName(file);
                Program.gestiscoPDF(file);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
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

        public static void writeLineLv(List<string> msg)
        {
            //scrivo una line nella listview
        }
    }
}
