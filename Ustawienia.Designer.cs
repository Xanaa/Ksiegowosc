namespace Księgowość
{
    partial class Ustawienia
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
            this.TextBox_ImieNazwisko = new System.Windows.Forms.TextBox();
            this.TextBox_Adres = new System.Windows.Forms.TextBox();
            this.TextBox_KodMiasto = new System.Windows.Forms.TextBox();
            this.TabControl_Ustawienia = new System.Windows.Forms.TabControl();
            this.TabPage_MojeDane = new System.Windows.Forms.TabPage();
            this.Llabel_Firma = new System.Windows.Forms.Label();
            this.Label_KodMiasto = new System.Windows.Forms.Label();
            this.Label_Adres = new System.Windows.Forms.Label();
            this.Label_ImieNazwisko = new System.Windows.Forms.Label();
            this.TextBox_Firma = new System.Windows.Forms.TextBox();
            this.TabPage_Inne = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.TextBox_FolderRachunkow = new System.Windows.Forms.TextBox();
            this.Label_FolderRachunkow = new System.Windows.Forms.Label();
            this.CheckBox_Aktualizacje = new System.Windows.Forms.CheckBox();
            this.TabControl_Ustawienia.SuspendLayout();
            this.TabPage_MojeDane.SuspendLayout();
            this.TabPage_Inne.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox_ImieNazwisko
            // 
            this.TextBox_ImieNazwisko.Location = new System.Drawing.Point(6, 6);
            this.TextBox_ImieNazwisko.Name = "TextBox_ImieNazwisko";
            this.TextBox_ImieNazwisko.Size = new System.Drawing.Size(198, 20);
            this.TextBox_ImieNazwisko.TabIndex = 0;
            // 
            // TextBox_Adres
            // 
            this.TextBox_Adres.Location = new System.Drawing.Point(6, 32);
            this.TextBox_Adres.Name = "TextBox_Adres";
            this.TextBox_Adres.Size = new System.Drawing.Size(198, 20);
            this.TextBox_Adres.TabIndex = 1;
            // 
            // TextBox_KodMiasto
            // 
            this.TextBox_KodMiasto.Location = new System.Drawing.Point(6, 58);
            this.TextBox_KodMiasto.Name = "TextBox_KodMiasto";
            this.TextBox_KodMiasto.Size = new System.Drawing.Size(198, 20);
            this.TextBox_KodMiasto.TabIndex = 2;
            // 
            // TabControl_Ustawienia
            // 
            this.TabControl_Ustawienia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl_Ustawienia.Controls.Add(this.TabPage_MojeDane);
            this.TabControl_Ustawienia.Controls.Add(this.TabPage_Inne);
            this.TabControl_Ustawienia.Location = new System.Drawing.Point(12, 12);
            this.TabControl_Ustawienia.Name = "TabControl_Ustawienia";
            this.TabControl_Ustawienia.SelectedIndex = 0;
            this.TabControl_Ustawienia.Size = new System.Drawing.Size(510, 254);
            this.TabControl_Ustawienia.TabIndex = 3;
            // 
            // TabPage_MojeDane
            // 
            this.TabPage_MojeDane.Controls.Add(this.Llabel_Firma);
            this.TabPage_MojeDane.Controls.Add(this.Label_KodMiasto);
            this.TabPage_MojeDane.Controls.Add(this.Label_Adres);
            this.TabPage_MojeDane.Controls.Add(this.Label_ImieNazwisko);
            this.TabPage_MojeDane.Controls.Add(this.TextBox_Firma);
            this.TabPage_MojeDane.Controls.Add(this.TextBox_ImieNazwisko);
            this.TabPage_MojeDane.Controls.Add(this.TextBox_KodMiasto);
            this.TabPage_MojeDane.Controls.Add(this.TextBox_Adres);
            this.TabPage_MojeDane.Location = new System.Drawing.Point(4, 22);
            this.TabPage_MojeDane.Name = "TabPage_MojeDane";
            this.TabPage_MojeDane.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_MojeDane.Size = new System.Drawing.Size(502, 228);
            this.TabPage_MojeDane.TabIndex = 0;
            this.TabPage_MojeDane.Text = "Moje dane";
            this.TabPage_MojeDane.UseVisualStyleBackColor = true;
            // 
            // Llabel_Firma
            // 
            this.Llabel_Firma.AutoSize = true;
            this.Llabel_Firma.Location = new System.Drawing.Point(210, 87);
            this.Llabel_Firma.Name = "Llabel_Firma";
            this.Llabel_Firma.Size = new System.Drawing.Size(64, 13);
            this.Llabel_Firma.TabIndex = 7;
            this.Llabel_Firma.Text = "Nazwa firmy";
            // 
            // Label_KodMiasto
            // 
            this.Label_KodMiasto.AutoSize = true;
            this.Label_KodMiasto.Location = new System.Drawing.Point(210, 61);
            this.Label_KodMiasto.Name = "Label_KodMiasto";
            this.Label_KodMiasto.Size = new System.Drawing.Size(112, 13);
            this.Label_KodMiasto.TabIndex = 6;
            this.Label_KodMiasto.Text = "Kod pocztowy i miasto";
            // 
            // Label_Adres
            // 
            this.Label_Adres.AutoSize = true;
            this.Label_Adres.Location = new System.Drawing.Point(210, 35);
            this.Label_Adres.Name = "Label_Adres";
            this.Label_Adres.Size = new System.Drawing.Size(34, 13);
            this.Label_Adres.TabIndex = 5;
            this.Label_Adres.Text = "Adres";
            // 
            // Label_ImieNazwisko
            // 
            this.Label_ImieNazwisko.AutoSize = true;
            this.Label_ImieNazwisko.Location = new System.Drawing.Point(210, 9);
            this.Label_ImieNazwisko.Name = "Label_ImieNazwisko";
            this.Label_ImieNazwisko.Size = new System.Drawing.Size(78, 13);
            this.Label_ImieNazwisko.TabIndex = 4;
            this.Label_ImieNazwisko.Text = "Imię i nazwisko";
            // 
            // TextBox_Firma
            // 
            this.TextBox_Firma.Location = new System.Drawing.Point(6, 84);
            this.TextBox_Firma.Name = "TextBox_Firma";
            this.TextBox_Firma.Size = new System.Drawing.Size(198, 20);
            this.TextBox_Firma.TabIndex = 3;
            // 
            // TabPage_Inne
            // 
            this.TabPage_Inne.Controls.Add(this.CheckBox_Aktualizacje);
            this.TabPage_Inne.Controls.Add(this.Label_FolderRachunkow);
            this.TabPage_Inne.Controls.Add(this.TextBox_FolderRachunkow);
            this.TabPage_Inne.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Inne.Name = "TabPage_Inne";
            this.TabPage_Inne.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Inne.Size = new System.Drawing.Size(502, 228);
            this.TabPage_Inne.TabIndex = 1;
            this.TabPage_Inne.Text = "Inne";
            this.TabPage_Inne.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(447, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Zapisz";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button_Zapisz_Click);
            // 
            // TextBox_FolderRachunkow
            // 
            this.TextBox_FolderRachunkow.Location = new System.Drawing.Point(6, 6);
            this.TextBox_FolderRachunkow.Name = "TextBox_FolderRachunkow";
            this.TextBox_FolderRachunkow.Size = new System.Drawing.Size(339, 20);
            this.TextBox_FolderRachunkow.TabIndex = 1;
            // 
            // Label_FolderRachunkow
            // 
            this.Label_FolderRachunkow.AutoSize = true;
            this.Label_FolderRachunkow.Location = new System.Drawing.Point(351, 9);
            this.Label_FolderRachunkow.Name = "Label_FolderRachunkow";
            this.Label_FolderRachunkow.Size = new System.Drawing.Size(109, 13);
            this.Label_FolderRachunkow.TabIndex = 2;
            this.Label_FolderRachunkow.Text = "Folder dla rachunków";
            // 
            // CheckBox_Aktualizacje
            // 
            this.CheckBox_Aktualizacje.AutoSize = true;
            this.CheckBox_Aktualizacje.Location = new System.Drawing.Point(6, 32);
            this.CheckBox_Aktualizacje.Name = "CheckBox_Aktualizacje";
            this.CheckBox_Aktualizacje.Size = new System.Drawing.Size(288, 17);
            this.CheckBox_Aktualizacje.TabIndex = 3;
            this.CheckBox_Aktualizacje.Text = "Sprawdzanie dostępnej aktualizacji przy starcie aplikacji";
            this.CheckBox_Aktualizacje.UseVisualStyleBackColor = true;
            // 
            // Ustawienia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 307);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TabControl_Ustawienia);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Ustawienia";
            this.Text = "Ustawienia";
            this.TabControl_Ustawienia.ResumeLayout(false);
            this.TabPage_MojeDane.ResumeLayout(false);
            this.TabPage_MojeDane.PerformLayout();
            this.TabPage_Inne.ResumeLayout(false);
            this.TabPage_Inne.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_ImieNazwisko;
        private System.Windows.Forms.TextBox TextBox_Adres;
        private System.Windows.Forms.TextBox TextBox_KodMiasto;
        private System.Windows.Forms.TabControl TabControl_Ustawienia;
        private System.Windows.Forms.TabPage TabPage_MojeDane;
        private System.Windows.Forms.Label Llabel_Firma;
        private System.Windows.Forms.Label Label_KodMiasto;
        private System.Windows.Forms.Label Label_Adres;
        private System.Windows.Forms.Label Label_ImieNazwisko;
        private System.Windows.Forms.TextBox TextBox_Firma;
        private System.Windows.Forms.TabPage TabPage_Inne;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Label_FolderRachunkow;
        private System.Windows.Forms.TextBox TextBox_FolderRachunkow;
        private System.Windows.Forms.CheckBox CheckBox_Aktualizacje;
    }
}