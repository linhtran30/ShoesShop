using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _06_FinalProject.Data;
using _06_FinalProject.Models;

namespace _06_FinalProject.Controllers
{
    
    public class ProductsController : BaseApiController 
    {
        private readonly StoreContext context;

        public ProductsController(StoreContext context)
        {
           this.context = context;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<List<Product>>GetProducts()
        {
            var products =  context.Products.ToList();
            return  Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
           var product = await context.Products.FindAsync(id);
            if(product == null) return NotFound();
            return product;
        }
            
        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            context.Entry(product).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (context.Products == null)
          {
              return Problem("Entity set 'StoreContext.Products'  is null.");
          }
            context.Products.Add(product);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (context.Products == null)
            {
                return NotFound();
            }
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
