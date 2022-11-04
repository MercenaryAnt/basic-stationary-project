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
    public partial class KategoriIslemleri : Form
    {
        public KategoriIslemleri()
        {
            InitializeComponent();
        }
        Veritabani vt = new Veritabani(Ayarlar.Default.veritabaniAdi);

        Bunifu.Framework.UI.Drag drag = new Bunifu.Framework.UI.Drag();

        private void KategoriIslemleri_Load(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

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

        public void GridDoldur()
        {
            kategoriDataGridView.DataSource = vt.Select(@"select kategori_id,kategori Kategori from tbl_kategori");

            kategoriDataGridView.Columns["kategori_id"].Visible = false;

        }

        public void Temizle()
        {
            kategoriAdTextBox.Text = "";
            kategoriDataGridView.ClearSelection();
        }

        private void ekleThinButton_Click(object sender, EventArgs e)
        {
            if (kategoriAdTextBox.Text == "")
            {
                MessageBox.Show("Eklemek İstediğiniz Kategoriyi Giriniz.");
                return;
            }
            foreach (DataGridViewRow item in kategoriDataGridView.Rows)
            {
                if (item.Cells["Kategori"].Value.ToString() == kategoriAdTextBox.Text)
                {
                    MessageBox.Show("Bu Kategori Zaten Kayıtlı");
                    return;
                }
            }

            int kayitSay = vt.UpdateDelete(@"insert into tbl_kategori(kategori) values('"+kategoriAdTextBox.Text+"')");


            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Kategori Eklenmiştir.");
                Temizle();
            }
        }

        private void silThinButton_Click(object sender, EventArgs e)
        {
            if (kategoriDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek kategoriyi seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Kategori Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }
            try
            {
                DataTable dtKontrol = vt.Select("select count(*) from tbl_urunler where kategori_id='" + kategoriDataGridView.SelectedRows[0].Cells["kategori_id"].Value + "'");
                if (Convert.ToInt32(dtKontrol.Rows[0][0]) > 0)
                {
                    MessageBox.Show("Kategoriye Kayıtlı Ürün Bulunmakta!");
                    return;
                }
                else
                {

                    int kayitSay = vt.UpdateDelete("delete from tbl_kategori where kategori_id=" + kategoriDataGridView.SelectedRows[0].Cells["kategori_id"].Value);

                    if (kayitSay > 0)
                    {
                        GridDoldur();
                        MessageBox.Show("Kategori Silindi.");
                        Temizle();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Kategoriye Kayıtlı Ürün Bulunmakta");
            }
        }

        private void kategoriDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (kategoriDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            kategoriAdTextBox.Text = kategoriDataGridView.SelectedRows[0].Cells["Kategori"].Value.ToString();
        }

        private void guncelleThinButton_Click(object sender, EventArgs e)
        {
            if (kategoriAdTextBox.Text == "")
            {
                MessageBox.Show("Güncellemek İstediğiniz Kategoriyi Yazınız.");
                return;
            }

            foreach (DataGridViewRow item in kategoriDataGridView.Rows)
            {
                if (item.Cells["Kategori"].Value.ToString() == kategoriAdTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Bu Kategori Zaten Kayıtlı");
                    return;
                }
            }

            int kayitSay = vt.UpdateDelete("update tbl_kategori set kategori='"+kategoriAdTextBox.Text+"' where kategori_id='" + kategoriDataGridView.SelectedRows[0].Cells["kategori_id"].Value + "'");

            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Kategori Güncellendi");
                Temizle();
            }
        }

        private void kategoriyiSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kategoriDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek kategoriyi seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Kategori Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }
            try
            {

                int kayitSay = vt.UpdateDelete("delete from tbl_kategori where kategori_id=" + kategoriDataGridView.SelectedRows[0].Cells["kategori_id"].Value);

                if (kayitSay > 0)
                {
                    GridDoldur();
                    MessageBox.Show("Kategori Silindi.");
                    Temizle();
                }
            }
            catch
            {
                MessageBox.Show("Kategoriye Kayıtlı Ürün Bulunmakta");
            }
        }
    }
}
