using System.IO;
using System.Windows.Forms;

namespace Księgowość
{
    public partial class Lista_Klientow_okno : Form
    {
        public Lista_Klientow_okno()
        {
            InitializeComponent();
            Wczytaj_dane();
        }

        public void Wczytaj_dane()
        {
            dataGrid_Klienci.Rows.Clear();
            for (int a = 0; a < OknoGlowne.Lista_klientów.GetLength(0); a++)
            {
                dataGrid_Klienci.Rows.Add(
                    OknoGlowne.Lista_klientów[a, 0],
                    OknoGlowne.Lista_klientów[a, 1],
                    OknoGlowne.Lista_klientów[a, 2],
                    OknoGlowne.Lista_klientów[a, 3],
                    OknoGlowne.Lista_klientów[a, 4],
                    OknoGlowne.Lista_klientów[a, 5]
                    );
            }
        }

        public void Wczytaj_DGV()
        {
            int klienci_dlugosc = dataGrid_Klienci.Rows.GetRowCount(0) - 1;
            int klienci_szerokosc = 6;
            string[,] Nowi_klienci = new string[klienci_dlugosc, klienci_szerokosc];
            string[] Nowi_klienci_zapis = new string[klienci_dlugosc];
            string nazwa_klienta, Imie_i_nazwisko, adres, kod_i_miasto, email, telefon;

            for (int a = 0; a < klienci_dlugosc; a++)
            {
                nazwa_klienta = dataGrid_Klienci.Rows[a].Cells[0].Value.ToString();
                Imie_i_nazwisko = dataGrid_Klienci.Rows[a].Cells[1].Value.ToString();
                adres = dataGrid_Klienci.Rows[a].Cells[2].Value.ToString();
                kod_i_miasto = dataGrid_Klienci.Rows[a].Cells[3].Value.ToString();
                email = dataGrid_Klienci.Rows[a].Cells[4].Value.ToString();
                telefon = dataGrid_Klienci.Rows[a].Cells[5].Value.ToString();


                Nowi_klienci[a, 0] = nazwa_klienta;
                Nowi_klienci[a, 1] = Imie_i_nazwisko;
                Nowi_klienci[a, 2] = adres;
                Nowi_klienci[a, 3] = kod_i_miasto;
                Nowi_klienci[a, 4] = email;
                Nowi_klienci[a, 5] = telefon;
                Nowi_klienci_zapis[a] = nazwa_klienta + ";" + Imie_i_nazwisko + ";" + adres + ";" + kod_i_miasto + ";" + email + ";" + telefon;
            }

            OknoGlowne.Lista_klientów = Nowi_klienci;
            Zapisz_liste_klientow(Nowi_klienci_zapis);
        }

        public static void Zapisz_liste_klientow(string[] Lista_nowa)
        {
            File.WriteAllLines(OknoGlowne.sciezka_startowa + OknoGlowne.P_Lista_klientów, Lista_nowa);
        }

        private void zapiszToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Wczytaj_DGV();
        }
    }
}
