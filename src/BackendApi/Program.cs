using BusinessLogic.Services;
using DataAccess.Wrapper;
using Domain.Interfaces.Service;
using Domain.Interfaces.Wrapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BackendApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            await Task.Delay(10000);

            var builder = WebApplication.CreateBuilder(args);
            //Console.WriteLine(builder.Configuration["ConnectionString"]);
            builder.Services.AddDbContext<VitalityMasteryTestContext>(
                options => options.UseSqlServer(builder.Configuration["ConnectionStrings"]));



            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<ITrainingService, TrainingService>();
            builder.Services.AddScoped<IScheduleService, ScheduleService>();
            builder.Services.AddScoped<IRolesService, RolesService>();
            builder.Services.AddScoped<IPublicationService, PublicationService>();
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<IPhotoUsersService, PhotoUsersService>();
            builder.Services.AddScoped<INutritionService, NutritionService>();
            builder.Services.AddScoped<IMessageUsersService, MessageUsersService>();
            builder.Services.AddScoped<IGroupsService, GroupsService>();
            builder.Services.AddScoped<IGroupMembersService, GroupMembersService>();
            builder.Services.AddScoped<IFriendService, FriendService>();
            builder.Services.AddScoped<IDialogsService, DialogsService>();
            builder.Services.AddScoped<ICommentsService, CommentsService>();
            builder.Services.AddScoped<IAchievementsService, AchievementsService>();
            builder.Services.AddScoped<IUserToRuleService, UserToRuleService>();
            builder.Services.AddScoped<IUserToDialogsService, UserToDialogsService>();
            builder.Services.AddScoped<IUserToAchievementService, UserToAchievementsService>();
            builder.Services.AddScoped<IUserTrainingService, UserTrainingService>();
            builder.Services.AddScoped<IUserNutritionService, UserNutritionService>();
            builder.Services.AddScoped<ITrainersScheduleService, TrainersScheduleService>();

            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod();
            }));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Проект Контроля питания и тренеровок социальной сети и отслеживания их достижений API",
                    Description = "Описание ASP .NET Core web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Примеры работы проекта",
                        Url = new Uri("https://inskill.ru/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Пример Пример",
                        Url = new Uri("https://inskill.ru/")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;

                var context = service.GetRequiredService<VitalityMasteryTestContext>();
                await context.Database.MigrateAsync();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("MyPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}