using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            SimpleQuary();
            QueryWithParameter();
            Console.Read();
        }

        static void SimpleQuary() {
            try
            {
                //insert
                new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).Update(@"INSERT INTO Persons
              (Name, Address, Email, PhoneNumber, RegDate) VALUES('Chamith','Sri Lanka','iamchamith2@gmail.com','0718920205','" + DateTime.Today.ToShortDateString() + "');");
                Console.WriteLine("insert is success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }

            // select
            DataTable dt = new DataTable();
            try
            {
                dt = new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).SelectTable(@"SELECT Id, Name, Address, Email, PhoneNumber, RegDate FROM  Persons");
                foreach (DataRow item in dt.Rows)
                {
                    Console.WriteLine(string.Format("{0} \t {1} \t {2} \t {3} \t {4} \t {5}", item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(),
                        item[4].ToString(), item[5].ToString()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }
            Console.Read();
            //update
            try
            {
                new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).Update(@"UPDATE Persons SET Name = 'Ruwan', Email = 'ruwan@gmail.com' WHERE   (Id = '1')");
                Console.WriteLine("update is success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }
            // select
            try
            {
                dt = new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).SelectTable(@"SELECT Id, Name, Address, Email, PhoneNumber, RegDate FROM  Persons");
                foreach (DataRow item in dt.Rows)
                {
                    Console.WriteLine(string.Format("{0} \t {1} \t {2} \t {3} \t {4} \t {5}", item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(),
                        item[4].ToString(), item[5].ToString()));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }
            Console.Read();
            //delete
            try
            {
                new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).Update(@"DELETE FROM Persons WHERE (Id = 1)");
                Console.WriteLine("delete is success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }
            // select
            try
            {
                dt = new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).SelectTable(@"SELECT Id, Name, Address, Email, PhoneNumber, RegDate FROM  Persons");
                foreach (DataRow item in dt.Rows)
                {
                    Console.WriteLine(string.Format("{0} \t {1} \t {2} \t {3} \t {4} \t {5}", item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(),
                        item[4].ToString(), item[5].ToString()));

                    Console.Read();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }
        }

        static void QueryWithParameter() {
            try
            {
                //insert
                new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).Update(@"INSERT INTO Persons
              (Name, Address, Email, PhoneNumber, RegDate) VALUES(@Name,@Address,@Email,@PhoneNumber,@RegDate);",
               new List<SqlParameter>() {
                    new SqlParameter("Name","Chamith"),
                    new SqlParameter("Address","Sri Lanka"),
                    new SqlParameter("Email", string.Format("{0}@gmail.com",Guid.NewGuid().ToString())),
                    new SqlParameter("PhoneNumber", "12345678984"),
                    new SqlParameter("RegDate", DateTime.Today.ToShortDateString())
               });
                Console.WriteLine("insert is success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }

            // select
            DataTable dt = new DataTable();
            try
            {
                dt = new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).SelectTable(@"SELECT Id, Name, Address, Email, PhoneNumber, RegDate FROM  Persons WHERE Address = @Address",
                    new List<SqlParameter>() { new SqlParameter("@Address", "Sri Lanka") }
                    );
                foreach (DataRow item in dt.Rows)
                {
                    Console.WriteLine(string.Format("{0} \t {1} \t {2} \t {3} \t {4} \t {5}", item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(),
                        item[4].ToString(), item[5].ToString()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }
            Console.Read();
            //update
            try
            {
                new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).Update(@"UPDATE Persons SET Name = @Name, Email = @Email WHERE   (Id = @Id)",
                     new List<SqlParameter>() { new SqlParameter("@Name", "Gayan"),
                     new SqlParameter("Email",$"{Guid.NewGuid().ToString()}@gmail.com"),
                     new SqlParameter("Id","1")
                     }
                    );
                Console.WriteLine("update is success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }
            // select
            try
            {
                dt = new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).SelectTable(@"SELECT Id, Name, Address, Email, PhoneNumber, RegDate FROM  Persons");
                foreach (DataRow item in dt.Rows)
                {
                    Console.WriteLine(string.Format("{0} \t {1} \t {2} \t {3} \t {4} \t {5}", item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(),
                        item[4].ToString(), item[5].ToString()));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }
            Console.Read();
            //delete
            try
            {
                new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).Update(@"DELETE FROM Persons WHERE (Id = @Id)", 
                    new List<SqlParameter>() { new SqlParameter("Id", "2") });
                Console.WriteLine("delete is success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }
            // select
            try
            {
                dt = new HappyDbAccess.DbAccess(GlobleConfig.ConnectionString).SelectTable(@"SELECT Id, Name, Address, Email, PhoneNumber, RegDate FROM  Persons");
                foreach (DataRow item in dt.Rows)
                {
                    Console.WriteLine(string.Format("{0} \t {1} \t {2} \t {3} \t {4} \t {5}", item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(),
                        item[4].ToString(), item[5].ToString()));

                    Console.Read();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION = {0}", ex.Message);
            }

        }
    }
}
