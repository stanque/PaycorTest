using AutoMapper;
using PaycorTest.Data.Entities;
using PaycorTest.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Service.Infrastructure
{
    public class AutomapperServiceProfile : Profile
    {
        public AutomapperServiceProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product> ();

            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<ProductCategoryDto, ProductCategory> ();

            CreateMap<ProductModel, ProductModelDto>();
            CreateMap<ProductModelDto, ProductModel> ();

            CreateMap<ProductSubcategory, ProductSubcategoryDto>();
            CreateMap<ProductSubcategoryDto, ProductSubcategory> ();

            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailDto>();
            CreateMap<PurchaseOrderDetailDto, PurchaseOrderDetail> ();

            CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderDto>();
            CreateMap<PurchaseOrderHeaderDto, PurchaseOrderHeader> ();

            CreateMap<ShipMethod, ShipMethodDto> ();
            CreateMap<ShipMethodDto, ShipMethod>();

            CreateMap<UnitMeasure, UnitMeasureDto>();
            CreateMap<UnitMeasureDto, UnitMeasure>();

            CreateMap<Vendor, VendorDto>();
            CreateMap<VendorDto, Vendor >();
        }
    }
}
