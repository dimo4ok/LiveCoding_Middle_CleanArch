namespace LiveCoding_Middle_CleanArch.Application.Common.Models;
public record ReviewModel(
    int Rating,
    string Comment,
    DateTime Date,
    string ReviewerName,
    string ReviewerEmail
    );