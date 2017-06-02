namespace WindguruParser
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
            this.dgvWeatherViewer = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.tbWindSPD = new System.Windows.Forms.TextBox();
            this.lbEmail = new System.Windows.Forms.Label();
            this.lbWindSPD = new System.Windows.Forms.Label();
            this.lbWindDIR = new System.Windows.Forms.Label();
            this.tbWindDIR = new System.Windows.Forms.TextBox();
            this.btSave = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            this.tbGust = new System.Windows.Forms.TextBox();
            this.lbGust = new System.Windows.Forms.Label();
            this.lbnextUpdate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeatherViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvWeatherViewer
            // 
            this.dgvWeatherViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWeatherViewer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvWeatherViewer.Location = new System.Drawing.Point(0, 75);
            this.dgvWeatherViewer.Name = "dgvWeatherViewer";
            this.dgvWeatherViewer.Size = new System.Drawing.Size(745, 274);
            this.dgvWeatherViewer.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.Width = 90;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Date";
            this.Column2.Name = "Column2";
            this.Column2.Width = 110;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Weekday";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Temperature";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Gust";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Wind speed";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Wind direction";
            this.Column7.Name = "Column7";
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(30, 35);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(202, 20);
            this.tbEmail.TabIndex = 1;
            // 
            // tbWindSPD
            // 
            this.tbWindSPD.Location = new System.Drawing.Point(383, 35);
            this.tbWindSPD.Name = "tbWindSPD";
            this.tbWindSPD.Size = new System.Drawing.Size(60, 20);
            this.tbWindSPD.TabIndex = 2;
            // 
            // lbEmail
            // 
            this.lbEmail.AutoSize = true;
            this.lbEmail.Location = new System.Drawing.Point(33, 16);
            this.lbEmail.Name = "lbEmail";
            this.lbEmail.Size = new System.Drawing.Size(35, 13);
            this.lbEmail.TabIndex = 3;
            this.lbEmail.Text = "E-mail";
            // 
            // lbWindSPD
            // 
            this.lbWindSPD.AutoSize = true;
            this.lbWindSPD.Location = new System.Drawing.Point(380, 16);
            this.lbWindSPD.Name = "lbWindSPD";
            this.lbWindSPD.Size = new System.Drawing.Size(64, 13);
            this.lbWindSPD.TabIndex = 4;
            this.lbWindSPD.Text = "Wind speed";
            // 
            // lbWindDIR
            // 
            this.lbWindDIR.AutoSize = true;
            this.lbWindDIR.Location = new System.Drawing.Point(458, 16);
            this.lbWindDIR.Name = "lbWindDIR";
            this.lbWindDIR.Size = new System.Drawing.Size(75, 13);
            this.lbWindDIR.TabIndex = 5;
            this.lbWindDIR.Text = "Wind direction";
            // 
            // tbWindDIR
            // 
            this.tbWindDIR.Location = new System.Drawing.Point(461, 35);
            this.tbWindDIR.Name = "tbWindDIR";
            this.tbWindDIR.Size = new System.Drawing.Size(60, 20);
            this.tbWindDIR.TabIndex = 6;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(251, 31);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(93, 26);
            this.btSave.TabIndex = 7;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btApply
            // 
            this.btApply.Location = new System.Drawing.Point(620, 31);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(93, 26);
            this.btApply.TabIndex = 8;
            this.btApply.Text = "Apply";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // tbGust
            // 
            this.tbGust.Location = new System.Drawing.Point(539, 35);
            this.tbGust.Name = "tbGust";
            this.tbGust.Size = new System.Drawing.Size(60, 20);
            this.tbGust.TabIndex = 10;
            // 
            // lbGust
            // 
            this.lbGust.AutoSize = true;
            this.lbGust.Location = new System.Drawing.Point(536, 16);
            this.lbGust.Name = "lbGust";
            this.lbGust.Size = new System.Drawing.Size(29, 13);
            this.lbGust.TabIndex = 9;
            this.lbGust.Text = "Gust";
            // 
            // lbnextUpdate
            // 
            this.lbnextUpdate.AutoSize = true;
            this.lbnextUpdate.Location = new System.Drawing.Point(6, 355);
            this.lbnextUpdate.Name = "lbnextUpdate";
            this.lbnextUpdate.Size = new System.Drawing.Size(0, 13);
            this.lbnextUpdate.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 374);
            this.Controls.Add(this.lbnextUpdate);
            this.Controls.Add(this.tbGust);
            this.Controls.Add(this.lbGust);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.tbWindDIR);
            this.Controls.Add(this.lbWindDIR);
            this.Controls.Add(this.lbWindSPD);
            this.Controls.Add(this.lbEmail);
            this.Controls.Add(this.tbWindSPD);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.dgvWeatherViewer);
            this.Name = "Form1";
            this.Text = "WindguruParser";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeatherViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvWeatherViewer;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.TextBox tbWindSPD;
        private System.Windows.Forms.Label lbEmail;
        private System.Windows.Forms.Label lbWindSPD;
        private System.Windows.Forms.Label lbWindDIR;
        private System.Windows.Forms.TextBox tbWindDIR;
        public System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.TextBox tbGust;
        private System.Windows.Forms.Label lbGust;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Label lbnextUpdate;
    }
}

