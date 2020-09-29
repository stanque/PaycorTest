using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaycorTest.Service.Abstraction;
using PaycorTest.Service.Implementation.Product;
using PaycorTest.Service.Implementation.PurchaseOrderDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Service.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseOrderDetailService, PurchaseOrderDetailService>();
            return services;
        }
    }
}
