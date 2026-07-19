using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketsController : ControllerBase
{
    private static readonly List<Ticket> Tickets = new()
    {
        new Ticket
        {
            Id = 1,
            Title = "problem with water on my street",
            Description = "my street is full of toilet water is always runing down to my home enternce",
            Category = TicketCategory.Water,
            Priority = TicketPriority.High,
            ReportedBy = "Ahmad",
            Status = TicketStatus.Open,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }
    };

  
    [HttpGet]
    public IActionResult GetAll([FromQuery] TicketStatus? status, [FromQuery] TicketCategory? category)
    {
        var result = Tickets.AsEnumerable();

        if (status.HasValue)
        {
            result = result.Where(t => t.Status == status.Value);
        }

        if (category.HasValue)
        {
            result = result.Where(t => t.Category == category.Value);
        }

        return Ok(result.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var ticket = Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Ticket ticket)
    {
        ticket.Id = Tickets.Count > 0 ? Tickets.Max(t => t.Id) + 1 : 1;
        ticket.Status = TicketStatus.Open;
        ticket.CreatedAt = DateTime.UtcNow;
        ticket.UpdatedAt = DateTime.UtcNow;

        Tickets.Add(ticket);

        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Ticket ticket)
    {
        var eTicket = Tickets.FirstOrDefault(t => t.Id == id);
        if (eTicket == null)
        {
            return NotFound();
        }

        eTicket.Title = ticket.Title;
        eTicket.Description = ticket.Description;
        eTicket.Category = ticket.Category;
        eTicket.Priority = ticket.Priority;
        eTicket.UpdatedAt = DateTime.UtcNow;

        return Ok(eTicket);
    }
    
    [HttpPut("{id}/status")]
    public IActionResult UpdateStatus(int id, [FromBody] TicketStatus status)
    {
        var ticket = Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket == null)
        {
            return NotFound();
        }

        if (status <= ticket.Status)
        {
            return BadRequest($"Cannot move status from {ticket.Status} to {status}. Status can only move forward.");
        }

        ticket.Status = status;
        ticket.UpdatedAt = DateTime.UtcNow;
        

        return Ok(ticket);
    }
    
    
}