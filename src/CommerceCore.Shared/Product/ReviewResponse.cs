using System;

public class ReviewResponse
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Title { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}
