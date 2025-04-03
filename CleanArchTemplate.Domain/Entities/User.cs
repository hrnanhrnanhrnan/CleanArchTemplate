using CleanArchTemplate.Domain.Common;

namespace CleanArchTemplate.Domain.Entities;

public class User : EntityBase
{
    public required string Username { get; set; }
}
