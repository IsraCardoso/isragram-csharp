﻿using isragram_csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace DevagramCSharp.Models
{
    public class IsragramContext : DbContext
    {
        public IsragramContext(DbContextOptions<IsragramContext> option) : base(option)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
