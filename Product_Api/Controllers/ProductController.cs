using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCore;

namespace Product_Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        
        public ProductController(IProductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<Product>> Get() =>
            _productService.Get();

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public ActionResult<Product> Create([FromBody]ProductForCreateUpdate product)
        {
            var productEntity = _mapper.Map<Product>(product);
            _productService.Create(productEntity);
            
            return CreatedAtRoute("GetProduct", new { id = productEntity.Id }, productEntity);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, ProductForCreateUpdate productIn)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            var productEntity = _mapper.Map<Product>(productIn);
            productEntity.Id = id;
            _productService.Update(id, productEntity);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Remove(product.Id);

            return NoContent();
        }
    }
}