using System;
using System.Windows.Forms;

namespace Księgowość
{
    public partial class Wystaw_rachunek_zakupu : Form
    {
        private static readonly DateTime Dzisiaj = DateTime.Today;

        public Wystaw_rachunek_zakupu()
        {
            InitializeComponent();
            Wypelnij_listy();

            TextBox_WRZ_Data_Sprzedazy.Text = Dzisiaj.ToString("dd.MM.yyyy");
            TextBox_WRZ_Opis_rachunku.Text = Dzisiaj.ToString("ddMMyyyy.");          
        }

        private void Wypelnij_listy()
        {
            ComboBox_WRZ_sprzedajacy.Items.Clear();

            for (int a = 0; a < OknoGlowne.Lista_klientów.GetLength(0); a++)
            {
                ComboBox_WRZ_sprzedajacy.Items.Add(OknoGlowne.Lista_klientów[a, 0]);
            }

            ComboBox_WRZ_Nosnik.Items.Clear();

            for (int a = 0; a < OknoGlowne.Przychody.GetLength(0); a++)
            {
                ComboBox_WRZ_Nosnik.Items.Add(OknoGlowne.Przychody[a, 1]);
            }
        }

        private void ComboBox_WRZ_sprzedajacy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nazwa_klienta = ComboBox_WRZ_sprzedajacy.Text;
            string nazwa_pelna = OknoGlowne.Podaj_Imie_i_Nazwisko_klienta(nazwa_klienta); ;
            TextBox_WRZ_Nazwa.Text = nazwa_pelna;
            TextBox_WRZ_Adres.Text = OknoGlowne.Podaj_adres_klienta(nazwa_klienta);
            TextBox_WRZ_Kod_i_Miasto.Text = OknoGlowne.Podaj_kod_i_miasto_klienta(nazwa_klienta);
            TextBox_WRZ_Opis_rachunku.Text = Dzisiaj.ToString("ddMMyyyy.") + (nazwa_klienta.Remove(1, 1)).Remove(2);
        }

        private void Button_WRZ_Dalej_Click(object sender, EventArgs e)
        {
            TabControl_WRZ.SelectedTab = TabPage_WRZ_Lista;
        }

        private void Button_WRZ_Gotowe_Click(object sender, EventArgs e)
        {
            OknoGlowne.RZ_Sprzedawca = ComboBox_WRZ_sprzedajacy.Text;
            OknoGlowne.RZ_Nazwa_rachunku = TextBox_WRZ_Opis_rachunku.Text;
            OknoGlowne.RZ_Nosnik = ComboBox_WRZ_Nosnik.Text;
            OknoGlowne.RZ_Data = TextBox_WRZ_Data_Sprzedazy.Text;
            OknoGlowne.RZ_Lista_zakupu = new string[DataGridView_WRZ_Lista.Rows.Count -1, 3];
            OknoGlowne.RZ_Forma_Platnosci = ComboBox_WRZ_Forma_platnosci.Text;

            for (int a = 0; a < DataGridView_WRZ_Lista.Rows.Count -1; a++)
            {
                OknoGlowne.RZ_Lista_zakupu[a, 0] = DataGridView_WRZ_Lista.Rows[a].Cells[0].Value.ToString();
                OknoGlowne.RZ_Lista_zakupu[a, 1] = DataGridView_WRZ_Lista.Rows[a].Cells[1].Value.ToString();
                OknoGlowne.RZ_Lista_zakupu[a, 2] = DataGridView_WRZ_Lista.Rows[a].Cells[2].Value.ToString();
            }

            OknoGlowne OG = new OknoGlowne();
            OG.Generowanie_rachunku_zakupu();
        }
    }
}
