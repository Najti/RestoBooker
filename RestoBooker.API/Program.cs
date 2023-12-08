using Restobooker.Domain.Interfaces;
using Restobooker.Domain.Services;
using RestoBooker.Data.Repositories;

namespace RestoBooker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Data Source=LAPTOP-TLTEA25D\\SQLEXPRESS;Initial Catalog=RestoBooker;Integrated Security=True";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IUserRepository>(r => new UserRepository(connectionString));
            builder.Services.AddSingleton<IRestaurantRepository>(r => new RestaurantRepository(connectionString));
            builder.Services.AddSingleton<IReservationRepository>(r => new ReservationRepository(connectionString));
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<ReservationService>();
            builder.Services.AddSingleton<RestaurantService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}