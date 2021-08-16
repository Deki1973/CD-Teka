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

namespace CD_Teka
{


   
    public partial class Form1 : Form
    {
        string strConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NewDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection sqlkon1;
        private SqlConnection sqlkon2;

        private SqlDataAdapter sqlDa1;
        private SqlDataAdapter sqlDa2;
        private BindingSource bs1;

        private BindingNavigator bn1;
        private DataTable dt1;
        private DataTable dt2;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Napuni_Tabelu()
        {
            string strKomanda = "";
            sqlkon1 = new SqlConnection(strConnString);
            //strKomanda= "SELECT * FROM [dbo].[tabAlbumi] WHERE vchrAlbum='Buvlja pijaca' ORDER BY dateGodina ASC";
            strKomanda = "SELECT * FROM [dbo].[tabAlbumi] ORDER BY Id ASC";
            sqlDa1 = new SqlDataAdapter(strKomanda, sqlkon1);
            dt1 = new DataTable();
            sqlkon1.Open();
            sqlDa1.Fill(dt1);
            bs1 = new BindingSource();
            bn1 = new BindingNavigator(bs1);
            bs1.DataSource = dt1;

            try
            {
                textBoxID.DataBindings.Clear();
                textBoxAlbum.DataBindings.Clear();
                textBoxIzvodjac.DataBindings.Clear();
                textBoxIzdavac.DataBindings.Clear();
                textBoxGodina.DataBindings.Clear();
                textBoxID.DataBindings.Add("Text", bs1, "Id");
                textBoxAlbum.DataBindings.Add("Text", bs1, "vchrAlbum");
                textBoxIzvodjac.DataBindings.Add("Text", bs1, "vchrIzvodjac");
                textBoxGodina.DataBindings.Add("Text", bs1, "dateGodina");
                textBoxIzdavac.DataBindings.Add("Text", bs1, "vchrIzdavac");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            foreach (DataGridViewColumn co in dgv1.Columns)
            {
                co.HeaderText = string.Concat("Column ", co.Index.ToString());
            }
            dgv1.Columns[0].HeaderText = "Naziv pesme:";

            
            sqlkon1.Close();
        }
        private void Napuni_Tabelu_2(string strKolonaZaPretragu)
        {
            string strKomanda = "";
            sqlkon1 = new SqlConnection(strConnString);
            strKomanda= "SELECT * FROM [dbo].[tabAlbumi] WHERE "+strKolonaZaPretragu+"='"+textBoxUpit.Text.Trim()+"'";
            sqlDa1 = new SqlDataAdapter(strKomanda, sqlkon1);
            dt1 = new DataTable();
            sqlkon1.Open();
            sqlDa1.Fill(dt1);
            bs1 = new BindingSource();
            bn1 = new BindingNavigator(bs1);
            bs1.DataSource = dt1;

            try
            {
                textBoxID.DataBindings.Clear();
                textBoxAlbum.DataBindings.Clear();
                textBoxIzvodjac.DataBindings.Clear();
                textBoxIzdavac.DataBindings.Clear();
                textBoxGodina.DataBindings.Clear();
                textBoxID.DataBindings.Add("Text", bs1, "Id");
                textBoxAlbum.DataBindings.Add("Text", bs1, "vchrAlbum");
                textBoxIzvodjac.DataBindings.Add("Text", bs1, "vchrIzvodjac");
                textBoxGodina.DataBindings.Add("Text", bs1, "dateGodina");
                textBoxIzdavac.DataBindings.Add("Text", bs1, "vchrIzdavac");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            foreach (DataGridViewColumn co in dgv1.Columns)
            {
                co.HeaderText = string.Concat("Column ", co.Index.ToString());
            }
            dgv1.Columns[0].HeaderText = "Naziv pesme:";

            sqlkon1.Close();
        }

        private void Napuni_Tabelu_3(string IdAlbuma)
        {
            string strKomanda = "";
            sqlkon1 = new SqlConnection(strConnString);
            strKomanda = "SELECT * FROM [dbo].[tabAlbumi] WHERE Id='" + IdAlbuma+ "'";
            sqlDa1 = new SqlDataAdapter(strKomanda, sqlkon1);
            dt1 = new DataTable();
            sqlkon1.Open();
            sqlDa1.Fill(dt1);
            bs1 = new BindingSource();
            bn1 = new BindingNavigator(bs1);
            bs1.DataSource = dt1;

            try
            {
                textBoxID.DataBindings.Clear();
                textBoxAlbum.DataBindings.Clear();
                textBoxIzvodjac.DataBindings.Clear();
                textBoxIzdavac.DataBindings.Clear();
                textBoxGodina.DataBindings.Clear();
                textBoxID.DataBindings.Add("Text", bs1, "Id");
                textBoxAlbum.DataBindings.Add("Text", bs1, "vchrAlbum");
                textBoxIzvodjac.DataBindings.Add("Text", bs1, "vchrIzvodjac");
                textBoxGodina.DataBindings.Add("Text", bs1, "dateGodina");
                textBoxIzdavac.DataBindings.Add("Text", bs1, "vchrIzdavac");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            foreach (DataGridViewColumn co in dgv1.Columns)
            {
                co.HeaderText = string.Concat("Column ", co.Index.ToString());
            }
            dgv1.Columns[0].HeaderText = "Naziv pesme:";

            sqlkon1.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxTrazi.Items.Add("Naziv albuma");
            comboBoxTrazi.Items.Add("Izvodjac");
            comboBoxTrazi.Items.Add("Godina izdanja");
            comboBoxTrazi.Items.Add("Izdavac");
            comboBoxTrazi.Items.Add("Pesma");
            
            comboBoxTrazi.SelectedItem = "Naziv albuma";
            panelTrazi.Visible = false;

            Napuni_Tabelu();

            
        }

        

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            sqlkon2 = new SqlConnection(strConnString);
            string strKomanda = "SELECT vchrNazivPesme FROM [dbo].[tabPesme] WHERE vchrIdAlbuma='" + textBoxID.Text + "'";
            sqlDa2 = new SqlDataAdapter(strKomanda, sqlkon1);
            dt2 = new DataTable();
            sqlkon2.Open();
            sqlDa2.Fill(dt2);
            dgv1.DataSource = dt2;
            dgv1.DataMember = "";
            dgv1.Columns[0].HeaderText = "Naziv pesme:";
            
        }

        
        private void buttonObrisi_Click(object sender, EventArgs e)
        {
            if(textBoxID.Text!=null)
            {
                DialogResult odgovor;
                odgovor=MessageBox.Show("DA LI STE SIGURNI?", "UPOZORENJE!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if(odgovor==DialogResult.Yes)
                {
                    string strKomanda = "DELETE FROM [dbo].[tabAlbumi] WHERE Id='" + textBoxID.Text + "'";
                    string strConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NewDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    SqlConnection sqlConnBrisi = new SqlConnection(strConnString);
                    SqlCommand sqCmd = new SqlCommand(strKomanda, sqlConnBrisi);
                    sqlConnBrisi.Open();
                    sqCmd.ExecuteNonQuery();
                    sqlConnBrisi.Close();
                    MessageBox.Show("ZAPIS OBRISAN") ;
                
                }          
            }
        }

              

        private void buttonSledeci_Click(object sender, EventArgs e)
        {
            bs1.MoveNext();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bs1.MovePrevious();
        }

        private void buttonPrvi_Click(object sender, EventArgs e)
        {
            bs1.MoveFirst();
        }

        private void buttonPrethodni_Click(object sender, EventArgs e)
        {
            bs1.MovePrevious();
        }

        private void buttonSnimi_Click(object sender, EventArgs e)
        {
            //Snima izmene sadrzaja tekst-polja.
            string strKomandaSnimiIzmene = "UPDATE tabAlbumi SET vchrIzvodjac=@parIzvodjac,vchrAlbum=@parAlbum,dateGodina=@parGodina,vchrIzdavac=@parIzdavac WHERE Id='"+textBoxID.Text+"'";
            DateTime datumIzlaska = Convert.ToDateTime(textBoxGodina.Text);
            sqlkon1 = new SqlConnection(strConnString);
            SqlCommand sqCmd2 = new SqlCommand(strKomandaSnimiIzmene, sqlkon1);
            sqCmd2.Parameters.AddWithValue("@parIzvodjac", textBoxIzvodjac.Text.Trim());
            sqCmd2.Parameters.AddWithValue("@parAlbum", textBoxAlbum.Text.Trim());
            sqCmd2.Parameters.AddWithValue("@parGodina", datumIzlaska);
            sqCmd2.Parameters.AddWithValue("@parIzdavac", textBoxIzdavac.Text.Trim());
            sqlkon1.Open();
            sqCmd2.ExecuteNonQuery();
            sqlkon1.Close();
        }

        private void buttonDodajAlbum_Click(object sender, EventArgs e)
        {
            if (panelNoviAlbum.Visible == false)
            { panelNoviAlbum.Visible = true; }
        }

        private void buttonOdustani_Click(object sender, EventArgs e)
        {
            if (panelNoviAlbum.Visible == true) { panelNoviAlbum.Visible = false; }
        }

        private void buttonUnesiNovAlbum_Click(object sender, EventArgs e)
        {
            string parIdNov = textBoxIDnov.Text.Trim();
            string parIzvodjacNov = textBoxIzvodjacNov.Text.Trim();
            string parAlbumNov = textBoxAlbumNov.Text.Trim();
            DateTime parGodinaNov = Convert.ToDateTime(textBoxDatumNov.Text.Trim());
            string parIzdavacNov = textBoxIzdavacNov.Text.Trim();
            string strKomandaNov = "INSERT INTO [dbo].[tabAlbumi] (Id,vchrIzvodjac,vchrAlbum,dateGodina,vchrIzdavac) VALUES (@parIdNov,@parIzvodjacNov,@parAlbumNov,@parGodinaNov,@parIzdavacNov)";
            sqlkon1 = new SqlConnection(strConnString);
            SqlCommand sqlCmd1 = new SqlCommand(strKomandaNov, sqlkon1);
            sqlCmd1.Parameters.AddWithValue("@parIdNov",parIdNov);
            sqlCmd1.Parameters.AddWithValue("@parIzvodjacNov",parIzvodjacNov);
            sqlCmd1.Parameters.AddWithValue("@parAlbumNov",parAlbumNov);
            sqlCmd1.Parameters.AddWithValue("@parGodinaNov",parGodinaNov);
            sqlCmd1.Parameters.AddWithValue("@parIzdavacNov",parIzdavacNov);
            try
            {
                sqlkon1.Open();
                sqlCmd1.ExecuteNonQuery();
                sqlkon1.Close();
                MessageBox.Show("Novi album je uspesno unet u tabelu.", "OBAVESTENJE", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            panelNoviAlbum.Visible = false;
            
        }


        private void textBoxGodina_TextChanged(object sender, EventArgs e)
        {
            //eliminisanje nula za vreme iz formata datuma
            string izmeni1 = textBoxGodina.Text;
            string izmeni2=null;
            int i1=izmeni1.IndexOf(" ");
            if (i1 > 0) { izmeni2 = izmeni1.Substring(0, i1); }
            else { izmeni2 = izmeni1; }
            
            textBoxGodina.Text = izmeni2;
        }

        
        private void buttonGridView_Click(object sender, EventArgs e)
        {         
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2(textBoxID.Text.Trim());
            frm2.Show();
            this.Hide();
           
        }

        private void buttonZadnji_Click(object sender, EventArgs e)
        {
            bs1.MoveLast();
        }

        private void buttonUgasiAplikaciju_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonTraziAlbum_Click(object sender, EventArgs e)
        {
            if (panelTrazi.Visible == false)
            { panelTrazi.Visible = true; }
            else { panelTrazi.Visible = false; }
        }

        private void buttonPokreniUpit_Click(object sender, EventArgs e)
        {
            
            string strKolonaZaPretragu = null;
            string strIzabranaKolona = comboBoxTrazi.SelectedItem.ToString();
            if (strIzabranaKolona != "Pesma")
            {
                switch (strIzabranaKolona)
                {
                    case "Naziv albuma":
                        strKolonaZaPretragu = "vchrAlbum"; break;
                    case "Izvodjac":
                        strKolonaZaPretragu = "vchrIzvodjac"; break;
                    case "Izdavac":
                        strKolonaZaPretragu = "vchrIzdavac"; break;
                    case "Godina izdanja":


                        strKolonaZaPretragu = "dateGodina"; break;


                    default:
                        MessageBox.Show("NE POSTOJI ZADATI PARAMETAR.");
                        strKolonaZaPretragu = "vchrAlbum"; break;
                }
                Napuni_Tabelu_2(strKolonaZaPretragu);
            }
            else {
                string strIdAlbumaUpit = null;
                //MessageBox.Show("PRETRAGA PO PESMI");
                 string strUpitPesma = "SELECT vchrIdAlbuma FROM [dbo].[tabPesme] WHERE vchrNazivPesme='" + textBoxUpit.Text.Trim() +"'";
                //string strUpitPesma = "SELECT vchrIdAlbuma FROM [dbo].[tabPesme] WHERE vchrNazivPesme='Zagrli me'";
                MessageBox.Show(strUpitPesma);
                sqlkon2 = new SqlConnection(strConnString);
                SqlCommand sqCmd2 = new SqlCommand(strUpitPesma, sqlkon2);
                sqlkon2.Open();
                SqlDataReader sqRdr2 = sqCmd2.ExecuteReader();
                while (sqRdr2.Read())
                {
                    //MessageBox.Show(sqRdr2[0].ToString());
                    strIdAlbumaUpit = sqRdr2[0].ToString();
                }
                MessageBox.Show(strIdAlbumaUpit);
                Napuni_Tabelu_3(strIdAlbumaUpit);
                sqlkon2.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Napuni_Tabelu();
            panelTrazi.Visible = false;
        }

        private void panelTrazi_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    
}
