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
    public partial class SiparisListele : Form
    {
        public SiparisListele()
        {
            InitializeComponent();
        }
        Veritabani vt = new Veritabani(Ayarlar.Default.veritabaniAdi);

        Bunifu.Framework.UI.Drag drag = new Bunifu.Framework.UI.Drag();
        private void SiparisListele_Load(object sender, EventArgs e)
        {
            GridDoldur();

            musteriDropdown.DataSource = vt.Select("select musteri_id,ad+' '+soyad musteri,telefon from tbl_musteri");
            musteriDropdown.DisplayMember = "musteri";
            musteriDropdown.ValueMember = "musteri_id";

            musteriDropdown.SelectedIndex = -1;
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

        public void GridDoldur()
        {
            siparislerDataGridView.DataSource = vt.Select(@"select s.siparis_id,m.musteri_id,m.ad+' '+m.soyad Müşteri,m.telefon Telefon,s.tutar Tutar,ot.odemeTur_id,ot.odemeTur Ödeme,p.personel_id,p.ad+' '+p.soyad Personel from tbl_siparis s
                                                            join tbl_musteri m on s.musteri_id=m.musteri_id
                                                            join tbl_personel p on s.personel_id=p.personel_id
                                                            join tbl_odemeTur ot on s.odemeTur_id=ot.odemeTur_id");

            siparislerDataGridView.Columns["siparis_id"].Visible = false;
            siparislerDataGridView.Columns["musteri_id"].Visible = false;
            siparislerDataGridView.Columns["odemeTur_id"].Visible = false;
            siparislerDataGridView.Columns["personel_id"].Visible = false;

        }

        private void gridYenileThinButton_Click(object sender, EventArgs e)
        {
            GridDoldur();
            musteriDropdown.SelectedIndex = -1;
        }

        private void siparisAraThinButton_Click(object sender, EventArgs e)
        {
            if (musteriDropdown.SelectedIndex == -1)
            {
                MessageBox.Show("Aranacak Müşteriyi Seçiniz.");
                return;
            }

            siparislerDataGridView.DataSource = vt.Select(@"select s.siparis_id,m.musteri_id,m.ad+' '+m.soyad Müşteri,m.telefon Telefon,s.tutar Tutar,ot.odemeTur_id,ot.odemeTur Ödeme,p.personel_id,p.ad+' '+p.soyad Personel from tbl_siparis s
                                                            join tbl_musteri m on s.musteri_id=m.musteri_id
                                                            join tbl_personel p on s.personel_id=p.personel_id
                                                            join tbl_odemeTur ot on s.odemeTur_id=ot.odemeTur_id
                                                            where m.musteri_id='"+musteriDropdown.SelectedValue+"'");

            siparislerDataGridView.Columns["siparis_id"].Visible = false;
            siparislerDataGridView.Columns["musteri_id"].Visible = false;
            siparislerDataGridView.Columns["odemeTur_id"].Visible = false;
            siparislerDataGridView.Columns["personel_id"].Visible = false;

        }

        private void anaformKapaButton_Click(object sender, EventArgs e)
        {
            this.Close();
            SatisIslemleri stfrm = new SatisIslemleri();
            stfrm.Show();
        }

        private void sparişiSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (siparislerDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            DialogResult cevap = MessageBox.Show("Sipariş Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }

            try
            {

                vt.UpdateDelete("delete from tbl_siparisDetay where siparis_id = '"+ siparislerDataGridView.SelectedRows[0].Cells["siparis_id"].Value + "'");

                int kayitSay = vt.UpdateDelete("delete from tbl_siparis where siparis_id='" + siparislerDataGridView.SelectedRows[0].Cells["siparis_id"].Value + "'");


                if (kayitSay > 0)
                {
                    GridDoldur();
                    MessageBox.Show("Sipariş Silindi.");
                }

            }
            catch
            {
                MessageBox.Show("Silmek İstediğiniz Siparişte Sorun Oluştu!");
                return;
            }
        }

        private void listeyiYenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridDoldur();
            musteriDropdown.SelectedIndex = -1;
        }
    }
}
