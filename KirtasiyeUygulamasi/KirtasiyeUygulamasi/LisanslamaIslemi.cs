using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;
using VTIveDI;
using System.IO;

namespace KirtasiyeUygulamasi
{
    public partial class LisanslamaIslemi : Form
    {
        public LisanslamaIslemi()
        {
            InitializeComponent();
        }

        private void anaformKapaButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Bunifu.Framework.UI.Drag drag = new Bunifu.Framework.UI.Drag();
        private void bunifuPanel2_MouseDown(object sender, MouseEventArgs e)
        {
            drag.Grab(this);
        }

        private void bunifuPanel2_MouseMove(object sender, MouseEventArgs e)
        {
            drag.MoveObject();
        }

        private void bunifuPanel2_MouseUp(object sender, MouseEventArgs e)
        {
            drag.Release();
        }
        
        private void LisanslamaIslemi_Load(object sender, EventArgs e)
        {
            yazilimKoduTextBox.Text = LI.LisansIslemleri.YazilimKodu();
        }

        private void LisansThinButton_Click(object sender, EventArgs e)
        {
            string lisansKodu = LI.LisansIslemleri.LisansKodu(yazilimKoduTextBox.Text);

            if (lisansKoduTextBox.Text == lisansKodu)
            {
                StreamWriter sw = new StreamWriter("Lisansla.l",false,Encoding.Default);
                sw.WriteLine(lisansKodu);
                sw.Close();
                MessageBox.Show("Lisanslama İşlemi Başarılı. \n Uygulamayı Yeniden Açınız.");
                Application.Exit();

            }
            else
            {
                MessageBox.Show("Lisans Kodunuz Geçersizdir.");
            }
        }
    }
}
