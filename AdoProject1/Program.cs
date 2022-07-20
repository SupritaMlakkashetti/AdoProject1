using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AdoProject1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.Connection();
            Console.ReadLine();
        }

       static void Connection()
        {
            string cs = "Data Source=medge168;Initial Catalog=Ado1;User Id=sa;Password=pass@word1;";
            SqlConnection sqlConnection = new SqlConnection(cs);
            try
            {
                sqlConnection.Open();

                if (sqlConnection.State == ConnectionState.Open)
                {
                    Console.WriteLine("open");
                    Console.WriteLine("enter name and id");
                    string name = Console.ReadLine();
                    string id = Console.ReadLine();
                    // int id = Convert.ToInt32(Console.ReadLine());
                    SqlCommand sqlCommand1 = new SqlCommand("insert into info values(@name,@id)", sqlConnection);
                    //SqlCommand sqlCommand1 = new SqlCommand("update info set firstname=@name where id=@id",sqlConnection);
                  //  SqlCommand sqlCommand1 = new SqlCommand("delete from info where id=@id", sqlConnection);
                    sqlCommand1.CommandType = CommandType.Text;
                    sqlCommand1.Parameters.AddWithValue("@name", name);
                    sqlCommand1.Parameters.AddWithValue("@id", id);
                    int i = sqlCommand1.ExecuteNonQuery();
                    Console.WriteLine(i + "rows affected");

                    SqlCommand sqlCommand = new SqlCommand("spout", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sql = sqlCommand.ExecuteReader();
                    Console.WriteLine("id\tname");
                    while (sql.Read())
                    {
                        Console.WriteLine(sql["id"] + "\t" + sql["firstname"]);
                    }
                } 
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
