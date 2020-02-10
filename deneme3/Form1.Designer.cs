namespace sp_update
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.btnSP = new System.Windows.Forms.Button();
            this.lblUyari = new System.Windows.Forms.Label();
            this.btnTrigger = new System.Windows.Forms.Button();
            this.btnTable = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnFunctions = new System.Windows.Forms.Button();
            this.btnListKarsilastirma = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(55, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Sp ismini texte taşı ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSP
            // 
            this.btnSP.Location = new System.Drawing.Point(55, 85);
            this.btnSP.Name = "btnSP";
            this.btnSP.Size = new System.Drawing.Size(128, 35);
            this.btnSP.TabIndex = 1;
            this.btnSP.Text = "SP";
            this.btnSP.UseVisualStyleBackColor = true;
            this.btnSP.Click += new System.EventHandler(this.btnSP_Click);
            // 
            // lblUyari
            // 
            this.lblUyari.AutoSize = true;
            this.lblUyari.Location = new System.Drawing.Point(128, 59);
            this.lblUyari.Name = "lblUyari";
            this.lblUyari.Size = new System.Drawing.Size(0, 17);
            this.lblUyari.TabIndex = 2;
            // 
            // btnTrigger
            // 
            this.btnTrigger.Location = new System.Drawing.Point(224, 27);
            this.btnTrigger.Name = "btnTrigger";
            this.btnTrigger.Size = new System.Drawing.Size(128, 31);
            this.btnTrigger.TabIndex = 3;
            this.btnTrigger.Text = "Trigger";
            this.btnTrigger.UseVisualStyleBackColor = true;
            this.btnTrigger.Click += new System.EventHandler(this.btnTrigger_Click);
            // 
            // btnTable
            // 
            this.btnTable.Location = new System.Drawing.Point(55, 149);
            this.btnTable.Name = "btnTable";
            this.btnTable.Size = new System.Drawing.Size(128, 34);
            this.btnTable.TabIndex = 4;
            this.btnTable.Text = "Table";
            this.btnTable.UseVisualStyleBackColor = true;
            this.btnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(224, 149);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(128, 35);
            this.btnView.TabIndex = 5;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnFunctions
            // 
            this.btnFunctions.Location = new System.Drawing.Point(224, 85);
            this.btnFunctions.Name = "btnFunctions";
            this.btnFunctions.Size = new System.Drawing.Size(128, 35);
            this.btnFunctions.TabIndex = 6;
            this.btnFunctions.Text = "Functions";
            this.btnFunctions.UseVisualStyleBackColor = true;
            this.btnFunctions.Click += new System.EventHandler(this.btnFunctions_Click);
            // 
            // btnListKarsilastirma
            // 
            this.btnListKarsilastirma.Location = new System.Drawing.Point(55, 207);
            this.btnListKarsilastirma.Name = "btnListKarsilastirma";
            this.btnListKarsilastirma.Size = new System.Drawing.Size(297, 28);
            this.btnListKarsilastirma.TabIndex = 7;
            this.btnListKarsilastirma.Text = "Liste Karsilastirma";
            this.btnListKarsilastirma.UseVisualStyleBackColor = true;
            this.btnListKarsilastirma.Click += new System.EventHandler(this.btnListKarsilastirma_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 264);
            this.Controls.Add(this.btnListKarsilastirma);
            this.Controls.Add(this.btnFunctions);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.btnTrigger);
            this.Controls.Add(this.lblUyari);
            this.Controls.Add(this.btnSP);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Islemleri";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSP;
        private System.Windows.Forms.Label lblUyari;
        private System.Windows.Forms.Button btnTrigger;
        private System.Windows.Forms.Button btnTable;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnFunctions;
        private System.Windows.Forms.Button btnListKarsilastirma;
    }
}

