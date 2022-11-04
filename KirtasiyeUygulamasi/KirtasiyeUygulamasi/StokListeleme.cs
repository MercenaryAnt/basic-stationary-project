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
    public partial class StokListeleme : Form
    {
        public StokListeleme()
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

        private void StokListeleme_Load(object sender, EventArgs e)
        {
            GridDoldur();
            StokDataGridView.ClearSelection();
        }

        public void GridDoldur()
        {
            StokDataGridView.DataSource = vt.Select(@"select s.stok_id,u.urun_id,u.barkod Barkod,u.ad ÜrünAdı,u.fiyat Fiyat,s.adet Adet,k.kategori_id,k.kategori Kategori,t.islemTur_id,t.islemTur İşlem,tp.toptanci_id,tp.toptanciAd Toptancı from tbl_stok s
                                                        join tbl_islemTur t on s.islemTur_id=t.islemTur_id
                                                        join tbl_urunler u on s.urun_id = u.urun_id
														join tbl_toptanci tp on u.toptanci_id = tp.toptanci_id
														join tbl_kategori k on u.kategori_id = k.kategori_id");

            StokDataGridView.Columns["urun_id"].Visible = false;
            StokDataGridView.Columns["kategori_id"].Visible = false;
            StokDataGridView.Columns["toptanci_id"].Visible = false;
            StokDataGridView.Columns["islemTur_id"].Visible = false;
            StokDataGridView.Columns["stok_id"].Visible = false;
        }

        private void anaformKapaButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void stokAraThinButton_Click(object sender, EventArgs e)
        {
            if (barkodTextBox.Text.Length == 0 && urunAdTextBox.Text.Length == 0)
            {
                MessageBox.Show("Arama yapmak için barkod veya ürün adı ile arama yapınız.");
            }
            else
            {
                if (barkodTextBox.Text.Length != 0)
                {
                    StokDataGridView.DataSource = vt.Select(@"select s.stok_id,u.urun_id,u.barkod Barkod,u.ad ÜrünAdı,u.fiyat Fiyat,s.adet Adet,k.kategori_id,k.kategori Kategori,t.islemTur_id,t.islemTur İşlem,tp.toptanci_id,tp.toptanciAd Toptancı from tbl_stok s
                                                        join tbl_islemTur t on s.islemTur_id=t.islemTur_id
                                                        join tbl_urunler u on s.urun_id = u.urun_id
														join tbl_toptanci tp on u.toptanci_id = tp.toptanci_id
														join tbl_kategori k on u.kategori_id = k.kategori_id
                                                        where u.barkod='"+barkodTextBox.Text+"'");

                    StokDataGridView.Columns["urun_id"].Visible = false;
                    StokDataGridView.Columns["kategori_id"].Visible = false;
                    StokDataGridView.Columns["toptanci_id"].Visible = false;
                    StokDataGridView.Columns["islemTur_id"].Visible = false;
                    StokDataGridView.Columns["stok_id"].Visible = false;
                }
                if (urunAdTextBox.Text.Length != 0)
                {
                    StokDataGridView.DataSource = vt.Select(@"select s.stok_id,u.urun_id,u.barkod Barkod,u.ad ÜrünAdı,u.fiyat Fiyat,s.adet Adet,k.kategori_id,k.kategori Kategori,t.islemTur_id,t.islemTur İşlem,tp.toptanci_id,tp.toptanciAd Toptancı from tbl_stok s
                                                        join tbl_islemTur t on s.islemTur_id=t.islemTur_id
                                                        join tbl_urunler u on s.urun_id = u.urun_id
														join tbl_toptanci tp on u.toptanci_id = tp.toptanci_id
														join tbl_kategori k on u.kategori_id = k.kategori_id
                                                        where u.ad='"+urunAdTextBox.Text+"'");

                    StokDataGridView.Columns["urun_id"].Visible = false;
                    StokDataGridView.Columns["kategori_id"].Visible = false;
                    StokDataGridView.Columns["toptanci_id"].Visible = false;
                }
            }
        }

        private void Temizle()
        {
            barkodTextBox.Text = "";
            urunAdTextBox.Text = "";
            StokDataGridView.ClearSelection();
        }

        private void gridYenileThinButton_Click(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }
    }
}
