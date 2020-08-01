using System;
using System.IO;
using System.Windows.Forms;
using Pomocnik;
using GemBox.Document;
using GemBox.Document.Tables;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Globalization;
using System.Net;

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

        public static string[,] Koszty;
        public static string[,] Przychody;
        public static string[,] Sprzedaz;
        public static string[,] Statystyka = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, P_Statystyka, 3, "#", ";");
        public static string[,] Moje_dane = Obsluga_plikow.Wczytaj_plik_tekstowy(sciezka_startowa, P_Moje_dane, 2, "#", ";");

        public static decimal Stawka_PIT = decimal.Parse("0,17");
        public static string ROK = "1995";

        public OknoGlowne()
        {
            InitializeComponent();

            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("pl-PL");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            ROK = DateTime.Now.Year.ToString(); // Obecny rok
            Wczytaj_rok(ROK);
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            // Stop reading/writing a spreadsheet when the free limit is reached.
            ComponentInfo.FreeLimitReached += (sender, e) => e.FreeLimitReachedAction = FreeLimitReachedAction.ContinueAsTrial;

            Sprawdz_wersje();
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
                if (Statystyka[a, 0] == rok && Statystyka[a, 1] == "Minimalne wynagrodzenie")
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
                    // ToDo
                } // Faktura
            }
        }

        public void Generowanie_rachunku(string nr_kol)
        {
            string nazwa = "Rachunek-" + nr_kol;
            var document = new DocumentModel();

            // Style's font size is 24pt.
            var largeFont = new CharacterStyle("Large Font") { CharacterFormat = { Size = 24 } };
            document.Styles.Add(largeFont);
            var section = new Section(document);
            document.Sections.Add(section);

            // Część 1 - Info o stronach////////////////////////////////////          
            var table = new Table(document);
            table.TableFormat.RightToLeft = true;
            table.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
            table.TableFormat.Borders.SetBorders(MultipleBorderTypes.All, GemBox.Document.BorderStyle.Single, new GemBox.Document.Color(255, 255, 255), 1);
            var row = new TableRow(document);
            table.Rows.Add(row);

            string firma = Podaj_moja_firme();
            string sprzedawca = "Sprzedawca:\n";
            if (firma != "")
            {
                sprzedawca += firma + "\n";
            }
            sprzedawca += Podaj_moje_imie_i_nazwisko() + "\n";
            sprzedawca += Podaj_moj_adres() + "\n";
            sprzedawca += Podaj_moj_kod_i_miasto() + "\n";

            string nabywca = "Nabywca:\n";
            nabywca += Podaj_nazwe_klienta(nr_kol) + "\n";
            nabywca += Podaj_adres_klienta(nr_kol) + "\n";
            nabywca += Podaj_kod_i_miasto_klienta(nr_kol) + "\n";

            var firstCellPara = new Paragraph(document, nabywca);
            firstCellPara.ParagraphFormat.RightToLeft = true;
            row.Cells.Add(new TableCell(document, firstCellPara));

            var secondCellPara = new Paragraph(document, sprzedawca);
            row.Cells.Add(new TableCell(document, secondCellPara));

            section.Blocks.Add(table);

            // Część 2 - opis dokumentu////////////////////////////////////
            var paragraph2 = new Paragraph(document,
            new Run(document, "Rachunek " + nr_kol)
            {
                CharacterFormat = { Style = largeFont }
            },
            new SpecialCharacter(document, SpecialCharacterType.LineBreak),
            new Run(document, "Data sprzedaży - " + Podaj_date_sprzedazy(nr_kol))
            {
                CharacterFormat = { Style = largeFont, Size = 12 }
            });
            paragraph2.ParagraphFormat.Alignment = GemBox.Document.HorizontalAlignment.Center;
            section.Blocks.Add(paragraph2);

            // Część 3 - Sprzedaż////////////////////////////////////
            var table2 = new Table(document);
            table2.TableFormat.PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage);
            var row2 = new TableRow(document);
            table2.Rows.Add(row2);

            // Create and add a custom table style.
            var customTableStyle = new TableStyle("GemBox Table");
            document.Styles.Add(customTableStyle);
            var firstRowFormat = customTableStyle.ConditionalFormats[TableStyleFormatType.FirstRow];
            firstRowFormat.CellFormat.BackgroundColor = new GemBox.Document.Color(133, 230, 226);
            customTableStyle.TableFormat.Borders.SetBorders(MultipleBorderTypes.All, GemBox.Document.BorderStyle.Single, new GemBox.Document.Color(0, 0, 0), 1);

            var kol1 = new Paragraph(document, "Nazwa towaru lub usługi");    
            row2.Cells.Add(new TableCell(document, kol1));

            var kol2 = new Paragraph(document, "Cena [Zł]");
            row2.Cells.Add(new TableCell(document, kol2));

            var kol3 = new Paragraph(document, "Ilość [Szt.]");
            row2.Cells.Add(new TableCell(document, kol3));

            var kol4 = new Paragraph(document, "Wartość [Zł]");
            row2.Cells.Add(new TableCell(document, kol4));

            for(int a = 0; a < Sprzedaz.GetLength(0); a++)
            {
                if(Sprzedaz[a,0] == nr_kol)
                {
                    var rowek = new TableRow(document);
                    table2.Rows.Add(rowek);

                    var kolumna1 = new Paragraph(document, Sprzedaz[a, 1]);
                    rowek.Cells.Add(new TableCell(document, kolumna1));

                    var kolumna2 = new Paragraph(document, Sprzedaz[a, 2]);
                    rowek.Cells.Add(new TableCell(document, kolumna2));

                    var kolumna3 = new Paragraph(document, Sprzedaz[a, 3]);
                    rowek.Cells.Add(new TableCell(document, kolumna3));

                    decimal sum = decimal.Parse(Sprzedaz[a, 2]) * decimal.Parse(Sprzedaz[a, 3]);

                    var kolumna4 = new Paragraph(document, sum.ToString());
                    rowek.Cells.Add(new TableCell(document, kolumna4));
                }                
            }

            section.Blocks.Add(table2);
            table2.TableFormat.Style = customTableStyle;

            string ostatnie_linie = "\nRAZEM: " + Podaj_przychod_faktury(nr_kol).ToString("C2") + "\n";
            ostatnie_linie += "Forma płatności: " + Podaj_forme_platnosci_faktury(nr_kol);
            section.Blocks.Add(new Paragraph(document, ostatnie_linie));

            section.HeadersFooters.Add(
               new HeaderFooter(document, HeaderFooterType.FooterDefault,
               new Paragraph(document, "Wygenerowano automatycznie przez Księgowość " + Application.ProductVersion)));

            document.Save(Podaj_folder_rachunkow() + nazwa + ".jpg");
            Usun_smieci_z_rachunku(nr_kol);
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

        public static string Podaj_folder_rachunkow()
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

            if(zwrot == "")
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

        public static string Podaj_adres_klienta(string nr_kol)
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

        public static string Podaj_kod_i_miasto_klienta(string nr_kol)
        {
            string zwrot = "";
            for (int a = 0; a < Przychody.GetLength(0); a++)
            {
                if (Przychody[a, 1] == nr_kol)
                {
                    zwrot = Przychody[a, 4];
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
                    zwrot = Przychody[a, 5];
                    break;
                }
            }
            return zwrot;
        }

        public void Usun_smieci_z_rachunku(string nr_kol)
        {
            string nazwa = "Rachunek-" + nr_kol;
            string cala_sciezka = Podaj_folder_rachunkow() + nazwa + ".jpg";
            string cala_sciezka2 = Podaj_folder_rachunkow() + nazwa + ".jpeg";
            Image image1 = GetCopyImage(cala_sciezka);
            Point p1 = new Point(378, 152);
            Point p2 = new Point(2105, 152);
            Point p3 = new Point(378, 275);
            Point p4 = new Point(2105, 290);
            Point[] punkty = { p1, p2, p4, p3 };
            System.Drawing.Color Caly_kolor = System.Drawing.Color.FromArgb(255, 255, 255);
            Graphics g = Graphics.FromImage(image1);
            Brush Bruszka = new SolidBrush(Caly_kolor);
            g.FillPolygon(Bruszka, punkty);
            image1.Save(cala_sciezka2, ImageFormat.Jpeg);
            image1.Dispose();
            File.Delete(cala_sciezka);
        }

        private Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }

        private void AktualizacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            O_programie O_programie_ = new O_programie();
            O_programie_.Show();
        }

        public static string Sprawdz_wersje()
        {          
            string Adres_wersji = "https://raw.githubusercontent.com/Xanaa/Ksiegowosc_wer/master/Wersja";
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
    }   
}
