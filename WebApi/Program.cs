
using Library.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using WebApi.IRepository;
using WebApi.Mapper;
using WebApi.Repository;
using Quartz;
using WebApi.JobSchedule;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add DbContext
        builder.Services.AddDbContext<QuizManagementContext>(o =>
        {
            o.UseSqlServer(builder.Configuration.GetConnectionString("MyConStr"))
             .EnableSensitiveDataLogging()
             .LogTo(Console.WriteLine);
        });

        // Add Authentication and JWT Bearer configuration
        builder.Services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        }).AddGoogle(o =>
        {
            o.ClientId = builder.Configuration["GoogleKeys:ClientId"]!;
            o.ClientSecret = builder.Configuration["GoogleKeys:ClientSecret"]!;
        });

        //Add Quartz for auto send mail
        builder.Services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory(); 

            var jobKey = new JobKey("CheckRemindAssignExam");
            q.AddJob<CheckRemindAssignExam>(opts => opts.WithIdentity(jobKey));
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("CheckRemindAssignExam-trigger")
                .WithCronSchedule("0 0 0 * * ?")); // second minus hour dayOfMonth Month dayOfWeek
        });

        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        // Add DI for repositories and AutoMapper
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IExaminerRepository, ExaminerRepository>();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        builder.Services.AddScoped<IExamRepository, ExamRepository>();
        builder.Services.AddScoped<IMenuRepository, MenuRepository>();
        builder.Services.AddScoped<ICampusRepository, CampusRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IExamRepository, ExamRepository>();

        builder.Services.AddScoped<IAssignRepository, AssignRepository>();
        builder.Services.AddScoped<IReportRepository, ReportRepository>();

        builder.Services.AddScoped<IExamAssignRepository, ExamAssignRepository>();
		builder.Services.AddScoped<IEditStatusRepository, EditStatusRepository>();
        builder.Services.AddScoped<IStatusRepository, StatusRepository>();
        builder.Services.AddScoped<ISendMailRepository, SendMailRepository>();

        builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
        builder.Services.AddScoped<IInstructorAssignmentRepository, InstructorAssignmentRepository>();
        builder.Services.AddScoped<IReportRepository, ReportRepository>();

		builder.Services.AddScoped<IEstimatedTimeRepository, EstimatedTimeRepository>();

		builder.Services.AddScoped<IGenerateExcelRepository, GenerateExcelRepository>();
		builder.Services.AddScoped<ILecturerBySubjectRepository, LecturerBySubjectRepository>();
		builder.Services.AddScoped<ILogHistoryRepository, LogHistoryRepository>();

        builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
        


		var app = builder.Build();


        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Enable HTTPS redirection
        app.UseHttpsRedirection();

        // Enable authentication and authorization
        app.UseAuthentication(); // Phải được gọi trước UseAuthorization
        app.UseAuthorization();

        // Map Controllers
        app.MapControllers();

        app.Run();
    }
}
