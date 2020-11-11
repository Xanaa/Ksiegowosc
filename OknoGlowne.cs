using System;
using System.IO;
using System.Windows.Forms;
using Pomocnik;
using System.Threading;
using System.Globalization;
using System.Net;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Draw;

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
        public static string P_Moje_dane = F_Dane + @"/Moje dane.txt";
        public static string P_Lista_klientów = F_Dane + @"/Lista klientów.txt";

        public static string[,] Koszty;
        public static string[,] Przychody;
        public static string[,] Sprzedaz;
        public static string[,] Statystyka = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, P_Statystyka, 3, "#", ";");
        public static string[,] Moje_dane = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, P_Moje_dane, 2, "#", ";");
        public static string[,] Lista_klientów = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, P_Lista_klientów, 6, "#", ";");

        public static String FONT = sciezka_startowa + F_Dane + @"/FreeSans.ttf";
        public static decimal Stawka_PIT = 0;
        public static string ROK = "1995";
        public static bool Zapisane = true;

        // Do okien innych
        private static OknoGlowne Glowne_inst;

        public OknoGlowne()
        {
            InitializeComponent();
            Glowne_inst = this;
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("pl-PL");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            ROK = DateTime.Now.Year.ToString(); // Obecny rok
            Wczytaj_rok(ROK);

            if(Podaj_aktualizacje_na_start() == true)
            {
                Sprawdz_wersje();
            }           
        }

        public void Wczytaj_rok(string rok)
        {
            ROK = rok;
            this.Text = "Księgowość " + Application.ProductVersion + " - Rok " + ROK;
            toolStripComboBox_rok.Text = ROK;
            Czy_pliki_istnieja(rok);
            string Koszty_s2 = P_Koszty + rok + ".txt";
            string Przychody_s2 = P_Przychody + rok + ".txt";
            string Sprzedaz_s2 = P_Sprzedaz + rok + ".txt";
            Stawka_PIT = Podaj_stawke_PIT_roku(rok);

            Przychody = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, Przychody_s2, 4, "#", ";");
            Sprzedaz = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, Sprzedaz_s2, 4, "#", ";");
            Koszty = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, Koszty_s2, 3, "#", ";");

            if(Przychody != null)
            {
                Wczytaj_Przychody();
            }
            if (Sprzedaz != null)
            {
                Wczytaj_Sprzedaz();
            }
            if (Koszty != null)
            {
                Wczytaj_Koszty();
                Wczytaj_Info_PIT();
            }        
        }

        public void Wczytaj_Przychody()
        {
            dataGrid_Przychody.Rows.Clear();

            DateTime Data;
            string nrkol, Nazwa_klienta, Imie_i_Nazwisko_klienta, Adres, Kod_i_miasto, Forma_platnosci;
            decimal Przychod;

            for (int a = 0; a < Przychody.GetLength(0); a++)
            {
                Data = DateTime.Parse(Przychody[a, 0]);
                nrkol = Przychody[a, 1];
                Nazwa_klienta = Przychody[a, 2];
                Imie_i_Nazwisko_klienta = Podaj_Imie_i_Nazwisko_klienta(Nazwa_klienta);
                Adres = Podaj_adres_klienta(Nazwa_klienta);
                Kod_i_miasto = Podaj_kod_i_miasto_klienta(Nazwa_klienta);
                Forma_platnosci = Przychody[a, 3];
                Przychod = Podaj_przychod_faktury(nrkol);

                dataGrid_Przychody.Rows.Add(Data, nrkol, Imie_i_Nazwisko_klienta, Adres, Kod_i_miasto, Forma_platnosci, Przychod);
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

            if(Koszty != null)
            {
                for (int a = 0; a < Koszty.GetLength(0); a++)
                {
                    nrkol = Koszty[a, 0];
                    kwota = decimal.Parse(Koszty[a, 1]);
                    Opis = Koszty[a, 2];
                    dataGrid_Koszty.Rows.Add(nrkol, kwota, Opis);
                }
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

            for (int a = 0; a < 12; a++)
            {
                miech = (a + 1).ToString("00");
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
                mies = Podaj_date_sprzedazy(Sprzedaz[a, 0]);
                if(mies != "0")
                {
                    mies = mies.Remove(0, 3).Remove(2);
                    if (mies == miesiac)
                    {
                        cena = decimal.Parse(Sprzedaz[a, 2]);
                        ilosc = decimal.Parse(Sprzedaz[a, 3]);
                        Zwrot += cena * ilosc;
                    }
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
                mies = Podaj_date_sprzedazy(Koszty[a, 0]);
                if(mies != "0")
                {
                    mies = mies.Remove(0, 3).Remove(2);
                    if (mies == miesiac)
                    {
                        kwota = decimal.Parse(Koszty[a, 1]);
                        Zwrot += kwota;
                    }
                }               
            }

            return Zwrot;
        }

        public static decimal Oblicz_Dochody_miesiaca(string miesiac)
        {
            decimal Przychod = Oblicz_Przychody_miesiaca(miesiac);
            decimal Koszty = Oblicz_Koszty_miesiaca(miesiac);
            decimal Dochody = (Przychod - Koszty);
            if(Dochody < 0)
            {
                Dochody = 0;
            }
            return Dochody;
        }

        public static decimal Oblicz_Zaliczke_PIT_miesiaca(string miesiac)
        {
            decimal Dochodek = Oblicz_Dochody_miesiaca(miesiac);
            return decimal.Round((Dochodek * Stawka_PIT), 2, MidpointRounding.AwayFromZero);
        }

        public void Czy_pliki_istnieja(string rok)
        {
            string[] Do_zapisu = new string[0];
            string pth_koszty = P_Koszty + rok + ".txt";
            string pth_przychody = P_Przychody + rok + ".txt";
            string pth_sprzedaz = P_Sprzedaz + rok + ".txt";
            if (!File.Exists(sciezka_startowa + pth_koszty))
            {
                Directory.CreateDirectory(sciezka_startowa + F_Koszty);
                File.WriteAllLines(sciezka_startowa + pth_koszty, Do_zapisu);
            }
            if (!File.Exists(sciezka_startowa + pth_przychody))
            {
                Directory.CreateDirectory(sciezka_startowa + F_Przychody);
                File.WriteAllLines(sciezka_startowa + pth_przychody, Do_zapisu);
            }
            if (!File.Exists(sciezka_startowa + pth_sprzedaz))
            {
                Directory.CreateDirectory(sciezka_startowa + F_Sprzedaz);
                File.WriteAllLines(sciezka_startowa + pth_sprzedaz, Do_zapisu);
            }
        }

        public static decimal Podaj_Minimalne_wynagrodzenie(string rok)
        {
            decimal limit = 0;

            for (int a = 0; a < Statystyka.GetLength(0); a++)
            {
                if (Statystyka[a, 0] == rok)
                {
                    limit = decimal.Parse(Statystyka[a, 2]);
                    break;
                }
            }

            return limit;
        }

        public static decimal Podaj_stawke_PIT_roku(string rok)
        {
            decimal limit = 0;

            for (int a = 0; a < Statystyka.GetLength(0); a++)
            {
                if (Statystyka[a, 0] == rok)
                {
                    limit = decimal.Parse(Statystyka[a, 1]);
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

            for (int a = 0; a < 12; a++)
            {
                Dochodek += Oblicz_Zaliczke_PIT_miesiaca((a + 1).ToString("00"));
            }

            return Dochodek;
        }

        public static decimal Oblicz_netto_roku()
        {
            decimal doch = Oblicz_Dochody_roku();
            decimal PITy = Oblicz_Zaliczke_PIT_roku();
            return (doch - PITy);
        }

        public static string Podaj_date_sprzedazy(string nr_kol)
        {
            string data = "0";
            for (int a = 0; a < Przychody.GetLength(0); a++)
            {
                if (Przychody[a, 1] == nr_kol)
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
                if (Sprzedaz[a, 0] == nr_kol)
                {
                    cena = decimal.Parse(Sprzedaz[a, 2]);
                    ilosc = decimal.Parse(Sprzedaz[a, 3]);
                    kwota += cena * ilosc;
                }
            }


            return kwota;
        }

        public void Zapisz()
        {
            Wczytaj_DGV();
            Zapisz_pliki();
            Wczytaj_rok(ROK);
        }

        public void Wczytaj_DGV()
        {
            int przychody_dlugosc = dataGrid_Przychody.Rows.GetRowCount(0) - 1;
            int sprzedaz_dlugosc = dataGridView_Sprzedaz.Rows.GetRowCount(0) - 1;
            int koszty_dlugosc = dataGrid_Koszty.Rows.GetRowCount(0) - 1;
            int przychody_szerokosc = 6;
            int sprzedaz_szerokosc = 4;
            int koszty_szerokosc = 3;
            string[,] Nowe_przychody = new string[przychody_dlugosc, przychody_szerokosc];
            string[,] Nowe_sprzedaz = new string[sprzedaz_dlugosc, sprzedaz_szerokosc];
            string[,] Nowe_koszty = new string[koszty_dlugosc, koszty_szerokosc];
            DateTime data1;
            string data, data2, nr_kol, Nabywca, Adres, Kod_i_miasto, Forma_platnosci, nazwa, cena, ilosc;

            for (int a = 0; a < przychody_dlugosc; a ++)
            {
                data2 = dataGrid_Przychody.Rows[a].Cells[0].Value.ToString();
                data1 = DateTime.Parse(data2);
                data = data1.Day.ToString("00") + "." + data1.Month.ToString("00") + "." + data1.Year.ToString("0000");
                nr_kol = dataGrid_Przychody.Rows[a].Cells[1].Value.ToString();
                Nabywca = dataGrid_Przychody.Rows[a].Cells[2].Value.ToString().Replace(';', ',');
                Adres = dataGrid_Przychody.Rows[a].Cells[3].Value.ToString();
                Kod_i_miasto = dataGrid_Przychody.Rows[a].Cells[4].Value.ToString();
                Forma_platnosci = dataGrid_Przychody.Rows[a].Cells[5].Value.ToString();

                Nowe_przychody[a, 0] = data;
                Nowe_przychody[a, 1] = nr_kol;
                Nowe_przychody[a, 2] = Nabywca;
                Nowe_przychody[a, 3] = Adres;
                Nowe_przychody[a, 4] = Kod_i_miasto;
                Nowe_przychody[a, 5] = Forma_platnosci;
            } // Przychody

            for (int a = 0; a < sprzedaz_dlugosc; a++)
            {
                nr_kol = dataGridView_Sprzedaz.Rows[a].Cells[0].Value.ToString();
                nazwa = dataGridView_Sprzedaz.Rows[a].Cells[1].Value.ToString();
                cena = dataGridView_Sprzedaz.Rows[a].Cells[2].Value.ToString();
                ilosc = dataGridView_Sprzedaz.Rows[a].Cells[3].Value.ToString();

                Nowe_sprzedaz[a, 0] = nr_kol;
                Nowe_sprzedaz[a, 1] = nazwa;
                Nowe_sprzedaz[a, 2] = cena.Replace('.', ',');
                Nowe_sprzedaz[a, 3] = ilosc.Replace('.', ',');
            } // Sprzedaż

            for (int a = 0; a < koszty_dlugosc; a++)
            {
                nr_kol = dataGrid_Koszty.Rows[a].Cells[0].Value.ToString();
                cena = dataGrid_Koszty.Rows[a].Cells[1].Value.ToString();
                nazwa = dataGrid_Koszty.Rows[a].Cells[2].Value.ToString();

                Nowe_koszty[a, 0] = nr_kol;
                Nowe_koszty[a, 1] = cena.Replace('.',',');
                Nowe_koszty[a, 2] = nazwa;
            } // Koszty

            Przychody = Nowe_przychody;
            Sprzedaz = Nowe_sprzedaz;
            Koszty = Nowe_koszty;
        }

        public void Zapisz_pliki()
        {
            string[] zapis_Przychody = Obsluga_tekstu.Scal_tablice_2d_do_1d(Przychody,";");
            string[] zapis_Koszty = Obsluga_tekstu.Scal_tablice_2d_do_1d(Koszty, ";");
            string[] zapis_Sprzedaz = Obsluga_tekstu.Scal_tablice_2d_do_1d(Sprzedaz, ";");


            File.WriteAllLines(sciezka_startowa + P_Przychody + ROK + ".txt", zapis_Przychody);
            File.WriteAllLines(sciezka_startowa + P_Koszty + ROK + ".txt", zapis_Koszty);
            File.WriteAllLines(sciezka_startowa + P_Sprzedaz + ROK + ".txt", zapis_Sprzedaz);
        }

        private void ToolStripComboBox_rok_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ROK != toolStripComboBox_rok.Text)
            {
                Wczytaj_rok(toolStripComboBox_rok.Text);
            }
        }

        private void ZapiszToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Zapisz();
        }

        private void DataGrid_Przychody_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            int Wiersz = e.RowIndex;
            int liczba_wierszy = dataGrid_Przychody.Rows.GetRowCount(0);

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && Wiersz >= 0 && Wiersz < (liczba_wierszy-1))
            {
                int kolumna = e.ColumnIndex;
                if(kolumna == 7)
                {
                    Generowanie_rachunku(dataGrid_Przychody.Rows[Wiersz].Cells[1].Value.ToString());
                } // Rachunek
                else
                {
                    Generowanie_faktury(dataGrid_Przychody.Rows[Wiersz].Cells[1].Value.ToString());
                } // Faktura
            }
        }

        public void Generowanie_rachunku(string nr_kol)
        {
            string sciezka = Podaj_folder_rachunkow("0") + "Rachunek - " + nr_kol + ".pdf";
            string nazwa_klienta = Podaj_nazwe_klienta(nr_kol);
            string firma = Podaj_moja_firme();
            string sprzedawca = "";
            Color headerBg = new DeviceRgb(87, 235, 203);
            string nabywca = "";
            decimal cena, ilosc, wartosc;

            sprzedawca += "Sprzedawca:\r";
            if (firma != "")
            {
                sprzedawca += firma + "\r";
            }
            sprzedawca += Podaj_moje_imie_i_nazwisko() + "\r";
            sprzedawca += Podaj_moj_adres() + "\r";
            sprzedawca += Podaj_moj_kod_i_miasto() + "\r";

            nabywca += "Nabywca:\r";
            nabywca += Podaj_Imie_i_Nazwisko_klienta(nazwa_klienta) + "\r";
            nabywca += Podaj_adres_klienta(nazwa_klienta) + "\r";
            nabywca += Podaj_kod_i_miasto_klienta(nazwa_klienta) + "\r";

            PdfWriter Writer = new PdfWriter(sciezka);
            PdfDocument pdf = new PdfDocument(Writer);
            Document document = new Document(pdf);
            PdfFont Czcionka = PdfFontFactory.CreateFont(FONT, "Cp1250", true);

            // Dane sprzedawcy i nabywcy
            Table Adresy_Tabela = new Table(2, false).UseAllAvailableWidth();
            Cell cell1 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(sprzedawca));
            Cell cell2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT)
                .Add(new Paragraph(nabywca));
            Adresy_Tabela.AddCell(cell1);
            Adresy_Tabela.AddCell(cell2);
            document.Add(Adresy_Tabela);

            // Napis "Rachunek"
            Paragraph header = new Paragraph("Rachunek " + nr_kol)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(Czcionka)
                .SetFontSize(30);
            document.Add(header);

            // Napis "Data sprzedaży"
            Paragraph subheader = new Paragraph("Data sprzedaży - " + Podaj_date_sprzedazy(nr_kol))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(Czcionka)
                .SetFontSize(16);
            document.Add(subheader);

            // Tabela sprzedaży
            Table Sprzedaz_Tabela = new Table(4, false).UseAllAvailableWidth();
            Cell Sp_Cell11 = new Cell(1, 1)
                .SetBackgroundColor(headerBg)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Nazwa towaru lub usługi"));
            Cell Sp_Cell12 = new Cell(1, 1)
                .SetBackgroundColor(headerBg)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Cena [Zł]"));
            Cell Sp_Cell13 = new Cell(1, 1)
                .SetBackgroundColor(headerBg)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Ilość [Szt.]"));
            Cell Sp_Cell14 = new Cell(1, 1)
                .SetBackgroundColor(headerBg)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Wartość [Zł]"));
            Sprzedaz_Tabela.AddCell(Sp_Cell11);
            Sprzedaz_Tabela.AddCell(Sp_Cell12);
            Sprzedaz_Tabela.AddCell(Sp_Cell13);
            Sprzedaz_Tabela.AddCell(Sp_Cell14);

            for (int a = 0; a < Sprzedaz.GetLength(0); a++)
            {
                if (Sprzedaz[a, 0] == nr_kol)
                {
                    cena = decimal.Parse(Sprzedaz[a, 2]);
                    ilosc = decimal.Parse(Sprzedaz[a, 3]);
                    wartosc = cena * ilosc;

                    Sprzedaz_Tabela.AddCell(new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(Czcionka)
                        .SetFontSize(10)
                        .Add(new Paragraph(Sprzedaz[a, 1])) // nazwa
                    );
                    Sprzedaz_Tabela.AddCell(new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(Czcionka)
                        .SetFontSize(10)
                        .Add(new Paragraph(cena.ToString("N2"))) // cena
                    );
                    Sprzedaz_Tabela.AddCell(new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(Czcionka)
                        .SetFontSize(10)
                        .Add(new Paragraph(ilosc.ToString("N2"))) // ilość
                    );
                    Sprzedaz_Tabela.AddCell(new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(Czcionka)
                        .SetFontSize(10)
                        .Add(new Paragraph(wartosc.ToString("N2"))) // wartość
                    );
                }
            }
            document.Add(Sprzedaz_Tabela);

            // Razem
            Paragraph header_razem = new Paragraph("Razem: " + Podaj_przychod_faktury(nr_kol).ToString("C2"))
                .SetFont(Czcionka)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(header_razem);
            
            // Forma płatności
            Paragraph header_platnosc = new Paragraph("Forma płatności: " + Podaj_forme_platnosci_faktury(nr_kol))
                .SetFont(Czcionka)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(header_platnosc);

            // Stopka z wersją
            Paragraph header_wersja = new Paragraph("Wygenerowano automatycznie przez Księgowość " + Application.ProductVersion)
                .SetFont(Czcionka)
                .SetFontSize(8);
            for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
            {
                Rectangle pageSize = pdf.GetPage(i).GetPageSize();
                float x = pageSize.GetWidth() / 16;
                float y = pageSize.GetBottom() + 30;
                document.ShowTextAligned(header_wersja, x, y, i, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);
            }

            document.Close();
        }

        public void Generowanie_faktury(string nr_kol)
        {
            string sciezka = Podaj_folder_rachunkow("0") + "Faktura VAT - " + nr_kol + ".pdf";
            string nazwa_klienta = Podaj_nazwe_klienta(nr_kol);
            string firma = Podaj_moja_firme();
            string sprzedawca = "";
            Color headerBg = new DeviceRgb(87, 235, 203);
            string nabywca = "";
            decimal cena, ilosc, wartosc;

            sprzedawca += "Sprzedawca:\r";
            if (firma != "")
            {
                sprzedawca += firma + "\r";
            }
            sprzedawca += Podaj_moje_imie_i_nazwisko() + "\r";
            sprzedawca += Podaj_moj_adres() + "\r";
            sprzedawca += Podaj_moj_kod_i_miasto() + "\r";

            nabywca += "Nabywca:\r";
            nabywca += Podaj_Imie_i_Nazwisko_klienta(nazwa_klienta) + "\r";
            nabywca += Podaj_adres_klienta(nazwa_klienta) + "\r";
            nabywca += Podaj_kod_i_miasto_klienta(nazwa_klienta) + "\r";

            PdfWriter Writer = new PdfWriter(sciezka);
            PdfDocument pdf = new PdfDocument(Writer);
            Document document = new Document(pdf);
            PdfFont Czcionka = PdfFontFactory.CreateFont(FONT, "Cp1250", true);

            // Dane sprzedawcy i nabywcy
            Table Adresy_Tabela = new Table(2, false).UseAllAvailableWidth();
            Cell cell1 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(sprzedawca));
            Cell cell2 = new Cell(1, 1)
                .SetBorder(Border.NO_BORDER)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT)
                .Add(new Paragraph(nabywca));
            Adresy_Tabela.AddCell(cell1);
            Adresy_Tabela.AddCell(cell2);
            document.Add(Adresy_Tabela);

            // Napis "Rachunek"
            Paragraph header = new Paragraph("Faktura VAT nr " + nr_kol)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(Czcionka)
                .SetFontSize(30);
            document.Add(header);

            // Napis "Data sprzedaży"
            Paragraph subheader = new Paragraph("Data wystawienia - " + Podaj_date_sprzedazy(nr_kol))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(Czcionka)
                .SetFontSize(16);
            document.Add(subheader);

            // Tabela sprzedaży
            Table Sprzedaz_Tabela = new Table(7, false).UseAllAvailableWidth();
            Cell Sp_Cell11 = new Cell(1, 1)
                .SetBackgroundColor(headerBg)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Nazwa towaru lub usługi"));
            Cell Sp_Cell12 = new Cell(1, 1)
                .SetBackgroundColor(headerBg)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Cena netto [Zł]"));
            Cell Sp_Cell13 = new Cell(1, 1)
                .SetBackgroundColor(headerBg)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Ilość [Szt.]"));
            Cell Sp_Cell14 = new Cell(1, 1)
                .SetBackgroundColor(headerBg)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Wartość netto [Zł]"));
            Cell Sp_Cell15 = new Cell(1, 1)
                .SetBackgroundColor(headerBg)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("VAT [%]"));
            Cell Sp_Cell16 = new Cell(1, 1)
                .SetBackgroundColor(headerBg)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Wartość VAT [Zł]"));
            Cell Sp_Cell17 = new Cell(1, 1)
                .SetBackgroundColor(headerBg)
                .SetFont(Czcionka)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Wartość brutto [Zł]"));
            Sprzedaz_Tabela.AddCell(Sp_Cell11);
            Sprzedaz_Tabela.AddCell(Sp_Cell12);
            Sprzedaz_Tabela.AddCell(Sp_Cell13);
            Sprzedaz_Tabela.AddCell(Sp_Cell14);
            Sprzedaz_Tabela.AddCell(Sp_Cell15);
            Sprzedaz_Tabela.AddCell(Sp_Cell16);
            Sprzedaz_Tabela.AddCell(Sp_Cell17);

            for (int a = 0; a < Sprzedaz.GetLength(0); a++)
            {
                if (Sprzedaz[a, 0] == nr_kol)
                {
                    cena = decimal.Parse(Sprzedaz[a, 2]);
                    ilosc = decimal.Parse(Sprzedaz[a, 3]);
                    wartosc = cena * ilosc;

                    Sprzedaz_Tabela.AddCell(new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(Czcionka)
                        .SetFontSize(10)
                        .Add(new Paragraph(Sprzedaz[a, 1])) // nazwa
                    );
                    Sprzedaz_Tabela.AddCell(new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(Czcionka)
                        .SetFontSize(10)
                        .Add(new Paragraph(cena.ToString("N2"))) // cena netto
                    );
                    Sprzedaz_Tabela.AddCell(new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(Czcionka)
                        .SetFontSize(10)
                        .Add(new Paragraph(ilosc.ToString("N2"))) // ilość
                    );
                    Sprzedaz_Tabela.AddCell(new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(Czcionka)
                        .SetFontSize(10)
                        .Add(new Paragraph(wartosc.ToString("N2"))) // wartość netto
                    );
                    Sprzedaz_Tabela.AddCell(new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(Czcionka)
                        .SetFontSize(10)
                        .Add(new Paragraph("ZW")) // Stawka VAT
                    );
                    Sprzedaz_Tabela.AddCell(new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(Czcionka)
                        .SetFontSize(10)
                        .Add(new Paragraph("0,00")) // Wartość VAT
                    );
                    Sprzedaz_Tabela.AddCell(new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(Czcionka)
                        .SetFontSize(10)
                        .Add(new Paragraph(wartosc.ToString("N2"))) // Wartość brutto
                    );
                }
            }
            document.Add(Sprzedaz_Tabela);

            // Wartość netto
            Paragraph header_razem_netto = new Paragraph("Wartość netto: " + Podaj_przychod_faktury(nr_kol).ToString("C2"))
                .SetFont(Czcionka)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(header_razem_netto);

            // Wartość VAT
            Paragraph header_wartosc_vat = new Paragraph("Wartość VAT: 0,00 zł")
                .SetFont(Czcionka)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(header_wartosc_vat);

            // Wartość brutto
            Paragraph header_razem_brutto = new Paragraph("Wartość brutto: " + Podaj_przychod_faktury(nr_kol).ToString("C2"))
                .SetFont(Czcionka)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(header_razem_brutto);

            // Entery
            Paragraph header_entery = new Paragraph("\r\r")
                .SetFont(Czcionka)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(header_entery);

            // Linia
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            // Forma płatności
            Paragraph header_uwagi = new Paragraph("Uwagi: Powód zwolnienia z VAT: na podstawie art. 113 ust. 1 ustawy o VAT")
                .SetFont(Czcionka)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(header_uwagi);

            // Linia
            document.Add(ls);

            // Do zapłaty
            Paragraph header_do_zaplaty = new Paragraph("Do zapłaty: " + Podaj_przychod_faktury(nr_kol).ToString("C2"))
                .SetFont(Czcionka)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(header_do_zaplaty);

            // Forma płatności
            Paragraph header_platnosc = new Paragraph("Forma płatności: " + Podaj_forme_platnosci_faktury(nr_kol))
                .SetFont(Czcionka)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(header_platnosc);

            // Stopka z wersją
            Paragraph header_wersja = new Paragraph("Wygenerowano automatycznie przez Księgowość " + Application.ProductVersion)
                .SetFont(Czcionka)
                .SetFontSize(8);
            for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
            {
                Rectangle pageSize = pdf.GetPage(i).GetPageSize();
                float x = pageSize.GetWidth() / 16;
                float y = pageSize.GetBottom() + 30;
                document.ShowTextAligned(header_wersja, x, y, i, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);
            }

            document.Close();
        }

        public static string Podaj_moja_firme()
        {
            string zwrot = "";
            for (int a = 0; a < Moje_dane.GetLength(0); a++)
            {
                if (Moje_dane[a, 0] == "Firma")
                {
                    zwrot = Moje_dane[a, 1];
                    break;
                }
            }
            return zwrot;
        }

        public static string Podaj_moje_imie_i_nazwisko()
        {
            string zwrot = "";
            for(int a = 0; a < Moje_dane.GetLength(0); a++)
            {
                if(Moje_dane[a,0] == "Imię i nazwisko")
                {
                    zwrot = Moje_dane[a, 1];
                    break;
                }
            }
            return zwrot;
        }

        public static string Podaj_moj_adres()
        {
            string zwrot = "";
            for (int a = 0; a < Moje_dane.GetLength(0); a++)
            {
                if (Moje_dane[a, 0] == "Adres")
                {
                    zwrot = Moje_dane[a, 1];
                    break;
                }
            }
            return zwrot;
        }

        public static string Podaj_moj_kod_i_miasto()
        {
            string zwrot = "";
            for (int a = 0; a < Moje_dane.GetLength(0); a++)
            {
                if (Moje_dane[a, 0] == "Kod i miasto")
                {
                    zwrot = Moje_dane[a, 1];
                    break;
                }
            }
            return zwrot;
        }

        public static bool Podaj_aktualizacje_na_start()
        {
            string zwrot = "";
            bool zwrot2;
            for (int a = 0; a < Moje_dane.GetLength(0); a++)
            {
                if (Moje_dane[a, 0] == "Aktualizacja")
                {
                    zwrot = Moje_dane[a, 1];
                    break;
                }
            }

            if(zwrot == "0")
            {
                zwrot2 = false;
            }
            else
            {
                zwrot2 = true;
            }

            return zwrot2;
        }

        public static string Podaj_folder_rachunkow(string tryb)
        {
            string zwrot = "";
            for (int a = 0; a < Moje_dane.GetLength(0); a++)
            {
                if (Moje_dane[a, 0] == "Folder rachunków")
                {
                    zwrot = Moje_dane[a, 1];
                    break;
                }
            }

            if(zwrot == "" && tryb == "0")
            {
                zwrot = sciezka_startowa + "\\";
            }
            return zwrot;
        }

        public static string Podaj_nazwe_klienta(string nr_kol)
        {
            string zwrot = "";
            for (int a = 0; a < Przychody.GetLength(0); a++)
            {
                if (Przychody[a, 1] == nr_kol)
                {
                    zwrot = Przychody[a, 2];
                    break;
                }
            }
            return zwrot;
        }

        public static string Podaj_Imie_i_Nazwisko_klienta(string nazwa_klienta)
        {
            string zwrot = "";
            for (int a = 0; a < Lista_klientów.GetLength(0); a++)
            {
                if (Lista_klientów[a, 0] == nazwa_klienta)
                {
                    zwrot = Lista_klientów[a, 1];
                    break;
                }
            }
            return zwrot;
        }

        public static string Podaj_adres_klienta(string nazwa_klienta)
        {
            string zwrot = "";
            for (int a = 0; a < Lista_klientów.GetLength(0); a++)
            {
                if (Lista_klientów[a, 0] == nazwa_klienta)
                {
                    zwrot = Lista_klientów[a, 2];
                    break;
                }
            }
            return zwrot;
        }

        public static string Podaj_kod_i_miasto_klienta(string nazwa_klienta)
        {
            string zwrot = "";
            for (int a = 0; a < Lista_klientów.GetLength(0); a++)
            {
                if (Lista_klientów[a, 0] == nazwa_klienta)
                {
                    zwrot = Lista_klientów[a, 3];
                    break;
                }
            }
            return zwrot;
        }

        public static string Podaj_email_klienta(string nazwa_klienta)
        {
            string zwrot = "";
            for (int a = 0; a < Lista_klientów.GetLength(0); a++)
            {
                if (Lista_klientów[a, 0] == nazwa_klienta)
                {
                    zwrot = Lista_klientów[a, 4];
                    break;
                }
            }
            return zwrot;
        }

        public static string Podaj_telefon_klienta(string nazwa_klienta)
        {
            string zwrot = "";
            for (int a = 0; a < Lista_klientów.GetLength(0); a++)
            {
                if (Lista_klientów[a, 0] == nazwa_klienta)
                {
                    zwrot = Lista_klientów[a, 5];
                    break;
                }
            }
            return zwrot;
        }

        public static string Podaj_forme_platnosci_faktury(string nr_kol)
        {
            string zwrot = "";
            for (int a = 0; a < Przychody.GetLength(0); a++)
            {
                if (Przychody[a, 1] == nr_kol)
                {
                    zwrot = Przychody[a, 3];
                    break;
                }
            }
            return zwrot;
        }

        private void AktualizacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            O_programie O_programie_ = new O_programie();
            O_programie_.Show();
        }

        public static string Sprawdz_wersje()
        {          
            string Adres_wersji = "https://raw.githubusercontent.com/Xanaa/Serwer_wersji/master/W_Ksiegowosc";
            WebClient webClient = new WebClient();
            string moja = Application.ProductVersion;
            string test = webClient.DownloadString(Adres_wersji).Remove(7);
            if(moja != test)
            {
                string message = "Obecnie zainstalowana wersja to " + moja + "\nDostępna jest nowsza wersja " + test + " !";
                string caption = "Aktualizacja";
                MessageBox.Show(message, caption,MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            return test;
        }

        private void DodajPrzychodyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dodaj_przychody Dodaj_przychody_ = new Dodaj_przychody();
            Dodaj_przychody_.Show();
        }

        public static void Dodaj_sprzedaz()
        {
            if(Przychody.GetLength(0) != 0)
            {
                Przychody = Obsluga_tekstu.Polacz_tablice_2d(Przychody, Dodaj_przychody.K_przychody, ";");
            }
            else
            {
                Przychody = Dodaj_przychody.K_przychody;
            }

            if (Sprzedaz.GetLength(0) != 0)
            {
                Sprzedaz = Obsluga_tekstu.Polacz_tablice_2d(Sprzedaz, Dodaj_przychody.K_sprzedaz, ";");
            }
            else
            {
                Sprzedaz = Dodaj_przychody.K_sprzedaz;
            }

            if (Koszty.GetLength(0) != 0)
            {
                Koszty = Obsluga_tekstu.Polacz_tablice_2d(Koszty, Dodaj_przychody.K_koszty, ";");
            }
            else
            {
                Koszty = Dodaj_przychody.K_koszty;
            }

            if (Przychody != null)
            {
                Glowne_inst.Wczytaj_Przychody();
            }
            if (Sprzedaz != null)
            {
                Glowne_inst.Wczytaj_Sprzedaz();
            }
            if (Koszty != null)
            {
                Glowne_inst.Wczytaj_Koszty();
                Glowne_inst.Wczytaj_Info_PIT();
            }

            Zapisane = false;
        }

        public static string Podaj_nowy_numer_kolejny(string miesiac)
        {
            string zwrot;
            string licznik = "";
            string t1, mies, dzielnik;
            string[] sort;
            for(int a = 0; a < Przychody.GetLength(0); a++)
            {
                t1 = Przychody[a, 1];
                mies = t1.Remove(2);
                if(mies == miesiac)
                {
                    dzielnik = t1.Remove(0, 7);
                    licznik += dzielnik + ",";
                }
            }
            if(licznik.Length > 1)
            {
                licznik = licznik.Remove(licznik.Length - 1);
                sort = licznik.Split(',');
                zwrot = (sort.GetLength(0) + 1).ToString();
            }
            else
            {
                zwrot = "1";
            }
            return zwrot;
        }

        private void UstawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ustawienia Ustawienia_ = new Ustawienia();
            Ustawienia_.Show();
        }

        private void OknoGlowne_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Zapisane == false)
            {
                string message = "Czy zapisać zmiany?";
                string title = "Zapisywanie";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    Zapisz();
                }
            }
        }

        private void DataGrid_Przychody_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Zapisane = false;
        }

        private void DataGridView_Sprzedaz_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Zapisane = false;
        }

        private void DataGrid_Koszty_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Zapisane = false;
        }

        private void ListaKlientówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lista_Klientow_okno Lista_Klientow_okno_ = new Lista_Klientow_okno();
            Lista_Klientow_okno_.Show();
        }
    }   
}
