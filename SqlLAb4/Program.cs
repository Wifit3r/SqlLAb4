using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Buhalteria
{
    class BuhDB
    {
        public BuhDB() { }
        SqlConnection connection = new SqlConnection(@"Data Source= ASUSVIVOBOOK\SQLEXPRESS; Initial Catalog = Buhalteriya; Integrated Security = True");

        public void OpenConection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            else
            {
                Console.WriteLine("Підключення уже встановлено");
            }
        }
        public void CloseConection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            else
            {
                Console.WriteLine("Немає підключення");
            }
        }
        public SqlConnection GetConection()
        {
            return connection;
        }

    }
    class Program
    {
        public static void Main()
        {
            BuhDB dB = new BuhDB();
            dB.OpenConection();
            string query = "SELECT * FROM Buhalteria_db";
            SqlCommand command = new SqlCommand(query, dB.GetConection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["surname"]}, {reader["salary"]}, {reader["position"]}, {reader["children_count"]}, {reader["experiance"]}");
            }
            reader.Close();

            Console.WriteLine("Введіть імя");
            string surnameToFind = Console.ReadLine();
            Console.WriteLine("Введіть посаду");
            string x = Console.ReadLine();
            Console.WriteLine("Введіть к-сть дітей");
            int y = Convert.ToInt32(Console.ReadLine());
            string query2 = $"SELECT * FROM Buhalteria_db WHERE surname = '{surnameToFind}'";
            string query3 = $"Select * FROM Buhalteria_db WHERE position = '{x}' AND children_count <='{y}'";
            
            SqlCommand coma = new SqlCommand(query2, dB.GetConection());
            reader = coma.ExecuteReader();
            Console.WriteLine("Знайдені збіжності");
            while (reader.Read())
            { 
                Console.WriteLine($"{reader["surname"]}, {reader["salary"]}, {reader["position"]}, {reader["children_count"]}, {reader["experiance"]}");
            }
            
            reader.Close ();
            SqlCommand command2 = new SqlCommand(query3, dB.GetConection());
            reader = command2.ExecuteReader();
            Console.WriteLine("Знайдені збіжності");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["surname"]}, {reader["salary"]}, {reader["position"]}, {reader["children_count"]}, {reader["experiance"]}");
            }
            dB.CloseConection();
        }
    }
}
            