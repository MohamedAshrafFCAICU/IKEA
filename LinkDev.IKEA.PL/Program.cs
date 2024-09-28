using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using LinkDev.IKEA.PL.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
           
            
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>((optionsBuilder) =>
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));

           


            //builder.Services.AddScoped<ApplicationDbContext>();
            //builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>((ServiceProvider) =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            //    optionsBuilder.UseSqlServer("");

            //    var options = optionsBuilder.Options;

            //    return options;
            //});

            #endregion

            var app = builder.Build();

            #region Configure Kestrel Middlewares
           
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); 
           
            
            #endregion

            app.Run();
        }
    }
}
 