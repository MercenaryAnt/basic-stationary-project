using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace KirtasiyeUygulamasi
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (File.Exists("Lisansla.l"))
            {
                StreamReader sr = new StreamReader("Lisansla.l", Encoding.Default);
                string lisansKoduOku = sr.ReadLine();
                string lisansKodu = LI.LisansIslemleri.LisansKodu(LI.LisansIslemleri.YazilimKodu());
                if (lisansKodu == lisansKoduOku)
                {
                    Application.Run(new GirisEkrani());
                }
                else
                {
                    MessageBox.Show("Lisansınız uyuşmuyor! Lisans dosyasını silip programı tekrar açınız! Yardım için boraberkuzun@hotmail.com.");
                }
            }
            else
            {
                Application.Run(new LisanslamaIslemi());
            }
        }
    }
}
