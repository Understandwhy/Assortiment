﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assortiment
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ASSORTIMENT> ASSORTIMENT { get; set; }
        public DbSet<CATEGORY> CATEGORY { get; set; }
        public DbSet<OTCHETI> OTCHETI { get; set; }
        public DbSet<USER> USER { get; set; }
        public DbSet<USER_HISTIRY> USER_HISTIRY { get; set; }
    }
}
