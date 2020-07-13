using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsContactos
{
    class DataAccessLayer
    {

        private SqlConnection conn = new SqlConnection("Data Source=LAPTOP-E7CQBJ8B;Initial Catalog=WinFormsContacts; Integrated Security=True");

        public void InsertContact(Contact c)
        {
            try
            {
                conn.Open();
                String query = @"INSERT INTO Contacts (FirstName, LastName, Phone, Address) 
                                 values (@FirstName, @LastName, @Phone, @Address) ";

                //1er. Forma de agregar un parametro.
                SqlParameter firstname = new SqlParameter();
                firstname.ParameterName = "@FirstName";
                firstname.Value = c.FirstName;
                firstname.DbType = System.Data.DbType.String;

                //2da. Forma de agregar un parametro.
                SqlParameter lastname = new SqlParameter("@LastName", c.LastName);
                SqlParameter phone = new SqlParameter("@Phone", c.Phone);
                SqlParameter address = new SqlParameter("@Address", c.Address);

                //Al final SqlCommand es lo que ejecuta todo.
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(firstname);
                command.Parameters.Add(lastname);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                //Devuelve un 0 cuando no ejecuta nada.
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Contact> GetContacts(string search = null)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                conn.Open();
                string query = @" SELECT Id, FirstName, LastName, Phone, Address 
                                  FROM Contacts ";

                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(search))
                {
                    query += @" WHERE FirstName LIKE @Search OR LastName LIKE @Search OR Phone LIKE @Search OR 
                                    Address LIKE @Search";

                    command.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }

                command.CommandText = query;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    contacts.Add(new Contact
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString()
                    });
                }

            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                conn.Close();
            }

            return contacts;
        }


        public void UpdateContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @" UPDATE Contacts 
                                   SET FirstName = @FirstName,
                                       LastName = @LastName,
                                       Phone = @Phone, 
                                       Address = @Address 
                                   WHERE Id = @Id ";

                SqlParameter id = new SqlParameter("@Id", contact.Id);
                SqlParameter firstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(id);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteContact(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contacts WHERE Id = @Id ";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
