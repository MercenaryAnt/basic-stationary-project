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
    public partial class StokSilmeDuzenleme : Form
    {
        public StokSilmeDuzenleme()
        {
            InitializeComponent();
        }
        Veritabani vt = new Veritabani(Ayarlar.Default.veritabaniAdi);

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

        private void StokEklemeSilme_Load(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        private void Temizle()
        {
            barkodTextBox.Text = "";
            urunIdTextBox.Text = "";
            adetTextBox.Text = "";
            StokDataGridView.ClearSelection();
        }
        

        public void GridDoldur()
        {
            StokDataGridView.DataSource = vt.Select(@"select s.stok_id,u.urun_id,u.barkod Barkod,u.ad ÜrünAdı,s.adet Adet,t.islemTur_id,t.islemTur İşlem from tbl_stok s
                                                        join tbl_islemTur t on s.islemTur_id=t.islemTur_id
                                                        join tbl_urunler u on s.urun_id = u.urun_id");

            StokDataGridView.Columns["urun_id"].Visible = false;
            StokDataGridView.Columns["islemTur_id"].Visible = false;
            StokDataGridView.Columns["stok_id"].Visible = false;

        }

        private void StokDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (StokDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            barkodTextBox.Text = StokDataGridView.SelectedRows[0].Cells["Barkod"].Value.ToString();
            urunIdTextBox.Text = StokDataGridView.SelectedRows[0].Cells["ÜrünAdı"].Value.ToString();
            adetTextBox.Text = StokDataGridView.SelectedRows[0].Cells["Adet"].Value.ToString();
        }


        private void silThinButton_Click(object sender, EventArgs e)
        {

            if (StokDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek stok seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Ürün stoğunu silerseniz ürün'de silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }

            try
            {
                DataTable dtKontrol = vt.Select("select count(*) from tbl_stok where stok_id='" + StokDataGridView.SelectedRows[0].Cells["stok_id"].Value + "'");

                if (Convert.ToInt32(dtKontrol.Rows[0][0]) > 0)
                {
                    MessageBox.Show("Satışı Yapılan Ürünlerin Stokları Silinemez!");
                    return;
                }
                else
                {

                    int kayitSay = vt.UpdateDelete("delete from tbl_stok where stok_id=" + StokDataGridView.SelectedRows[0].Cells["stok_id"].Value);
                    vt.UpdateDelete("delete from tbl_urunler where urun_id=" + StokDataGridView.SelectedRows[0].Cells["urun_id"].Value);

                    if (kayitSay > 0)
                    {
                        GridDoldur();
                        MessageBox.Show("Kayıt Silindi.");
                        Temizle();
                    }
                }

            }
            catch
            {
                MessageBox.Show("Stok Silinemedi!");
            }
        }

        private void guncelleThinButton_Click(object sender, EventArgs e)
        {
            if (StokDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek stok seçiniz.");
                return;
            }
            if (adetTextBox.Text == "")
            {
                MessageBox.Show("Ürün adetini boş bırakmayınız.");
                return;
            }
            try
            {
                int kayitSay = vt.UpdateDelete(@"update tbl_stok set adet='" + adetTextBox.Text + 
                                                    "',islemTur_id= '2' where stok_id=" + StokDataGridView.SelectedRows[0].Cells["stok_id"].Value);


                if (kayitSay > 0)
                {
                    GridDoldur();
                    MessageBox.Show("Stok Güncellendi");
                    Temizle();
                }
            }
            catch
            {
                MessageBox.Show("Stok Güncellenemedi!");
            }

        }

        private void stokSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StokDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek stok seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Ürün stoğunu silerseniz ürün'de silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }

            try
            {
                int kayitSay = vt.UpdateDelete("delete from tbl_stok where stok_id=" + StokDataGridView.SelectedRows[0].Cells["stok_id"].Value);
                vt.UpdateDelete("delete from tbl_urunler where urun_id=" + StokDataGridView.SelectedRows[0].Cells["urun_id"].Value);

                if (kayitSay > 0)
                {
                    GridDoldur();
                    MessageBox.Show("Kayıt Silindi.");
                    Temizle();
                }

            }
            catch
            {
                MessageBox.Show("Stok Silinemedi!");
            }
        }

        private void adetTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
