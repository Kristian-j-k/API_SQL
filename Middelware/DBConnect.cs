using API_SQL.Models;
using Microsoft.Data.SqlClient;

namespace API_SQL
{
    public class DBConnect

    {
        public SqlConnectionStringBuilder ConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "tcp:sweden.database.windows.net";
            builder.UserID = "test_db_gæs";
            builder.Password = "12ahkk##";
            builder.InitialCatalog = "test_1";

            return builder;
        }


        public int SQLLatest(int UserID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString().ConnectionString))
                {  
                    Console.WriteLine("SQLLatest\n");

                    connection.Open();


                    String sqlTableName = "SELECT TableName FROM USERS WHERE id = '" + UserID + "'";
                    string CardSaleTable_UserID = null;
                    using (SqlCommand command = new SqlCommand(sqlTableName, connection))
                    {
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CardSaleTable_UserID = reader.GetString(0);
                                Console.WriteLine(CardSaleTable_UserID);
                            }
                            
                        }
                    }
                    String sql = "SELECT Max(companyTraceNo) FROM "+ CardSaleTable_UserID;

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        int last = -1;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try {
                                    last = reader.GetInt32(0);
                                }
                                catch {
                                    return -1;
                                }

                            }
                            Console.WriteLine(last);
                            return last;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }

        }

   

        public int SQLGetUser(int UserID, string Password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString().ConnectionString))
                {
                    Console.WriteLine("SQLGetUser\n");

                    connection.Open();

                    String sql = "SELECT id FROM USERS WHERE CompanyNo = "+UserID+" AND Password = "+Password+" ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        int temp = -1;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                temp = reader.GetInt32(0);
                                Console.WriteLine(temp);
                            }
                            return temp;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }
        }

        public int SQLNewUser(int UserID, string Password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString().ConnectionString))
                {
                    Console.WriteLine("SQLNewUser\n");
                    connection.Open();

                    String sql = "INSERT INTO users (CompanyNo, Password) VALUES("+UserID+","+Password+")";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader());
                    }

                    
                    int newId = SQLGetUser(UserID, Password);

                    string newCardsaleTable = "Cardsale_"; 
                    newCardsaleTable += newId;
                    Console.WriteLine("Create "+ newCardsaleTable);
                    String sqlTableName = "update users SET TableName = '"+ newCardsaleTable +"' WHERE id = '"+ newId +"'";
                    using (SqlCommand command = new SqlCommand(sqlTableName, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader());
                    }

                    String sqlTable = "create table "+ newCardsaleTable + " (";
                    sqlTable += "id int not null identity(1, 1) primary key,";
                    sqlTable += "ProductName varchar(255),";
                    sqlTable += "InvoiceUnitPrice float,";
                    sqlTable += "Quantity float,";
                    sqlTable += "ServerSubTotal float,";
                    sqlTable += "ServerTimestamp datetime,";
                    sqlTable += "ServerUnitPrice float,";
                    sqlTable += "SiteName varchar(255),";
                    sqlTable += "SiteNo float,";
                    sqlTable += "TerminalTimestamp datetime,";
                    sqlTable += "Latitude float,";
                    sqlTable += "Longitude float,";
                    sqlTable += "BiTimestamp datetime default(GETDATE()),";
                    sqlTable += "CompanyTraceNo int";
                    sqlTable += ")";

                    using (SqlCommand command = new SqlCommand(sqlTable, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader());
                    }

                    return newId;
                }
                
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }

        }

        public string GetTheUsersTable(int UserID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString().ConnectionString))
                {
                    Console.WriteLine("SQLGetTheUsersTable\n");

                    connection.Open();

                    String sql = "SELECT TableName FROM USERS WHERE id = " + UserID ;

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        string temp = "";

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                temp = reader.GetString(0);
                                
                            }
                            return temp;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }

        
        public List<CardSaleTransactions>? SQLGetCardSaleTransactions(string table)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString().ConnectionString))
                {
                    Console.WriteLine("SQLGetCardSaleTransactions\n");
                    Console.WriteLine("=========================================\n");

                    connection.Open();
                    
                    String sql = "SELECT * FROM "+ table;
            
   
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {            
         
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<CardSaleTransactions> res = new List<CardSaleTransactions>();

                            while (reader.Read())
                            {

                                CardSaleTransactions c = new CardSaleTransactions();
                                c.ProductName = (string)reader.GetValue(1);
                                c.InvoiceUnitPrice = (double)reader.GetValue(2);
                                c.Quantity = (double)reader.GetValue(3);
                                c.ServerSubTotal = (double)reader.GetValue(4);
                                c.ServerTimestamp = (string)reader.GetValue(5);
                                c.ServerUnitPrice = reader.GetDouble(6);
                                c.SiteName = (string)reader.GetValue(7);
                                c.SiteNo = reader.GetDouble(8);
                                c.TerminalTimestamp = (string)reader.GetValue(9);
                                c.Latitude = (double)reader.GetValue(10);
                                c.Longitude = (double)reader.GetValue(11);
                                c.BiTimestamp = (string)reader.GetValue(12);

                                res.Add(c);
                                
                              
                            }
                            Console.WriteLine(res);

                            return res;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }


     
        public void INSERTCardSaleTransactions( string table, CardSaleTransactions ct )
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString().ConnectionString))
                {
                    Console.WriteLine("SQLGetCardSaleTransactions\n");
                    Console.WriteLine("=========================================\n");

                    
                
                    Console.WriteLine("writing to "+table);
                 
                    string into = "";
                    string values = "";

                    into += "ProductName"; into += ", "; values += "'" + ct.ProductName + "'"; values += ", ";
                    into += "InvoiceUnitPrice"; into += ", "; values += ct.InvoiceUnitPrice; values += ", ";
                    into += "Quantity"; into += ", "; values += ct.Quantity; values += ", ";
                    into += "ServerSubTotal"; into += ", "; values += ct.ServerSubTotal; values += ", ";
                    into += "ServerTimestamp"; into += ", "; values += "'"+ ct.ServerTimestamp+"'"; values += ", ";
                    into += "ServerUnitPrice"; into += ", "; values += ct.ServerUnitPrice; values += ", ";
                    into += "SiteName"; into += ", "; values += "'" + ct.SiteName + "'"; values += ", ";
                    into += "SiteNo"; into += ", "; values += ct.SiteNo; values += ", ";
                    into += "TerminalTimestamp"; into += ", "; values += "'"+ct.TerminalTimestamp+"'"; values += ", ";
                    into += "Latitude"; into += ", "; values += ct.Latitude; values += ", ";
                    into += "Longitude"; into += ", "; values += ct.Longitude; values += ", ";
                    into += "BiTimestamp"; values += "'"+ct.BiTimestamp+"'";
                  

                    connection.Open();

                    String sql = "INSERT INTO "+ table + " ("+into+") VALUES("+values+")"; 
                    Console.WriteLine(sql);

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {}
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}