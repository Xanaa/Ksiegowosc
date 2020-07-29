using System;
using System.Windows.Forms;
using Pomocnik;

namespace Księgowość
{
    public partial class OknoGlowne : Form
    {
        public static string sciezka_startowa = Application.StartupPath;
        public static string P_Koszty = @"\Dane\Koszty.txt";
        public static string P_Przychody = @"\Dane\Przychody.txt";

        public static string[,] Koszty = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, P_Koszty, 3, "#", ";");
        public static string[,] Przychody = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, P_Przychody, 4, "#", ";");

        public static decimal Stawka_PIT = decimal.Parse("0,17");

        public OknoGlowne()
        {
            InitializeComponent();
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

    }
}
