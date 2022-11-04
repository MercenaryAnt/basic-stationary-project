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
    public partial class ToptanciListesi : Form
    {
        public ToptanciListesi()
        {
            InitializeComponent();
        }
        Veritabani vt = new Veritabani(Ayarlar.Default.veritabaniAdi);

        Bunifu.Framework.UI.Drag drag = new Bunifu.Framework.UI.Drag();
        private void ToptanciListesi_Load(object sender, EventArgs e)
        {
            GridDoldur();
            ToptanciDataGridView.ClearSelection();
        }

        private void bunifuPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag.Grab(this);
        }

        private void bunifuPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            drag.MoveObject();
        }

        private void bunifuPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag.Release();
        }

        private void anaformKapaButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void GridDoldur()
        {
            ToptanciDataGridView.DataSource = vt.Select(@"select toptanci_id,toptanciAd Toptancı,sirketYetkilisi Yetkili,mail Email,telefon Telefon,adres Adres from tbl_toptanci");

            ToptanciDataGridView.Columns["toptanci_id"].Visible = false;


        }

        private void gridYenileThinButton_Click(object sender, EventArgs e)
        {
            GridDoldur();
            ToptanciDataGridView.ClearSelection();
            toptanciAdiTextBox.Text = "";
        }

        private void toptanciAraThinButton_Click(object sender, EventArgs e)
        {
            if (toptanciAdiTextBox.Text.Length == 0)
            {
                MessageBox.Show("Arama yapmak için toptancı yazınız.");
            }
            else
            {
                ToptanciDataGridView.DataSource = vt.Select(@"select toptanci_id,toptanciAd Toptancı,sirketYetkilisi Yetkili,mail Email,telefon Telefon,adres Adres from tbl_toptanci where toptanciAd='"+toptanciAdiTextBox.Text+"'");

                ToptanciDataGridView.Columns["toptanci_id"].Visible = false;
            }
        }
    }
}
