using System;
using System.Windows.Forms;

namespace Księgowość
{
    public partial class Dodaj_przychody : Form
    {
        public static string K_nazwa, K_ulica_dom, K_kod_miasto, K_data, K_nr_kol, K_forma_platnosci;
        public static string[,] K_przychody, K_sprzedaz, K_koszty;

        public Dodaj_przychody()
        {
            InitializeComponent();

            DateTime Dzisiaj = DateTime.Today;
            TextBox_Data_sprzedazy.Text = Dzisiaj.ToString("dd.MM.yyyy");
            TextBox_nr_kol_1.Text = Dzisiaj.ToString("MMyyyy");
            TextBox_nr_kol_2.Text = OknoGlowne.Podaj_nowy_numer_kolejny(Dzisiaj.ToString("MM"));
            Wypelnij_klientow();
        }

        private void Wypelnij_klientow()
        {
            ComboBox_Klienci.Items.Clear();

            for(int a = 0; a < OknoGlowne.Lista_klientów.GetLength(0); a++)
            {
                ComboBox_Klienci.Items.Add(OknoGlowne.Lista_klientów[a, 0]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lista_Klientow_okno Lista_Klientow_okno_ = new Lista_Klientow_okno();
            Lista_Klientow_okno_.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Wypelnij_klientow();
        }

        private void Button_Str1_Dalej_Click(object sender, EventArgs e)
        {
            TabControl_Dodaj.SelectedTab = TabPage_Przychody;
        }

        private void Button_Str2_Dalej_Click(object sender, EventArgs e)
        {
            TabControl_Dodaj.SelectedTab = TabPage_Koszty;
        }

        private void Button_Str3_Dodaj_Click(object sender, EventArgs e)
        {
            K_nazwa = TextBox_Kupujacy_nazwa.Text;
            K_ulica_dom = TextBox_Kupujacy_ulica.Text;
            K_kod_miasto = TextBox_Kupujacy_kod_miasto.Text;
            K_data = TextBox_Data_sprzedazy.Text;
            K_nr_kol = TextBox_nr_kol_1.Text + "." + TextBox_nr_kol_2.Text;
            K_forma_platnosci = ComboBox_Forma_platnosci.Text;

            K_przychody = new string[1, 6];
            K_przychody[0, 0] = K_data;
            K_przychody[0, 1] = K_nr_kol;
            K_przychody[0, 2] = K_nazwa;
            K_przychody[0, 3] = K_ulica_dom;
            K_przychody[0, 4] = K_kod_miasto;
            K_przychody[0, 5] = K_forma_platnosci;

            Zczytaj_Sprzedaz();
            Zczytaj_Koszty();

            OknoGlowne.Dodaj_sprzedaz();
            this.Close();
        }

        private void Zczytaj_Sprzedaz()
        {
            int dlugosc = DataGrid_Przychody.RowCount - 1;
            int szerokosc = 4;
            string nazwa2, cena2, ilosc2;
            K_sprzedaz = new string[dlugosc, szerokosc];

            for (int a = 0; a < dlugosc; a++)
            {
                nazwa2 = DataGrid_Przychody.Rows[a].Cells[0].Value.ToString();
                cena2 = DataGrid_Przychody.Rows[a].Cells[1].Value.ToString();
                ilosc2 = DataGrid_Przychody.Rows[a].Cells[2].Value.ToString();

                K_sprzedaz[a, 0] = K_nr_kol;
                K_sprzedaz[a, 1] = nazwa2;
                K_sprzedaz[a, 2] = cena2;
                K_sprzedaz[a, 3] = ilosc2;
            }
        }

        private void Zczytaj_Koszty()
        {
            int dlugosc = DataGrid_Koszty.RowCount - 1;
            int szerokosc = 3;
            string nazwa2, koszt2;
            K_koszty = new string[dlugosc, szerokosc];

            for (int a = 0; a < dlugosc; a++)
            {
                nazwa2 = DataGrid_Koszty.Rows[a].Cells[0].Value.ToString();
                koszt2 = DataGrid_Koszty.Rows[a].Cells[1].Value.ToString();

                K_koszty[a, 0] = K_nr_kol;
                K_koszty[a, 1] = nazwa2;
                K_koszty[a, 2] = koszt2;
            }
        }

        private void TextBox_Data_sprzedazy_KeyUp(object sender, KeyEventArgs e)
        {
            string texcik = TextBox_Data_sprzedazy.Text;
            if (texcik.Length == 10)
            {               
                DateTime datka;
                try
                {
                    datka = DateTime.Parse(texcik);
                    TextBox_nr_kol_1.Text = datka.ToString("MMyyyy");
                    TextBox_nr_kol_2.Text = OknoGlowne.Podaj_nowy_numer_kolejny(datka.ToString("MM"));
                }
                catch
                {

                }
            }
        }

        private void ComboBox_Klienci_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nazwa_klienta = ComboBox_Klienci.Text;

            TextBox_Kupujacy_nazwa.Text = OknoGlowne.Podaj_Imie_i_Nazwisko_klienta(nazwa_klienta);
            TextBox_Kupujacy_ulica.Text = OknoGlowne.Podaj_adres_klienta(nazwa_klienta);
            TextBox_Kupujacy_kod_miasto.Text = OknoGlowne.Podaj_kod_i_miasto_klienta(nazwa_klienta);
        }
    }
}
