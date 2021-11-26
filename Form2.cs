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
    public partial class Form2 : Form
    {
        private SqlConnection sqConnFrm2 = null;
        private SqlDataAdapter sqDaFrm2 = null;
      //  private SqlCommand sqCmdFrm2 = null;
        private SqlCommandBuilder sqCmdBldFrm2;
        private DataTable dtFrm2 = null;
        private BindingSource bsFrm2 = null;

        //private string strConnStrGrid = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NewDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private string strConnStrGrid = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = 'C:\USERS\TAMNAVA\APPDATA\LOCAL\MICROSOFT\MICROSOFT SQL SERVER LOCAL DB\INSTANCES\MSSQLLOCALDB\NEWDB.MDF'; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Form2(string strTextBox)
        {
            InitializeComponent();
            string strIdAlbuma = strTextBox;
            label1.Text = strIdAlbuma;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            sqConnFrm2 = new SqlConnection(strConnStrGrid);
            sqConnFrm2.Open();
            string strUpitFrm2 = "SELECT * FROM [dbo].[tabPesme] WHERE vchrIdAlbuma='"+label1.Text+"' ORDER BY vchrNazivPesme ASC";
            sqDaFrm2 = new SqlDataAdapter(strUpitFrm2, sqConnFrm2);
            //sqCmdBld = new SqlCommandBuilder(sqDaGrid);
            dtFrm2 = new DataTable();
            sqDaFrm2.Fill(dtFrm2);
            sqCmdBldFrm2 = new SqlCommandBuilder(sqDaFrm2);
            bsFrm2 = new BindingSource();
            bsFrm2.DataSource = dtFrm2;
            dataGridView1.DataSource = bsFrm2;


            // dataGridView1.Columns[0].Visible = false;
            // dataGridView1.Rows[0].Visible = false;


            dataGridView1.Columns[0].HeaderText = "ID albuma:";
            dataGridView1.Columns[1].HeaderText = "Naziv pesme:";
            sqConnFrm2.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sqDaFrm2.Update(dtFrm2);
            }
            catch (Exception exceptionObj)
            {
                MessageBox.Show(exceptionObj.Message.ToString());
            }
        }

        private void buttonObrisiZapis_Click(object sender, EventArgs e)
        {
            DialogResult drGrid = MessageBox.Show("DA LI ZAISTE HOCETE DA OBRISETE OVAJ ZAPIS?", "UPOZORENJE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); ;


            if (drGrid == DialogResult.Yes)
            {
                try
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    sqDaFrm2.Update(dtFrm2);
                }
                catch (Exception exceptionObj)
                {
                    MessageBox.Show(exceptionObj.Message.ToString());
                }
            }
        }

        private void buttonGotovo_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();frm1.Show();
            this.Hide();
            
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            MessageBox.Show("Kreiranje novog zapisa");
            int BrojZapisa = dataGridView1.Rows.Count;
           // dataGridView1.Rows[BrojZapisa].Cells[0].Value = "aaa";
            MessageBox.Show("Row="+BrojZapisa.ToString());
             dataGridView1.Rows[(BrojZapisa-2)].Cells[0].Value = label1.Text;
            //MessageBox.Show(sadrzaj);
            //MessageBox.Show(e.ToString());


        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("Cell enter: "+e.ColumnIndex.ToString());
        }
    }
}
