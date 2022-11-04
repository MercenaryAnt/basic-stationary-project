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
    public partial class SatisIslemleri : Form
    {
        public SatisIslemleri()
        {
            InitializeComponent();
        }
        Veritabani vt = new Veritabani(Ayarlar.Default.veritabaniAdi);

        int toplam = 0;
        int veriToplam = 0;
        int urunFiyat = 0;
        int urunAdet = 0;
        int secilenUrun = 0;
        int silinenUrun = 0;
        int silinenAdet = 0;
        int satisKontrol = 0;

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

        private void anasayfaKucultButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void anaformKapaButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SatisIslemleri_Load(object sender, EventArgs e)
        {
            KategoriGridDoldur();
            DropDownDoldur();
            siparisGridYenile();
            Temizle();
            toplamHesapla();
            satisKontrolu();
        }

        public void satisKontrolu()
        {
            DataTable dtKontrolGrid = vt.Select("select count(*) from tbl_siparis");
            if (Convert.ToInt32(dtKontrolGrid.Rows[0][0]) != 0)
            {
                DataTable dtSiparisId = vt.Select("select IDENT_CURRENT ('tbl_siparis') ");
                DataTable dtSiparisKontrol = vt.Select("select COUNT(*) from tbl_siparis where siparis_id='" + dtSiparisId.Rows[0][0].ToString() + "'");
                DataTable dtSiparisTutarKontrol = vt.Select("select tutar from tbl_siparis where siparis_id='" + dtSiparisId.Rows[0][0].ToString() + "'");
                if (Convert.ToInt32(dtSiparisKontrol.Rows[0][0]) != 0)
                {
                    if (Convert.ToInt32(dtSiparisTutarKontrol.Rows[0][0]) != 0)
                    {
                        satisKontrol = 1;
                    }
                    else
                    {
                        satisKontrol = 0;
                    }
                }
                else 
                {
                    satisKontrol = 1;
                }
            }
        }

        public void siparisGridYenile()
        {
            DataTable dtSiparis = vt.Select("select IDENT_CURRENT ('tbl_siparis') ");

            DataTable dtKontrol = vt.Select("select COUNT(*) from tbl_siparis where siparis_id='"+dtSiparis.Rows[0][0].ToString()+"'");


            if (Convert.ToInt32(dtKontrol.Rows[0][0]) != 0)
            {
                

                siparisDetayDataGridView.DataSource = vt.Select(@"select siparisDetay_id,u.urun_id,u.barkod Barkod,u.ad Ürün,sd.adet Adet,s.siparis_id,sd.fiyat Fiyat from tbl_siparisDetay sd
                                                                join tbl_urunler u on sd.urun_id=u.urun_id
                                                                join tbl_siparis s on sd.siparis_id=s.siparis_id
                                                                where sd.siparis_id='" + dtSiparis.Rows[0][0].ToString() + "'");

                siparisDetayDataGridView.Columns["siparisDetay_id"].Visible = false;
                siparisDetayDataGridView.Columns["urun_id"].Visible = false;
                siparisDetayDataGridView.Columns["siparis_id"].Visible = false;

                DataTable dtpm = vt.Select(@"select s.siparis_id,p.ad+' '+p.soyad personel,m.ad +' '+ m.soyad musteri from tbl_siparis s
                                            join tbl_personel p on s.personel_id = p.personel_id
                                            join tbl_musteri m on s.musteri_id = m.musteri_id
                                            where s.siparis_id = '" + dtSiparis.Rows[0][0].ToString() + "'");

                    hangiPersonelLabel.Text = dtpm.Rows[0]["personel"].ToString();
                    hangiMusteriLabel.Text = dtpm.Rows[0]["musteri"].ToString();

                satisAdetLabel.Text = siparisDetayDataGridView.Rows.Count.ToString();
            }
        }

        private void Temizle()
        {
            kategorilerDataGridView.ClearSelection();
            urunlerDataGridView.ClearSelection();
            siparisDetayDataGridView.ClearSelection();

            barkodTextBox.Text = "";
            urunTextBox.Text = "";
            fiyatTextBox.Text = "";
            urunAdetTextBox.Text = "";

            personelDropdown.SelectedIndex = -1;
            musteriDropdown.SelectedIndex = -1;
        }

        public void KategoriGridDoldur()
        {
            kategorilerDataGridView.DataSource = vt.Select(@"select kategori_id,kategori Kategori from tbl_kategori");

            kategorilerDataGridView.Columns["kategori_id"].Visible = false;
        }

        public void DropDownDoldur()
        {
            personelDropdown.DataSource = vt.Select("select personel_id,ad +' '+ soyad Personel from tbl_personel");
            personelDropdown.DisplayMember = "Personel";
            personelDropdown.ValueMember = "personel_id";

            musteriDropdown.DataSource = vt.Select("select musteri_id,ad+' '+soyad Müsteri from tbl_musteri");
            musteriDropdown.DisplayMember = "Müsteri";
            musteriDropdown.ValueMember = "musteri_id";
        }

        private void kategorilerDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (kategorilerDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            urunlerGridYenile();
        }

        public void urunlerGridYenile()
        {
            if (kategorilerDataGridView.SelectedRows.Count == 0)
            {
                urunlerDataGridView.DataSource = vt.Select(@"select u.urun_id,u.barkod Barkod,u.ad Ürün,u.fiyat Fiyat,s.adet,kategori_id,toptanci_id from tbl_urunler u
                                                            join tbl_stok s on u.urun_id=s.urun_id
                                                            where kategori_id='1'");

                urunlerDataGridView.Columns["urun_id"].Visible = false;
                urunlerDataGridView.Columns["kategori_id"].Visible = false;
                urunlerDataGridView.Columns["toptanci_id"].Visible = false;
            }
            else
            {
                urunlerDataGridView.DataSource = vt.Select(@"select u.urun_id,u.barkod Barkod,u.ad Ürün,u.fiyat Fiyat,s.adet,kategori_id,toptanci_id from tbl_urunler u
                                                            join tbl_stok s on u.urun_id=s.urun_id
                                                            where kategori_id='" + kategorilerDataGridView.SelectedRows[0].Cells["kategori_id"].Value + "'");

                urunlerDataGridView.Columns["urun_id"].Visible = false;
                urunlerDataGridView.Columns["kategori_id"].Visible = false;
                urunlerDataGridView.Columns["toptanci_id"].Visible = false;
            }
           
        }

        private void urunlerDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (urunlerDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            barkodTextBox.Text = urunlerDataGridView.SelectedRows[0].Cells["Barkod"].Value.ToString();
            urunTextBox.Text = urunlerDataGridView.SelectedRows[0].Cells["Ürün"].Value.ToString();
            fiyatTextBox.Text = urunlerDataGridView.SelectedRows[0].Cells["Fiyat"].Value.ToString();
        }

        private void urunEkleThinButton_Click(object sender, EventArgs e)
        {
            if (satisKontrol == 0)
            {
                DataTable udt = vt.Select("select adet from tbl_stok where urun_id='" + urunlerDataGridView.SelectedRows[0].Cells["urun_id"].Value + "'");
                DataTable udf = vt.Select("select fiyat from tbl_urunler where urun_id='" + urunlerDataGridView.SelectedRows[0].Cells["urun_id"].Value + "'");
                urunAdet = Convert.ToInt32(udt.Rows[0]["adet"]);
                urunFiyat = Convert.ToInt32(udf.Rows[0]["fiyat"]);
                if (urunAdetTextBox.Text == "")
                {
                    MessageBox.Show("Adeti Boş Bırakmayınız.");
                    return;
                }
                if (urunAdetTextBox.Text == "0")
                {
                    MessageBox.Show("Adetini 0 dan Büyük Giriniz.");
                    return;
                }
                if (Convert.ToInt32(urunAdetTextBox.Text) > urunAdet)
                {
                    MessageBox.Show("Stoktaki Ürün Adetiniz Yetersiz!");
                    return;
                }
                if (barkodTextBox.Text == "")
                {
                    MessageBox.Show("Eklenecek Ürünü Seçiniz.");
                    return;
                }

                DataTable dtSiparis = vt.Select("select IDENT_CURRENT ('tbl_siparis') ");
                DataTable dtTablo = vt.Select("select count(*) from tbl_siparis");

                if (Convert.ToInt32(dtTablo.Rows[0][0]) == 0)
                {
                    MessageBox.Show("Ürün Eklemeden Önce Sipariş Oluşturunuz.");
                    return;
                }



                int kayitSay = vt.UpdateDelete("insert into tbl_siparisDetay(adet,fiyat,urun_id,siparis_id) values('" + urunAdetTextBox.Text + "','" + Convert.ToInt32(urunAdetTextBox.Text) * urunFiyat + "','" + urunlerDataGridView.SelectedRows[0].Cells["urun_id"].Value + "','" + dtSiparis.Rows[0][0].ToString() + "')");

                urunAdet = urunAdet - Convert.ToInt32(urunAdetTextBox.Text);


                vt.UpdateDelete("update tbl_stok set adet='" + urunAdet + "' where urun_id='" + urunlerDataGridView.SelectedRows[0].Cells["urun_id"].Value + "'");

                if (kayitSay > 0)
                {
                    MessageBox.Show("Ürün eklendi..");
                    siparisGridYenile();
                    toplamHesapla();
                    urunlerGridYenile();
                    satisKontrol = 0;
                }
            }
            else
            {
                MessageBox.Show("Eski Siparişe Ürün Ekleyemezsiniz.");
            }
            

        }

        public void siparisEkle()
        {
            if (satisKontrol == 1)
            {
                if (personelDropdown.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen İşlem Yapacak Personeli Seçiniz.");
                    return;
                }
                if (musteriDropdown.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen işlem Yapılacak Müşteriyi Seçiniz.");
                    return;
                }

                DataTable vtOdeme = vt.Select("select odemeTur_id from tbl_odemeTur where odemeTur='Ödenmedi'");

                vt.UpdateDelete("insert into tbl_siparis(tutar,musteri_id,personel_id,odemeTur_id) values('0','" + musteriDropdown.SelectedValue + "','" + personelDropdown.SelectedValue + "','" + vtOdeme.Rows[0][0].ToString() + "')");


                siparisGridYenile();
                satisKontrol = 0;
                toplamTutarLabel.Text = "0";
                hangiMusteriLabel.Text = "";
                hangiPersonelLabel.Text = "";
            }
            else
            {
                DataTable dtKontrol = vt.Select("select count(*) from tbl_siparis");

                if (Convert.ToInt32(dtKontrol.Rows[0][0]) == 0)
                {
                    if (personelDropdown.SelectedIndex == -1)
                    {
                        MessageBox.Show("Lütfen İşlem Yapacak Personeli Seçiniz.");
                        return;
                    }
                    if (musteriDropdown.SelectedIndex == -1)
                    {
                        MessageBox.Show("Lütfen işlem Yapılacak Müşteriyi Seçiniz.");
                        return;
                    }

                    DataTable vtOdeme = vt.Select("select odemeTur_id from tbl_odemeTur where odemeTur='Nakit'");

                    vt.UpdateDelete("insert into tbl_siparis(tutar,musteri_id,personel_id,odemeTur_id) values('0','" + musteriDropdown.SelectedValue + "','" + personelDropdown.SelectedValue + "','" + vtOdeme.Rows[0][0].ToString() + "')");


                    siparisGridYenile();
                    satisKontrol = 0;
                    toplamTutarLabel.Text = "0";
                    hangiMusteriLabel.Text = "";
                    hangiPersonelLabel.Text = "";
                }
                else
                {
                    MessageBox.Show("Açık Olan Siparişiniz Bulunmakta");
                }
            }
        }


        void toplamHesapla()
        {
            toplamTutarLabel.Text = "..";
            for (int i = 0; i < siparisDetayDataGridView.Rows.Count; ++i)
            {
                toplam += Convert.ToInt32(siparisDetayDataGridView.Rows[i].Cells["Fiyat"].Value);
            }
            toplamTutarLabel.Text = toplam.ToString();
            veriToplam = toplam;
            toplam = 0;
        }

        private void SiparisOlusturThinButton_Click(object sender, EventArgs e)
        {
            siparisEkle();
        }

        private void musteriGuncelleThinButton_Click(object sender, EventArgs e)
        {
            DataTable dtKontrol = vt.Select("select count(*) from tbl_siparis");

            if (satisKontrol == 1)
            {
                MessageBox.Show("Satışı Yapılan Siparişin Müşterisini Değiştiremezsiniz.");
                return;
            }
            else
            {
                if (musteriDropdown.SelectedIndex == -1)
                {
                    MessageBox.Show("Güncellenecek Müşteriyi Seçiniz.");
                    return;
                }

                DataTable dtSiparis = vt.Select("select IDENT_CURRENT ('tbl_siparis')");
                DataTable dtTablo = vt.Select("select count(*) from tbl_siparis");

                if (Convert.ToInt32(dtTablo.Rows[0][0]) == 0)
                {
                    MessageBox.Show("Müşteri Güncellemek İçin Önce Sipariş Oluştunuz.");
                    return;
                }

                int kayitSay = vt.UpdateDelete("update tbl_siparis set musteri_id='" + musteriDropdown.SelectedValue + "' where siparis_id='"+dtSiparis.Rows[0][0].ToString()+"'");

                if (kayitSay > 0)
                {
                    MessageBox.Show("Müşteri Güncellendi");

                    siparisGridYenile();
                }
            }
        }

        private void personelGuncelleThinButton_Click(object sender, EventArgs e)
        {
            DataTable dtKontrol = vt.Select("select count(*) from tbl_siparis");

            if (satisKontrol == 1)
            {
                MessageBox.Show("Satışı Yapılan Siparişin Personelini Değiştiremezsiniz.");
                return;
            }
            else
            {
                if (personelDropdown.SelectedIndex == -1)
                {
                    MessageBox.Show("Güncellenecek Personeli Seçiniz.");
                    return;
                }

                DataTable dtSiparis = vt.Select("select IDENT_CURRENT ('tbl_siparis') ");
                DataTable dtTablo = vt.Select("select count(*) from tbl_siparis");

                if (Convert.ToInt32(dtTablo.Rows[0][0]) == 0)
                {
                    MessageBox.Show("Personel Güncellemek İçin Önce Sipariş Oluştunuz.");
                    return;
                }

                int kayitSay = vt.UpdateDelete("update tbl_siparis set personel_id='" + personelDropdown.SelectedValue + "' where siparis_id='" + dtSiparis.Rows[0][0].ToString() + "'");

                if (kayitSay > 0)
                {
                    MessageBox.Show("Personel Güncellendi");

                    siparisGridYenile();
                }
            }
        }
        private void siparisSilThinButton_Click(object sender, EventArgs e)
        {
            if (satisKontrol == 0)
            {
                if (siparisDetayDataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Silinecek ürünü seçiniz.");
                    return;
                }
                DialogResult cevap = MessageBox.Show("Ürün sepetten silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap != DialogResult.Yes)
                {
                    MessageBox.Show("Silme işlemi iptal edildi!");
                    return;
                }

                DataTable dtStokUrun = vt.Select("select adet from tbl_stok where urun_id='" + silinenUrun + "'");

                int stokUrun = Convert.ToInt32(dtStokUrun.Rows[0][0]);

                int guncelAdet = silinenAdet + stokUrun;


                int kayitSay = vt.UpdateDelete("delete from tbl_siparisDetay where siparisDetay_id=" + secilenUrun);

                if (kayitSay > 0)
                {
                    siparisGridYenile();
                    MessageBox.Show("ürün Silindi.");

                    vt.UpdateDelete("update tbl_stok set adet='" + guncelAdet + "' where urun_id='" + silinenUrun + "'");

                    DataTable dtSiparis = vt.Select("select IDENT_CURRENT ('tbl_siparis') ");

                    urunlerGridYenile();
                    toplamHesapla();
                    Temizle();
                    satisKontrol = 0;
                }
            }
            else
            {
                MessageBox.Show("Satışı Yapılan Siparişin İçerisinden Ürün Silemezsiniz!");
            }
            

        }
        
        private void siparisDetayDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (siparisDetayDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            secilenUrun = Convert.ToInt32(siparisDetayDataGridView.SelectedRows[0].Cells["siparisDetay_id"].Value);
            silinenUrun = Convert.ToInt32(siparisDetayDataGridView.SelectedRows[0].Cells["urun_id"].Value);
            silinenAdet = Convert.ToInt32(siparisDetayDataGridView.SelectedRows[0].Cells["Adet"].Value);

            

        }

        private void barkodEkleThinButton_Click(object sender, EventArgs e)
        {
            if (satisKontrol == 0)
            {
                DataTable dtUrun = vt.Select("select urun_id,fiyat from tbl_urunler where barkod='" + barkodAramaTextBox.Text + "'");

                int barkodluUrun = Convert.ToInt32(dtUrun.Rows[0]["urun_id"]);
                int barkodluFiyat = Convert.ToInt32(dtUrun.Rows[0]["fiyat"]);

                DataTable udt = vt.Select("select adet from tbl_stok where urun_id='" + barkodluUrun + "'");
                DataTable udf = vt.Select("select fiyat from tbl_urunler where urun_id='" + barkodluUrun + "'");
                urunAdet = Convert.ToInt32(udt.Rows[0]["adet"]);
                urunFiyat = Convert.ToInt32(udf.Rows[0]["fiyat"]);

                if (barkodAdetTextBox.Text == "")
                {
                    MessageBox.Show("Adet Girmelisiniz.");
                    return;
                }
                if (barkodAdetTextBox.Text == "0")
                {
                    MessageBox.Show("Adetini 0 dan Büyük Giriniz.");
                    return;
                }
                if (Convert.ToInt32(barkodAdetTextBox.Text) > urunAdet)
                {
                    MessageBox.Show("Stoktaki Ürün Adetiniz Yetersiz!");
                    return;
                }



                DataTable dtSiparis = vt.Select("select IDENT_CURRENT ('tbl_siparis') ");

                int kayitSay = vt.UpdateDelete("insert into tbl_siparisDetay(adet,fiyat,urun_id,siparis_id) values('" + barkodAdetTextBox.Text + "','" + (Convert.ToInt32(barkodAdetTextBox.Text) * barkodluFiyat) + "','" + barkodluUrun + "','" + dtSiparis.Rows[0][0].ToString() + "')");

                urunAdet = urunAdet - Convert.ToInt32(barkodAdetTextBox.Text);

                vt.UpdateDelete("update tbl_siparis set tutar='" + veriToplam + "' where  siparis_id='" + dtSiparis.Rows[0][0].ToString() + "'");

                vt.UpdateDelete("update tbl_stok set adet='" + urunAdet + "' where urun_id='" + barkodluUrun + "'");

                if (kayitSay > 0)
                {
                    MessageBox.Show("Ürün eklendi..");
                    siparisGridYenile();
                    toplamHesapla();
                    urunlerGridYenile();
                    barkodAramaTextBox.Text = "";
                    barkodAdetTextBox.Text = "";
                    satisKontrol = 0;
                }
            }
            else
            {
                MessageBox.Show("Satışı Yapılan Siparişe Ürün Ekleyemezsiniz.");
            }
            
        }

        private void siparisIptalThinButton_Click(object sender, EventArgs e)
        {
            DataTable dtSiparisId = vt.Select("select IDENT_CURRENT ('tbl_siparis') ");
            DataTable dtSiparisDetay = vt.Select("select count(*) from tbl_siparisDetay where siparis_id='"+ dtSiparisId.Rows[0][0].ToString() + "'");
            DataTable dtSiparisKontrol = vt.Select("select count(*) from tbl_siparis where siparis_id='" + dtSiparisId.Rows[0][0].ToString() + "'");
            if (Convert.ToInt32(dtSiparisKontrol.Rows[0][0]) == 0)
            {
                MessageBox.Show("Silinecek Sipariş Bulunmamakta.");
                return;
            }

            DialogResult cevap = MessageBox.Show("Sipariş Silinecektir! Devam etmek istiyor musunuz?", "Silme Uyarısı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap != DialogResult.Yes)
            {
                MessageBox.Show("Silme işlemi iptal edildi!");
                return;
            }

            if (Convert.ToInt32(dtSiparisDetay.Rows[0][0]) > 0)
            {
                MessageBox.Show("Sipariş İçerisinde Ürün Bulunmakta.");
                return;
            }

            int kayitSay = vt.UpdateDelete("delete from tbl_siparis where siparis_id='" + dtSiparisId.Rows[0][0].ToString()+ "'");

            if (kayitSay > 0)
            {
                MessageBox.Show("Kayıt Silindi.");
                Temizle();
                siparisGridYenile();
                personelLabel.Text = "";
                hangiMusteriLabel.Text = "";
            }

        }

        private void NakitOdemeButton_Click(object sender, EventArgs e)
        {
            if (satisKontrol == 0)
            {
                if (siparisDetayDataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("Satış Yapmak İçin Ürün Eklemelisiniz.");
                    return;
                }
                DataTable dtSiparis = vt.Select("select IDENT_CURRENT ('tbl_siparis') ");
                DataTable dtOdeme = vt.Select("select odemeTur_id from tbl_odemeTur where odemeTur='Nakit'");

                int kayitSay = vt.UpdateDelete("update tbl_siparis set tutar='" + veriToplam + "',odemeTur_id='" + dtOdeme.Rows[0][0].ToString() + "' where  siparis_id='" + dtSiparis.Rows[0][0].ToString() + "'");


                if (kayitSay > 0)
                {
                    MessageBox.Show("Satış Yapıldı.");
                    Temizle();
                    siparisGridYenile();
                    personelDropdown.SelectedIndex = 0;
                    musteriDropdown.SelectedIndex = 0;
                    satisKontrol = 1;
                }
            }
            else
            {
                MessageBox.Show("Satış Zaten Yapılmış.");
            }
            
        }

        private void krediKartiOdemeButton_Click(object sender, EventArgs e)
        {
            if (satisKontrol == 0)
            {
                if (siparisDetayDataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("Satış Yapmak İçin Ürün Eklemelisiniz.");
                    return;
                }
                DataTable dtSiparis = vt.Select("select IDENT_CURRENT ('tbl_siparis') ");
                DataTable dtOdeme = vt.Select("select odemeTur_id from tbl_odemeTur where odemeTur='Kredi Kartı'");

                int kayitSay = vt.UpdateDelete("update tbl_siparis set tutar='" + veriToplam + "',odemeTur_id='" + dtOdeme.Rows[0][0].ToString() + "' where  siparis_id='" + dtSiparis.Rows[0][0].ToString() + "'");


                if (kayitSay > 0)
                {
                    MessageBox.Show("Satış Yapıldı.");
                    Temizle();
                    siparisGridYenile();
                    personelDropdown.SelectedIndex = 0;
                    musteriDropdown.SelectedIndex = 0;
                    satisKontrol = 1;
                }
            }
            else
            {
                MessageBox.Show("Satış Zaten Yapılmış.");
            }
            
        }

        private void siparisListeleThinButton_Click(object sender, EventArgs e)
        {
            SiparisListele sprfrm = new SiparisListele();
            sprfrm.Show();
            this.Close();
        }

        private void barkodAramaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
