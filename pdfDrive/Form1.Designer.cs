namespace pdfDrive
{
    partial class mainI
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPdf = new System.Windows.Forms.Button();
            this.lblPdf = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.cbDrive = new System.Windows.Forms.CheckBox();
            this.btnJson = new System.Windows.Forms.Button();
            this.lblJson = new System.Windows.Forms.Label();
            this.stsB = new System.Windows.Forms.StatusStrip();
            this.pb = new System.Windows.Forms.ToolStripProgressBar();
            this.nameProc = new System.Windows.Forms.ToolStripStatusLabel();
            this.lv1 = new System.Windows.Forms.ListView();
            this.stsB.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPdf
            // 
            this.btnPdf.Location = new System.Drawing.Point(26, 32);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Size = new System.Drawing.Size(87, 23);
            this.btnPdf.TabIndex = 0;
            this.btnPdf.Text = "Pdf";
            this.btnPdf.UseVisualStyleBackColor = true;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            // 
            // lblPdf
            // 
            this.lblPdf.AutoSize = true;
            this.lblPdf.Location = new System.Drawing.Point(151, 37);
            this.lblPdf.Name = "lblPdf";
            this.lblPdf.Size = new System.Drawing.Size(115, 13);
            this.lblPdf.TabIndex = 1;
            this.lblPdf.Text = "Nessun file selezionato";
            this.lblPdf.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnStart.Location = new System.Drawing.Point(382, 32);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Avvia";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cbDrive
            // 
            this.cbDrive.AutoSize = true;
            this.cbDrive.Location = new System.Drawing.Point(26, 85);
            this.cbDrive.Name = "cbDrive";
            this.cbDrive.Size = new System.Drawing.Size(96, 17);
            this.cbDrive.TabIndex = 3;
            this.cbDrive.Text = "Carica su drive";
            this.cbDrive.UseVisualStyleBackColor = true;
            this.cbDrive.CheckedChanged += new System.EventHandler(this.cbDrive_CheckedChanged);
            // 
            // btnJson
            // 
            this.btnJson.Location = new System.Drawing.Point(26, 130);
            this.btnJson.Name = "btnJson";
            this.btnJson.Size = new System.Drawing.Size(87, 23);
            this.btnJson.TabIndex = 5;
            this.btnJson.Text = "Credenziali";
            this.btnJson.UseVisualStyleBackColor = true;
            this.btnJson.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblJson
            // 
            this.lblJson.AutoSize = true;
            this.lblJson.Location = new System.Drawing.Point(151, 135);
            this.lblJson.Name = "lblJson";
            this.lblJson.Size = new System.Drawing.Size(115, 13);
            this.lblJson.TabIndex = 6;
            this.lblJson.Text = "Nessun file selezionato";
            // 
            // stsB
            // 
            this.stsB.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pb,
            this.nameProc});
            this.stsB.Location = new System.Drawing.Point(0, 289);
            this.stsB.Name = "stsB";
            this.stsB.Size = new System.Drawing.Size(504, 22);
            this.stsB.TabIndex = 7;
            this.stsB.Text = "statusStrip1";
            // 
            // pb
            // 
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(100, 16);
            // 
            // nameProc
            // 
            this.nameProc.Name = "nameProc";
            this.nameProc.Size = new System.Drawing.Size(61, 17);
            this.nameProc.Text = "Nome File";
            // 
            // lv1
            // 
            this.lv1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lv1.HideSelection = false;
            this.lv1.Location = new System.Drawing.Point(0, 192);
            this.lv1.Name = "lv1";
            this.lv1.Size = new System.Drawing.Size(504, 97);
            this.lv1.TabIndex = 8;
            this.lv1.UseCompatibleStateImageBehavior = false;
            this.lv1.SelectedIndexChanged += new System.EventHandler(this.lv1_SelectedIndexChanged);
            // 
            // mainI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 311);
            this.Controls.Add(this.lv1);
            this.Controls.Add(this.stsB);
            this.Controls.Add(this.lblJson);
            this.Controls.Add(this.btnJson);
            this.Controls.Add(this.cbDrive);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblPdf);
            this.Controls.Add(this.btnPdf);
            this.MinimumSize = new System.Drawing.Size(520, 350);
            this.Name = "mainI";
            this.Text = "Splitter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.stsB.ResumeLayout(false);
            this.stsB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPdf;
        private System.Windows.Forms.Label lblPdf;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox cbDrive;
        private System.Windows.Forms.Button btnJson;
        private System.Windows.Forms.Label lblJson;
        private System.Windows.Forms.StatusStrip stsB;
        private System.Windows.Forms.ToolStripProgressBar pb;
        private System.Windows.Forms.ToolStripStatusLabel nameProc;
        public System.Windows.Forms.ListView lv1;
    }
}

