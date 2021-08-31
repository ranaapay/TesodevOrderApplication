using System;
using System.Collections.Generic;
using AutoMapper;
using CustomerCore;
using CustomerCore.DataTransferObjects;
using LoggerService;
using Microsoft.AspNetCore.Mvc;

namespace Customer_Api.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ICustomerServices _customerServices;
        private readonly IMapper _mapper;
        
        public CustomerController(ILoggerManager logger, ICustomerServices customerServices, IMapper mapper)
        {
            _logger = logger;
            _customerServices = customerServices;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _customerServices.GetCustomers();
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customersDto);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetCustomer(string id)
        {
            var customer = _customerServices.GetCustomer(id);
            if (customer == null)
            {
                _logger.LogInfo($"Customer with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }
        
        [HttpGet("address/{id}")]
        public IActionResult GetCustomerForAddress(string id)
        {
            var customer = _customerServices.GetCustomer(id);
            if (customer == null)
            {
                _logger.LogInfo($"Customer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody]CustomerForCreationDto customer)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CustomerForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            
            var customerEntity = _mapper.Map<Customer>(customer);
            _customerServices.CreateCustomer(customerEntity);

            var customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
            return CreatedAtRoute("GetCustomer", new {id = customerToReturn.Id}, customerToReturn);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteCustomer(string id)
        {
            var customer = _customerServices.GetCustomer(id);
            if (customer == null)
            {
                _logger.LogInfo($"Customer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _customerServices.DeleteCustomer(id);
            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateCustomer(string id, [FromBody]CustomerForUpdateDto customer)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CustomerForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var customerForCheck = _customerServices.GetCustomer(id);
            if (customerForCheck == null)
            {
                _logger.LogInfo($"Customer with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var customerEntity = _mapper.Map<Customer>(customer);
            customerEntity.Id = id;
            customerEntity.CreatedAt = customerForCheck.CreatedAt;
            customerEntity.UpdatedAt=DateTime.Now;
            _customerServices.UpdateCustomer(customerEntity);
            return NoContent();
            
        }
    }
}