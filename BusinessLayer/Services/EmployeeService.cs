using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync()
        {
            var employees = _employeeRepository.GetAll();
            return await Task.Run(() => _mapper.Map<IEnumerable<EmployeeInfo>>(employees));
        }

        public async Task<bool> SaveEmployeeAsync(EmployeeInfo employeeInfo)
        {
            var employee = _mapper.Map<Employee>(employeeInfo);
            return await _employeeRepository.SaveEmployeeAsync(employee);
        }

        public async Task<bool> UpdateEmployeeAsync(string employeeCode, EmployeeInfo employeeInfo)
        {
            var employee = _mapper.Map<Employee>(employeeInfo);
            employee.EmployeeCode = employeeCode;
            return await _employeeRepository.UpdateEmployeeAsync(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
            return await _employeeRepository.DeleteEmployeeAsync(employeeCode);
        }

    }
}