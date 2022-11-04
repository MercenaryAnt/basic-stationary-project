using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTIveDI;

namespace KirtasiyeUygulamasi
{
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }
        Veritabani vt = new Veritabani(Ayarlar.Default.veritabaniAdi);
        private void anaformKapaButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GirisEkrani_Load(object sender, EventArgs e)
        {
            if (Ayarlar.Default.beniHatirla == true)
            {
                beniHatirlaCheckBox.Checked = true;
                mailTextBox.Text = Ayarlar.Default.email;
                sifreTextBox.Text = Ayarlar.Default.sifre;
            }
        }

        private void GirisThinButton_Click(object sender, EventArgs e)
        {
            if (mailTextBox.Text == "" || sifreTextBox.Text == "")
            {
                MessageBox.Show("Boş bırakmayınız...");
                return;
            }
            string sifreMd5 = DigerIslemler.CreateMD5(sifreTextBox.Text);
            DataTable dtSonuc = vt.Select(@"select personel_id,ad,soyad,tcNo,telefon,email,sifre,personelTur_id from tbl_personel where email='" + mailTextBox.Text + "' and sifre='" + sifreMd5 + "'");

            if (dtSonuc.Rows.Count == 0)
            {
                MessageBox.Show("E-Mail veya Şifre Hatalı...");
                return;
            }

            if (beniHatirlaCheckBox.Checked == true)
            {
                Ayarlar.Default.beniHatirla = true;
                Ayarlar.Default.email = mailTextBox.Text;
                Ayarlar.Default.sifre = sifreTextBox.Text;
                Ayarlar.Default.Save();
            }
            else
            {
                Ayarlar.Default.Reset();
            }

            Anasayfa afrm = new Anasayfa();

            this.Hide();
            afrm.Show();

            DataTable dtPersonelTur = vt.Select("select personelTur_id from tbl_personelTur where personelTur='Kullanici'");

            DataTable dtGirilen = vt.Select("select personelTur_id from tbl_personel where email='"+mailTextBox.Text+"'");

            if (dtGirilen.Rows[0][0].ToString() == dtPersonelTur.Rows[0][0].ToString())
            {
                afrm.menu4PictureBox4.Image = Properties.Resources.capi;
                afrm.menu4ThinButton.Visible = false;

                afrm.menu6PictureBox6.Image = Properties.Resources.capi;
                afrm.menu6ThinButton.Visible = false;

                afrm.menu7PictureBox7.Image = Properties.Resources.capi;
                afrm.menu7ThinButton.Visible = false;
            }
        }

        private void SifreGosterCheckBox_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (SifreGosterCheckBox.Checked == true)
                sifreTextBox.PasswordChar = '\0';
            else
                sifreTextBox.PasswordChar = '*';
        }

        private void cikisThinButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
