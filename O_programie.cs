using System.IO;
using System.Windows.Forms;

namespace Księgowość
{
    public partial class O_programie : Form
    {
        public static string[] Lista_zmian = new string[1];
        private static string moja_wer, nowa_wer;

        public O_programie()
        {
            InitializeComponent();
            moja_wer = Application.ProductVersion;
            TextBox_Moja_wersja.Text = moja_wer;

            try
            {
                Lista_zmian = File.ReadAllLines(OknoGlowne.sciezka_startowa + @"\Lista zmian.txt");
            }
            catch
            {
                Lista_zmian[0] = "Błąd wczytywania pliku 'Lista zmian.txt'";
            }

            RichTextBox_Historia.Clear();
            for(int a = 0; a< Lista_zmian.GetLength(0); a++)
            {
                RichTextBox_Historia.Text += Lista_zmian[a] + "\n";
            }
        }

        private void Button_pobierz_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Xanaa/Ksiegowosc/releases");
        }

        private void Button_Zglos_blad_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Xanaa/Ksiegowosc/issues");
        }

        private void Button_Spr_upd_Click(object sender, System.EventArgs e)
        {
            nowa_wer = OknoGlowne.Sprawdz_wersje();
            TextBox_Nowa_wersja.Text = nowa_wer;

            if(moja_wer != nowa_wer)
            {
                Button_pobierz.Visible = true;
            }
            else
            {
                Button_pobierz.Visible = false;
            }
        }
    }
}
