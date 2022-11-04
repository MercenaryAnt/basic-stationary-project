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
    public partial class UrunListele : Form
    {
        public UrunListele()
        {
            InitializeComponent();
        }
        Veritabani vt = new Veritabani(Ayarlar.Default.veritabaniAdi);

        Bunifu.Framework.UI.Drag drag = new Bunifu.Framework.UI.Drag();
        private void anaformKapaButton_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void UrunListele_Load(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        
        public void GridDoldur()
        {
            UrunlerDataGridView.DataSource = vt.Select(@"select u.urun_id,u.barkod Barkod,u.ad ÜrünAdı,u.fiyat Fiyatı,k.kategori Kategorisi,k.kategori_id,t.toptanci_id,t.toptanciAd Toptancı from tbl_urunler u
                                                        join tbl_kategori k on u.kategori_id = k.kategori_id
                                                        join tbl_toptanci t on t.toptanci_id = u.toptanci_id");

            UrunlerDataGridView.Columns["urun_id"].Visible = false;
            UrunlerDataGridView.Columns["kategori_id"].Visible = false;
            UrunlerDataGridView.Columns["toptanci_id"].Visible = false;

        }

        private void urunAraThinButton_Click(object sender, EventArgs e)
        {
            if (barkodTextBox.Text.Length == 0 && urunAdTextBox.Text.Length == 0)
            {
                MessageBox.Show("Arama yapmak için barkod veya ürün adı ile arama yapınız.");
            }
            else
            {
                if (barkodTextBox.Text.Length != 0)
                {
                    UrunlerDataGridView.DataSource = vt.Select(@"select u.urun_id,u.barkod Barkod,u.ad ÜrünAdı,u.fiyat Fiyatı,k.kategori Kategorisi,k.kategori_id,t.toptanci_id,t.toptanciAd Toptancı from tbl_urunler u
                                                        join tbl_kategori k on u.kategori_id = k.kategori_id
                                                        join tbl_toptanci t on t.toptanci_id = u.toptanci_id
                                                        where u.barkod='"+barkodTextBox.Text+"'");

                    UrunlerDataGridView.Columns["urun_id"].Visible = false;
                    UrunlerDataGridView.Columns["kategori_id"].Visible = false;
                    UrunlerDataGridView.Columns["toptanci_id"].Visible = false;
                }
                if (urunAdTextBox.Text.Length != 0)
                {
                    UrunlerDataGridView.DataSource = vt.Select(@"select u.urun_id,u.barkod Barkod,u.ad ÜrünAdı,u.fiyat Fiyatı,k.kategori Kategorisi,k.kategori_id,t.toptanci_id,t.toptanciAd Toptancı from tbl_urunler u
                                                        join tbl_kategori k on u.kategori_id = k.kategori_id
                                                        join tbl_toptanci t on t.toptanci_id = u.toptanci_id
                                                        where u.ad='"+urunAdTextBox.Text+"'");

                    UrunlerDataGridView.Columns["urun_id"].Visible = false;
                    UrunlerDataGridView.Columns["kategori_id"].Visible = false;
                    UrunlerDataGridView.Columns["toptanci_id"].Visible = false;
                }
            }
        }

        private void barkodTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (barkodTextBox.Text.Length > 0)
            {
                urunAdTextBox.Enabled = false;
            }
            else if (barkodTextBox.Text.Length == 0)
            {
                urunAdTextBox.Enabled = true;
            }
        }

        private void urunAdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (urunAdTextBox.Text.Length > 0)
            {
                barkodTextBox.Enabled = false;
            }
            else if (urunAdTextBox.Text.Length == 0)
            {
                barkodTextBox.Enabled = true;
            }
        }

        private void gridYenileThinButton_Click(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        private void Temizle()
        {
            barkodTextBox.Text = "";
            urunAdTextBox.Text = "";
            UrunlerDataGridView.ClearSelection();
        }
    }
}
