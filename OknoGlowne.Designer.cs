namespace Księgowość
{
    partial class OknoGlowne
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OknoGlowne));
            this.tabControl_Glowne = new System.Windows.Forms.TabControl();
            this.tabPage_Sprzedaz = new System.Windows.Forms.TabPage();
            this.dataGrid_Przychody = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage_Koszty = new System.Windows.Forms.TabPage();
            this.dataGrid_Koszty = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage_Info = new System.Windows.Forms.TabPage();
            this.groupBox_PIT = new System.Windows.Forms.GroupBox();
            this.groupBox_ZailczkiPIT = new System.Windows.Forms.GroupBox();
            this.dataGrid_PITMiesiecznie = new System.Windows.Forms.DataGridView();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zmianaRokuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox_rok = new System.Windows.Forms.ToolStripComboBox();
            this.tabControl_Glowne.SuspendLayout();
            this.tabPage_Sprzedaz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Przychody)).BeginInit();
            this.tabPage_Koszty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Koszty)).BeginInit();
            this.tabPage_Info.SuspendLayout();
            this.groupBox_ZailczkiPIT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_PITMiesiecznie)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_Glowne
            // 
            this.tabControl_Glowne.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_Glowne.Controls.Add(this.tabPage_Sprzedaz);
            this.tabControl_Glowne.Controls.Add(this.tabPage_Koszty);
            this.tabControl_Glowne.Controls.Add(this.tabPage_Info);
            this.tabControl_Glowne.Location = new System.Drawing.Point(12, 27);
            this.tabControl_Glowne.Name = "tabControl_Glowne";
            this.tabControl_Glowne.SelectedIndex = 0;
            this.tabControl_Glowne.Size = new System.Drawing.Size(857, 415);
            this.tabControl_Glowne.TabIndex = 0;
            // 
            // tabPage_Sprzedaz
            // 
            this.tabPage_Sprzedaz.Controls.Add(this.dataGrid_Przychody);
            this.tabPage_Sprzedaz.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Sprzedaz.Name = "tabPage_Sprzedaz";
            this.tabPage_Sprzedaz.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Sprzedaz.Size = new System.Drawing.Size(849, 389);
            this.tabPage_Sprzedaz.TabIndex = 0;
            this.tabPage_Sprzedaz.Text = "Przychody";
            this.tabPage_Sprzedaz.UseVisualStyleBackColor = true;
            // 
            // dataGrid_Przychody
            // 
            this.dataGrid_Przychody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid_Przychody.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Przychody.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGrid_Przychody.Location = new System.Drawing.Point(6, 6);
            this.dataGrid_Przychody.Name = "dataGrid_Przychody";
            this.dataGrid_Przychody.Size = new System.Drawing.Size(837, 373);
            this.dataGrid_Przychody.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "Data";
            this.Column1.Name = "Column1";
            this.Column1.ToolTipText = "Dzień.Miesiąc.Rok (dd.mm.rrrr)";
            this.Column1.Width = 55;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "Nr kol.";
            this.Column2.Name = "Column2";
            this.Column2.ToolTipText = "Numer kolejny, (MiesiącRok.Numer)";
            this.Column2.Width = 63;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Format = "N2";
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "Kwota";
            this.Column3.Name = "Column3";
            this.Column3.ToolTipText = "Kwota bez symboli";
            this.Column3.Width = 62;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.HeaderText = "Opis";
            this.Column4.Name = "Column4";
            this.Column4.ToolTipText = "Nazwa/Właściwości/Opis internetowy";
            this.Column4.Width = 53;
            // 
            // tabPage_Koszty
            // 
            this.tabPage_Koszty.Controls.Add(this.dataGrid_Koszty);
            this.tabPage_Koszty.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Koszty.Name = "tabPage_Koszty";
            this.tabPage_Koszty.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Koszty.Size = new System.Drawing.Size(849, 389);
            this.tabPage_Koszty.TabIndex = 1;
            this.tabPage_Koszty.Text = "Koszty";
            this.tabPage_Koszty.UseVisualStyleBackColor = true;
            // 
            // dataGrid_Koszty
            // 
            this.dataGrid_Koszty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid_Koszty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Koszty.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column7});
            this.dataGrid_Koszty.Location = new System.Drawing.Point(6, 6);
            this.dataGrid_Koszty.Name = "dataGrid_Koszty";
            this.dataGrid_Koszty.Size = new System.Drawing.Size(837, 392);
            this.dataGrid_Koszty.TabIndex = 0;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.HeaderText = "Nr Kol.";
            this.Column5.Name = "Column5";
            this.Column5.Width = 64;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Format = "N2";
            this.Column6.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column6.HeaderText = "Kwota";
            this.Column6.Name = "Column6";
            this.Column6.Width = 62;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column7.HeaderText = "Opis";
            this.Column7.Name = "Column7";
            this.Column7.Width = 53;
            // 
            // tabPage_Info
            // 
            this.tabPage_Info.Controls.Add(this.groupBox_PIT);
            this.tabPage_Info.Controls.Add(this.groupBox_ZailczkiPIT);
            this.tabPage_Info.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Info.Name = "tabPage_Info";
            this.tabPage_Info.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Info.Size = new System.Drawing.Size(849, 389);
            this.tabPage_Info.TabIndex = 2;
            this.tabPage_Info.Text = "Info";
            this.tabPage_Info.UseVisualStyleBackColor = true;
            // 
            // groupBox_PIT
            // 
            this.groupBox_PIT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_PIT.Location = new System.Drawing.Point(522, 6);
            this.groupBox_PIT.Name = "groupBox_PIT";
            this.groupBox_PIT.Size = new System.Drawing.Size(321, 392);
            this.groupBox_PIT.TabIndex = 2;
            this.groupBox_PIT.TabStop = false;
            this.groupBox_PIT.Text = "Rozliczenie PIT";
            // 
            // groupBox_ZailczkiPIT
            // 
            this.groupBox_ZailczkiPIT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox_ZailczkiPIT.Controls.Add(this.dataGrid_PITMiesiecznie);
            this.groupBox_ZailczkiPIT.Location = new System.Drawing.Point(6, 6);
            this.groupBox_ZailczkiPIT.Name = "groupBox_ZailczkiPIT";
            this.groupBox_ZailczkiPIT.Size = new System.Drawing.Size(510, 392);
            this.groupBox_ZailczkiPIT.TabIndex = 1;
            this.groupBox_ZailczkiPIT.TabStop = false;
            this.groupBox_ZailczkiPIT.Text = "Zaliczki PIT miesięcznie";
            // 
            // dataGrid_PITMiesiecznie
            // 
            this.dataGrid_PITMiesiecznie.AllowUserToAddRows = false;
            this.dataGrid_PITMiesiecznie.AllowUserToDeleteRows = false;
            this.dataGrid_PITMiesiecznie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid_PITMiesiecznie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_PITMiesiecznie.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column11,
            this.Column8,
            this.Column9,
            this.Column13,
            this.Column10,
            this.Column12});
            this.dataGrid_PITMiesiecznie.Location = new System.Drawing.Point(6, 19);
            this.dataGrid_PITMiesiecznie.Name = "dataGrid_PITMiesiecznie";
            this.dataGrid_PITMiesiecznie.ReadOnly = true;
            this.dataGrid_PITMiesiecznie.Size = new System.Drawing.Size(498, 367);
            this.dataGrid_PITMiesiecznie.TabIndex = 0;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column11.HeaderText = "Miesiąc";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 68;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Format = "C2";
            this.Column8.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column8.HeaderText = "Przychód";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 76;
            // 
            // Column9
            // 
            dataGridViewCellStyle5.Format = "C2";
            this.Column9.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column9.HeaderText = "Koszty";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.Format = "C2";
            this.Column13.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column13.HeaderText = "Dochód";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 70;
            // 
            // Column10
            // 
            dataGridViewCellStyle7.Format = "C2";
            this.Column10.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column10.HeaderText = "Zaliczka PIT";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column12.HeaderText = "P.P.";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.ToolTipText = "Przekroczenie progu (50%)";
            this.Column12.Width = 33;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.zmianaRokuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(881, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // zmianaRokuToolStripMenuItem
            // 
            this.zmianaRokuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox_rok});
            this.zmianaRokuToolStripMenuItem.Name = "zmianaRokuToolStripMenuItem";
            this.zmianaRokuToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.zmianaRokuToolStripMenuItem.Text = "Zmiana roku";
            // 
            // toolStripComboBox_rok
            // 
            this.toolStripComboBox_rok.Items.AddRange(new object[] {
            "2019",
            "2020",
            "2021"});
            this.toolStripComboBox_rok.Name = "toolStripComboBox_rok";
            this.toolStripComboBox_rok.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox_rok.Sorted = true;
            this.toolStripComboBox_rok.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox_rok_SelectedIndexChanged);
            // 
            // OknoGlowne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 454);
            this.Controls.Add(this.tabControl_Glowne);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "OknoGlowne";
            this.Text = "Księgowość";
            this.tabControl_Glowne.ResumeLayout(false);
            this.tabPage_Sprzedaz.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Przychody)).EndInit();
            this.tabPage_Koszty.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Koszty)).EndInit();
            this.tabPage_Info.ResumeLayout(false);
            this.groupBox_ZailczkiPIT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_PITMiesiecznie)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_Glowne;
        private System.Windows.Forms.TabPage tabPage_Sprzedaz;
        private System.Windows.Forms.DataGridView dataGrid_Przychody;
        private System.Windows.Forms.TabPage tabPage_Koszty;
        private System.Windows.Forms.DataGridView dataGrid_Koszty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.TabPage tabPage_Info;
        private System.Windows.Forms.GroupBox groupBox_PIT;
        private System.Windows.Forms.GroupBox groupBox_ZailczkiPIT;
        private System.Windows.Forms.DataGridView dataGrid_PITMiesiecznie;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column12;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zmianaRokuToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_rok;
    }
}

