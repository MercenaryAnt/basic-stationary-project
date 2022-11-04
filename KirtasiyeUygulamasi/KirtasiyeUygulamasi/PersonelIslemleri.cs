using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KirtasiyeUygulamasi
{
    public partial class PersonelIslemleri : Form
    {
        public PersonelIslemleri()
        {
            InitializeComponent();
        }

        Bunifu.Framework.UI.Drag drag = new Bunifu.Framework.UI.Drag();
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

        private void PersonelIslemleri_Load(object sender, EventArgs e)
        {
            personel1ThinButton.BackColor = Color.FromArgb(39, 45, 59);
            personel2ThinButton.BackColor = Color.FromArgb(39, 45, 59);
        }

        private void anasayfaKucultButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void anaformKapaButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void personel1ThinButton_Click(object sender, EventArgs e)
        {
            PersonelEkleSilGuncelle prekfrm = new PersonelEkleSilGuncelle();
            prekfrm.Show();
        }

        private void personel2ThinButton_Click(object sender, EventArgs e)
        {
            PersonelListesi prlfrm = new PersonelListesi();
            prlfrm.Show();
        }
    }
}
