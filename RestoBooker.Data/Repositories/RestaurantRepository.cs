using Restobooker.Domain.Interfaces;
using Restobooker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Restobooker.Domain.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly string connectionString;

        public RestaurantRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO [Restaurant] (Name, LocationId, Cuisine, ContactInfoId)
            VALUES (@Name, @LocationId, @Cuisine, @ContactInfoId);
            SELECT SCOPE_IDENTITY();
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", restaurant.Name);
                    command.Parameters.AddWithValue("@LocationId", restaurant.Location.LocationID);
                    command.Parameters.AddWithValue("@Cuisine", restaurant.Cuisine);
                    command.Parameters.AddWithValue("@ContactInfoId", restaurant.ContactDetails.ContactInfoID);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        restaurant.RestaurantId = Convert.ToInt32(result);
                    }
                }
            }
            return restaurant;
        }


        public void DeleteRestaurant(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    DELETE FROM [Restaurant]
                    WHERE RestaurantId = @RestaurantId
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RestaurantId", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public Restaurant GetRestaurantById(int id)
        {
            Restaurant restaurant = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT r.Name, r.Cuisine, l.Postcode, l.MunicipalityName, l.StreetName, l.HouseNumberLabel, ci.PhoneNumber, ci.Email
                    FROM [Restaurant] r
                    INNER JOIN Location l ON r.LocationId = l.LocationId
                    INNER JOIN ContactInfo ci ON r.ContactInfoId = ci.ContactInfoId
                    WHERE r.RestaurantId = @RestaurantId
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RestaurantId", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ContactInfo ci = new ContactInfo(reader.GetString(6), reader.GetString(7));
                            Location l = new Location(reader.GetInt32(2), reader.GetString(3), reader.IsDBNull(4) ? null : reader.GetString(4), reader.IsDBNull(5) ? null : reader.GetString(5));

                            restaurant = new Restaurant(reader.GetString(0), l, reader.GetString(1), ci);
                        }
                    }
                }
            }
            return restaurant;
        }

        public List<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT r.RestaurantId, r.Name, r.Cuisine, l.Postcode, l.MunicipalityName, l.StreetName, l.HouseNumberLabel, ci.PhoneNumber, ci.Email
                    FROM [Restaurant] r
                    INNER JOIN Location l ON r.LocationId = l.LocationId
                    INNER JOIN ContactInfo ci ON r.ContactInfoId = ci.ContactInfoId
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ContactInfo ci = new ContactInfo(reader.GetString(7), reader.GetString(8));
                            Location l = new Location(reader.GetInt32(3), reader.GetString(4), reader.IsDBNull(5) ? null : reader.GetString(5), reader.IsDBNull(6) ? null : reader.GetString(6));

                            Restaurant restaurant = new Restaurant(reader.GetString(1), l, reader.GetString(2), ci);
                            restaurants.Add(restaurant);
                        }
                    }
                }
            }

            return restaurants;
        }

        public List<Restaurant> GetRestaurantsByFilter(string filter)
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT r.RestaurantId, r.Name, r.Cuisine, l.Postcode, l.MunicipalityName, l.StreetName, l.HouseNumberLabel, ci.PhoneNumber, ci.Email
                    FROM [Restaurant] r
                    INNER JOIN Location l ON r.LocationId = l.LocationId
                    INNER JOIN ContactInfo ci ON r.ContactInfoId = ci.ContactInfoId
                    WHERE r.Name LIKE '%' + @Filter + '%'
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Filter", filter);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ContactInfo ci = new ContactInfo(reader.GetString(7), reader.GetString(8));
                            Location l = new Location(reader.GetInt32(3), reader.GetString(4), reader.IsDBNull(5) ? null : reader.GetString(5), reader.IsDBNull(6) ? null : reader.GetString(6));

                            Restaurant restaurant = new Restaurant(reader.GetString(1), l, reader.GetString(2), ci);
                            restaurants.Add(restaurant);
                        }
                    }
                }
            }

            return restaurants;
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE [Restaurant]
                    SET Name = @Name,
                        LocationId = @LocationId,
                        Cuisine = @Cuisine,
                        ContactInfoId = @ContactInfoId
                    WHERE RestaurantId = @RestaurantId
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", restaurant.Name);
                    command.Parameters.AddWithValue("@LocationId", restaurant.Location.LocationID);
                    command.Parameters.AddWithValue("@Cuisine", restaurant.Cuisine);
                    command.Parameters.AddWithValue("@ContactInfoId", restaurant.ContactDetails.ContactInfoID);
                    command.Parameters.AddWithValue("@RestaurantId", restaurant.RestaurantId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return GetRestaurantById(restaurant.RestaurantId);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
