namespace WebApplication1.Models;

public class Comment
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string Author { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
}