using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync()
        {
            var res = await _companyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public async Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode)
        {
            var result = await _companyRepository.FindByCodeAsync(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task<bool> SaveCompanyAsync(CompanyInfo companyInfo)
        {
            var company = _mapper.Map<Company>(companyInfo);
            return await _companyRepository.SaveCompanyAsync(company);
        }

        public async Task<bool> UpdateCompanyAsync(string companyCode, CompanyInfo companyInfo)
        {
            var existingCompany = await _companyRepository.GetByCodeAsync(companyCode);

            if (existingCompany == null)
            {
                return false;
            }

            _mapper.Map(companyInfo, existingCompany);
            return await _companyRepository.UpdateCompanyAsync(existingCompany);
        }

        public async Task<bool> DeleteCompanyAsync(string companyCode)
        {
            var existingCompany = await _companyRepository.GetByCodeAsync(companyCode);

            if (existingCompany == null)
            {
                return false;
            }

            return await _companyRepository.DeleteCompanyAsync(existingCompany);
        }
    }
}
