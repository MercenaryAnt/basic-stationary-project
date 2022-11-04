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
    public partial class ToptanciEkleSilDuzenle : Form
    {
        public ToptanciEkleSilDuzenle()
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

        private void ToptanciEkleSilDuzenle_Load(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        private void Temizle()
        {
            toptanciTextBox.Text = "";
            yetkiliTextBox.Text = "";
            mailTextBox.Text = "";
            telefonTextBox.Text = "";
            adresTextBox.Text = "";
            toptanciDataGridView.ClearSelection();
        }

        public void GridDoldur()
        {
            toptanciDataGridView.DataSource = vt.Select(@"select toptanci_id,toptanciAd Toptancı,sirketYetkilisi Yetkilisi,mail Email,telefon Telefon,adres Adres from tbl_toptanci");

            toptanciDataGridView.Columns["toptanci_id"].Visible = false;

        }

        private void toptanciDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (toptanciDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            toptanciTextBox.Text = toptanciDataGridView.SelectedRows[0].Cells["Toptancı"].Value.ToString();
            yetkiliTextBox.Text = toptanciDataGridView.SelectedRows[0].Cells["Yetkilisi"].Value.ToString();
            mailTextBox.Text = toptanciDataGridView.SelectedRows[0].Cells["Email"].Value.ToString();
            telefonTextBox.Text = toptanciDataGridView.SelectedRows[0].Cells["Telefon"].Value.ToString();
            adresTextBox.Text = toptanciDataGridView.SelectedRows[0].Cells["Adres"].Value.ToString();
        }

        private void ekleThinButton_Click(object sender, EventArgs e)
        {
            if (toptanciTextBox.Text == "")
            {
                MessageBox.Show("Toptancı Adını Boş Bırakmayınız.");
                return;
            }
            if (yetkiliTextBox.Text == "")
            {
                MessageBox.Show("Yetkiliyi Boş Bırakmayınız.");
                return;
            }
            if (mailTextBox.Text == "")
            {
                MessageBox.Show("Mail Boş Bırakmayınız");
                return;
            }
            if (telefonTextBox.Text.Length != 10)
            {
                MessageBox.Show("Telefon Numarası 10 Karakter Olmalı.");
                return;
            }
            if (adresTextBox.Text == "")
            {
                MessageBox.Show("Adresi Boş Bırakmayınız.");
                return;
            }
            
            foreach (DataGridViewRow item in toptanciDataGridView.Rows)
            {
                if (item.Cells["Telefon"].Value.ToString() == telefonTextBox.Text)
                {
                    MessageBox.Show("Bu Telefon Numarası Başka Toptancıda Kayıtlı");
                    return;
                }
            }

            int kayitSay = vt.UpdateDelete(@"insert into tbl_toptanci(toptanciAd,sirketYetkilisi,mail,telefon,adres) values('"+toptanciTextBox.Text+"','"+yetkiliTextBox.Text+"','"+mailTextBox.Text+"','"+telefonTextBox.Text+"','"+adresTextBox.Text+"')");


            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Toptancı Eklenmiştir.");
                Temizle();
            }
        }

        private void silThinButton_Click(object sender, EventArgs e)
        {
            if (toptanciDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek stok seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Toptancı Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }
            try
            {
                DataTable dtToptanciKontrol = vt.Select("select count(*) from tbl_urunler where toptanci_id='"+ toptanciDataGridView.SelectedRows[0].Cells["toptanci_id"].Value + "'");

                if (Convert.ToInt32(dtToptanciKontrol.Rows[0][0]) > 0)
                {
                    MessageBox.Show("Toptancıya Kayıtlı Ürün Bulunmakta");
                    return;
                }
                else
                {
                    int kayitSay = vt.UpdateDelete("delete from tbl_toptanci where toptanci_id=" + toptanciDataGridView.SelectedRows[0].Cells["toptanci_id"].Value);

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
                MessageBox.Show("Toptancıya Kayıtlı Ürün Bulunmakta");
            }

        }

        private void duzenleThinButton_Click(object sender, EventArgs e)
        {
            if (toptanciTextBox.Text == "")
            {
                MessageBox.Show("Toptancı Adını Boş Bırakmayınız.");
                return;
            }
            if (yetkiliTextBox.Text == "")
            {
                MessageBox.Show("Yetkiliyi Boş Bırakmayınız.");
                return;
            }
            if (mailTextBox.Text == "")
            {
                MessageBox.Show("Mail Boş Bırakmayınız");
                return;
            }
            if (telefonTextBox.Text.Length != 10)
            {
                MessageBox.Show("Telefon Numarası 10 Karakter Olmalı.");
                return;
            }
            if (adresTextBox.Text == "")
            {
                MessageBox.Show("Adresi Boş Bırakmayınız.");
                return;
            }

            foreach (DataGridViewRow item in toptanciDataGridView.Rows)
            {
                if (item.Cells["Telefon"].Value.ToString() == telefonTextBox.Text && item.Selected == false)
                {
                    MessageBox.Show("Bu Telefon Numarası Başka Toptancıda Kayıtlı");
                    return;
                }
            }

            int kayitSay = vt.UpdateDelete("update tbl_toptanci set toptanciAd='"+toptanciTextBox.Text+"',sirketYetkilisi='"+yetkiliTextBox.Text+"',mail='"+mailTextBox.Text+"',telefon='"+telefonTextBox.Text+"',adres='"+adresTextBox.Text+"' where toptanci_id='"+toptanciDataGridView.SelectedRows[0].Cells["toptanci_id"].Value + "'");

            if (kayitSay > 0)
            {
                GridDoldur();
                MessageBox.Show("Toptancı Güncellendi");

            }
        }

        private void telefonTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void toptancıyıSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toptanciDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek stok seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Toptancı Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }
            try
            {

                int kayitSay = vt.UpdateDelete("delete from tbl_toptanci where toptanci_id=" + toptanciDataGridView.SelectedRows[0].Cells["toptanci_id"].Value);

                if (kayitSay > 0)
                {
                    GridDoldur();
                    MessageBox.Show("Kayıt Silindi.");
                    Temizle();
                }
            }
            catch
            {
                MessageBox.Show("Toptancıya Kayıtlı Ürün Bulunmakta");
            }
        }

        private void listeyiYenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }
    }
}
