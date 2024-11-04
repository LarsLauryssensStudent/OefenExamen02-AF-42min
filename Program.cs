
using Microsoft.AspNetCore.RateLimiting;
using OefenExamen02.Services;

namespace OefenExamen02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("ThreeRequestThirty", ops =>
                {
                    ops.PermitLimit = 3;
                    ops.QueueLimit = 2;
                    ops.Window = TimeSpan.FromSeconds(30);
                    ops.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                });
                options.AddFixedWindowLimiter("TenReq20", ops =>
                {
                    ops.PermitLimit = 10;
                    ops.QueueLimit = 5;
                    ops.Window = TimeSpan.FromSeconds(20);
                    ops.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                });
                options.AddFixedWindowLimiter("OneRequestFifty", ops =>
                {
                    ops.PermitLimit = 1;
                    ops.QueueLimit = 2;
                    ops.Window = TimeSpan.FromSeconds(50);
                    ops.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                });
            });
            // Add services to the container.

            builder.Services.AddSingleton<IRealEstateData, InMemoryRealEstateData>();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseRateLimiter();

            app.MapControllers();

            app.Run();
        }
    }
}
