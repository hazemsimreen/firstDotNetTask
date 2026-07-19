using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;
using WebApplication1.Models;
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var counts = Enum.GetValues<TicketCategory>()
            .Select(category => new
            {
                Category = category.ToString(),
                TicketCount = TicketsController.Tickets.Count(t => t.Category == category)
            });

        return Ok(counts);
    }
}