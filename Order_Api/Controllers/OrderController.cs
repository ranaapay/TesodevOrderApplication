using System;
using System.Collections.Generic;
using AutoMapper;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using OrderCore;
using OrderCore.DataTransferObjects;
using OrderCore.Models;

namespace Order_Api.Controllers
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
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(ordersDto);
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
        public IActionResult CreateOrder([FromBody]OrderForCreationDto order)
        {
            var orderEntity = _mapper.Map<Order>(order);
            _orderServices.AddOrder(orderEntity);
            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);
            return CreatedAtRoute("GetOrder", new {id = orderToReturn.Id}, orderToReturn);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteOrder(string id)
        {
            var orderForCheck = _orderServices.GetOrder(id);
            if (orderForCheck == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _orderServices.DeleteOrder(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(string id, [FromBody]OrderForUpdateDto order)
        {
            var orderForCheck = _orderServices.GetOrder(id);
            if (orderForCheck == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var orderEntity = _mapper.Map<Order>(order);
            orderEntity.Id = id;
            orderEntity.CreatedAt = orderForCheck.CreatedAt;
            orderEntity.UpdatedAt=DateTime.Now;
            _orderServices.UpdateOrder(orderEntity);
            return NoContent();
            //return Ok(_orderServices.UpdateOrder(order));
        }
    }
}