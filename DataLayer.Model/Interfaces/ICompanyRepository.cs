using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> FindByCodeAsync(string companyCode);
        Task<Company> GetByCodeAsync(string companyCode);
        Task<bool> SaveCompanyAsync(Company company);
        Task<bool> UpdateCompanyAsync(Company company);
        Task<bool> DeleteCompanyAsync(Company company);
    }
}
