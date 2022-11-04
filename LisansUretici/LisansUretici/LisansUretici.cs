using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;
using System.IO;
using System.Text;

namespace LisansUretici
{
    public partial class LisansUretici : Form
    {
        public LisansUretici()
        {
            InitializeComponent();
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

        private void cikisThinButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void LisansUretThinButton_Click(object sender, EventArgs e)
        {
            if (yazilimKoduTextBox.Text.Length != 32)
            {
                MessageBox.Show("Yazılım Kodunuz Geçersizdir.");
                return;
            }


            lisansKoduTextBox.Text = lisans.LisansKodu(yazilimKoduTextBox.Text);
            if (File.Exists("uretilenLisanslar.l"))
            {
                StreamWriter sw = new StreamWriter("uretilenLisanslar.l",false,Encoding.Default);
                sw.Close();
            }

            if(!uretilenLisanslarListBox.Items.Contains(yazilimKoduTextBox.Text + " = " + lisansKoduTextBox.Text))
            {
                StreamWriter sw2 = new StreamWriter("Lisansla.l", false, Encoding.Default);
                sw2.WriteLine(yazilimKoduTextBox.Text + " = " + lisansKoduTextBox.Text);
                uretilenLisanslarListBox.Items.Add(yazilimKoduTextBox.Text + " = " + lisansKoduTextBox.Text);
                sw2.Close();
            }


        }


        LisansIslemleri lisans = new LisansIslemleri();

        private void LisansUretici_Load(object sender, EventArgs e)
        {
            if (!File.Exists("uretilenLisanslar.l"))
            {
                return;
            }
            StreamReader sr = new StreamReader("uretilenLisanslar.l", Encoding.Default);
            string oku = sr.ReadLine();
            while (oku != null)
            {
                uretilenLisanslarListBox.Items.Add(oku);
                oku = sr.ReadLine();
            }
            
            sr.Close();
        }
    }
}
