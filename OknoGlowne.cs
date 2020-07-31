using System;
using System.IO;
using System.Windows.Forms;
using Pomocnik;

namespace Księgowość
{
    public partial class OknoGlowne : Form
    {
        public static string sciezka_startowa = Application.StartupPath;
        public static string F_Dane = @"/Dane";
        public static string F_Koszty = F_Dane + @"/Koszty";
        public static string F_Przychody = F_Dane + @"/Przychody";
        public static string P_Koszty = F_Koszty + @"/Koszty-";
        public static string P_Przychody = F_Przychody + @"/Przychody-";
        public static string P_Statystyka = F_Dane + @"/Statystyka.txt";

        public static string[,] Koszty;
        public static string[,] Przychody;
        public static string[,] Statystyka = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, P_Statystyka, 3, "#", ";");

        public static decimal Stawka_PIT = decimal.Parse("0,17");
        public static string ROK = "1995";

        public OknoGlowne()
        {
            InitializeComponent();
            ROK = DateTime.Now.Year.ToString(); // Obecny rok
            Wczytaj_rok(ROK);            
        }

        public void Wczytaj_rok(string rok)
        {
            ROK = rok;
            this.Text = "Księgowość - Rok " + ROK;
            toolStripComboBox_rok.Text = ROK;
            Czy_pliki_istnieja(rok);
            string Koszty_s2 = P_Koszty + rok + ".txt";
            string Przychody_s2 = P_Przychody + rok + ".txt";
            Koszty = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, Koszty_s2, 3, "#", ";");
            Przychody = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, Przychody_s2, 4, "#", ";");

            Wczytaj_Przychody();
            Wczytaj_Koszty();
            Wczytaj_Info_PIT();
        }

        public void Wczytaj_Przychody()
        {
            dataGrid_Przychody.Rows.Clear();

            for(int a = 0; a < Przychody.GetLength(0); a++)
            {
                DateTime Data = DateTime.Parse(Przychody[a, 0]);
                string nrkol = Przychody[a, 1];
                decimal kwota = decimal.Parse(Przychody[a, 2]);
                string Opis = Przychody[a, 3];
                dataGrid_Przychody.Rows.Add(Data, nrkol, kwota, Opis);
            }
        }

        public void Wczytaj_Koszty()
        {
            dataGrid_Koszty.Rows.Clear();

            for (int a = 0; a < Przychody.GetLength(0); a++)
            {
                string nrkol = Koszty[a, 0];
                decimal kwota = decimal.Parse(Koszty[a, 1]);
                string Opis = Koszty[a, 2];
                dataGrid_Koszty.Rows.Add(nrkol, kwota, Opis);
            }
        }

        public void Wczytaj_Info_PIT()
        {
            dataGrid_PITMiesiecznie.Rows.Clear();

            for(int a = 0; a < 12; a ++)
            {
                string miech = (a+1).ToString("00");
                decimal Przychod = Oblicz_Przychody_miesiaca(miech);
                decimal Koszty = Oblicz_Koszty_miesiaca(miech);
                decimal Dochody = Oblicz_Dochody_miesiaca(miech);
                decimal Zal_PIT = Oblicz_Zaliczke_PIT_miesiaca(miech);
                dataGrid_PITMiesiecznie.Rows.Add(miech, Przychod, Koszty, Dochody, Zal_PIT, false);
            }

            textBox_MiesLimPrzych.Text = Podaj_limit_przychodu(ROK).ToString("C2");
        }

        public static decimal Oblicz_Przychody_miesiaca(string miesiac)
        {
            decimal Zwrot = 0;

            for (int a = 0; a < Przychody.GetLength(0); a++)
            {
                string mies = Przychody[a, 0].Remove(0, 3).Remove(2);
                if(mies == miesiac)
                {
                    decimal kwota = decimal.Parse(Przychody[a, 2]);
                    Zwrot += kwota;
                }
            }

            return Zwrot;
        }

        public static decimal Oblicz_Koszty_miesiaca(string miesiac)
        {
            decimal Zwrot = 0;

            for (int a = 0; a < Koszty.GetLength(0); a++)
            {
                string mies = Koszty[a, 0].Remove(2);
                if (mies == miesiac)
                {
                    decimal kwota = decimal.Parse(Koszty[a, 1]);
                    Zwrot += kwota;
                }
            }

            return Zwrot;
        }

        public static decimal Oblicz_Dochody_miesiaca(string miesiac)
        {
            decimal Przychod = Oblicz_Przychody_miesiaca(miesiac);
            decimal Koszty = Oblicz_Koszty_miesiaca(miesiac);
            return (Przychod- Koszty);
        }

        public static decimal Oblicz_Zaliczke_PIT_miesiaca(string miesiac)
        {
            decimal Dochodek = Oblicz_Dochody_miesiaca(miesiac);
            return decimal.Round((Dochodek* Stawka_PIT),2, MidpointRounding.AwayFromZero);
        }

        public void Czy_pliki_istnieja(string rok)
        {
            string[] Do_zapisu = new string[0];
            string pth_koszty = P_Koszty + rok + ".txt";
            string pth_przychody = P_Przychody + rok + ".txt";
            if (!File.Exists(sciezka_startowa + pth_koszty))
            {
                Directory.CreateDirectory(sciezka_startowa + F_Koszty);
                File.WriteAllLines(sciezka_startowa+ pth_koszty, Do_zapisu);
            }
            if (!File.Exists(sciezka_startowa + pth_przychody))
            {
                Directory.CreateDirectory(sciezka_startowa + F_Przychody);
                File.WriteAllLines(sciezka_startowa + pth_przychody, Do_zapisu);
            }
        }

        public static decimal Podaj_Minimalne_wynagrodzenie(string rok)
        {
            decimal limit = 0;

            for (int a = 0; a < Statystyka.GetLength(0); a++)
            {
                if(Statystyka[a,0] == rok && Statystyka[a, 1] == "Minimalne wynagrodzenie")
                {
                    limit = decimal.Parse(Statystyka[a, 2]);
                    break;
                }
            }

            return limit;
        }

        public static decimal Podaj_limit_przychodu(string rok)
        {
            decimal Min_Wynagrodzenie = Podaj_Minimalne_wynagrodzenie(rok);
            decimal mnoznik_temp = decimal.Parse("0,5");
            return (Min_Wynagrodzenie * mnoznik_temp);
        }

        private void toolStripComboBox_rok_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ROK != toolStripComboBox_rok.Text)
            {
                Wczytaj_rok(toolStripComboBox_rok.Text);
            }
        }
    }
}
