using System.Collections.Generic;
using AutoMapper;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using OrderCore;
using OrderCore.DataTransferObjects;
using OrderCore.Models;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        
        public OrderController(IOrderServices orderServices, ILoggerManager logger, IMapper mapper)
        {
            _orderServices = orderServices;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _orderServices.GetOrders();
            var companiesDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(companiesDto);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult GetOrder(string id)
        {
            var order = _orderServices.GetOrder(id);
            if (order == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
            //return Ok(_orderServices.GetOrder(id));
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            _orderServices.AddOrder(order);
            return CreatedAtRoute("GetOrder", new {id = order.Id}, order);
        }

        [HttpDelete]
        public IActionResult DeleteOrder(string id)
        {
            _orderServices.DeleteOrder(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateOrder(Order order)
        {
            return Ok(_orderServices.UpdateOrder(order));
        }
    }
}