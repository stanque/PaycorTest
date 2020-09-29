using AutoMapper;
using PaycorTest.Application.Models;
using PaycorTest.Application.Models.AggregationModels;
using PaycorTest.Service.ServiceModels;
using PaycorTest.Service.ServiceModels.AggregationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Application.Infrastructure
{
    public class AutomapperApplicationProfile : Profile
    {
        public AutomapperApplicationProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<ProductCategoryDto, ProductCategory>();

            CreateMap<ProductModel, ProductModelDto>();
            CreateMap<ProductModelDto, ProductModel>();

            CreateMap<ProductSubcategory, ProductSubcategoryDto>();
            CreateMap<ProductSubcategoryDto, ProductSubcategory>();

            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailDto>();
            CreateMap<PurchaseOrderDetailDto, PurchaseOrderDetail>();

            CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderDto>();
            CreateMap<PurchaseOrderHeaderDto, PurchaseOrderHeader>();

            CreateMap<ShipMethod, ShipMethodDto>();
            CreateMap<ShipMethodDto, ShipMethod>();

            CreateMap<UnitMeasure, UnitMeasureDto>();
            CreateMap<UnitMeasureDto, UnitMeasure>();

            CreateMap<Vendor, VendorDto>();
            CreateMap<VendorDto, Vendor>();

            CreateMap<LineTotalAndOrderQtySumPerDayDto, LineTotalAndOrderQtySumPerDay>();
            CreateMap<LineTotalAndOrderQtySumPerTimePeriodDto, LineTotalAndOrderQtySumPerTimePeriod>();
        }
    }
}
