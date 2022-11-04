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
    public partial class FiyatGor : Form
    {
        public FiyatGor()
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

        private void FiyatGor_Load(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        private void Temizle()
        {
            barkodTextBox.Text = "";
            urunAdiLabel.Text = "";
            urunBarkodLabel.Text = "";
            urunFiyatLabel.Text = "";
            fiyatDataGridView.ClearSelection();
        }

        public void GridDoldur()
        {
            fiyatDataGridView.DataSource = vt.Select(@"select urun_id,barkod Barkod,ad Ürün,fiyat Fiyat from tbl_urunler ");

            fiyatDataGridView.Columns["urun_id"].Visible = false;

        }

        private void urunAraThinButton_Click(object sender, EventArgs e)
        {
            if (barkodTextBox.Text.Length == 0)
            {
                MessageBox.Show("Arama yapmak için barkod ile arama yapınız.");
                return;
            }
                
            fiyatDataGridView.DataSource = vt.Select(@"select urun_id,barkod Barkod,ad Ürün,fiyat Fiyat from tbl_urunler where barkod = '"+barkodTextBox.Text+"'");

            fiyatDataGridView.Columns["urun_id"].Visible = false;

                
        }

        private void gridYenileThinButton_Click(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        private void fiyatDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (fiyatDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            urunBarkodLabel.Text = fiyatDataGridView.SelectedRows[0].Cells["Barkod"].Value.ToString();
            urunAdiLabel.Text = fiyatDataGridView.SelectedRows[0].Cells["Ürün"].Value.ToString();
            urunFiyatLabel.Text = fiyatDataGridView.SelectedRows[0].Cells["Fiyat"].Value.ToString();
        }

        private void barkodTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
