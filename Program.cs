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
            Console.WriteLine("Please select command:\n1. Регистрация\n2. Поиск клиента по логину\n3. Заполнять анкету для получение кредита \n4. Вход на личный кабинет");
            Console.Write("Выберите команду: "); int a = Convert.ToInt32(Console.ReadLine());
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
                        Console.WriteLine("Client not found"); sqlConnection.Close(); Console.Clear() ; sqlConnection.Open(); goto start;
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
                    con: Console.Write("По какому логину хотите создать анкету: "); var login1 = Console.ReadLine();
                    
                    var client2 = new Clients();
                    int ID = 0;
                    client2 = FindClientByLogin(sqlConnection, login1);
                    if (client2.Login == null)
                    {
                        Console.WriteLine("Client not found"); sqlConnection.Close();  sqlConnection.Open(); goto con;
                    }
                    else
                    {
                        login1 = client2.Login;
                        ID = client2.Id;
                       
                    }
                    Console.WriteLine();
                    Console.WriteLine("----------Заполнение анкеты для получение кредита----------");
                    int count = 0;
                    Console.Write("Введите ваш возраст: "); int age = Convert.ToInt32(Console.ReadLine());
                    if (age<=25)
                    {
                        count +=0;
                    }
                    else if (age<=35 && age >=26)
                    {
                        count += 1;
                    }
                    else if (age>=36 && age <= 62)
                    {
                        count += 2;
                    }
                    else
                    {
                        count += 1;
                    }
                    Console.Write("Введите ваш доход на месяц: "); decimal income = Convert.ToDecimal(Console.ReadLine());
                    if (income<=1000m)
                    {
                        count += 1;
                    }
                    else
                    {
                        count += 2;
                    }
                    Console.Write("На какую сумму хотите брать кредит: "); decimal creditSum = Convert.ToDecimal(Console.ReadLine());
                    var r = (creditSum / income) * 100;
                    Console.WriteLine($"Сумма кредита от общего дохода составляет: {r}%");
                    if (r<80)
                    {
                        count += 4;
                    }
                    else if (r>=80 && r< 150)
                    {
                        count += 3;
                    }
                    else if (r>=150 && r<250)
                    {
                        count += 2;
                    }
                    else
                    {
                        count += 1;
                    }
                    Console.Write("Сколько закрытых кредитных историй у ввас на счету: "); var creditHistory = Convert.ToInt32(Console.ReadLine());
                    if (creditHistory>=3)
                    {
                        count += 2;
                    }
                    else if (creditHistory>=1 && creditHistory <=2)
                    {
                        count += 1;
                    }
                    else
                    {
                        count -= 1;
                    }
                    Console.Write("Скалько раз просрочки в вашей кредитной истории: ");var overdueCredit = Convert.ToInt32(Console.ReadLine());
                    if (overdueCredit>7)
                    {
                        count -= 3;
                    }
                    else if (overdueCredit>=5 && overdueCredit<=7)
                    {
                        count -= 2;
                    }
                    else if (overdueCredit == 4)
                    {
                        count -= 1;
                    }
                    else
                    {
                        count += 0;
                    }
                    Console.Write("Напишите цель кредита: "); var purposeCredit = Console.ReadLine();
                    Console.Write("Цель кредита: \n1.(Бытовая техника) \n2.(Ремонт) \n3.(Телефон) \n(Прочее-> любая кнопка) \n Выберите один: ");var p = Convert.ToInt32(Console.ReadLine());
                    switch (p)
                    {
                        case 1:
                            
                            count += 2;
                            break;
                        case 2:
                           
                            count += 1;
                            break;
                        case 3:
                            
                            count += 0;
                            break;
                        default:
                           
                            count -= 1;
                            break;
                    }
                    Console.Write("На сколько месяцев хотите взять кредит: "); var termCredit = Convert.ToInt32(Console.ReadLine());
                    if (termCredit>12)
                    {
                        count += 2;
                    }else
                    {
                        count += 2;
                    }
                    Console.WriteLine(count);
                    if (count>11)
                    {
                        Console.WriteLine("Поздравляем ваша заявка одобрена");
                        try
                        {
                            CreateForm(sqlConnection, ID, new Form() { Age = age, Income = income, CreditSum = creditSum, LoanAmountFromIncome = (int)r, CreditHistory = creditHistory, OverdueCredit = overdueCredit, PurposeCredit = purposeCredit, TermCredit = termCredit, CreatedAt = "2021-09-30", ClientId = ID });

                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message); ;
                        }
                    }
                    else
                    {
                        Console.WriteLine("К сожелению ваша заявка не одобрена");
                    }
                    
                    
                    break;
                default:
                    break;
            }

            sqlConnection.Close();
        }

        private static void CreateForm(SqlConnection sqlConnection, int id, Form form)
        {
            var sqlQuery = $"INSERT INTO FORME ( Age,Income,CreditSum,LoanAmountFromIncome,CreditHistory, OverdueCredit, PurposeCredit, TermCredit, CreatedAt, ClientId) VALUES" +
                $" ({form.Age},{form.Income},{form.CreditSum},{form.LoanAmountFromIncome},{form.CreditHistory},{form.OverdueCredit},'{form.PurposeCredit}',{form.TermCredit},'{form.CreatedAt}',{id})";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Form accepted for processing");
            }
            
            
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
                sqlReader.Close();

                return clients;
            }
            sqlReader.Close();
            return clients;
           
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
    public class Form
    {
        public int Age { get; set; }
        public decimal Income { get; set; }
        public decimal CreditSum { get; set; }
        public int LoanAmountFromIncome { get; set; }
        public int CreditHistory { get; set; }
        public int OverdueCredit { get; set; }
        public string PurposeCredit { get; set; }
        public int TermCredit { get; set; }
        public string CreatedAt { get; set; }
        public int ClientId { get; set; }
        
        public Form() { }
        public Form(int age,decimal income,decimal creditSum,int loanAmountFromIncome,int creditHistory,int overdueCredit,string purposeCredit,int termCredit,string createdAt,int clientId)
        {
            Age = age;
            Income = income;
            CreditSum = creditSum;
            LoanAmountFromIncome = loanAmountFromIncome;
            CreditHistory = creditHistory;
            OverdueCredit = overdueCredit;
            PurposeCredit = purposeCredit;
            TermCredit = termCredit;
            CreatedAt = createdAt;
            ClientId = clientId;
        }
    }

}
