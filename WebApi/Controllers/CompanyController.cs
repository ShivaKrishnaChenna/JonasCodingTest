using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;

        public CompanyController(ICompanyService companyService, IMapper mapper, Serilog.ILogger logger)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<CompanyDto> Get(string companyCode)
        {
            var item = await _companyService.GetCompanyByCodeAsync(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            var items = await _companyService.GetAllCompaniesAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        [HttpGet]
        [Route("GetCompany")]
        public async Task<CompanyDto> GetAsync(string companyCode)
        {
            var item = await _companyService.GetCompanyByCodeAsync(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(CompanyInfo companyInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.Information("Post method called from Company");

            var result = await _companyService.SaveCompanyAsync(companyInfo);

            if (!result)
                throw new Exception("The Post Operation failed");

            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(string companyCode, CompanyDto companyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.Information("Put method called from Company");

            var company = _mapper.Map<CompanyInfo>(companyDto);
            var result = await _companyService.UpdateCompanyAsync(companyCode, company);

            if (!result)
                throw new Exception("The PUT Operation failed");
            return Ok();

        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string companyCode)
        {
            var result = await _companyService.DeleteCompanyAsync(companyCode);

            if (!result)
                throw new Exception("The DELETE Operation failed");
            return Ok();
        }
    }
}