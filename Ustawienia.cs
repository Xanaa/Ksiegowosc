using System;
using System.IO;
using System.Windows.Forms;

namespace Księgowość
{
    public partial class Ustawienia : Form
    {
        public Ustawienia()
        {
            InitializeComponent();
            Wczytaj_ustawienia();
        }

        private void Wczytaj_ustawienia()
        {
            TextBox_ImieNazwisko.Text = OknoGlowne.Podaj_moje_imie_i_nazwisko();
            TextBox_Adres.Text = OknoGlowne.Podaj_moj_adres();
            TextBox_KodMiasto.Text = OknoGlowne.Podaj_moj_kod_i_miasto();
            TextBox_Firma.Text = OknoGlowne.Podaj_moja_firme();
            TextBox_FolderRachunkow.Text = OknoGlowne.Podaj_folder_rachunkow("1");
            CheckBox_Aktualizacje.Checked = OknoGlowne.Podaj_aktualizacje_na_start();
            TextBox_Logo.Text = OknoGlowne.Podaj_nazwe_Logo();
            CheckBox_Logo.Checked = OknoGlowne.Podaj_czy_Logo();
        }

        private void Button_Zapisz_Click(object sender, EventArgs e)
        {
            Zapisz_ustawienia();
        }

        public void Zapisz_ustawienia()
        {
            string[] DoZapisu = new string[15];
            DoZapisu[0] = "# Jeśli sprzedajesz jako firma, wpisz nazwę";
            DoZapisu[1] = "Firma;" + TextBox_Firma.Text;
            DoZapisu[2] = "Imię i nazwisko;" + TextBox_ImieNazwisko.Text;
            DoZapisu[3] = "# ul. \"Nazwa ulicy\" dom/mieszkanie";
            DoZapisu[4] = "Adres;" + TextBox_Adres.Text;
            DoZapisu[5] = "Kod i miasto;" + TextBox_KodMiasto.Text;
            DoZapisu[6] = "# Folder zostaw pusty lub podaj z ukośnikiem na końcu";
            DoZapisu[7] = "# np. C:\\Users\\Janusz\\Desktop\\Rachunki\\";
            DoZapisu[8] = "Folder rachunków;" + TextBox_FolderRachunkow.Text;
            DoZapisu[9] = "# Czy aktualizacje mają być sprawdzane przy starcie aplikacji";
            if (CheckBox_Aktualizacje.Checked == true)
            {
                DoZapisu[10] = "Aktualizacja;1";
            }
            else
            {
                DoZapisu[10] = "Aktualizacja;0";
            }
            DoZapisu[11] = "# Logo - czy ma być wyświetlane";
            if (CheckBox_Logo.Checked == true)
            {
                DoZapisu[12] = "Logo;1";
            }
            else
            {
                DoZapisu[12] = "Logo;0";
            }
            DoZapisu[13] = "# Logo - nazwa pliku";
            DoZapisu[14] = "Logo nazwa;" + TextBox_Logo.Text;

            File.WriteAllLines(OknoGlowne.sciezka_startowa + OknoGlowne.P_Moje_dane, DoZapisu);
        }
    }
}
