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
    public partial class MusteriIslemleri : Form
    {
        public MusteriIslemleri()
        {
            InitializeComponent();
        }
        Veritabani vt = new Veritabani(Ayarlar.Default.veritabaniAdi);

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

        private void anaformKapaButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MusteriIslemleri_Load(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        private void Temizle()
        {
            isimTextBox.Text = "";
            soyisimTextBox.Text = "";
            telefonTextBox.Text = "";
            tcNoTextBox.Text = "";
            adresTextBox.Text = "";
            MusteriDataGridView.ClearSelection();
        }


        public void GridDoldur()
        {
            MusteriDataGridView.DataSource = vt.Select(@"select musteri_id,ad İsim,soyad Soyisim,telefon Telefon,tcNo TC,adres Adres from tbl_musteri");

            MusteriDataGridView.Columns["musteri_id"].Visible = false;

        }

        private void MusteriDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (MusteriDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            isimTextBox.Text = MusteriDataGridView.SelectedRows[0].Cells["İsim"].Value.ToString();
            soyisimTextBox.Text = MusteriDataGridView.SelectedRows[0].Cells["Soyisim"].Value.ToString();
            telefonTextBox.Text = MusteriDataGridView.SelectedRows[0].Cells["Telefon"].Value.ToString();
            tcNoTextBox.Text = MusteriDataGridView.SelectedRows[0].Cells["TC"].Value.ToString();
            adresTextBox.Text = MusteriDataGridView.SelectedRows[0].Cells["Adres"].Value.ToString();
        }

        private void ekleThinButton_Click(object sender, EventArgs e)
        {
            if (isimTextBox.Text == "")
            {
                MessageBox.Show("Müşteri İsmini Boş Bırakmayınız.");
                return;
            }
            if (soyisimTextBox.Text == "")
            {
                MessageBox.Show("Müşteri Soyismini Boş Bırakmayınız.");
                return;
            }
            if (telefonTextBox.Text == "")
            {
                MessageBox.Show("Müşteri Telefon Numarasını Boş Bırakmayınız");
                return;
            }
            if (telefonTextBox.Text.Length != 10)
            {
                MessageBox.Show("Müşteri Telefon Numarası 10 Karakter Olmalı.");
                return;
            }
            if (tcNoTextBox.Text == "")
            {
                MessageBox.Show("Müşteri Tc Numarasını Boş Bırakmayınız");
                return;
            }
            if (tcNoTextBox.Text.Length != 11)
            {
                MessageBox.Show("Müşteri Tc Numarası 11 Karakter Olmalı.");
                return;
            }
            if (adresTextBox.Text == "")
            {
                MessageBox.Show("Adresi Boş Bırakmayınız.");
                return;
            }

            foreach (DataGridViewRow item in MusteriDataGridView.Rows)
            {
                if (item.Cells["Telefon"].Value.ToString() == telefonTextBox.Text)
                {
                    MessageBox.Show("Bu Telefon Numarası Başka Müşteriye Kayıtlı");
                    return;
                }
            }
            foreach (DataGridViewRow item in MusteriDataGridView.Rows)
            {
                if (item.Cells["TC"].Value.ToString() == tcNoTextBox.Text)
                {
                    MessageBox.Show("Bu Tc Numarası Başka Müşteriye Kayıtlı");
                    return;
                }
            }

            int kayitSay = vt.UpdateDelete(@"insert into tbl_musteri(ad,soyad,telefon,tcNo,adres)
                                            values('"+isimTextBox.Text+"','"+soyisimTextBox.Text+"','"+telefonTextBox.Text+"','"+tcNoTextBox.Text+"','"+adresTextBox.Text+"')");


            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Müşteri Eklenmiştir.");
                Temizle();
            }
        }

        private void silThinButton_Click(object sender, EventArgs e)
        {
            if (MusteriDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek Müşteriyi Seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Müşteri Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }
            try
            {
                DataTable dtKontrol = vt.Select("select count(*) from tbl_siparis where musteri_id= '"+ MusteriDataGridView.SelectedRows[0].Cells["musteri_id"].Value + "'");

                if (Convert.ToInt32(dtKontrol.Rows[0][0]) > 0)
                {
                    MessageBox.Show("Müşteri Üzerine Kayıtlı Satış Bulunmakta.");
                    return;
                }
                else
                {
                    int kayitSay = vt.UpdateDelete(@"delete from tbl_musteri where musteri_id=" + MusteriDataGridView.SelectedRows[0].Cells["musteri_id"].Value);

                    if (kayitSay > 0)
                    {
                        GridDoldur();
                        MessageBox.Show("Müşteri Silindi.");
                        Temizle();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Müşteri Siparişi Mevcut");
            }
        }

        private void guncelleThinButton_Click(object sender, EventArgs e)
        {
            if (isimTextBox.Text == "")
            {
                MessageBox.Show("Müşteri İsmini Boş Bırakmayınız.");
                return;
            }
            if (soyisimTextBox.Text == "")
            {
                MessageBox.Show("Müşteri Soyismini Boş Bırakmayınız.");
                return;
            }
            if (telefonTextBox.Text == "")
            {
                MessageBox.Show("Müşteri Telefon Numarasını Boş Bırakmayınız");
                return;
            }
            if (telefonTextBox.Text.Length != 10)
            {
                MessageBox.Show("Müşteri Telefon Numarası 10 Karakter Olmalı.");
                return;
            }
            if (tcNoTextBox.Text == "")
            {
                MessageBox.Show("Müşteri Tc Numarasını Boş Bırakmayınız");
                return;
            }
            if (tcNoTextBox.Text.Length != 11)
            {
                MessageBox.Show("Müşteri Tc Numarası 11 Karakter Olmalı.");
                return;
            }
            if (adresTextBox.Text == "")
            {
                MessageBox.Show("Adresi Boş Bırakmayınız.");
                return;
            }

            foreach (DataGridViewRow item in MusteriDataGridView.Rows)
            {
                if (item.Cells["Telefon"].Value.ToString() == telefonTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Bu Telefon Numarası Başka Müşteriye Kayıtlı");
                    return;
                }
            }
            foreach (DataGridViewRow item in MusteriDataGridView.Rows)
            {
                if (item.Cells["TC"].Value.ToString() == tcNoTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Bu Tc Numarası Başka Müşteriye Kayıtlı");
                    return;
                }
            }

            int kayitSay = vt.UpdateDelete(@"update tbl_musteri set 
                                                ad='"+isimTextBox.Text+
                                                "',soyad='"+soyisimTextBox.Text+
                                                "',telefon='"+telefonTextBox.Text+
                                                "',tcNo='"+tcNoTextBox.Text+
                                                "',adres='"+adresTextBox.Text+"'" +
                                                "where musteri_id='"+ MusteriDataGridView.SelectedRows[0].Cells["musteri_id"].Value + "'");

            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Müşteri Güncellendi");

            }
        }

        private void telNoAramaThinButton_Click(object sender, EventArgs e)
        {
            if (telNoAramaTextBox.Text.Length == 0)
            {
                MessageBox.Show("Arama Yapmak İçin Telefon Numarası Giriniz.");
            }
            else
            {
                    MusteriDataGridView.DataSource = vt.Select(@"select musteri_id,ad İsim,soyad Soyisim,telefon Telefon,tcNo TC,adres Adres from tbl_musteri where telefon='" + telNoAramaTextBox.Text+"'");

                MusteriDataGridView.Columns["musteri_id"].Visible = false;
                Temizle();
            }
        }

        private void isimAramaThinButton_Click(object sender, EventArgs e)
        {
            if (tcAramaTextBox.Text.Length == 0)
            {
                MessageBox.Show("Arama Yapmak İçin Müşteri TC Numarası Giriniz.");
            }
            else
            {
                MusteriDataGridView.DataSource = vt.Select(@"select musteri_id,ad İsim,soyad Soyisim,telefon Telefon,tcNo TC,adres Adres from tbl_musteri where tcNo='" + tcAramaTextBox.Text + "'");

                MusteriDataGridView.Columns["musteri_id"].Visible = false;
            }
        }

        private void gridTemizleThinButton_Click(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        private void müşteriyiSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MusteriDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek Müşteriyi Seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Müşteri Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }
            try
            {

                int kayitSay = vt.UpdateDelete(@"delete from tbl_musteri where musteri_id=" + MusteriDataGridView.SelectedRows[0].Cells["musteri_id"].Value);

                if (kayitSay > 0)
                {
                    GridDoldur();
                    MessageBox.Show("Müşteri Silindi.");
                    Temizle();
                }
            }
            catch
            {
                MessageBox.Show("Müşteri Siparişi Mevcut");
            }
        }

        private void listeyiYenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        private void telefonTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tcNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void telNoAramaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tcAramaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
