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
    public partial class Form5 : Form
    {
        string strConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = 'C:\USERS\TAMNAVA\APPDATA\LOCAL\MICROSOFT\MICROSOFT SQL SERVER LOCAL DB\INSTANCES\MSSQLLOCALDB\NEWDB.MDF'; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        private string tabelaAktuelna="";
        SqlDataAdapter sqda51;
        SqlDataAdapter sqda52;
        DataTable dtAlbumi = new DataTable();
        DataTable dtPesme = new DataTable();
        DataSet ds5 = new DataSet();
        string vrednostPolja = "";
        SqlCommandBuilder sqCmdBld51;
        SqlCommandBuilder sqCmdBld52;
        BindingSource bsFrm51;
        BindingSource bsFrm52;
        
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dset1.tabPesme' table. You can move, or remove it, as needed.

            SqlConnection sqlCon5 = new SqlConnection(strConnString);
            sqlCon5.Open();
            string strUpitParent = "SELECT * FROM [dbo].[tabAlbumi]";
            //SqlCommand sqCmd51 = new SqlCommand(strUpitParent,sqlCon5);
            string strUpitChild = "SELECT * FROM [dbo].[tabPesme]";
            //SqlCommand sqCmd52 = new SqlCommand(strUpitChild,sqlCon5);
            
            sqda51 = new SqlDataAdapter(strUpitParent, sqlCon5);
            sqda51.Fill(dtAlbumi);
            sqda52 = new SqlDataAdapter(strUpitChild, sqlCon5);
            sqda52.Fill(dtPesme);
            sqCmdBld51 = new SqlCommandBuilder(sqda51);
            bsFrm51 = new BindingSource();
            bsFrm51.DataSource = dtAlbumi;
            sqCmdBld52 = new SqlCommandBuilder(sqda52);
            bsFrm52 = new BindingSource();
            bsFrm52.DataSource = dtPesme;
            //dataGrid1.DataSource = bsFrm51;
            
            //JA POJMA NEMAM KAKO OVO RADI
            //ALI JE PRORADILO :LUDA:
            //BAR ZA CHILD TABELU


            ds5.Tables.Add(dtAlbumi);
            ds5.Tables.Add(dtPesme);

           DataRelation rel5 = new DataRelation("Relacija5", ds5.Tables[0].Columns[0], ds5.Tables[1].Columns[0], true);
          ds5.Relations.Add(rel5);

            dataGrid1.DataSource = ds5.Tables[0];
          
            sqlCon5.Close();
            tabelaAktuelna = "parent";


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
         
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string poruka = "";
            //MessageBox.Show(dataGrid1.CurrentRowIndex.ToString());
            poruka+=dataGrid1.CurrentCell.ColumnNumber.ToString();
            poruka+=dataGrid1.CurrentCell.RowNumber.ToString();
            poruka += dataGrid1.CurrentCell.ToString();

            //  ds5.Tables.Add(dtAlbumi); nije potrebno, zapamtio je
            //  ds5.Tables.Add(dtPesme);

            //  DataRelation rel5 = new DataRelation("Relacija5", ds5.Tables[0].Columns[0], ds5.Tables[1].Columns[0], true);
            //  ds5.Relations.Add(rel5);
            //ds5.Tables[1].Rows[dataGrid1.CurrentCell.RowNumber][dataGrid1.CurrentCell.ColumnNumber] = "Novi unos";
            
            MessageBox.Show(poruka);
            
        }

        private void dataGrid1_ParentChanged(object sender, EventArgs e)
        {
          MessageBox.Show("Parent changed");

        }

        private void dataGrid1_ContextMenuStripChanged(object sender, EventArgs e)
        {
           // MessageBox.Show("Context menu strip changed.");
        }

        private void dataGrid1_ParentRowsVisibleChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Parent rows visible changed.");
        }

        private void dataGrid1_Navigate(object sender, NavigateEventArgs ne)
        {
            MessageBox.Show("Navigate");

        }

        private void dataGrid1_DataSourceChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Data source changed.");
            string a=dataGrid1.DataSource.ToString();
            MessageBox.Show("New data source is "+a);

        }

        private void dataGrid1_BackButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("back button click");
            if (tabelaAktuelna == "child")
            {
                tabelaAktuelna = "parent";
            }

        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Col is " + dataGrid1.CurrentCell.ColumnNumber
            //+ ", Row is " + dataGrid1.CurrentCell.RowNumber
            //+ ", Value is " + dataGrid1[dataGrid1.CurrentCell]);
            vrednostPolja=(dataGrid1[dataGrid1.CurrentCell]).ToString();
            MessageBox.Show(vrednostPolja);

        }

        private void dataGrid1_Enter(object sender, EventArgs e)
        {
            var a = dataGrid1.CurrentRowIndex;
        }

        private void dataGrid1_MouseDown(object sender, MouseEventArgs e)
        {
            
            
                DataGrid myGrid = (DataGrid)sender;
                System.Windows.Forms.DataGrid.HitTestInfo hti;
                hti = myGrid.HitTest(e.X, e.Y);
                string message = "You clicked ";

                switch (hti.Type)
                {
                    case System.Windows.Forms.DataGrid.HitTestType.None:
                        message += "the background.";
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.Cell:
                        message += "cell at row " + hti.Row + ", col " + hti.Column;
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
                        message += "the column header for column " + hti.Column;
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.RowHeader:
                        message += "the row header for row " + hti.Row;
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.ColumnResize:
                        message += "the column resizer for column " + hti.Column;
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.RowResize:
                        message += "the row resizer for row " + hti.Row;
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.Caption:
                        message += "the caption";
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.ParentRows:
                        message += "the parent row";
                        break;
                }

            textBoxMessage.Text = message;
            }

        private void dataGrid1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {

            MessageBox.Show("data grid 1 key down");
        }

        private void dataGrid1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           
        
        }

        private void Form5_KeyDown(object sender, KeyEventArgs e)
        {
            textBox2.Text = e.KeyCode.ToString();
        }

        private void dataGrid1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Update dtPesme and dtAlbumi");
                sqda51.Update(dtAlbumi);
                sqda52.Update(dtPesme);
            }
            catch (Exception exceptionObj)
            {
                MessageBox.Show(exceptionObj.Message.ToString());
            }
        }

        private void dataGrid1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("DUPLI KLIK.");
        }
    }
}
