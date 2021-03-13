namespace Księgowość
{
    partial class O_programie
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(O_programie));
            this.RichTextBox_Historia = new System.Windows.Forms.RichTextBox();
            this.Label_Ksiegowosc = new System.Windows.Forms.Label();
            this.Button_Spr_upd = new System.Windows.Forms.Button();
            this.GroupBox_Wersje = new System.Windows.Forms.GroupBox();
            this.GroupBox_Aktualizacje = new System.Windows.Forms.GroupBox();
            this.Button_Zglos_blad = new System.Windows.Forms.Button();
            this.Button_pobierz = new System.Windows.Forms.Button();
            this.Label_Nowa_wersja = new System.Windows.Forms.Label();
            this.Label_Moja_wersja = new System.Windows.Forms.Label();
            this.TextBox_Nowa_wersja = new System.Windows.Forms.TextBox();
            this.TextBox_Moja_wersja = new System.Windows.Forms.TextBox();
            this.GroupBox_Wersje.SuspendLayout();
            this.GroupBox_Aktualizacje.SuspendLayout();
            this.SuspendLayout();
            // 
            // RichTextBox_Historia
            // 
            this.RichTextBox_Historia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextBox_Historia.Location = new System.Drawing.Point(6, 48);
            this.RichTextBox_Historia.Name = "RichTextBox_Historia";
            this.RichTextBox_Historia.ReadOnly = true;
            this.RichTextBox_Historia.Size = new System.Drawing.Size(492, 328);
            this.RichTextBox_Historia.TabIndex = 0;
            this.RichTextBox_Historia.Text = "";
            // 
            // Label_Ksiegowosc
            // 
            this.Label_Ksiegowosc.AutoSize = true;
            this.Label_Ksiegowosc.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label_Ksiegowosc.Location = new System.Drawing.Point(6, 16);
            this.Label_Ksiegowosc.Name = "Label_Ksiegowosc";
            this.Label_Ksiegowosc.Size = new System.Drawing.Size(162, 29);
            this.Label_Ksiegowosc.TabIndex = 1;
            this.Label_Ksiegowosc.Text = "Księgowość ";
            // 
            // Button_Spr_upd
            // 
            this.Button_Spr_upd.Location = new System.Drawing.Point(6, 71);
            this.Button_Spr_upd.Name = "Button_Spr_upd";
            this.Button_Spr_upd.Size = new System.Drawing.Size(198, 22);
            this.Button_Spr_upd.TabIndex = 2;
            this.Button_Spr_upd.Text = "Sprawdź aktualizacje";
            this.Button_Spr_upd.UseVisualStyleBackColor = true;
            this.Button_Spr_upd.Click += new System.EventHandler(this.Button_Spr_upd_Click);
            // 
            // GroupBox_Wersje
            // 
            this.GroupBox_Wersje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox_Wersje.Controls.Add(this.Label_Ksiegowosc);
            this.GroupBox_Wersje.Controls.Add(this.RichTextBox_Historia);
            this.GroupBox_Wersje.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_Wersje.Name = "GroupBox_Wersje";
            this.GroupBox_Wersje.Size = new System.Drawing.Size(504, 382);
            this.GroupBox_Wersje.TabIndex = 3;
            this.GroupBox_Wersje.TabStop = false;
            this.GroupBox_Wersje.Text = "Wersje";
            // 
            // GroupBox_Aktualizacje
            // 
            this.GroupBox_Aktualizacje.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Aktualizacje.Controls.Add(this.Button_Zglos_blad);
            this.GroupBox_Aktualizacje.Controls.Add(this.Button_pobierz);
            this.GroupBox_Aktualizacje.Controls.Add(this.Label_Nowa_wersja);
            this.GroupBox_Aktualizacje.Controls.Add(this.Label_Moja_wersja);
            this.GroupBox_Aktualizacje.Controls.Add(this.TextBox_Nowa_wersja);
            this.GroupBox_Aktualizacje.Controls.Add(this.TextBox_Moja_wersja);
            this.GroupBox_Aktualizacje.Controls.Add(this.Button_Spr_upd);
            this.GroupBox_Aktualizacje.Location = new System.Drawing.Point(522, 12);
            this.GroupBox_Aktualizacje.Name = "GroupBox_Aktualizacje";
            this.GroupBox_Aktualizacje.Size = new System.Drawing.Size(234, 382);
            this.GroupBox_Aktualizacje.TabIndex = 4;
            this.GroupBox_Aktualizacje.TabStop = false;
            this.GroupBox_Aktualizacje.Text = "Aktualizacje";
            // 
            // Button_Zglos_blad
            // 
            this.Button_Zglos_blad.Location = new System.Drawing.Point(6, 128);
            this.Button_Zglos_blad.Name = "Button_Zglos_blad";
            this.Button_Zglos_blad.Size = new System.Drawing.Size(198, 23);
            this.Button_Zglos_blad.TabIndex = 8;
            this.Button_Zglos_blad.Text = "Zgłoś błąd";
            this.Button_Zglos_blad.UseVisualStyleBackColor = true;
            this.Button_Zglos_blad.Click += new System.EventHandler(this.Button_Zglos_blad_Click);
            // 
            // Button_pobierz
            // 
            this.Button_pobierz.Location = new System.Drawing.Point(6, 99);
            this.Button_pobierz.Name = "Button_pobierz";
            this.Button_pobierz.Size = new System.Drawing.Size(198, 23);
            this.Button_pobierz.TabIndex = 7;
            this.Button_pobierz.Text = "Pobierz aktualizacje";
            this.Button_pobierz.UseVisualStyleBackColor = true;
            this.Button_pobierz.Visible = false;
            this.Button_pobierz.Click += new System.EventHandler(this.Button_pobierz_Click);
            // 
            // Label_Nowa_wersja
            // 
            this.Label_Nowa_wersja.AutoSize = true;
            this.Label_Nowa_wersja.Location = new System.Drawing.Point(112, 48);
            this.Label_Nowa_wersja.Name = "Label_Nowa_wersja";
            this.Label_Nowa_wersja.Size = new System.Drawing.Size(92, 13);
            this.Label_Nowa_wersja.TabIndex = 6;
            this.Label_Nowa_wersja.Text = "Najnowsza wersja";
            // 
            // Label_Moja_wersja
            // 
            this.Label_Moja_wersja.AutoSize = true;
            this.Label_Moja_wersja.Location = new System.Drawing.Point(112, 22);
            this.Label_Moja_wersja.Name = "Label_Moja_wersja";
            this.Label_Moja_wersja.Size = new System.Drawing.Size(110, 13);
            this.Label_Moja_wersja.TabIndex = 5;
            this.Label_Moja_wersja.Text = "Wersja zainstalowana";
            // 
            // TextBox_Nowa_wersja
            // 
            this.TextBox_Nowa_wersja.Location = new System.Drawing.Point(6, 45);
            this.TextBox_Nowa_wersja.Name = "TextBox_Nowa_wersja";
            this.TextBox_Nowa_wersja.ReadOnly = true;
            this.TextBox_Nowa_wersja.Size = new System.Drawing.Size(100, 20);
            this.TextBox_Nowa_wersja.TabIndex = 4;
            // 
            // TextBox_Moja_wersja
            // 
            this.TextBox_Moja_wersja.Location = new System.Drawing.Point(6, 19);
            this.TextBox_Moja_wersja.Name = "TextBox_Moja_wersja";
            this.TextBox_Moja_wersja.ReadOnly = true;
            this.TextBox_Moja_wersja.Size = new System.Drawing.Size(100, 20);
            this.TextBox_Moja_wersja.TabIndex = 3;
            // 
            // O_programie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 406);
            this.Controls.Add(this.GroupBox_Aktualizacje);
            this.Controls.Add(this.GroupBox_Wersje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "O_programie";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "O programie";
            this.GroupBox_Wersje.ResumeLayout(false);
            this.GroupBox_Wersje.PerformLayout();
            this.GroupBox_Aktualizacje.ResumeLayout(false);
            this.GroupBox_Aktualizacje.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichTextBox_Historia;
        private System.Windows.Forms.Label Label_Ksiegowosc;
        private System.Windows.Forms.Button Button_Spr_upd;
        private System.Windows.Forms.GroupBox GroupBox_Wersje;
        private System.Windows.Forms.GroupBox GroupBox_Aktualizacje;
        private System.Windows.Forms.Label Label_Nowa_wersja;
        private System.Windows.Forms.Label Label_Moja_wersja;
        private System.Windows.Forms.TextBox TextBox_Nowa_wersja;
        private System.Windows.Forms.TextBox TextBox_Moja_wersja;
        private System.Windows.Forms.Button Button_pobierz;
        private System.Windows.Forms.Button Button_Zglos_blad;
    }
}