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
            textBox_moja_wer.Text = moja_wer;

            try
            {
                Lista_zmian = File.ReadAllLines(OknoGlowne.sciezka_startowa + @"\Lista zmian.txt");
            }
            catch
            {
                Lista_zmian[0] = "Błąd wczytywania pliku 'Lista zmian.txt'";
            }

            richTextBox1.Clear();
            for(int a = 0; a< Lista_zmian.GetLength(0); a++)
            {
                richTextBox1.Text += Lista_zmian[a] + "\n";
            }
        }

        private void Button_pobierz_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("https://drive.google.com/file/d/1pbEIP2rqtr1taN7HfUJNXS5-PPs8GgXC/view?usp=sharing");
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            nowa_wer = OknoGlowne.Sprawdz_wersje();
            textBox_najnowsza_wer.Text = nowa_wer;

            if(moja_wer != nowa_wer)
            {
                button_pobierz.Visible = true;
            }
            else
            {
                button_pobierz.Visible = false;
            }
        }
    }
}
