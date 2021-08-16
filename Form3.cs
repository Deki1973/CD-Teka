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
    public partial class Form3 : Form
    {
        private SqlConnection sqConnFrm3 = null;
        private SqlDataAdapter sqDaFrm3 = null;
        //  private SqlCommand sqCmdFrm2 = null;
        private SqlCommandBuilder sqCmdBldFrm3;
        private DataTable dtFrm3 = null;
        private BindingSource bsFrm3 = null;
        private string strConnStrFrm3 = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NewDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Form3()
        {
            InitializeComponent();
        }

        private void buttonSnimiPromene_Click(object sender, EventArgs e)
        {
            try
            {
                sqDaFrm3.Update(dtFrm3);
            }
            catch (Exception exceptionObj)
            {
                MessageBox.Show(exceptionObj.Message.ToString());
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
            sqConnFrm3 = new SqlConnection(strConnStrFrm3);
            sqConnFrm3.Open();
            string strUpitFrm3 = "SELECT * FROM [dbo].[tabAlbumi]";
            sqDaFrm3 = new SqlDataAdapter(strUpitFrm3, sqConnFrm3);
            dtFrm3 = new DataTable();
            sqDaFrm3.Fill(dtFrm3);
            sqCmdBldFrm3 = new SqlCommandBuilder(sqDaFrm3); //Obavezno
            bsFrm3 = new BindingSource();
            bsFrm3.DataSource = dtFrm3;
            dataGridView1.DataSource = bsFrm3;


            // dataGridView1.Columns[0].Visible = false;
            // dataGridView1.Rows[0].Visible = false;
                              

            dataGridView1.Columns[0].HeaderText = "ID albuma:";
            dataGridView1.Columns[1].HeaderText = "Izvodjac:";
            dataGridView1.Columns[2].HeaderText = "Naziv albuma:";
            dataGridView1.Columns[3].HeaderText = "Datum izlaska:";
            dataGridView1.Columns[4].HeaderText = "Izdavac:";
            sqConnFrm3.Close();



        }

        private void buttonObrisi_Click(object sender, EventArgs e)
        {
            DialogResult drGrid = MessageBox.Show("DA LI ZAISTE HOCETE DA OBRISETE OVAJ ZAPIS?", "UPOZORENJE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); ;


            if (drGrid == DialogResult.Yes)
            {
                try
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    sqDaFrm3.Update(dtFrm3);
                }
                catch (Exception exceptionObj)
                {
                    MessageBox.Show(exceptionObj.Message.ToString());
                }
            }
        }

        private void buttonGotovo_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();
        }
    }
}
