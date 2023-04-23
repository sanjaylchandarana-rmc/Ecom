namespace Ecom.Api.Products.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<DB.Product, Model.Product>();
        }
    }
}
