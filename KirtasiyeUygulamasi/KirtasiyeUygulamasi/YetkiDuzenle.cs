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
    public partial class YetkiDuzenle : Form
    {
        public YetkiDuzenle()
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
            PersonelEkleSilGuncelle pesgfrm = new PersonelEkleSilGuncelle();
            pesgfrm.Show();
        }

        private void ekleThinButton_Click(object sender, EventArgs e)
        {
            if (personelLabel.Text == "")
            {
                MessageBox.Show("Yetki İsmini Giriniz.");
                return;
            }

            foreach (DataGridViewRow item in yetkiDataGridView.Rows)
            {
                if (item.Cells["Yetki"].Value.ToString() == yetkiTextBox.Text)
                {
                    MessageBox.Show("Bu Yetki Zaten Kayıtlı");
                    return;
                }
            }
            
            int kayitSay = vt.UpdateDelete(@"insert into tbl_personelTur(personelTur) values('"+yetkiTextBox.Text+"')");

            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Yetki Eklenmiştir.");
                Temizle();
            }
        }

        private void YetkiDuzenle_Load(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        public void Temizle()
        {
            yetkiTextBox.Text = "";
            yetkiDataGridView.ClearSelection();
        }

        public void GridDoldur()
        {
            yetkiDataGridView.DataSource = vt.Select(@"select personelTur_id,personelTur Yetki from tbl_personelTur");

            yetkiDataGridView.Columns["personelTur_id"].Visible = false;
        }

        private void silThinButton_Click(object sender, EventArgs e)
        {
            if (yetkiDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek Yetkiyi Seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Yetki Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }
            try
            {

                int kayitSay = vt.UpdateDelete("delete from tbl_personelTur where personelTur_id=" + yetkiDataGridView.SelectedRows[0].Cells["personelTur_id"].Value);

                if (kayitSay > 0)
                {
                    GridDoldur();
                    MessageBox.Show("Yetki Silindi.");
                    Temizle();
                }
            }
            catch
            {
                MessageBox.Show("Yetkiye Kayıtlı Personel Bulunmakta");
            }
        }

        private void duzenleThinButton_Click(object sender, EventArgs e)
        {
            if (personelLabel.Text == "")
            {
                MessageBox.Show("Yetki İsmini Giriniz.");
                return;
            }

            foreach (DataGridViewRow item in yetkiDataGridView.Rows)
            {
                if (item.Cells["Yetki"].Value.ToString() == yetkiTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Bu Yetki Zaten Kayıtlı");
                    return;
                }
            }

            int kayitSay = vt.UpdateDelete(@"update tbl_personelTur set personelTur='"+yetkiTextBox.Text+"' where personelTur_id='"+ yetkiDataGridView.SelectedRows[0].Cells["personelTur_id"].Value + "'");

            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Yetki Güncellenmiştir.");
                Temizle();
            }
        }

        private void yetkiDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (yetkiDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            yetkiTextBox.Text = yetkiDataGridView.SelectedRows[0].Cells["Yetki"].Value.ToString();
        }

        private void yetkiyiSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (yetkiDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek Yetkiyi Seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Yetki Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }
            try
            {

                int kayitSay = vt.UpdateDelete("delete from tbl_personelTur where personelTur_id=" + yetkiDataGridView.SelectedRows[0].Cells["personelTur_id"].Value);

                if (kayitSay > 0)
                {
                    GridDoldur();
                    MessageBox.Show("Yetki Silindi.");
                    Temizle();
                }
            }
            catch
            {
                MessageBox.Show("Yetkiye Kayıtlı Personel Bulunmakta");
            }
        }

        private void listeyiYenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }
    }
}
