using AutoMapper;
using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;

namespace CosmeticMVC.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, Customer>();
                //.ForMember(kh => kh.FullName, option => option.MapFrom(RegisterVM => RegisterVM.FullName));
                //.ReverseMap(); //2 chieu
        }
    }
}
