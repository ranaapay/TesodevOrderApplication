using AutoMapper;
using ProductCore;

namespace Product_Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductForCreateUpdate>();
            CreateMap<ProductForCreateUpdate,Product>();
        }
    }
}