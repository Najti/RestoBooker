using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;

namespace RestoBooker.FrontEnd
{
    public partial class UserWindow : Window
    {
        private int UserID;
        private string UserName;

        public UserWindow()
        {
            InitializeComponent();
            // API-oproep om UserID en UserName op te halen
            FetchUserIDAndName();
            // API-oproep om reservaties op te halen
            FetchReservations();
        }

        private async void FetchUserIDAndName()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("http://localhost:5126/api/User/Logon");
                    response.EnsureSuccessStatusCode();
                    var user = await response.Content.ReadAsAsync<User>(); // Gebruik het juiste User model
                    UserID = user.UserID;
                    UserName = user.UserName;
                    DataContext = this; // Stelt de DataContext in om bindingen te gebruiken
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het ophalen van UserID en UserName: {ex.Message}");
            }
        }

        private async void FetchReservations()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"http://localhost:5126/api/Reservation/User/{UserID}");
                    response.EnsureSuccessStatusCode();
                    var reservations = await response.Content.ReadAsAsync<List<Reservation>>(); // Gebruik het juiste Reservation model
                    reservationsGrid.ItemsSource = reservations;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het ophalen van reservaties: {ex.Message}");
            }
        }
    }
}
