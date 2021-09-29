using System;
using System.Data.SqlClient;


namespace intermediateProj
{
    class Program
    {
        static void Main(string[] args)
        {
            string сonnectionString = @"Data Source=WIN-HFC12JL6G7P\SQLEXPRESS;Initial Catalog=Clients;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(сonnectionString);
            sqlConnection.Open();

            if ((sqlConnection.State == System.Data.ConnectionState.Open))
            {
                Console.WriteLine("БД готова к работе.");
            }
            else
            {
                throw new Exception("Бд не готова к работе");
            }
            Console.WriteLine("Please select command:\n1. Регистрация\n2. Поиск клиента по логину\n3. Создание анкеты \n4. Вход на личный кабинет");
            int a = Convert.ToInt32(Console.ReadLine());
            switch (a)
            {
                case 1:
                    Console.Write("Login: "); string login = Console.ReadLine();
                    Console.Write("FirstName: "); string firstName = Console.ReadLine();
                    Console.Write("LastName: "); string lastName = Console.ReadLine();
                    Console.Write("MiddleName: "); string middleName = Console.ReadLine();
                    Console.Write("Gender: "); string gender = Console.ReadLine();
                    Console.Write("BirthDate: "); string birthDate = Console.ReadLine();
                    CreateClients(sqlConnection, new Clients() { Login = login, FirstName = firstName, LastName = lastName, MiddleName = middleName, Gender = gender, BirthDate = birthDate });
                    break;
                case 2:
                start:
                    
                    Console.Write("Login: "); login = Console.ReadLine();
                    var client1 = new Clients();
                    client1 = FindClientByLogin(sqlConnection, login);
                    if (client1.Login == null)
                    {
                        Console.WriteLine("client not found"); sqlConnection.Close(); Console.Clear() ; sqlConnection.Open(); goto start;
                    }
                    else
                    {
                        Console.WriteLine($"ID:{client1.Id}," +
                    $"LOGIN: {client1.Login}," + $"" +
                    $"FIRSTNAME: {client1.FirstName}," +
                    $"LASTNAME: {client1.LastName}," +
                    $"MIDDLENAME: {client1.MiddleName}," +
                    $"GENDER: {client1.Gender}," +
                    $"BIRTHDATE: {client1.BirthDate}");
                    }
                    break;
                case 3:
                    

                    break;
                default:
                    break;
            }

            sqlConnection.Close();
        }

        private static Clients FindClientByLogin(SqlConnection sqlConnection, string login)
        {
            var sqlQuery = $"SELECT * FROM CLIENTS WHERE LOGIN ={login}";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var sqlReader = sqlCommand.ExecuteReader();
            Clients clients = new Clients();

            while (sqlReader.Read())
            {
                clients.Id = (int)sqlReader.GetValue(0);
                clients.Login = (string)sqlReader.GetValue(1);
                clients.FirstName = (string)sqlReader.GetValue(2);
                clients.LastName = (string)sqlReader.GetValue(3);
                clients.MiddleName = (string)sqlReader.GetValue(4);
                clients.Gender = (string)sqlReader.GetValue(5);
                clients.BirthDate = (string)sqlReader.GetValue(6);
                return clients;
            }
            return clients;

            sqlReader.Close();
        }

        private static void SelectAllClients(SqlConnection sqlConnection)
        {
            var sqlQuery = "SELECT * FROM CLIENTS";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                Console.WriteLine($"ID:{sqlReader.GetValue(0)}, LOGIN: {sqlReader.GetValue(1)}, FIRSTNAME: {sqlReader.GetValue(2)}, LASTNAME: {sqlReader.GetValue(3)}, MIDDLENAME: {sqlReader.GetValue(4)}, GENDER: {sqlReader.GetValue(5)}, BIRTHDATE: {sqlReader.GetValue(6)}");
            }
            sqlReader.Close();
        }
        static void CreateClients(SqlConnection sqlConnection, Clients clients)
        {
            var sqlQuery = $"INSERT INTO CLIENTS (Login,FirstName,LastName,MiddleName,Gender, BirthDate) VALUES ('{clients.Login}','{clients.FirstName}','{clients.LastName}','{clients.MiddleName}','{clients.Gender}','{clients.BirthDate}')";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Client added succesfuly");
            }
        }
    }
    public class Clients
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public int FormId { get; set; }
        public int CreditId { get; set; }
        public Clients() { }
        public Clients(string login,string firstName,string lastName,string middleName, string gender,string birthDate)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Gender = gender;
            BirthDate = birthDate;

        }

    }
}
