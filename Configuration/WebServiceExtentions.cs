﻿using DataStorage.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using DataStorage.BLL.Interfaces;
using DataStorage.BLL.Services;
using DataStorage.DAL.Interfaces;
using DataStorage.DAL.Repositories;
using DataStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using DataStorage.BLL.DTOs;

namespace Configuration
{
    public static class WebServiceExtentions
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DataStorage;Trusted_Connection=True;",
                    x => x.MigrationsAssembly("DataStorage.DAL")));

            services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            services.TryAddScoped<IUsersRepository, UsersRepository>();
            services.TryAddScoped<IUsersService, UserService>();
            services.TryAddScoped<IDocumentRepository, DocumentRepository>();
            services.TryAddScoped<IDocumentService, DocumentService>();
            services.TryAddScoped<IPathProvider, PathProvider>();

            services.AddTransient<IPathProvider, PathProvider>();

            return services;
        }
    }
}
