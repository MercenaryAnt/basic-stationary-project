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
    public partial class PersonelListesi : Form
    {
        public PersonelListesi()
        {
            InitializeComponent();
        }
        Veritabani vt = new Veritabani(Ayarlar.Default.veritabaniAdi);

        Bunifu.Framework.UI.Drag drag = new Bunifu.Framework.UI.Drag();
        private void PersonelListesi_Load(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }

        private void Temizle()
        {
            telefonTextBox.Text = "";
            mailTextBox.Text = "";
            PersonelDataGridView.ClearSelection();
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
            PersonelDataGridView.DataSource = vt.Select(@"select p.personel_id,p.ad Ad,p.soyad Soyad,p.tcNo Tc,p.telefon Telefon,p.email EMail,pt.personelTur_id,pt.personelTur Yetki from tbl_personel p
                                                            join tbl_personelTur pt on p.personelTur_id = pt.personelTur_id");

            PersonelDataGridView.Columns["personel_id"].Visible = false;
            PersonelDataGridView.Columns["personelTur_id"].Visible = false;
        }

        private void PersonelAraThinButton_Click(object sender, EventArgs e)
        {
            if (mailTextBox.Text.Length == 0 && telefonTextBox.Text.Length == 0)
            {
                MessageBox.Show("Arama yapmak için E-Mail veya Telefon ile arama yapınız.");
            }
            else
            {
                if (mailTextBox.Text.Length != 0)
                {
                    PersonelDataGridView.DataSource = vt.Select(@"select p.personel_id,p.ad Ad,p.soyad Soyad,p.tcNo Tc,p.telefon Telefon,p.email EMail,pt.personelTur_id,pt.personelTur Yetki  from tbl_personel p
                                                                        join tbl_personelTur pt on p.personelTur_id = pt.personelTur_id where email='" + mailTextBox.Text+"'");

                    PersonelDataGridView.Columns["personel_id"].Visible = false;
                    PersonelDataGridView.Columns["personelTur_id"].Visible = false;
                }
                if (telefonTextBox.Text.Length != 0)
                {
                    PersonelDataGridView.DataSource = vt.Select(@"select p.personel_id,p.ad Ad,p.soyad Soyad,p.tcNo Tc,p.telefon Telefon,p.email EMail,pt.personelTur_id,pt.personelTur Yetki  from tbl_personel p
                                                                    join tbl_personelTur pt on p.personelTur_id = pt.personelTur_id where telefon='" + telefonTextBox.Text+"'");

                    PersonelDataGridView.Columns["personel_id"].Visible = false;
                    PersonelDataGridView.Columns["personelTur_id"].Visible = false;
                }
            }
        }

        private void telefonTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (telefonTextBox.Text.Length > 0)
            {
                mailTextBox.Enabled = false;
            }
            else if (telefonTextBox.Text.Length == 0)
            {
                mailTextBox.Enabled = true;
            }
        }

        private void mailTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (mailTextBox.Text.Length > 0)
            {
                telefonTextBox.Enabled = false;
            }
            else if (mailTextBox.Text.Length == 0)
            {
                telefonTextBox.Enabled = true;
            }
        }

        private void gridYenileThinButton_Click(object sender, EventArgs e)
        {
            GridDoldur();
            Temizle();
        }
    }
}
