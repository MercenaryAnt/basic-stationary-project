using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KirtasiyeUygulamasi
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }
        public int yetki = 0;
        private void anasayfaKucultButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void anaformKapaButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menu1ThinButton_Click(object sender, EventArgs e)
        {
            UrunIslemleri urfrm = new UrunIslemleri();
            urfrm.Show();
        }

        private void menu2ThinButton_Click(object sender, EventArgs e)
        {
            SatisIslemleri strfrm = new SatisIslemleri();
            strfrm.Show();
        }

        private void menu3ThinButton_Click(object sender, EventArgs e)
        {
            StokIslemleri stfrm = new StokIslemleri();
            stfrm.Show();
        }

        private void menu4ThinButton_Click(object sender, EventArgs e)
        {
            PersonelIslemleri prfrm = new PersonelIslemleri();
            prfrm.Show();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            menu1ThinButton.BackColor = Color.FromArgb(39, 45, 59);
            menu2ThinButton.BackColor = Color.FromArgb(39, 45, 59);
            menu3ThinButton.BackColor = Color.FromArgb(39, 45, 59);
            menu4ThinButton.BackColor = Color.FromArgb(39, 45, 59);
            menu5ThinButton.BackColor = Color.FromArgb(39, 45, 59);
            menu6ThinButton.BackColor = Color.FromArgb(39, 45, 59);
            menu7ThinButton.BackColor = Color.FromArgb(39, 45, 59);
            menu8ThinButton.BackColor = Color.FromArgb(39, 45, 59);

            if (Ayarlar.Default.tema)
            {
                anasayfaToggleSwitch.Value = true;
            }
            else
            {
                anasayfaToggleSwitch.Value = false;
            }
        }

        private void menu6ThinButton_Click(object sender, EventArgs e)
        {
            ToptanciIslemleri tprfrm = new ToptanciIslemleri();
            tprfrm.Show();
        }

        private void menu5ThinButton_Click(object sender, EventArgs e)
        {
            FiyatGor fytfrm = new FiyatGor();
            fytfrm.Show();
        }

        private void menu7ThinButton_Click(object sender, EventArgs e)
        {
            KategoriIslemleri ktfrm = new KategoriIslemleri();
            ktfrm.Show();
        }

        private void menu8ThinButton_Click(object sender, EventArgs e)
        {
            MusteriIslemleri msfrm = new MusteriIslemleri();
            msfrm.Show();
        }

        private void AnasayfaNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                this.Show();
            }
            else
            {
                this.Hide();
            }
        }

        private void anasayfaToggleSwitch_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            if (anasayfaToggleSwitch.Checked)
            {
                Ayarlar.Default.tema = true;
                Ayarlar.Default.Save();
                this.BackColor = Color.FromArgb(32, 36, 47);
                anasayfaThemeLabel.ForeColor = Color.FromArgb(255, 255, 255);
                anasayfaLabel.BackColor = Color.FromArgb(32, 36, 47);

                menu1UserControl.BackgroundColor = Color.FromArgb(39, 45, 59);
                menu1UserControl.BorderColor = Color.FromArgb(39, 45, 59);
                menu1Label.BackColor = Color.FromArgb(39, 45, 59);
                menu1Shapes.BackColor = Color.FromArgb(39, 45, 59);
                menu1Shapes.FillColor = Color.FromArgb(36, 40, 52);
                menu1Shapes.BorderColor = Color.FromArgb(39, 45, 59);
                menu1ThinButton.BackColor = Color.FromArgb(39, 45, 59);
                menu1ThinButton.ActiveFillColor = Color.FromArgb(39, 40, 50);
                menu1ThinButton.IdleFillColor = Color.FromArgb(48, 53, 78);

                menu2UserControl.BackgroundColor = Color.FromArgb(39, 45, 59);
                menu2UserControl.BorderColor = Color.FromArgb(39, 45, 59);
                menu2Label.BackColor = Color.FromArgb(39, 45, 59);
                menu2Shapes.BackColor = Color.FromArgb(39, 45, 59);
                menu2Shapes.FillColor = Color.FromArgb(36, 40, 52);
                menu2Shapes.BorderColor = Color.FromArgb(39, 45, 59);
                menu2ThinButton.BackColor = Color.FromArgb(39, 45, 59);
                menu2ThinButton.ActiveFillColor = Color.FromArgb(39, 40, 50);
                menu2ThinButton.IdleFillColor = Color.FromArgb(48, 53, 78);

                menu3UserControl.BackgroundColor = Color.FromArgb(39, 45, 59);
                menu3UserControl.BorderColor = Color.FromArgb(39, 45, 59);
                menu3Label.BackColor = Color.FromArgb(39, 45, 59);
                menu3Shapes.BackColor = Color.FromArgb(39, 45, 59);
                menu3Shapes.FillColor = Color.FromArgb(36, 40, 52);
                menu3Shapes.BorderColor = Color.FromArgb(39, 45, 59);
                menu3ThinButton.BackColor = Color.FromArgb(39, 45, 59);
                menu3ThinButton.ActiveFillColor = Color.FromArgb(39, 40, 50);
                menu3ThinButton.IdleFillColor = Color.FromArgb(48, 53, 78);

                menu4UserControl.BackgroundColor = Color.FromArgb(39, 45, 59);
                menu4UserControl.BorderColor = Color.FromArgb(39, 45, 59);
                menu4Label.BackColor = Color.FromArgb(39, 45, 59);
                menu4Shapes.BackColor = Color.FromArgb(39, 45, 59);
                menu4Shapes.FillColor = Color.FromArgb(36, 40, 52);
                menu4Shapes.BorderColor = Color.FromArgb(39, 45, 59);
                menu4ThinButton.BackColor = Color.FromArgb(39, 45, 59);
                menu4ThinButton.ActiveFillColor = Color.FromArgb(39, 40, 50);
                menu4ThinButton.IdleFillColor = Color.FromArgb(48, 53, 78);

                menu5UserControl.BackgroundColor = Color.FromArgb(39, 45, 59);
                menu5UserControl.BorderColor = Color.FromArgb(39, 45, 59);
                menu5Label.BackColor = Color.FromArgb(39, 45, 59);
                menu5Shapes.BackColor = Color.FromArgb(39, 45, 59);
                menu5Shapes.FillColor = Color.FromArgb(36, 40, 52);
                menu5Shapes.BorderColor = Color.FromArgb(39, 45, 59);
                menu5ThinButton.BackColor = Color.FromArgb(39, 45, 59);
                menu5ThinButton.ActiveFillColor = Color.FromArgb(39, 40, 50);
                menu5ThinButton.IdleFillColor = Color.FromArgb(48, 53, 78);

                menu6UserControl.BackgroundColor = Color.FromArgb(39, 45, 59);
                menu6UserControl.BorderColor = Color.FromArgb(39, 45, 59);
                menu6Label.BackColor = Color.FromArgb(39, 45, 59);
                menu6Shapes.BackColor = Color.FromArgb(39, 45, 59);
                menu6Shapes.FillColor = Color.FromArgb(36, 40, 52);
                menu6Shapes.BorderColor = Color.FromArgb(39, 45, 59);
                menu6ThinButton.BackColor = Color.FromArgb(39, 45, 59);
                menu6ThinButton.ActiveFillColor = Color.FromArgb(39, 40, 50);
                menu6ThinButton.IdleFillColor = Color.FromArgb(48, 53, 78);

                menu7UserControl.BackgroundColor = Color.FromArgb(39, 45, 59);
                menu7UserControl.BorderColor = Color.FromArgb(39, 45, 59);
                menu7Label.BackColor = Color.FromArgb(39, 45, 59);
                menu7Shapes.BackColor = Color.FromArgb(39, 45, 59);
                menu7Shapes.FillColor = Color.FromArgb(36, 40, 52);
                menu7Shapes.BorderColor = Color.FromArgb(39, 45, 59);
                menu7ThinButton.BackColor = Color.FromArgb(39, 45, 59);
                menu7ThinButton.ActiveFillColor = Color.FromArgb(39, 40, 50);
                menu7ThinButton.IdleFillColor = Color.FromArgb(48, 53, 78);

                menu8UserControl.BackgroundColor = Color.FromArgb(39, 45, 59);
                menu8UserControl.BorderColor = Color.FromArgb(39, 45, 59);
                menu8Label.BackColor = Color.FromArgb(39, 45, 59);
                menu8Shapes.BackColor = Color.FromArgb(39, 45, 59);
                menu8Shapes.FillColor = Color.FromArgb(36, 40, 52);
                menu8Shapes.BorderColor = Color.FromArgb(39, 45, 59);
                menu8ThinButton.BackColor = Color.FromArgb(39, 45, 59);
                menu8ThinButton.ActiveFillColor = Color.FromArgb(39, 40, 50);
                menu8ThinButton.IdleFillColor = Color.FromArgb(48, 53, 78);
            }
            else
            {
                Ayarlar.Default.tema = false;
                Ayarlar.Default.Save();
                this.BackColor = Color.FromArgb(255, 255, 255);
                anasayfaThemeLabel.ForeColor = Color.FromArgb(0, 0, 0);
                anasayfaLabel.BackColor = Color.FromArgb(255, 255, 255);

                menu1UserControl.BackgroundColor = Color.FromArgb(223, 227, 222);
                menu1UserControl.BorderColor = Color.FromArgb(223, 227, 222);
                menu1Label.BackColor = Color.FromArgb(223, 227, 222);
                menu1Shapes.BackColor = Color.FromArgb(223, 227, 222);
                menu1Shapes.FillColor = Color.FromArgb(255, 255, 255);
                menu1Shapes.BorderColor = Color.FromArgb(255, 255, 255);
                menu1ThinButton.BackColor = Color.FromArgb(223, 227, 222);
                menu1ThinButton.ActiveFillColor = Color.FromArgb(154, 145, 150);
                menu1ThinButton.IdleFillColor = Color.FromArgb(255, 255, 255);

                menu2UserControl.BackgroundColor = Color.FromArgb(223, 227, 222);
                menu2UserControl.BorderColor = Color.FromArgb(223, 227, 222);
                menu2Label.BackColor = Color.FromArgb(223, 227, 222);
                menu2Shapes.BackColor = Color.FromArgb(223, 227, 222);
                menu2Shapes.FillColor = Color.FromArgb(255, 255, 255);
                menu2Shapes.BorderColor = Color.FromArgb(255, 255, 255);
                menu2ThinButton.BackColor = Color.FromArgb(223, 227, 222);
                menu2ThinButton.ActiveFillColor = Color.FromArgb(154, 145, 150);
                menu2ThinButton.IdleFillColor = Color.FromArgb(255, 255, 255);

                menu3UserControl.BackgroundColor = Color.FromArgb(223, 227, 222);
                menu3UserControl.BorderColor = Color.FromArgb(223, 227, 222);
                menu3Label.BackColor = Color.FromArgb(223, 227, 222);
                menu3Shapes.BackColor = Color.FromArgb(223, 227, 222);
                menu3Shapes.FillColor = Color.FromArgb(255, 255, 255);
                menu3Shapes.BorderColor = Color.FromArgb(255, 255, 255);
                menu3ThinButton.BackColor = Color.FromArgb(223, 227, 222);
                menu3ThinButton.ActiveFillColor = Color.FromArgb(154, 145, 150);
                menu3ThinButton.IdleFillColor = Color.FromArgb(255, 255, 255);

                menu4UserControl.BackgroundColor = Color.FromArgb(223, 227, 222);
                menu4UserControl.BorderColor = Color.FromArgb(223, 227, 222);
                menu4Label.BackColor = Color.FromArgb(223, 227, 222);
                menu4Shapes.BackColor = Color.FromArgb(223, 227, 222);
                menu4Shapes.FillColor = Color.FromArgb(255, 255, 255);
                menu4Shapes.BorderColor = Color.FromArgb(255, 255, 255);
                menu4ThinButton.BackColor = Color.FromArgb(223, 227, 222);
                menu4ThinButton.ActiveFillColor = Color.FromArgb(154, 145, 150);
                menu4ThinButton.IdleFillColor = Color.FromArgb(255, 255, 255);

                menu5UserControl.BackgroundColor = Color.FromArgb(223, 227, 222);
                menu5UserControl.BorderColor = Color.FromArgb(223, 227, 222);
                menu5Label.BackColor = Color.FromArgb(223, 227, 222);
                menu5Shapes.BackColor = Color.FromArgb(223, 227, 222);
                menu5Shapes.FillColor = Color.FromArgb(255, 255, 255);
                menu5Shapes.BorderColor = Color.FromArgb(255, 255, 255);
                menu5ThinButton.BackColor = Color.FromArgb(223, 227, 222);
                menu5ThinButton.ActiveFillColor = Color.FromArgb(154, 145, 150);
                menu5ThinButton.IdleFillColor = Color.FromArgb(255, 255, 255);

                menu6UserControl.BackgroundColor = Color.FromArgb(223, 227, 222);
                menu6UserControl.BorderColor = Color.FromArgb(223, 227, 222);
                menu6Label.BackColor = Color.FromArgb(223, 227, 222);
                menu6Shapes.BackColor = Color.FromArgb(223, 227, 222);
                menu6Shapes.FillColor = Color.FromArgb(255, 255, 255);
                menu6Shapes.BorderColor = Color.FromArgb(255, 255, 255);
                menu6ThinButton.BackColor = Color.FromArgb(223, 227, 222);
                menu6ThinButton.ActiveFillColor = Color.FromArgb(154, 145, 150);
                menu6ThinButton.IdleFillColor = Color.FromArgb(255, 255, 255);

                menu7UserControl.BackgroundColor = Color.FromArgb(223, 227, 222);
                menu7UserControl.BorderColor = Color.FromArgb(223, 227, 222);
                menu7Label.BackColor = Color.FromArgb(223, 227, 222);
                menu7Shapes.BackColor = Color.FromArgb(223, 227, 222);
                menu7Shapes.FillColor = Color.FromArgb(255, 255, 255);
                menu7Shapes.BorderColor = Color.FromArgb(255, 255, 255);
                menu7ThinButton.BackColor = Color.FromArgb(223, 227, 222);
                menu7ThinButton.ActiveFillColor = Color.FromArgb(154, 145, 150);
                menu7ThinButton.IdleFillColor = Color.FromArgb(255, 255, 255);

                menu8UserControl.BackgroundColor = Color.FromArgb(223, 227, 222);
                menu8UserControl.BorderColor = Color.FromArgb(223, 227, 222);
                menu8Label.BackColor = Color.FromArgb(223, 227, 222);
                menu8Shapes.BackColor = Color.FromArgb(223, 227, 222);
                menu8Shapes.FillColor = Color.FromArgb(255, 255, 255);
                menu8Shapes.BorderColor = Color.FromArgb(255, 255, 255);
                menu8ThinButton.BackColor = Color.FromArgb(223, 227, 222);
                menu8ThinButton.ActiveFillColor = Color.FromArgb(154, 145, 150);
                menu8ThinButton.IdleFillColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void gizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
