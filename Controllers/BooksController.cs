using ShoppingListApi.Models;
using ShoppingListApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingListApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingListController : ControllerBase
{
    private readonly ShoppingListService _shoppinglistService;

    public ShoppingListController(ShoppingListService ShoppingListService) =>
        _shoppinglistService = ShoppingListService;

    [HttpGet]
    public async Task<List<ShoppingList>> Get() =>
        await _shoppinglistService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<ShoppingList>> Get(string id)
    {
        var ShoppingList = await _shoppinglistService.GetAsync(id);

        if (ShoppingList is null)
        {
            return NotFound();
        }

        return ShoppingList;
    }
 
    [HttpPost]
    public async Task<IActionResult> Post(ShoppingList newShoppingList)
    {
        await _shoppinglistService.CreateAsync(newShoppingList);

        return CreatedAtAction(nameof(Get), new { id = newShoppingList.Id }, newShoppingList);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, ShoppingList updatedShoppingList)
    {
        var ShoppingList = await _shoppinglistService.GetAsync(id);

        if (ShoppingList is null)
        {
            return NotFound();
        }

        updatedShoppingList.Id = ShoppingList.Id;

        await _shoppinglistService.UpdateAsync(id, updatedShoppingList);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var ShoppingList = await _shoppinglistService.GetAsync(id);

        if (ShoppingList is null)
        {
            return NotFound();
        }

        await _shoppinglistService.RemoveAsync(id);

        return NoContent();
    }
}