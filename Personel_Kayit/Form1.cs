using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Personel_Kayit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-6CRR4TF\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");
        public void temizle()
        {
            txtid.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            cmbSehir.Text = "";
            mskMaas.Text = "";
            radioButton1.Checked = false;
            radioButton1.Checked = false;
            txtMeslek.Text = "";
        }
        public void listele()
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'personelVeriTabaniDataSet.Tbl_Personel' table. You can move, or remove it, as needed.
            listele();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT distinct(PerSehir) FROM Tbl_Personel",baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbSehir.Items.Add(dr[0].ToString());
            }
            baglanti.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Personel (PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek,PerDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)",baglanti);
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",cmbSehir.Text);
            komut.Parameters.AddWithValue("@p4",mskMaas.Text);
            komut.Parameters.AddWithValue("@p5",txtMeslek.Text);
            komut.Parameters.AddWithValue("@p6",label8.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Eklendi!!");
            listele();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true)
            {
                label8.Text = "True";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked==true)
            {
                label8.Text = "False";
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text= dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text= dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbSehir.Text= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskMaas.Text= dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label8.Text= dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtMeslek.Text= dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text=="True")
            {
                radioButton1.Checked = true;
            }
            if (label8.Text=="False")
            {
                radioButton2.Checked = true;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("Delete From Tbl_Personel where Perid=@p1",baglanti);
            komutsil.Parameters.AddWithValue("@p1",txtid.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Silindi");
            listele();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("Update Tbl_Personel set PerAd=@p1,PerSoyad=@p2,PerSehir=@p3,PerMaas=@p4,PerDurum=@p5,PerMeslek=@p6 where Perid=@p7",baglanti);
            komutguncelle.Parameters.AddWithValue("@p1",txtAd.Text);
            komutguncelle.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komutguncelle.Parameters.AddWithValue("@p3",cmbSehir.Text);
            komutguncelle.Parameters.AddWithValue("@p4",mskMaas.Text);
            komutguncelle.Parameters.AddWithValue("@p5",label8.Text);
            komutguncelle.Parameters.AddWithValue("@p6",txtMeslek.Text);
            komutguncelle.Parameters.AddWithValue("@p7",txtid.Text);
            komutguncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Güncellendi");
            listele();
        }

        private void btnİstatistik_Click(object sender, EventArgs e)
        {
            Frmistatistik frm = new Frmistatistik();
            frm.Show();
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafik frm = new FrmGrafik();
            frm.Show();
        }
    }
}
