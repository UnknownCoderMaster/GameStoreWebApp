using GameStoreWebApp.Domain.Commons;
using System;

namespace GameStoreWebApp.Domain.Entities.Users;

public class Verification : BaseEntity
{
    public Guid Code { get; set; }
    public string Email { get; set; }
}
