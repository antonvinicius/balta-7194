using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
  [Route("products")]
  public class ProductController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> Get(
      [FromServices]
      DataContext context
    )
    {
      var products = await context
        .Products
        .Include(x => x.Category)
        .AsNoTracking()
        .ToListAsync();

      return Ok(products);
    }

    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<Product>> GetById(
      int id,
      [FromServices]
      DataContext context
    )
    {
      var product = await context
        .Products
        .Include(x => x.Category)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);

      return Ok(product);
    }

    [HttpGet]
    [Route("categories/{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> GetByCategory(
      int id,
      [FromServices]
      DataContext context
    )
    {
      var products = await context
        .Products
        .Include(x => x.Category)
        .AsNoTracking()
        .Where(x => x.CategoryId == id)
        .OrderBy(x => x.Title)
        .ToListAsync();

      return Ok(products);
    }

    [HttpPost]
    [Route("")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<Product>> Post(
      [FromBody]
      Product model,
      [FromServices]
      DataContext context
    )
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        context.Products.Add(model);
        await context.SaveChangesAsync();
        return Ok(model);
      }
      catch (System.Exception)
      {
        return BadRequest(new { message = "Não foi possível adicionar o produto" });
      }
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<Product>> Put(
      int id,
      [FromServices]
      DataContext context,
      [FromBody] Product model
    )
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      if (id != model.Id)
        return BadRequest(new { message = "Id não encontrado!" });

      try
      {
        context.Entry(model).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return model;
      }
      catch (System.Exception)
      {
        return BadRequest(new { message = "Não foi possível alterar o produto." });
      }
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<Product>> Delete(
      [FromServices] DataContext context,
      int id
    )
    {
      var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

      if (product == null)
        return BadRequest(new { message = "Produto não encontrado" });

      try
      {
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return product;
      }
      catch (System.Exception)
      {
        return BadRequest(new { message = "Não foi possível excluir o produto." });
      }
    }
  }
}