using API.BusinessLogic.Interface.Customer;
using API.BusinessLogic.Interface.ILogin;
using API.BusinessLogic.Interface.IModuleAndMenu;
using API.BusinessLogic.Services.Customers;
using API.BusinessLogic.Services.Logins;
using API.BusinessLogic.Services.ModuleAndMenu;

namespace API.ServiceRegister
{
    public static class RegisteredServices
    {
        public static void Register(WebApplicationBuilder builder)
        {
            //With a scoped service we get the same instance within the scope of a given http request
            //but a new instance across different http requests.
            //Dependency Injection of Services
            //Injecting DA(Data Access) layer into the BL(Business Logic) layer.It will set Loosely coupling and increase Testability.
            builder.Services.AddScoped<ICustomerServices, CustomerServices>();
            //builder.Services.AddScoped<ICustomerServicesOld, CustomerServicesOld>();
            builder.Services.AddScoped<ILoginServices, LoginServices>();
            builder.Services.AddScoped<IModuleAndMenuServiceCommands, ModuleAndMenuServiceCommands>();
            builder.Services.AddScoped<IModuleAndMenuServiceQueries, ModuleAndMenuServiceQueries>();

        }
    }
}
