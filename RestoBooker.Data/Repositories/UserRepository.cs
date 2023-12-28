using Restobooker.Domain.Interfaces;
using Restobooker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;

namespace RestoBooker.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string connectionString;
        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public User AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertQuery = @"
            INSERT INTO [User] (Name, ContactInfoId, LocationId)
            VALUES (@Name, @ContactInfoId, @LocationId);
            SELECT SCOPE_IDENTITY();"; // Dit retourneert de ID van de ingevoegde rij

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Name", user.Name);
                    insertCommand.Parameters.AddWithValue("@ContactInfoId", user.ContactInfo.ContactInfoID);
                    insertCommand.Parameters.AddWithValue("@LocationId", user.Location.LocationID);

                    connection.Open();

                    // Voer de insert-query uit en haal de geïnsereerde UserId op
                    int userId = Convert.ToInt32(insertCommand.ExecuteScalar());

                    // Haal de gegevens van de ingevoegde gebruiker op met behulp van de UserId
                    string selectQuery = @"
                SELECT UserId, Name, ContactInfoId, LocationId
                FROM [User]
                WHERE UserId = @UserId";

                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Bouw het User object op basis van de gelezen gegevens
                                return new User
                                {
                                    CustomerId = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    ContactInfo = user.ContactInfo,
                                    Location = user.Location
                                };
                            }
                        }
                    }
                }
            }

            // Geef null terug als er geen gebruiker gevonden wordt of bij een fout
            return null;
        }



        public void DeleteUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            UPDATE [User]
            SET DeletedAt = GETDATE()
            WHERE UserId = @UserId
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public User GetUserById(int id)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT u.UserId, u.Name, ci.PhoneNumber, ci.Email, l.Postcode, l.MunicipalityName, l.StreetName, l.HouseNumberLabel
            FROM [User] u
            LEFT JOIN ContactInfo ci ON u.ContactInfoId = ci.ContactInfoId
            LEFT JOIN Location l ON u.LocationId = l.LocationId
            WHERE u.UserId = @UserId AND u.DeletedAt IS NULL
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Haal de gegevens uit de database en maak een nieuwe User met ContactInfo en Location
                            ContactInfo ci = new ContactInfo(reader.GetString(2), reader.GetString(3));
                            Location l = new Location(reader.GetInt32(4), reader.GetString(5), reader.IsDBNull(6) ? null : reader.GetString(6), reader.IsDBNull(7) ? null : reader.GetString(7));

                            user = new User
                            {
                                CustomerId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ContactInfo = ci,
                                Location = l
                            };
                        }
                    }
                }
            }
            return user;
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT u.UserId, u.Name, ci.PhoneNumber, ci.Email, l.Postcode, l.MunicipalityName, l.StreetName, l.HouseNumberLabel
            FROM [User] u
            LEFT JOIN ContactInfo ci ON u.ContactInfoId = ci.ContactInfoId
            LEFT JOIN Location l ON u.LocationId = l.LocationId
            WHERE u.DeletedAt IS NULL
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Haal de gegevens uit de database en maak nieuwe User-objecten met ContactInfo en Location
                            ContactInfo ci = new ContactInfo(reader.GetString(2), reader.GetString(3));
                            Location l = new Location(reader.GetInt32(4), reader.GetString(5), reader.IsDBNull(6) ? null : reader.GetString(6), reader.IsDBNull(7) ? null : reader.GetString(7));

                            User user = new User
                            {
                                CustomerId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ContactInfo = ci,
                                Location = l
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public List<User> GetDeletedUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT u.UserId, u.Name, ci.PhoneNumber, ci.Email, l.Postcode, l.MunicipalityName, l.StreetName, l.HouseNumberLabel
            FROM [User] u
            LEFT JOIN ContactInfo ci ON u.ContactInfoId = ci.ContactInfoId
            LEFT JOIN Location l ON u.LocationId = l.LocationId
            WHERE u.DeletedAt IS NOT NULL
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Haal de gegevens uit de database en maak nieuwe User-objecten met ContactInfo en Location
                            ContactInfo ci = new ContactInfo(reader.GetString(2), reader.GetString(3));
                            Location l = new Location(reader.GetInt32(4), reader.GetString(5), reader.IsDBNull(6) ? null : reader.GetString(6), reader.IsDBNull(7) ? null : reader.GetString(7));

                            User user = new User
                            {
                                CustomerId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ContactInfo = ci,
                                Location = l
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }


        public List<User> GetUsersByFilter(string filter)
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT u.UserId, u.Name, ci.PhoneNumber, ci.Email, l.Postcode, l.MunicipalityName, l.StreetName, l.HouseNumberLabel
            FROM [User] u
            LEFT JOIN ContactInfo ci ON u.ContactInfoId = ci.ContactInfoId
            LEFT JOIN Location l ON u.LocationId = l.LocationId
            WHERE u.Name LIKE '%' + @Filter + '%' AND u.DeletedAt IS NULL
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Filter", filter);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Haal de gegevens uit de database en maak nieuwe User-objecten met ContactInfo en Location
                            ContactInfo ci = new ContactInfo(reader.GetString(2), reader.GetString(3));
                            Location l = new Location(reader.GetInt32(4), reader.GetString(5), reader.IsDBNull(6) ? null : reader.GetString(6), reader.IsDBNull(7) ? null : reader.GetString(7));

                            User user = new User
                            {
                                CustomerId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ContactInfo = ci,
                                Location = l
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }


        public User UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            UPDATE [User]
            SET Name = @Name,
                ContactInfoId = @ContactInfoId,
                LocationId = @LocationId
            WHERE UserId = @UserId
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@ContactInfoId", user.ContactInfo.ContactInfoID);
                    command.Parameters.AddWithValue("@LocationId", user.Location.LocationID);
                    command.Parameters.AddWithValue("@UserId", user.CustomerId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Controleer of de update succesvol is uitgevoerd
                    if (rowsAffected > 0)
                    {
                        // Als de update succesvol was, retourneer de bijgewerkte gebruiker
                        return GetUserById(user.CustomerId);
                    }
                    else
                    {
                        // Gebruiker niet bijgewerkt, returneer bijvoorbeeld null of geef een foutmelding terug
                        return null;
                    }
                }
            }
        }

    }
}

//ContactInfo ci = new ContactInfo("johndoe@example.com", "123456789");
//Location l = new Location(1234, "City", "Main Street", "10");
//User u = new User("John Doe", ci, l);
//if (id == 1) // Veronderstel dat id 1 de dummygebruiker is
//{
//    return u;
//}
//else
//{
//    throw new InvalidOperationException("Gebruiker niet gevonden");
//}