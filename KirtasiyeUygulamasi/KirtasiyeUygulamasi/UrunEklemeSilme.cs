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
    public partial class UrunEklemeSilme : Form
    {
        public UrunEklemeSilme()
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

        private void UrunEklemeSilme_Load(object sender, EventArgs e)
        {
            GridDoldur();
            DropDownDoldur();
            Temizle();
        }

        private void Temizle()
        {
            barkodTextBox.Text = "";
            urunAdiTextBox.Text = "";
            fiyatTextBox.Text = "";
            kategoriDropdown.SelectedIndex = -1;
            toptanciDropdown.SelectedIndex = -1;
            UrunlerDataGridView.ClearSelection();
        }

        public void DropDownDoldur()
        {
            kategoriDropdown.DataSource = vt.Select("select kategori_id,kategori from tbl_kategori");
            kategoriDropdown.DisplayMember = "kategori";
            kategoriDropdown.ValueMember = "kategori_id";

            toptanciDropdown.DataSource = vt.Select("select toptanci_id,toptanciAd from tbl_toptanci");
            toptanciDropdown.DisplayMember = "toptanciAd";
            toptanciDropdown.ValueMember = "toptanci_id";

        }

        public void GridDoldur()
        {
            UrunlerDataGridView.DataSource = vt.Select(@"select u.urun_id,u.barkod Barkod,u.ad ÜrünAdı,u.fiyat Fiyatı,k.kategori Kategorisi,k.kategori_id,t.toptanci_id,t.toptanciAd Toptancı from tbl_urunler u
                                                        join tbl_kategori k on u.kategori_id = k.kategori_id
                                                        join tbl_toptanci t on t.toptanci_id = u.toptanci_id");

            UrunlerDataGridView.Columns["urun_id"].Visible = false;
            UrunlerDataGridView.Columns["kategori_id"].Visible = false;
            UrunlerDataGridView.Columns["toptanci_id"].Visible = false;

            Temizle();
        }

        private void ekleThinButton_Click(object sender, EventArgs e)
        {
            if (barkodTextBox.Text == "")
            {
                MessageBox.Show("Ürün barkodunu boş bırakmayınız.");
                return;
            }
            if (barkodTextBox.Text.Length != 10)
            {
                MessageBox.Show("Ürün barkodunu uzunluğu 10 karakter olmalıdır");
                return;
            }
            if (urunAdiTextBox.Text == "")
            {
                MessageBox.Show("Ürün adını boş bırakmayınız.");
                return;
            }
            if (fiyatTextBox.Text == "")
            {
                MessageBox.Show("Ürün fiyatını boş bırakmayınız.");
                return;
            }
            if (kategoriDropdown.SelectedIndex == -1)
            {
                MessageBox.Show("Ürün kategorisini boş bırakmayınız.");
                return;
            }
            if (toptanciDropdown.SelectedIndex == -1)
            {
                MessageBox.Show("Ürün toptancısını boş bırakmayınız.");
                return;
            }
            foreach (DataGridViewRow item in UrunlerDataGridView.Rows)
            {
                if (item.Cells["Barkod"].Value.ToString() == barkodTextBox.Text)
                {
                    MessageBox.Show("Ürün barkodu başka ürüne ait.");
                    return;
                }
            }

            try
            { 
                int kayitSay = vt.UpdateDelete(@"insert into tbl_urunler(barkod,ad,fiyat,kategori_id,toptanci_id) 
                                                values('"+barkodTextBox.Text+"','"+urunAdiTextBox.Text+"','"+fiyatTextBox.Text+"',"+ kategoriDropdown.SelectedValue + ","+toptanciDropdown.SelectedValue+ ")");

                DataTable dtEklenen = vt.Select("select IDENT_CURRENT ('tbl_urunler')");

                DataTable dtStok = vt.Select("select islemTur_id from tbl_islemTur where islemTur = 'Stok Eklendi'");

                vt.UpdateDelete(@"insert into tbl_stok(urun_id,adet,islemTur_id) values(" + dtEklenen.Rows[0][0].ToString() + ",0,'"+ dtStok.Rows[0][0].ToString() + "')");


                if (kayitSay > 0)
                {
                    GridDoldur();
                    MessageBox.Show("Ürün Eklenmiştir.");
                    Temizle();
                }

            }
            catch
            {
                MessageBox.Show("Ürün Eklemede Sorun Oluştu Tekrar Deneyiniz!");
            }
        }

        private void silThinButton_Click(object sender, EventArgs e)
        {
            if (UrunlerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Ürün kaydını silerseniz stok bilgileride silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }

            try
            {

                DataTable dtKontrol = vt.Select("select count(*) from tbl_siparisDetay where urun_id='"+ UrunlerDataGridView.SelectedRows[0].Cells["urun_id"].Value + "'");

                if (Convert.ToInt32(dtKontrol.Rows[0][0]) > 0)
                {
                    MessageBox.Show("Satışı Yapılan Ürünler Silinemez!");
                    return;
                }
                else
                {
                    vt.UpdateDelete("delete from tbl_stok where urun_id='" + UrunlerDataGridView.SelectedRows[0].Cells["urun_id"].Value + "'");

                    int kayitSay = vt.UpdateDelete("delete from tbl_urunler where urun_id='" + UrunlerDataGridView.SelectedRows[0].Cells["urun_id"].Value + "'");



                    if (kayitSay > 0)
                    {
                        GridDoldur();
                        MessageBox.Show("Ürün Silindi.");
                        Temizle();
                    }
                }

            }
            catch
            {
                MessageBox.Show("Satışı Yapılan Ürünler Silinemez!");
                Temizle();
                return;
            }
        }

        private void UrunlerDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (UrunlerDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            barkodTextBox.Text = UrunlerDataGridView.SelectedRows[0].Cells["Barkod"].Value.ToString();
            urunAdiTextBox.Text = UrunlerDataGridView.SelectedRows[0].Cells["ÜrünAdı"].Value.ToString();
            fiyatTextBox.Text = Convert.ToInt32(UrunlerDataGridView.SelectedRows[0].Cells["Fiyatı"].Value).ToString();
            kategoriDropdown.SelectedValue = UrunlerDataGridView.SelectedRows[0].Cells["kategori_id"].Value;
            toptanciDropdown.SelectedValue = UrunlerDataGridView.SelectedRows[0].Cells["toptanci_id"].Value;
        }

        private void fiyatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


        private void duzenleThinButton_Click(object sender, EventArgs e)
        {
            if (barkodTextBox.Text == "")
            {
                MessageBox.Show("Ürün barkodunu boş bırakmayınız.");
                return;
            }
            if (barkodTextBox.Text.Length != 10)
            {
                MessageBox.Show("Ürün barkodunu uzunluğu 10 karakter olmalıdır");
                return;
            }
            if (urunAdiTextBox.Text == "")
            {
                MessageBox.Show("Ürün adını boş bırakmayınız.");
                return;
            }
            if (fiyatTextBox.Text == "")
            {
                MessageBox.Show("Ürün fiyatını boş bırakmayınız.");
                return;
            }
            if (kategoriDropdown.SelectedIndex == -1)
            {
                MessageBox.Show("Ürün kategorisini boş bırakmayınız.");
                return;
            }
            if (toptanciDropdown.SelectedIndex == -1)
            {
                MessageBox.Show("Ürün toptancısını boş bırakmayınız.");
                return;
            }
            foreach (DataGridViewRow item in UrunlerDataGridView.Rows)
            {
                if (item.Cells["Barkod"].Value.ToString() == barkodTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Ürün barkodu başka ürüne ait.");
                    return;
                }
            }

            int kayitSay = vt.UpdateDelete(@"update tbl_urunler
                                             set barkod='" + barkodTextBox.Text +
                                             "',ad='" + urunAdiTextBox.Text +
                                             "',fiyat='" + fiyatTextBox.Text +
                                             "',kategori_id='" + kategoriDropdown.SelectedValue +
                                             "',toptanci_id='" + toptanciDropdown.SelectedValue +
                                             "'where urun_id=" + UrunlerDataGridView.SelectedRows[0].Cells["urun_id"].Value);

            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Ürün Güncellendi");

            }
        }

        private void barkodTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ürünüSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UrunlerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Ürün kaydını silerseniz stok bilgileride silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }

            vt.UpdateDelete("delete from tbl_stok where urun_id=" + UrunlerDataGridView.SelectedRows[0].Cells["urun_id"].Value);

            int kayitSay = vt.UpdateDelete("delete from tbl_urunler where urun_id=" + UrunlerDataGridView.SelectedRows[0].Cells["urun_id"].Value);



            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Ürün Silindi.");
                Temizle();
            }
            else
            {
                MessageBox.Show("Ürün Silindi");
            }
        }

        private void listeyiYenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        private void barkodTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
