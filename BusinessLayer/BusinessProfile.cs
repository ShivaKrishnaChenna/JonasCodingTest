using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;

namespace BusinessLayer
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<DataEntity, BaseInfo>().ReverseMap();
            CreateMap<Company, CompanyInfo>().ReverseMap();
            CreateMap<ArSubledger, ArSubledgerInfo>().ReverseMap();
            CreateMap<Employee, EmployeeInfo>().ReverseMap();
        }
    }

}