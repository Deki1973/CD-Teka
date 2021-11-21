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
using System.IO;

namespace CD_Teka
{



    public partial class Form1 : Form
    {
        //string strConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NewDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string strConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = 'C:\USERS\TAMNAVA\APPDATA\LOCAL\MICROSOFT\MICROSOFT SQL SERVER LOCAL DB\INSTANCES\MSSQLLOCALDB\NEWDB.MDF'; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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


        private void Form1_Load(object sender, EventArgs e)
        {

            comboBoxTrazi.Items.Add("Naziv albuma");
            comboBoxTrazi.Items.Add("Izvodjac");
            comboBoxTrazi.Items.Add("Datum izlaska");
            comboBoxTrazi.Items.Add("Izdavac");
            comboBoxTrazi.Items.Add("Pesma");
            comboBoxTrazi.Items.Add("Zanr");

            comboBoxTrazi.SelectedItem = "Naziv albuma";
            comboBoxTrazi2.Items.Add("Naziv albuma");
            comboBoxTrazi2.Items.Add("Izvodjac");
            comboBoxTrazi2.Items.Add("Datum izlaska");
            comboBoxTrazi2.Items.Add("Izdavac");
            comboBoxTrazi2.Items.Add("Pesma");
            comboBoxTrazi2.Items.Add("Zanr");

            comboBoxTrazi2.SelectedItem = "Naziv albuma";

            comboBoxZanrNov.Items.Add("Pop");
            comboBoxZanrNov.Items.Add("Rock");
            comboBoxZanrNov.Items.Add("Bluez");
            comboBoxZanrNov.Items.Add("Jazz");
            comboBoxZanrNov.Items.Add("Classic");
            comboBoxZanrNov.Items.Add("Disco");
            comboBoxZanrNov.Items.Add("Other");
            comboBoxZanrNov.SelectedItem = "Other";
            panelTrazi.Visible = false;

            Napuni_tabelu(0, null);
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
            sqlkon2.Close();//10092021
        }

        private void buttonObrisi_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text != null)
            {
                DialogResult odgovor;
                odgovor = MessageBox.Show("DA LI STE SIGURNI?", "UPOZORENJE!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (odgovor == DialogResult.Yes)
                {
                    string strKomanda = "DELETE FROM [dbo].[tabAlbumi] WHERE Id='" + textBoxID.Text + "'";
                    string strConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NewDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    SqlConnection sqlConnBrisi = new SqlConnection(strConnString);
                    SqlCommand sqCmd = new SqlCommand(strKomanda, sqlConnBrisi);
                    sqlConnBrisi.Open();
                    sqCmd.ExecuteNonQuery();
                    MessageBox.Show("ZAPIS OBRISAN");
                    Napuni_tabelu(0, null);
                    sqlConnBrisi.Close();//10092021
                }
            }
        }

        private void buttonSledeci_Click(object sender, EventArgs e)
        { bs1.MoveNext(); }

        private void button1_Click(object sender, EventArgs e)
        { bs1.MovePrevious(); }

        private void buttonPrvi_Click(object sender, EventArgs e)
        { bs1.MoveFirst(); }

        private void buttonPrethodni_Click(object sender, EventArgs e)
        { bs1.MovePrevious(); }

        private void buttonSnimi_Click(object sender, EventArgs e)
        {
            //Snima izmene sadrzaja tekst-polja.
            string strKomandaSnimiIzmene = "UPDATE tabAlbumi SET vchrIzvodjac=@parIzvodjac,vchrAlbum=@parAlbum,dateGodina=@parGodina,vchrIzdavac=@parIzdavac,vchrZanr=@parZanr WHERE Id='" + textBoxID.Text + "'";
            DateTime datumIzlaska = Convert.ToDateTime(textBoxGodina.Text);
            sqlkon1 = new SqlConnection(strConnString);
            SqlCommand sqCmd2 = new SqlCommand(strKomandaSnimiIzmene, sqlkon1);
            sqCmd2.Parameters.AddWithValue("@parIzvodjac", textBoxIzvodjac.Text.Trim());
            sqCmd2.Parameters.AddWithValue("@parAlbum", textBoxAlbum.Text.Trim());
            sqCmd2.Parameters.AddWithValue("@parGodina", datumIzlaska);
            sqCmd2.Parameters.AddWithValue("@parIzdavac", textBoxIzdavac.Text.Trim());
            sqCmd2.Parameters.AddWithValue("@parZanr", textBoxZanr.Text.Trim());
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
            if (panelNoviAlbum.Visible == true)
            { panelNoviAlbum.Visible = false; }
        }

        private void buttonUnesiNovAlbum_Click(object sender, EventArgs e)
        {
            string parIdNov = textBoxIDnov.Text.Trim();
            string parIzvodjacNov = textBoxIzvodjacNov.Text.Trim();
            string parAlbumNov = textBoxAlbumNov.Text.Trim();
            //Ovde treba ubaciti kod za preradu formata datuma iz dd.mm.gggg u mm.dd.gggg
            DateTime parGodinaNov = Convert.ToDateTime(textBoxDatumNov.Text.Trim());
            string parIzdavacNov = textBoxIzdavacNov.Text.Trim();
            string parZanrNov = comboBoxZanrNov.SelectedItem.ToString().Trim();
            MessageBox.Show(parZanrNov);
            string strKomandaNov = "INSERT INTO [dbo].[tabAlbumi] (Id,vchrIzvodjac,vchrAlbum,dateGodina,vchrIzdavac,vchrZanr) VALUES (@parIdNov,@parIzvodjacNov,@parAlbumNov,@parGodinaNov,@parIzdavacNov,@parZanrNov)";
            sqlkon1 = new SqlConnection(strConnString);
            SqlCommand sqlCmd1 = new SqlCommand(strKomandaNov, sqlkon1);
            sqlCmd1.Parameters.AddWithValue("@parIdNov", parIdNov);
            sqlCmd1.Parameters.AddWithValue("@parIzvodjacNov", parIzvodjacNov);
            sqlCmd1.Parameters.AddWithValue("@parAlbumNov", parAlbumNov);
            sqlCmd1.Parameters.AddWithValue("@parGodinaNov", parGodinaNov);
            sqlCmd1.Parameters.AddWithValue("@parIzdavacNov", parIzdavacNov);
            sqlCmd1.Parameters.AddWithValue("@parZanrNov", parZanrNov);
            try
            {
                sqlkon1.Open();
                sqlCmd1.ExecuteNonQuery();
                sqlkon1.Close();
                MessageBox.Show("Novi album je uspesno unet u tabelu.", "OBAVESTENJE", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            panelNoviAlbum.Visible = false;
            Napuni_tabelu(0, null);

        }


        private void textBoxGodina_TextChanged(object sender, EventArgs e)
        {
            //eliminisanje nula za vreme iz formata datuma
            string izmeni1 = textBoxGodina.Text;
            string izmeni2 = null;
            int i1 = izmeni1.IndexOf(" ");
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
            if (radioButtonNoOperator.Checked == true)
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
                        case "Zanr":
                            strKolonaZaPretragu = "vchrZanr"; break;

                        case "Datum izlaska":
                            //ovde treba dodati kod za preradu formata iz dd.mm.gggg u mm/dd/gggg.
                            string strPrivremMM = null;
                            string strPrivremDD = null;
                            string strPrivremGGGG = null;
                            strPrivremDD = textBoxUpit.Text.Substring(0, 2);
                            strPrivremMM = textBoxUpit.Text.Substring(3, 2);
                            strPrivremGGGG = textBoxUpit.Text.Substring(6, 4);
                            MessageBox.Show(strPrivremMM + "/" + strPrivremDD + "/" + strPrivremGGGG);
                            textBoxUpit.Text = strPrivremMM + "/" + strPrivremDD + "/" + strPrivremGGGG;
                            strKolonaZaPretragu = "dateGodina"; break;


                        default:
                            MessageBox.Show("NE POSTOJI ZADATI PARAMETAR.");
                            strKolonaZaPretragu = "vchrAlbum"; break;
                    }
                    Napuni_tabelu(1, strKolonaZaPretragu);
                }
                else
                {
                    string strIdAlbumaUpit = null;
                    //MessageBox.Show("PRETRAGA PO PESMI");
                    string strUpitPesma = "SELECT vchrIdAlbuma FROM [dbo].[tabPesme] WHERE vchrNazivPesme='" + textBoxUpit.Text.Trim() + "'";

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
                    Napuni_tabelu(2, strIdAlbumaUpit);
                    sqlkon2.Close();
                }
            }
            else
            {
                //Pretraga sa AND ili OR logickim operatorom
                MessageBox.Show("PRETRAGA SA OR ILI AND OPERATOROM.");
                string strUpit = null;
                string strKolonaZaPretragu1 = null;
                string strKolonaZaPretragu2 = null;


                string strIzabranaKolona1 = null;
                string strIzabranaKolona2 = null;
                strIzabranaKolona1 = comboBoxTrazi.SelectedItem.ToString();
                strIzabranaKolona2 = comboBoxTrazi2.SelectedItem.ToString();

                if (strIzabranaKolona1 != "Pesma" && strIzabranaKolona2 != "Pesma")
                {
                    MessageBox.Show("Izabrane su kolone " + strIzabranaKolona1 + " i " + strIzabranaKolona2);
                    switch (strIzabranaKolona1)
                    {
                        case "Naziv albuma":
                            strKolonaZaPretragu1 = "vchrAlbum"; break;
                        case "Izvodjac":
                            strKolonaZaPretragu1 = "vchrIzvodjac"; break;
                        case "Izdavac":
                            strKolonaZaPretragu1 = "vchrIzdavac"; break;
                        case "Zanr":
                            strKolonaZaPretragu1 = "vchrZanr"; break;

                        case "Datum izlaska":
                            //ovde treba dodati kod za preradu formata iz dd.mm.gggg u mm/dd/gggg.
                            string strPrivremMM = null;
                            string strPrivremDD = null;
                            string strPrivremGGGG = null;
                            strPrivremDD = textBoxUpit.Text.Substring(0, 2);
                            strPrivremMM = textBoxUpit.Text.Substring(3, 2);
                            strPrivremGGGG = textBoxUpit.Text.Substring(6, 4);
                            MessageBox.Show(strPrivremMM + "/" + strPrivremDD + "/" + strPrivremGGGG);
                            textBoxUpit.Text = strPrivremMM + "/" + strPrivremDD + "/" + strPrivremGGGG;
                            strKolonaZaPretragu1 = "dateGodina"; break;


                        default:
                            MessageBox.Show("NE POSTOJI ZADATI PARAMETAR.");
                            strKolonaZaPretragu1 = "vchrAlbum"; break;
                    }

                    switch (strIzabranaKolona2)
                    {
                        case "Naziv albuma":
                            strKolonaZaPretragu2 = "vchrAlbum"; break;
                        case "Izvodjac":
                            strKolonaZaPretragu2 = "vchrIzvodjac"; break;
                        case "Izdavac":
                            strKolonaZaPretragu2 = "vchrIzdavac"; break;
                        case "Zanr":
                            strKolonaZaPretragu2 = "vchrZanr"; break;

                        case "Datum izlaska":
                            //ovde treba dodati kod za preradu formata iz dd.mm.gggg u mm/dd/gggg.
                            string strPrivremMM = null;
                            string strPrivremDD = null;
                            string strPrivremGGGG = null;
                            strPrivremDD = textBoxUpit2.Text.Substring(0, 2);
                            strPrivremMM = textBoxUpit2.Text.Substring(3, 2);
                            strPrivremGGGG = textBoxUpit2.Text.Substring(6, 4);
                            MessageBox.Show(strPrivremMM + "/" + strPrivremDD + "/" + strPrivremGGGG);
                            textBoxUpit2.Text = strPrivremMM + "/" + strPrivremDD + "/" + strPrivremGGGG;
                            strKolonaZaPretragu2 = "dateGodina"; break;


                        default:
                            MessageBox.Show("NE POSTOJI ZADATI PARAMETAR.");
                            strKolonaZaPretragu2 = "vchrAlbum"; break;
                    }

                    MessageBox.Show(strKolonaZaPretragu1);
                    MessageBox.Show(strKolonaZaPretragu2);
                    string strOperator = null;
                    if (radioButtonAND.Checked == true) { strOperator = "AND"; }
                    if (radioButtonOR.Checked == true) { strOperator = "OR"; }

                    //Situacija 1: kada su obe kolone za pretragu iz roditeljske tabele



                    strUpit = "SELECT * FROM dbo.tabAlbumi WHERE " + strKolonaZaPretragu1 + "='" + textBoxUpit.Text.Trim() + "' " + strOperator + " " + strKolonaZaPretragu2 + "='" + textBoxUpit2.Text.Trim() + "'";
                    // string strUpit = "SELECT * FROM dbo.tabAlbumi WHERE vchrAlbum = 'Buvlja pijaca' OR vchrAlbum = 'Ako pridjes blize'";

                    // string strUpit = "SELECT * FROM [dbo].[tabAlbumi] WHERE (vchrAlbum='Buvlja pijaca' OR vchrAlbum='Ako pridjes blize')";
                    MessageBox.Show(strUpit);
                    sqlkon1 = new SqlConnection(strConnString);
                    sqlDa1 = new SqlDataAdapter(strUpit, sqlkon1);
                    dt1 = new DataTable();
                    sqlDa1.Fill(dt1);
                    bs1 = new BindingSource();
                    bn1 = new BindingNavigator(bs1);
                    bs1.DataSource = dt1;
                    if (dt1.Rows.Count == 0) { MessageBox.Show("NEMA TRAZENIH REZULTATA."); }
                    Resetuj_povezivanje_kontrola_sa_podacima();

                    dgv1.Columns[0].HeaderText = "Naziv pesme:";
                    sqlkon1.Close();

                }
                //////////////////////////////////////////
                //Situacija 2: kada su obe kolone za pretragu iz decje tabele

                if (strIzabranaKolona1 == "Pesma" && strIzabranaKolona2 == "Pesma")
                {
                    string strIdAlb1 = null;
                    string strIdAlb2 = null;
                    strKolonaZaPretragu1 = "vchrNazivPesme";
                    strKolonaZaPretragu2 = "vchrNazivPesme";
                    // string strUpitRodit = "SELECT * FROM dbo.tabPesme WHERE vchrNazivPesme='Berlin' OR vchrNazivPesme='Zagrli me'";
                    //string strUpitRodit = "SELECT * FROM dbo.tabAlbumi INNER JOIN dbo.tabPesme ON tabAlbumi.Id = tabPesme.vchrIdAlbuma";
                    //  string strUpitRodit = "SELECT * FROM dbo.tabAlbumi INNER JOIN dbo.tabPesme ON tabAlbumi.Id = tabPesme.vc";
                    string strUpitRodit = "select * from tabAlbumi inner join tabPesme on tabAlbumi.id=tabPesme.vchrIdAlbuma where vchrNazivPesme='Case lomim' OR vchrNazivPesme='Zagrli me'";
                    sqlkon2 = new SqlConnection(strConnString);
                    sqlDa2 = new SqlDataAdapter(strUpitRodit, sqlkon2);
                    dt2 = new DataTable();
                    sqlDa2.Fill(dt2);
                    dgv1.DataSource = dt2;
                    dgv1.DataMember = "";
                    dgv1.Width = 480;
                    //ako hoces da povezes texbox kontrole u gornjem delu forme sa dobijenim podacima, skini komentare sa redova ispod.
                    //  bs1 = new BindingSource();
                    //  bn1 = new BindingNavigator(bs1);
                    //  bs1.DataSource = dt2;
                    //  
                    //   try
                    //  {
                    //      textBoxID.DataBindings.Clear();
                    //      textBoxAlbum.DataBindings.Clear();
                    //      textBoxIzvodjac.DataBindings.Clear();
                    //      textBoxIzdavac.DataBindings.Clear();
                    //      textBoxGodina.DataBindings.Clear();
                    //      textBoxZanr.DataBindings.Clear();
                    //      textBoxID.DataBindings.Add("Text", bs1, "Id");
                    //      textBoxAlbum.DataBindings.Add("Text", bs1, "vchrAlbum");
                    //      textBoxIzvodjac.DataBindings.Add("Text", bs1, "vchrIzvodjac");
                    //      textBoxGodina.DataBindings.Add("Text", bs1, "dateGodina");
                    //      textBoxIzdavac.DataBindings.Add("Text", bs1, "vchrIzdavac");
                    //      textBoxZanr.DataBindings.Add("Text", bs1, "vchrZanr");
                    //  }
                    //  catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                }

                //Situacija 3: kada je jedna kolona za pretragu iz roditeljske a druga iz decije tabele
                //Ovo je najzanimljivija situacija jer se pretraga vrsi i po decijoj i po roditeljskoj tabeli
                //a rezultate treba prikazati kao objedinjeni dataset.

                if (strIzabranaKolona1 == "Pesma" ^ strIzabranaKolona2 == "Pesma")
                {
                    MessageBox.Show(strIzabranaKolona1 + ", " + strIzabranaKolona2);
                    string strUpitObe = "select * from dbo.tabAlbumi inner join dbo.tabPesme on tabPesme.vchrIdAlbuma = tabAlbumi.Id where vchrNazivPesme ='" + textBoxUpit.Text.Trim() + "' OR vchrZanr = '" + textBoxUpit2.Text.Trim() + "'";

                    sqlkon2 = new SqlConnection(strConnString);
                    sqlDa2 = new SqlDataAdapter(strUpitObe, sqlkon2);
                    dt2 = new DataTable();
                    sqlDa2.Fill(dt2);
                    dgv1.DataSource = dt2;
                    if (dt2.Rows.Count == 0) { MessageBox.Show("NEMA NISTA."); }
                    dgv1.DataMember = "";
                    dgv1.Width = 480;


                    //ako hoces da povezes texbox kontrole u gornjem delu forme sa dobijenim podacima, skini komentare sa redova ispod.
                    bs1 = new BindingSource();
                    bn1 = new BindingNavigator(bs1);
                    bs1.DataSource = dt2;

                    try
                    {
                        textBoxID.DataBindings.Clear();
                        textBoxAlbum.DataBindings.Clear();
                        textBoxIzvodjac.DataBindings.Clear();
                        textBoxIzdavac.DataBindings.Clear();
                        textBoxGodina.DataBindings.Clear();
                        textBoxZanr.DataBindings.Clear();
                        textBoxID.DataBindings.Add("Text", bs1, "Id");
                        textBoxAlbum.DataBindings.Add("Text", bs1, "vchrAlbum");
                        textBoxIzvodjac.DataBindings.Add("Text", bs1, "vchrIzvodjac");
                        textBoxGodina.DataBindings.Add("Text", bs1, "dateGodina");
                        textBoxIzdavac.DataBindings.Add("Text", bs1, "vchrIzdavac");
                        textBoxZanr.DataBindings.Add("Text", bs1, "vchrZanr");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Napuni_tabelu(0, null);
            panelTrazi.Visible = false;
            dgv1.Width = 240;
            //treba osveziti datasource za textbox kontrole
            panelSnimiPretragu.Visible = false;
        }

        private void panelTrazi_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Napuni_tabelu(int intOpcija, string strKolonaZaPretragu)
        {

            string strKomanda = "";
            sqlkon1 = new SqlConnection(strConnString);
            if (intOpcija == 1)
            {
                strKomanda = "SELECT * FROM [dbo].[tabAlbumi] WHERE " + strKolonaZaPretragu + "='" + textBoxUpit.Text.Trim() + "'";
            }
            if (intOpcija == 0)
            { strKomanda = "SELECT * FROM [dbo].[tabAlbumi] ORDER BY Id ASC"; }
            if (intOpcija == 2)
            { strKomanda = "SELECT * FROM [dbo].[tabAlbumi] WHERE Id='" + strKolonaZaPretragu + "'"; }

            sqlkon1 = new SqlConnection(strConnString);
            //strKomanda= "SELECT * FROM [dbo].[tabAlbumi] WHERE vchrAlbum='Buvlja pijaca' ORDER BY dateGodina ASC";
            //strKomanda = "SELECT * FROM [dbo].[tabAlbumi] ORDER BY Id ASC";
            sqlDa1 = new SqlDataAdapter(strKomanda, sqlkon1);
            dt1 = new DataTable();
            sqlkon1.Open();
            sqlDa1.Fill(dt1);
            if (dt1.Rows.Count == 0) { MessageBox.Show("NAO!"); }
            bs1 = new BindingSource();
            bn1 = new BindingNavigator(bs1);
            bs1.DataSource = dt1;

            //Osvezavanje kontrola
            Resetuj_povezivanje_kontrola_sa_podacima();

            dgv1.Columns[0].HeaderText = "Naziv pesme:";
            sqlkon1.Close();
        }

        private void textBoxGodina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((45 < e.KeyChar) && (e.KeyChar < 58) == false)
            { e.Handled = true; }

        }

        private void textBoxGodina_Validating(object sender, CancelEventArgs e)
        {



        }

        private void Resetuj_povezivanje_kontrola_sa_podacima()
        {
            try
            {
                textBoxID.DataBindings.Clear();
                textBoxAlbum.DataBindings.Clear();
                textBoxIzvodjac.DataBindings.Clear();
                textBoxIzdavac.DataBindings.Clear();
                textBoxGodina.DataBindings.Clear();
                textBoxZanr.DataBindings.Clear();
                textBoxID.DataBindings.Add("Text", bs1, "Id");
                textBoxAlbum.DataBindings.Add("Text", bs1, "vchrAlbum");
                textBoxIzvodjac.DataBindings.Add("Text", bs1, "vchrIzvodjac");
                textBoxGodina.DataBindings.Add("Text", bs1, "dateGodina");
                textBoxIzdavac.DataBindings.Add("Text", bs1, "vchrIzdavac");
                textBoxZanr.DataBindings.Add("Text", bs1, "vchrZanr");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            foreach (DataGridViewColumn co in dgv1.Columns)
            {
                co.HeaderText = string.Concat("Column ", co.Index.ToString());
            }
        }

        private void Napuni_tabelu_2(string strKolonaZaPretragu1, string strKolonaZaPretragu2, string strOperator)
        {
            MessageBox.Show(strKolonaZaPretragu1 + "\n" + strKolonaZaPretragu2 + "\n" + strOperator + "\n", "POZVANA PROCEDURA 2");
            if (textBoxUpit.Text == "" && textBoxUpit2.Text == "")
            {
                MessageBox.Show("Nisu zadate sve vrednosti.", "OOOPS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string strUpit = "SELECT * FROM [dbo].[tabAlbumi] WHERE " + strKolonaZaPretragu1 + "='" + textBoxUpit.Text + "' " + strOperator + " " + strKolonaZaPretragu2 + "='" + textBoxUpit2.Text + "'";
                MessageBox.Show(strUpit);
                sqlkon1 = new SqlConnection(strConnString);
                sqlDa1 = new SqlDataAdapter(strUpit, sqlkon1);
                dt1 = new DataTable();
                sqlDa1.Fill(dt1);
                bs1 = new BindingSource();
                bn1 = new BindingNavigator(bs1);
                bs1.DataSource = dt1;

                //Osvezavanje kontrola
                Resetuj_povezivanje_kontrola_sa_podacima();

                dgv1.Columns[0].HeaderText = "Naziv pesme:";
                sqlkon1.Close();

            }

        }

        private void btnIzvestaj_Click(object sender, EventArgs e)
        {
            //Form3 frm3 = new Form3();
            //frm3.Show();
            Form4 frm4 = new Form4();
            frm4.Show();


        }

        private void buttonKreirajFajl_Click(object sender, EventArgs e)
        {
            string stReader;
            sqlkon1 = new SqlConnection(strConnString);
            sqlkon1.Open();
            SqlCommand sqlCmdReport = new SqlCommand("SELECT * FROM [dbo].[tabAlbumi] ORDER BY Id ASC", sqlkon1);
            SqlDataReader rdReport = sqlCmdReport.ExecuteReader();
            while (rdReport.Read())
            {
                stReader = "";
                stReader += "ID: " + rdReport.GetValue(0) + "   ";
                stReader += "Izvodjac: " + rdReport.GetValue(1) + "   ";
                stReader += "Album: " + rdReport.GetValue(2) + "   ";
                stReader += "Godina: " + rdReport.GetValue(4) + "   ";
                stReader += "Izdavac: " + rdReport.GetValue(5) + "   ";
                stReader += "\r\n";
                textBoxFajl.Text += stReader + "\r\n";
            }

            rdReport.Close();

        }

        private void buttonSnimiFajl_Click(object sender, EventArgs e)
        {
            DialogResult odgovor;
            odgovor = MessageBox.Show("Da li hocete da snimite rezultate kao text fajl?", "SNIMANJE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (odgovor == DialogResult.No)
            {
                return;
            }
            else
            {//Snimi sadrzaj textBoxa u tekstualni fajl:
                FileStream fParam = new FileStream(@"C:\Users\Tamnava\Documents\Test1\TestFajl.txt", FileMode.Create, FileAccess.Write);
                StreamWriter m_writerParameter = new StreamWriter(fParam);
                m_writerParameter.BaseStream.Seek(0, SeekOrigin.End);
                m_writerParameter.Write(textBoxFajl.Text);
                m_writerParameter.Flush();
                m_writerParameter.Close();



            }


        }

        private void buttonSnimiRezultat_Click(object sender, EventArgs e)
        {
            if (panelSnimiPretragu.Visible == false) { panelSnimiPretragu.Visible = true; }
            string stReader;
            sqlkon1 = new SqlConnection(strConnString);
            sqlkon1.Open();
            SqlCommand sqlCmdReport = new SqlCommand("SELECT * FROM [dbo].[tabAlbumi] ORDER BY Id ASC", sqlkon1);
            SqlDataReader rdReport = sqlCmdReport.ExecuteReader();
            while (rdReport.Read())
            {
                stReader = "";
                stReader += "ID: " + rdReport.GetValue(0) + "   ";
                stReader += "Izvodjac: " + rdReport.GetValue(1) + "   ";
                stReader += "Album: " + rdReport.GetValue(2) + "   ";
                stReader += "Godina: " + rdReport.GetValue(4) + "   ";
                stReader += "Izdavac: " + rdReport.GetValue(5) + "   ";
                stReader += "\r\n";
                textBoxFajl.Text += stReader + "\r\n";
            }

            rdReport.Close();
            

            DialogResult odgovor;
            odgovor = MessageBox.Show("Da li hocete da snimite rezultate kao text fajl?", "SNIMANJE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (odgovor == DialogResult.No)
            {
                return;
            }
            else
            {//Snimi sadrzaj textBoxa u tekstualni fajl:
                FileStream fParam = new FileStream(@"C:\Users\Tamnava\Documents\Test1\TestFajl.txt", FileMode.Create, FileAccess.Write);
                StreamWriter m_writerParameter = new StreamWriter(fParam);
                m_writerParameter.BaseStream.Seek(0, SeekOrigin.End);
                m_writerParameter.Write(textBoxFajl.Text);
                m_writerParameter.Flush();
                m_writerParameter.Close();
            }
            panelSnimiPretragu.Visible = false;
        }
    }
    
    
}
