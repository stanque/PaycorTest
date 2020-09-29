using AutoMapper;
using PaycorTest.Api.Models;
using PaycorTest.API.Models.AggregationModels;
using PaycorTest.API.Models.Request;
using PaycorTest.Application.Commands;
using PaycorTest.Application.Models;
using PaycorTest.Application.Models.AggregationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaycorTest.API.Infrastructure
{
    public class AutomapperApiProfile : Profile
    {
        public AutomapperApiProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();

            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategoryViewModel, ProductCategory>();

            CreateMap<ProductModel, ProductModelViewModel>();
            CreateMap<ProductModelViewModel, ProductModel>();

            CreateMap<ProductSubcategory, ProductSubcategoryViewModel>();
            CreateMap<ProductSubcategoryViewModel, ProductSubcategory>();

            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailViewModel>();
            CreateMap<PurchaseOrderDetailViewModel, PurchaseOrderDetail>();

            CreateMap<PurchaseOrderHeader, PurchaseOrderHeaderViewModel>();
            CreateMap<PurchaseOrderHeaderViewModel, PurchaseOrderHeader>();

            CreateMap<ShipMethod, ShipMethodViewModel>();
            CreateMap<ShipMethodViewModel, ShipMethod>();

            CreateMap<UnitMeasure, UnitMeasureViewModel>();
            CreateMap<UnitMeasureViewModel, UnitMeasure>();

            CreateMap<Vendor, VendorViewModel>();
            CreateMap<VendorViewModel, Vendor>();

            CreateMap<GetFilteredProductsRequest, GetFilteredProductsCommand>();
            CreateMap<GetAggregatedLineTotalAndOrderQtyInTimePeriodRequest, GetAggregatedLineTotalAndOrderQtyInTimePeriodCommand>();

            CreateMap<LineTotalAndOrderQtySumPerDay, LineTotalAndOrderQtySumPerDayViewModel>();
            CreateMap<LineTotalAndOrderQtySumPerTimePeriod, LineTotalAndOrderQtySumPerTimePeriodViewModel>();
        }
    }
}
