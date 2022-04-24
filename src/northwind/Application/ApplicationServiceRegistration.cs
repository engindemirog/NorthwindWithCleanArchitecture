using Application.Features.Products.Rules;
using Application.Services.AuthService;
using Core.Application.Pipelines.Validation;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Core.Packages.Jwt;
using Core.Security.Jwt;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<ProductBusinessRules>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<IAuthService, AuthManager>();

            services.AddTransient<ITokenHelper, JwtHelper>();

            services.AddSingleton<IMailService, MailkitMailService>();

            return services;
        }
    }
}
