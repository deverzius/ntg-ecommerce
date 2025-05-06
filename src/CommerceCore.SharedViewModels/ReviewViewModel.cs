using System;

public class ReviewViewModel
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Title { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
    public string FullName { get; set; } = null;
    public string PhoneNumber { get; set; } = null;
    public string Email { get; set; } = null;
}
