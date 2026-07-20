using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/Tickets")]
public class CommentsController : ControllerBase
{

    [HttpGet("{ticketId}/comments")]
    public IActionResult GetAll(int ticketId)
    {
        var ticket = TicketsController.Tickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket.Comments);
    }

    [HttpPost("{ticketId}/comments")]
    public IActionResult Create(int ticketId, [FromBody] Comment comment)
    {
        var ticket = TicketsController.Tickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket == null)
        {
            return NotFound();
        }

        comment.Id = ticket.Comments.Count > 0 ? ticket.Comments.Max(c => c.Id) + 1 : 1;
        comment.TicketId = ticketId;
        comment.CreatedAt = DateTime.UtcNow;

        ticket.Comments.Add(comment);

        return CreatedAtAction(nameof(GetAll), new { ticketId }, comment);
    }
}