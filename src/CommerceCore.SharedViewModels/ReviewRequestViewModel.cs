using System;

public class ReviewRequestViewModel
{
    public int Rating { get; set; }
    public string Title { get; set; }
    public string Comment { get; set; }
    public Guid ProductId { get; set; }
    public string FullName { get; set; } = null;
    public string PhoneNumber { get; set; } = null;
    public string Email { get; set; } = null;
}
