using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbWrapper<Company> _companyDbWrapper;
        //private readonly ILogger<CompanyRepository> _logger;

        public CompanyRepository(IDbWrapper<Company> companyDbWrapper
            //ILogger<CompanyRepository> logger
            )
        {
            _companyDbWrapper = companyDbWrapper;
            //_logger = logger;
        }

        public async Task<IEnumerable<Company>> FindAllAsync()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Company> FindByCodeAsync(string companyCode)
        {
            var result = await _companyDbWrapper.FindAsync(c => c.CompanyCode.Equals(companyCode));
            return result.FirstOrDefault();
        }

        public async Task<bool> SaveCompanyAsync(Company company)
        {
            var existingCompany = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode))?.FirstOrDefault();

            if (existingCompany != null)
            {
                //Add Logging
                //_logger.LogInformation($"Saving as Company with Company Code {company} exists");
                // Update existing company properties
                existingCompany.CompanyName = company.CompanyName;
                existingCompany.AddressLine1 = company.AddressLine1;
                existingCompany.AddressLine2 = company.AddressLine2;
                existingCompany.AddressLine3 = company.AddressLine3;
                existingCompany.Country = company.Country;
                existingCompany.EquipmentCompanyCode = company.EquipmentCompanyCode;
                existingCompany.FaxNumber = company.FaxNumber;
                existingCompany.PhoneNumber = company.PhoneNumber;
                existingCompany.PostalZipCode = company.PostalZipCode;
                existingCompany.LastModified = DateTime.UtcNow;

                return await _companyDbWrapper.UpdateAsync(existingCompany);
            }
            else
            {
                company.LastModified = DateTime.UtcNow; // Set the creation date
                //Add Logging
                //_logger.LogInformation($"Saving as NEW Company with Company Code {company}");
                return await _companyDbWrapper.InsertAsync(company);
            }
        }

        public async Task<bool> UpdateCompanyAsync(Company company)
        {
            return await _companyDbWrapper.UpdateAsync(company);
        }

        public async Task<bool> DeleteCompanyAsync(Company company)
        {
            return await _companyDbWrapper.DeleteAsync(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode));
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Company> GetByCodeAsync(string companyCode)
        {
            return _companyDbWrapper.Find(c => c.CompanyCode == companyCode).FirstOrDefault();

        }
    }
}