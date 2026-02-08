namespace LiveCoding_Middle_CleanArch.Domain.Entities;

public class Review
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
    public string ReviewerName { get; set; }
    public string ReviewerEmail { get; set; }
}