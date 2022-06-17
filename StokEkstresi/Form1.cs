using System.Data;
using System.Data.SqlClient;

namespace StokEkstresi
{
    public partial class Form1 : Form
    {

        string connectionString = @"Server=SALTUK\MSSQLSALTUK;Database=Test;Trusted_Connection=True;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT DISTINCT MalKodu from STK";
            SqlCommand command = new SqlCommand(query, connection);
           
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["MalKodu"]);
            }
            connection.Close();


        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);

           
            string query = "[TariheGoreStok]";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@malkodu", listBox1.Text);
            command.Parameters.AddWithValue("@tarih1", Convert.ToInt32(dateTimePicker1.Value.ToOADate()));
            command.Parameters.AddWithValue("@tarih2", Convert.ToInt32(dateTimePicker2.Value.ToOADate()));

            DataTable table = new DataTable();
            table.Columns.Add("SiraNo", typeof(string));
            table.Columns.Add("IslemTur", typeof(short));
            table.Columns.Add("EvrakNo", typeof(string));
            table.Columns.Add("Tarih2", typeof(string));
            table.Columns.Add("GirisMiktar", typeof(int));
            table.Columns.Add("CikisMiktar", typeof(int));
            table.Columns.Add("Stok", typeof(int));


            connection.Open();

          

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                row["SiraNo"] = (string)reader["SiraNo"];
                row["IslemTur"] = reader["IslemTur"];
                row["EvrakNo"] = reader["EvrakNo"];
                row["Tarih2"] = reader["Tarih2"];
                row["GirisMiktar"] = reader["GirisMiktar"];
                row["CikisMiktar"] = reader["CikisMiktar"];
                row["Stok"] = reader["Stok"];


                table.Rows.Add(row);
            }
         
            connection.Close();
            command.Dispose();
            connection.Dispose();

            dataGridView1.DataSource = table;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      
    }
}