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
    public partial class ToptanciIslemleri : Form
    {
        public ToptanciIslemleri()
        {
            InitializeComponent();
        }

        private void toptanci1ThinButton_Click(object sender, EventArgs e)
        {
            ToptanciEkleSilDuzenle tpcifrm = new ToptanciEkleSilDuzenle();
            tpcifrm.Show();
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

        private void anaformKapaButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToptanciIslemleri_Load(object sender, EventArgs e)
        {
            toptanci1ThinButton.BackColor = Color.FromArgb(39, 45, 59);
            toptanci2ThinButton.BackColor = Color.FromArgb(39, 45, 59);
        }

        private void toptanci2ThinButton_Click(object sender, EventArgs e)
        {
            ToptanciListesi tpfrm = new ToptanciListesi();
            tpfrm.Show();
        }
    }
}
