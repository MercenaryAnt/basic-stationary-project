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
    public partial class PersonelEkleSilGuncelle : Form
    {
        public PersonelEkleSilGuncelle()
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

        private void Temizle()
        {
            adTextBox.Text = "";
            soyadTextBox.Text = "";
            tcNoTextBox.Text = "";
            telefonTextBox.Text = "";
            sifreTextBox.Text = "";
            mailTextBox.Text = "";
            personelTurDropdown.SelectedIndex = -1;
            personelDataGridView.ClearSelection();
        }

        public void GridDoldur()
        {
            personelDataGridView.DataSource = vt.Select(@"select p.personel_id,p.ad Ad,p.soyad Soyad,p.tcNo Tc,p.telefon Telefon,p.email Mail,pt.personelTur_id,pt.personelTur Yetki from tbl_personel p
                                                            join tbl_personelTur pt on p.personelTur_id = pt.personelTur_id");

            personelDataGridView.Columns["personel_id"].Visible = false;
            personelDataGridView.Columns["personelTur_id"].Visible = false;

        }

        public void DropDownDoldur()
        {
            personelTurDropdown.DataSource = vt.Select("select personelTur_id,personelTur from tbl_personelTur");
            personelTurDropdown.DisplayMember = "personelTur";
            personelTurDropdown.ValueMember = "personelTur_id";

        }

        private void PersonelEkleSilGuncelle_Load(object sender, EventArgs e)
        {
            GridDoldur();
            DropDownDoldur();
            Temizle();
        }

        private void ekleThinButton_Click(object sender, EventArgs e)
        {
            if (adTextBox.Text == "")
            {
                MessageBox.Show("Personel Adını Boş Bırakmayınız.");
                return;
            }
            if (soyadTextBox.Text == "")
            {
                MessageBox.Show("Personel Soyadını Boş Bırakmayınız.");
                return;
            }
            if (tcNoTextBox.Text == "")
            {
                MessageBox.Show("Personel Tc Boş Bırakmayınız");
                return;
            }
            if (mailTextBox.Text == "")
            {
                MessageBox.Show("Personel Mail Boş Bırakmayınız");
                return;
            }
            if (tcNoTextBox.Text.Length != 11)
            {
                MessageBox.Show("Personel Tc Numarası 11 Karakter Olmalı.");
                return;
            }
            if (telefonTextBox.Text.Length != 10)
            {
                MessageBox.Show("Personel Telefon Numarası 10 Karakter Olmalı.");
                return;
            }
            if (telefonTextBox.Text == "")
            {
                MessageBox.Show("Personel Telefon Numarasını Boş Bırakmayınız.");
                return;
            }
            if (sifreTextBox.Text.Length <= 3)
            {
                MessageBox.Show("Şifre 3 Karakterden Büyük Olmalıdır.");
                return;
            }
            if (sifreTextBox.Text == "")
            {
                MessageBox.Show("Personel Şifresini Boş Bırakmayınız.");
                return;
            }
            if (personelTurDropdown.SelectedIndex == -1)
            {
                MessageBox.Show("Personel Yetkisini Seçiniz.");
                return;
            }

            foreach (DataGridViewRow item in personelDataGridView.Rows)
            {
                if (item.Cells["Telefon"].Value.ToString() == telefonTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Bu Telefon Numarası Başka Personele Kayıtlı");
                    return;
                }
                if (item.Cells["Tc"].Value.ToString() == telefonTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Bu Tc Numarası Başka Personele Kayıtlı");
                    return;
                }
                if (item.Cells["Mail"].Value.ToString() == mailTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Bu Mail Başka Personele Kayıtlı");
                    return;
                }
            }
            string sifreMd5 = DigerIslemler.CreateMD5(sifreTextBox.Text);
            int kayitSay = vt.UpdateDelete(@"insert into tbl_personel(ad,soyad,tcNo,telefon,email,sifre,personelTur_id)
                                                    values('"+adTextBox.Text+
                                                    "','"+soyadTextBox.Text+
                                                    "','" + tcNoTextBox.Text +
                                                    "','" +telefonTextBox.Text+
                                                    "','" + mailTextBox.Text +
                                                    "','"+ sifreMd5 +
                                                    "','"+personelTurDropdown.SelectedValue+"')");

            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Personel Eklenmiştir.");
                Temizle();
            }
        }

        private void duzenleThinButton_Click(object sender, EventArgs e)
        {
            if (adTextBox.Text == "")
            {
                MessageBox.Show("Personel Adını Boş Bırakmayınız.");
                return;
            }
            if (soyadTextBox.Text == "")
            {
                MessageBox.Show("Personel Soyadını Boş Bırakmayınız.");
                return;
            }
            if (tcNoTextBox.Text == "")
            {
                MessageBox.Show("Personel Tc Boş Bırakmayınız");
                return;
            }
            if (mailTextBox.Text == "")
            {
                MessageBox.Show("Personel Mail Boş Bırakmayınız");
                return;
            }
            if (tcNoTextBox.Text.Length != 11)
            {
                MessageBox.Show("Personel Tc Numarası 11 Karakter Olmalı.");
                return;
            }
            if (telefonTextBox.Text.Length != 10)
            {
                MessageBox.Show("Personel Telefon Numarası 10 Karakter Olmalı.");
                return;
            }
            if (telefonTextBox.Text == "")
            {
                MessageBox.Show("Personel Telefon Numarasını Boş Bırakmayınız.");
                return;
            }
            if (sifreTextBox.Text == "")
            {
                MessageBox.Show("Personel Şifresini Boş Bırakmayınız.");
                return;
            }
            if (personelTurDropdown.SelectedIndex == -1)
            {
                MessageBox.Show("Personel Yetkisini Seçiniz.");
                return;
            }

            foreach (DataGridViewRow item in personelDataGridView.Rows)
            {
                if (item.Cells["Telefon"].Value.ToString() == telefonTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Bu Telefon Numarası Başka Personele Kayıtlı");
                    return;
                }
                if (item.Cells["Tc"].Value.ToString() == telefonTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Bu Tc Numarası Başka Personele Kayıtlı");
                    return;
                }
                if (item.Cells["Mail"].Value.ToString() == mailTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Bu Mail Başka Personele Kayıtlı");
                    return;
                }
            }
            string sifreMd5 = DigerIslemler.CreateMD5(sifreTextBox.Text);
            int kayitSay = vt.UpdateDelete(@"update tbl_personel set 
                                                ad='"+adTextBox.Text+
                                                "',soyad='"+soyadTextBox.Text+
                                                "',tcNo='" + tcNoTextBox.Text +
                                                "',telefon='" +telefonTextBox.Text+
                                                "',email='"+mailTextBox.Text+
                                                "',sifre='"+ sifreMd5 +
                                                "',personelTur_id='"+personelTurDropdown.SelectedValue+"'" +
                                                "where personel_id='"+personelDataGridView.SelectedRows[0].Cells["personel_id"].Value + "'");

            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Personel Güncellendi");
                Temizle();
            }
        }

        private void silThinButton_Click(object sender, EventArgs e)
        {
            if (personelDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek Personeli Seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Personel Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }
            try
            {
                DataTable dtKontrol = vt.Select("select count(*) from tbl_siparis where personel_id='"+ personelDataGridView.SelectedRows[0].Cells["personel_id"].Value + "'");

                if (Convert.ToInt32(dtKontrol.Rows[0][0]) > 0)
                {
                    MessageBox.Show("Personele Kayıtlı Satış Bulunmakta!");
                    return;
                }
                else
                {
                    int kayitSay = vt.UpdateDelete("delete from tbl_personel where personel_id=" + personelDataGridView.SelectedRows[0].Cells["personel_id"].Value);

                    if (kayitSay > 0)
                    {
                        GridDoldur();
                        MessageBox.Show("Personel Silindi.");
                        Temizle();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Personele Kayıtlı Satış Bulunmakta");
            }
        }

        private void personelDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (personelDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            adTextBox.Text = personelDataGridView.SelectedRows[0].Cells["Ad"].Value.ToString();
            soyadTextBox.Text = personelDataGridView.SelectedRows[0].Cells["Soyad"].Value.ToString();
            tcNoTextBox.Text = personelDataGridView.SelectedRows[0].Cells["Tc"].Value.ToString();
            telefonTextBox.Text = personelDataGridView.SelectedRows[0].Cells["Telefon"].Value.ToString();
            mailTextBox.Text = personelDataGridView.SelectedRows[0].Cells["Mail"].Value.ToString();
            personelTurDropdown.SelectedValue = personelDataGridView.SelectedRows[0].Cells["personelTur_id"].Value;
        }

        private void personeliSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (personelDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek Personeli Seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Personel Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }
            try
            {

                int kayitSay = vt.UpdateDelete("delete from tbl_personel where personel_id=" + personelDataGridView.SelectedRows[0].Cells["personel_id"].Value);

                if (kayitSay > 0)
                {
                    GridDoldur();
                    MessageBox.Show("Personel Silindi.");
                    Temizle();
                }
            }
            catch
            {
                MessageBox.Show("Personele Kayıtlı Satış Bulunmakta");
            }
        }

        private void personelListesiniYenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        private void yetkiImageButton_Click(object sender, EventArgs e)
        {
            YetkiDuzenle ytfrm = new YetkiDuzenle();
            ytfrm.Show();
            this.Close();
        }

        private void telefonTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tcNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
