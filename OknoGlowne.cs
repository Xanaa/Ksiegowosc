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
        public static string F_Sprzedaz = F_Dane + @"/Sprzedaż";
        public static string P_Koszty = F_Koszty + @"/Koszty-";
        public static string P_Przychody = F_Przychody + @"/Przychody-";
        public static string P_Sprzedaz = F_Sprzedaz + @"/Sprzedaż-";
        public static string P_Statystyka = F_Dane + @"/Statystyka.txt";

        public static string[,] Koszty;
        public static string[,] Przychody;
        public static string[,] Sprzedaz;
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
            string Sprzedaz_s2 = P_Sprzedaz + rok + ".txt";

            Przychody = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, Przychody_s2, 6, "#", ";");
            Sprzedaz = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, Sprzedaz_s2, 4, "#", ";");
            Koszty = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, Koszty_s2, 3, "#", ";");

            Wczytaj_Przychody();
            Wczytaj_Sprzedaz();
            Wczytaj_Koszty();
            Wczytaj_Info_PIT();
        }

        public void Wczytaj_Przychody()
        {
            dataGrid_Przychody.Rows.Clear();

            DateTime Data;
            string nrkol;
            string Nabywca;
            string Adres;
            string Kod_i_miasto;
            string Forma_platnosci;
            decimal Przychod;

            for (int a = 0; a < Przychody.GetLength(0); a++)
            {
                Data = DateTime.Parse(Przychody[a, 0]);
                nrkol = Przychody[a, 1];
                Nabywca = Przychody[a, 2];
                Adres = Przychody[a, 3];
                Kod_i_miasto = Przychody[a, 4];
                Forma_platnosci = Przychody[a, 5];
                Przychod = Podaj_przychod_faktury(nrkol);

                dataGrid_Przychody.Rows.Add(Data, nrkol, Nabywca, Adres, Kod_i_miasto, Forma_platnosci, Przychod);
            }
        }

        public void Wczytaj_Sprzedaz()
        {
            dataGridView_Sprzedaz.Rows.Clear();

            string nrkol;
            string Opis;
            decimal cena;
            decimal ilosc;
            decimal wartosc;

            for (int a = 0; a < Sprzedaz.GetLength(0); a++)
            {
                nrkol = Sprzedaz[a, 0];
                Opis = Sprzedaz[a, 1];
                cena = decimal.Parse(Sprzedaz[a, 2]);
                ilosc = decimal.Parse(Sprzedaz[a, 3]);
                wartosc = cena * ilosc;

                dataGridView_Sprzedaz.Rows.Add(nrkol, Opis, cena, ilosc, wartosc);
            }
        }

        public void Wczytaj_Koszty()
        {
            dataGrid_Koszty.Rows.Clear();

            string nrkol;
            decimal kwota;
            string Opis;

            for (int a = 0; a < Przychody.GetLength(0); a++)
            {
                nrkol = Koszty[a, 0];
                kwota = decimal.Parse(Koszty[a, 1]);
                Opis = Koszty[a, 2];
                dataGrid_Koszty.Rows.Add(nrkol, kwota, Opis);
            }
        }

        public void Wczytaj_Info_PIT()
        {
            dataGrid_PITMiesiecznie.Rows.Clear();
            decimal mies_limit = Podaj_limit_przychodu(ROK);
            string miech;
            decimal Przychod;
            decimal Koszty;
            decimal Dochod;
            decimal Zal_PIT;
            decimal netto;
            decimal pozostaly_limit;

            for (int a = 0; a < 12; a ++)
            {
                miech = (a+1).ToString("00");
                Przychod = Oblicz_Przychody_miesiaca(miech);
                Koszty = Oblicz_Koszty_miesiaca(miech);
                Dochod = Oblicz_Dochody_miesiaca(miech);
                Zal_PIT = Oblicz_Zaliczke_PIT_miesiaca(miech);
                netto = Dochod - Zal_PIT;
                pozostaly_limit = mies_limit - Przychod;
                dataGrid_PITMiesiecznie.Rows.Add(miech, Przychod, Koszty, Dochod, Zal_PIT, netto, pozostaly_limit, false);
            }

            textBox_MiesLimPrzych.Text = mies_limit.ToString("C2");
            textBox_S_Przychodow.Text = Oblicz_Przychody_roku().ToString("C2");
            textBox_S_Kosztow.Text = Oblicz_Koszty_roku().ToString("C2");
            textBox_S_Dochodow.Text = Oblicz_Dochody_roku().ToString("C2");
            textBox_S_ZaliczekPIT.Text = Oblicz_Zaliczke_PIT_roku().ToString("C2");
            textBox_S_netto.Text = Oblicz_netto_roku().ToString("C2");
        }

        public static decimal Oblicz_Przychody_miesiaca(string miesiac)
        {
            decimal Zwrot = 0;
            string mies;
            decimal cena;
            decimal ilosc;

            for (int a = 0; a < Sprzedaz.GetLength(0); a++)
            {
                mies = Podaj_date_sprzedazy(Sprzedaz[a, 0]).Remove(0, 3).Remove(2);
                if(mies == miesiac)
                {
                    cena = decimal.Parse(Sprzedaz[a, 2]);
                    ilosc = decimal.Parse(Sprzedaz[a, 3]);
                    Zwrot += cena* ilosc;
                }
            }

            return Zwrot;
        }

        public static decimal Oblicz_Koszty_miesiaca(string miesiac)
        {
            decimal Zwrot = 0;
            string mies;
            decimal kwota;

            for (int a = 0; a < Koszty.GetLength(0); a++)
            {
                mies = Podaj_date_sprzedazy(Koszty[a, 0]).Remove(0, 3).Remove(2);
                if (mies == miesiac)
                {
                    kwota = decimal.Parse(Koszty[a, 1]);
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

        public static decimal Oblicz_Przychody_roku()
        {
            decimal Zwrot = 0;

            for (int a = 0; a < 12; a++)
            {
                Zwrot += Oblicz_Przychody_miesiaca((a + 1).ToString("00"));
            }

            return Zwrot;
        }

        public static decimal Oblicz_Koszty_roku()
        {
            decimal Zwrot = 0;

            for (int a = 0; a < Koszty.GetLength(0); a++)
            {
                decimal kwota = decimal.Parse(Koszty[a, 1]);
                Zwrot += kwota;
            }

            return Zwrot;
        }

        public static decimal Oblicz_Dochody_roku()
        {
            decimal Przychod = Oblicz_Przychody_roku();
            decimal Koszty = Oblicz_Koszty_roku();
            return (Przychod - Koszty);
        }

        public static decimal Oblicz_Zaliczke_PIT_roku()
        {
            decimal Dochodek = 0;

            for(int a = 0; a < 12; a ++)
            {
                Dochodek += Oblicz_Zaliczke_PIT_miesiaca((a + 1).ToString("00"));
            }

            return Dochodek;
        }

        public static decimal Oblicz_netto_roku()
        {
            decimal doch = Oblicz_Dochody_roku();
            decimal PITy = Oblicz_Zaliczke_PIT_roku();
            return (doch- PITy);
        }

        public static string Podaj_date_sprzedazy(string nr_kol)
        {
            string data = "0";
            for (int a = 0; a < Przychody.GetLength(0); a++)
            {
                if(Przychody[a,1] == nr_kol)
                {
                    data = Przychody[a, 0];
                    break;
                }
            }
            return data;
        }

        public static decimal Podaj_przychod_faktury(string nr_kol)
        {
            decimal kwota = 0;
            decimal cena;
            decimal ilosc;

            for (int a = 0; a < Sprzedaz.GetLength(0); a++)
            {
                if (Sprzedaz[a,0] == nr_kol)
                {
                    cena = decimal.Parse(Sprzedaz[a, 2]);
                    ilosc = decimal.Parse(Sprzedaz[a, 3]);
                    kwota += cena * ilosc;
                }
            }


            return kwota;
        }

        private void ToolStripComboBox_rok_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ROK != toolStripComboBox_rok.Text)
            {
                Wczytaj_rok(toolStripComboBox_rok.Text);
            }
        }
    }
}
