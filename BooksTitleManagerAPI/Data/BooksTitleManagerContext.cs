using System;
using System.Collections.Generic;
using BooksTitleManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksTitleManagerAPI.Data;

public partial class BooksTitleManagerContext : DbContext
{
    public BooksTitleManagerContext()
    {
    }

    public BooksTitleManagerContext(DbContextOptions<BooksTitleManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BooksModelApi> BooksModelApis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BooksModelApi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BooksAPI__3214EC0799A9F136");

            entity.ToTable("BooksModelAPI");

            entity.Property(e => e.Title).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
