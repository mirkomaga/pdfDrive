﻿namespace pdfDrive
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainI));
            this.btnPdf = new System.Windows.Forms.Button();
            this.lblPdf = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.cbDrive = new System.Windows.Forms.CheckBox();
            this.btnJson = new System.Windows.Forms.Button();
            this.lblJson = new System.Windows.Forms.Label();
            this.stsB = new System.Windows.Forms.StatusStrip();
            this.pb = new System.Windows.Forms.ToolStripProgressBar();
            this.lv1 = new System.Windows.Forms.ListView();
            this.btnDestF = new System.Windows.Forms.Button();
            this.lblFoldDest = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cleanToken = new System.Windows.Forms.Button();
            this.stsB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPdf
            // 
            this.btnPdf.Location = new System.Drawing.Point(14, 31);
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
            this.lblPdf.Location = new System.Drawing.Point(139, 36);
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
            this.btnStart.Location = new System.Drawing.Point(386, 36);
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
            this.cbDrive.Location = new System.Drawing.Point(14, 32);
            this.cbDrive.Name = "cbDrive";
            this.cbDrive.Size = new System.Drawing.Size(96, 17);
            this.cbDrive.TabIndex = 3;
            this.cbDrive.Text = "Carica su drive";
            this.cbDrive.UseVisualStyleBackColor = true;
            this.cbDrive.CheckedChanged += new System.EventHandler(this.cbDrive_CheckedChanged);
            // 
            // btnJson
            // 
            this.btnJson.Location = new System.Drawing.Point(14, 77);
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
            this.lblJson.Location = new System.Drawing.Point(139, 82);
            this.lblJson.Name = "lblJson";
            this.lblJson.Size = new System.Drawing.Size(115, 13);
            this.lblJson.TabIndex = 6;
            this.lblJson.Text = "Nessun file selezionato";
            // 
            // stsB
            // 
            this.stsB.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pb});
            this.stsB.Location = new System.Drawing.Point(0, 527);
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
            // lv1
            // 
            this.lv1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lv1.GridLines = true;
            this.lv1.HideSelection = false;
            this.lv1.Location = new System.Drawing.Point(0, 282);
            this.lv1.Name = "lv1";
            this.lv1.OwnerDraw = true;
            this.lv1.Size = new System.Drawing.Size(504, 245);
            this.lv1.TabIndex = 8;
            this.lv1.UseCompatibleStateImageBehavior = false;
            this.lv1.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lv1_DrawColumnHeader);
            this.lv1.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lv1_DrawSubItem);
            this.lv1.SelectedIndexChanged += new System.EventHandler(this.lv1_SelectedIndexChanged);
            // 
            // btnDestF
            // 
            this.btnDestF.Location = new System.Drawing.Point(14, 86);
            this.btnDestF.Name = "btnDestF";
            this.btnDestF.Size = new System.Drawing.Size(87, 23);
            this.btnDestF.TabIndex = 9;
            this.btnDestF.Text = "Destinazione";
            this.btnDestF.UseVisualStyleBackColor = true;
            this.btnDestF.Click += new System.EventHandler(this.btnDestF_Click);
            // 
            // lblFoldDest
            // 
            this.lblFoldDest.AutoSize = true;
            this.lblFoldDest.Location = new System.Drawing.Point(139, 91);
            this.lblFoldDest.Name = "lblFoldDest";
            this.lblFoldDest.Size = new System.Drawing.Size(142, 13);
            this.lblFoldDest.TabIndex = 10;
            this.lblFoldDest.Text = "Nessuna cartella selezionata";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-warning-48.ico");
            this.imageList1.Images.SetKeyName(1, "icons8-segno-di-spunta-64.ico");
            this.imageList1.Images.SetKeyName(2, "icons8-pdf-48.ico");
            this.imageList1.Images.SetKeyName(3, "Google-Drive-icon.ico");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPdf);
            this.groupBox1.Controls.Add(this.lblFoldDest);
            this.groupBox1.Controls.Add(this.lblPdf);
            this.groupBox1.Controls.Add(this.btnDestF);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 124);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PDF";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cleanToken);
            this.groupBox2.Controls.Add(this.cbDrive);
            this.groupBox2.Controls.Add(this.btnJson);
            this.groupBox2.Controls.Add(this.lblJson);
            this.groupBox2.Location = new System.Drawing.Point(12, 142);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 124);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Google Drive";
            // 
            // cleanToken
            // 
            this.cleanToken.Location = new System.Drawing.Point(386, 77);
            this.cleanToken.Name = "cleanToken";
            this.cleanToken.Size = new System.Drawing.Size(75, 23);
            this.cleanToken.TabIndex = 7;
            this.cleanToken.Text = "Elimina";
            this.cleanToken.UseVisualStyleBackColor = true;
            this.cleanToken.Click += new System.EventHandler(this.cleanToken_Click);
            // 
            // mainI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 549);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lv1);
            this.Controls.Add(this.stsB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(520, 588);
            this.MinimumSize = new System.Drawing.Size(520, 588);
            this.Name = "mainI";
            this.Text = "Splitter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.stsB.ResumeLayout(false);
            this.stsB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        public System.Windows.Forms.ListView lv1;
        private System.Windows.Forms.Button btnDestF;
        private System.Windows.Forms.Label lblFoldDest;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button cleanToken;
    }
}

