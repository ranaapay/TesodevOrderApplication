using System.Collections.Generic;
using System.Threading.Tasks;
using Aggreation_Api.Models;
using Aggreation_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aggreation_Api.Controllers
{
    [ApiController]
    [Route("api/aggreation")]
    public class AggreationContoller : ControllerBase
    {
        private readonly ICustomerGwService _customerGwService;
        private readonly IOrderGwService _orderGwService;
        private readonly IProductGwService _productGwService;
        public AggreationContoller(ICustomerGwService customerGwService, IOrderGwService orderGwService, IProductGwService productGwService)
        {
            _customerGwService = customerGwService;
            _orderGwService = orderGwService;
            _productGwService = productGwService;
        }
        //PRODUCT APİ
        [Route("product")]
        [HttpGet]
        public async Task<IEnumerable<ProductModel>> Get()
        {
            return await _productGwService.GetProducts();
        }
        
        [HttpGet("product/{id}", Name="GetProduct")]
        public async Task<ProductModel> GetProduct(string id)
        {
            return await _productGwService.GetProduct(id);
        }
        //COSTUMER APİ
        [Route("customer")]
        [HttpGet]
        public async Task<IEnumerable<CustomerModel>> GetCostumers()
        {
            return await _customerGwService.GetCustomers();
        }
        [HttpGet("customer/{id}", Name="GetCustomer")]
        public async Task<CustomerModel> GetCostumer(string id)
        {
            return await _customerGwService.GetCustomer(id);
        }
        [Route("customer")]
        [HttpPost]
        public async Task<CreatedAtRouteResult> CreateCustomer(CustomerForCreationModel customerForCreationModel)
        {
           var customer = await _customerGwService.CreateCustomer(customerForCreationModel);
           return CreatedAtRoute("GetCustomer", new {id = customer.Id}, customer);
        }
        [HttpPut("customer/{id}")]
        public async Task<NoContentResult> UpdateCustomer(string id, [FromBody]CustomerForCreationModel customerForCreationModel)
        {
            await _customerGwService.UpdateCustomer(id, customerForCreationModel);
            return NoContent();
        }
        //ORDER APİ
        [Route("order")]
        [HttpGet]
        public async Task<IEnumerable<OrderModel>> GetOrder()
        {
            return await _orderGwService.GetOrders();
        }
        
        [HttpGet("order/{id}", Name="GetOrder")]
        public async Task<OrderModel> GetOrder(string id)
        {
            return await _orderGwService.GetOrder(id);
        }
        
        [Route("order")]
        [HttpPost]
        public async Task<CreatedAtRouteResult> CreateOrder(OrderForPostRequestModel orderForPostRequestModel)
        {
            var address = _customerGwService.GetCustomerForAddress(orderForPostRequestModel.CustomerId).Result.Address;
            var product = _productGwService.GetProduct(orderForPostRequestModel.ProductId).Result;
            var orderForCreationModel = new OrderForCreationModel
            {
                CustomerId = orderForPostRequestModel.CustomerId,
                Quantity = orderForPostRequestModel.Quantity,
                Price = orderForPostRequestModel.Price,
                Status = orderForPostRequestModel.Status,
                Address = address,
                Product = product
            };
            var order = await _orderGwService.CreateOrder(orderForCreationModel);
            return CreatedAtRoute("GetOrder", new {id = order.Id}, order);
        }
        
    }
}