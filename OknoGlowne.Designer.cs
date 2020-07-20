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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OknoGlowne));
            this.tabControl_Glowne = new System.Windows.Forms.TabControl();
            this.tabPage_Sprzedaz = new System.Windows.Forms.TabPage();
            this.dataGrid_Sprzedaz = new System.Windows.Forms.DataGridView();
            this.tabControl_Glowne.SuspendLayout();
            this.tabPage_Sprzedaz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Sprzedaz)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl_Glowne
            // 
            this.tabControl_Glowne.Controls.Add(this.tabPage_Sprzedaz);
            this.tabControl_Glowne.Location = new System.Drawing.Point(12, 12);
            this.tabControl_Glowne.Name = "tabControl_Glowne";
            this.tabControl_Glowne.SelectedIndex = 0;
            this.tabControl_Glowne.Size = new System.Drawing.Size(776, 426);
            this.tabControl_Glowne.TabIndex = 0;
            // 
            // tabPage_Sprzedaz
            // 
            this.tabPage_Sprzedaz.Controls.Add(this.dataGrid_Sprzedaz);
            this.tabPage_Sprzedaz.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Sprzedaz.Name = "tabPage_Sprzedaz";
            this.tabPage_Sprzedaz.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Sprzedaz.Size = new System.Drawing.Size(768, 400);
            this.tabPage_Sprzedaz.TabIndex = 0;
            this.tabPage_Sprzedaz.Text = "Sprzedaż";
            this.tabPage_Sprzedaz.UseVisualStyleBackColor = true;
            // 
            // dataGrid_Sprzedaz
            // 
            this.dataGrid_Sprzedaz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Sprzedaz.Location = new System.Drawing.Point(6, 6);
            this.dataGrid_Sprzedaz.Name = "dataGrid_Sprzedaz";
            this.dataGrid_Sprzedaz.Size = new System.Drawing.Size(756, 388);
            this.dataGrid_Sprzedaz.TabIndex = 0;
            // 
            // OknoGlowne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl_Glowne);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OknoGlowne";
            this.Text = "Księgowość";
            this.tabControl_Glowne.ResumeLayout(false);
            this.tabPage_Sprzedaz.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Sprzedaz)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_Glowne;
        private System.Windows.Forms.TabPage tabPage_Sprzedaz;
        private System.Windows.Forms.DataGridView dataGrid_Sprzedaz;
    }
}

