using Restobooker.Domain.Interfaces;
using Restobooker.Domain.Model;
using Restobooker.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RestoBooker.Data.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private string connectionString;
        private UserRepository _userRepository;
        private RestaurantRepository _restaurantRepository;

        public ReservationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Reservation AddReservation(Reservation reservation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                INSERT INTO [Reservation] (ContactPersonId, NumberOfSeats, Date, Time, TableNumber, RestaurantId)
                VALUES (@ContactPersonId, @NumberOfSeats, @Date, @Time, @TableNumber, @RestaurantId);
                SELECT SCOPE_IDENTITY();
            ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ContactPersonId", reservation.ContactPerson.CustomerId);
                    command.Parameters.AddWithValue("@NumberOfSeats", reservation.NumberOfSeats);
                    command.Parameters.AddWithValue("@Date", reservation.Date);
                    command.Parameters.AddWithValue("@Time", reservation.Time);
                    command.Parameters.AddWithValue("@TableNumber", reservation.TableNumber);
                    command.Parameters.AddWithValue("@RestaurantId", reservation.Restaurant.RestaurantId);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        reservation.ReservationNumber = Convert.ToInt32(result);
                    }
                }
            }
            return reservation;
        }


        public void DeleteReservation(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    DELETE FROM [Reservation]
                    WHERE ReservationId = @ReservationId
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationId", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public Reservation GetReservationById(int id)
        {
            Reservation reservation = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT ReservationId, RestaurantId, NumberOfSeats, Date, Time, TableNumber, u.UserId, u.Name, ci.PhoneNumber, ci.Email
                FROM [Reservation] r
                INNER JOIN [User] u ON r.ContactPersonId = u.UserId
                INNER JOIN ContactInfo ci ON u.ContactInfoId = ci.ContactInfoId
                WHERE ReservationId = @ReservationId
            ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationId", id);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ContactInfo contactInfo = new ContactInfo(reader.GetString(9), reader.GetString(10));
                            User contactPerson = _userRepository.GetUserById(reader.GetInt32(7));
                            Restaurant restaurant = _restaurantRepository.GetRestaurantById(reader.GetInt32(1));
                            reservation = new Reservation(
                                restaurant,
                                contactPerson,
                                reader.GetInt32(2),
                                reader.GetDateTime(3),
                                reader.GetTimeSpan(4),
                                reader.GetInt32(5)
                            )
                            {
                                ReservationNumber = reader.GetInt32(0),
                            };
                        }
                    }
                }
            }

            return reservation;
        }

        public List<Reservation> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT ReservationId, NumberOfSeats, Date, Time, TableNumber, RestaurantId, u.UserId, u.Name, ci.PhoneNumber, ci.Email
            FROM [Reservation] r
            INNER JOIN [User] u ON r.ContactPersonId = u.UserId
            INNER JOIN ContactInfo ci ON u.ContactInfoId = ci.ContactInfoId
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ContactInfo contactInfo = new ContactInfo(reader.GetString(8), reader.GetString(9)); // Update index for ContactInfo
                            User contactPerson = _userRepository.GetUserById(reader.GetInt32(6)); // Update index for User
                            Restaurant restaurant = _restaurantRepository.GetRestaurantById(reader.GetInt32(5)); // Update index for Restaurant
                            Reservation reservation = new Reservation(
                                restaurant,
                                contactPerson,
                                reader.GetInt32(1), // Update index for NumberOfSeats
                                reader.GetDateTime(2), // Update index for Date
                                reader.GetTimeSpan(3), // Update index for Time
                                reader.GetInt32(4) // Update index for TableNumber
                            )
                            {
                                ReservationNumber = reader.GetInt32(0),
                            };

                            reservations.Add(reservation);
                        }
                    }
                }
            }

            return reservations;
        }

        public List<Reservation> GetReservationsByFilter(string filter)
        {
            List<Reservation> reservations = new List<Reservation>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT ReservationId, RestaurantInfo, NumberOfSeats, Date, Time, TableNumber, RestaurantId, u.UserId, u.Name, ci.PhoneNumber, ci.Email
                    FROM [Reservation] r
                    INNER JOIN [User] u ON r.ContactPersonId = u.UserId
                    INNER JOIN ContactInfo ci ON u.ContactInfoId = ci.ContactInfoId
                    WHERE RestaurantInfo LIKE '%' + @Filter + '%'
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Filter", filter);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ContactInfo contactInfo = new ContactInfo(reader.GetString(9), reader.GetString(10));
                            User contactPerson = _userRepository.GetUserById(reader.GetInt32(7));
                            Restaurant restaurant = _restaurantRepository.GetRestaurantById(reader.GetInt32(1));
                            Reservation reservation = new Reservation(
                                restaurant,
                                contactPerson,
                                reader.GetInt32(2),
                                reader.GetDateTime(3),
                                reader.GetTimeSpan(4),
                                reader.GetInt32(5)
                            )
                            {
                                ReservationNumber = reader.GetInt32(0),
                            };

                            reservations.Add(reservation);
                        }
                    }
                }
            }

            return reservations;
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE [Reservation]
                    SET ContactPersonId = @ContactPersonId,
                        NumberOfSeats = @NumberOfSeats,
                        Date = @Date,
                        Time = @Time,
                        TableNumber = @TableNumber,
                        RestaurantId = @RestaurantId  
                    WHERE ReservationId = @ReservationId
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ContactPersonId", reservation.ContactPerson.CustomerId);
                    command.Parameters.AddWithValue("@NumberOfSeats", reservation.NumberOfSeats);
                    command.Parameters.AddWithValue("@Date", reservation.Date);
                    command.Parameters.AddWithValue("@Time", reservation.Time);
                    command.Parameters.AddWithValue("@TableNumber", reservation.TableNumber);
                    command.Parameters.AddWithValue("@RestaurantId", reservation.Restaurant.RestaurantId);
                    command.Parameters.AddWithValue("@ReservationId", reservation.ReservationNumber);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return GetReservationById(reservation.ReservationNumber);
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
