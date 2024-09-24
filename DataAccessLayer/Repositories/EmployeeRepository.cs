using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper, ILogger<EmployeeRepository> logger)
        {
            _employeeDbWrapper = employeeDbWrapper;
            _logger = logger;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeDbWrapper.FindAll();
        }

        public Employee GetByEmployeeCode(string employeeCode)
        {
            return _employeeDbWrapper.Find(t => t.EmployeeCode.Equals(employeeCode))?.FirstOrDefault();
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            var existingEmployee = FindEmployee(employee);

            if (existingEmployee != null)
            {
                //Adding a logger
                _logger.LogInformation($"Saving an exisiting employee with Employee {employee.EmployeeCode}");

                // Update existing employee properties
                existingEmployee.EmployeeName = employee.EmployeeName;
                existingEmployee.Occupation = employee.Occupation;
                existingEmployee.EmployeeStatus = employee.EmployeeStatus;
                existingEmployee.EmailAddress = employee.EmailAddress;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.LastModified = DateTime.UtcNow;

                return await _employeeDbWrapper.UpdateAsync(existingEmployee);
            }
            else
            {
                employee.LastModified = DateTime.UtcNow; // Set the creation date

                //Adding a logger
                _logger.LogInformation($"Inserting an NEW employee with Employee {employee.EmployeeCode}");
                return await _employeeDbWrapper.InsertAsync(employee);
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            // Find and update employee by EmployeeCode
            var existingEmployee = FindEmployee(employee);

            if (existingEmployee != null)
            {
                // Update existing employee properties
                existingEmployee.EmployeeName = employee.EmployeeName;
                existingEmployee.Occupation = employee.Occupation;
                existingEmployee.EmployeeStatus = employee.EmployeeStatus;
                existingEmployee.EmailAddress = employee.EmailAddress;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.LastModified = DateTime.UtcNow;

                return await _employeeDbWrapper.UpdateAsync(existingEmployee);
            }

            return false;
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
            // Find and delete employee by EmployeeCode
            var employee = GetByEmployeeCode(employeeCode);
            if (employee != null)
            {
                return await _employeeDbWrapper.DeleteAsync(e => e.EmployeeCode.Equals(employeeCode));
            }

            return false;
        }

        private Employee FindEmployee(Employee employee)
        {
            var existingEmployee = _employeeDbWrapper.Find(t =>
                t.SiteId.Equals(employee.SiteId) && t.CompanyCode.Equals(employee.CompanyCode) &&
                t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
            return existingEmployee;

        }

    }
}