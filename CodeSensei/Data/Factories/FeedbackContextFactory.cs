using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CodeSensei.Data.Contexts;
using System;

public class FeedbackContextFactory : IDesignTimeDbContextFactory<FeedbackContext>
{
    public FeedbackContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FeedbackContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FeedbackDB;Trusted_Connection=True;");

        return new FeedbackContext(optionsBuilder.Options);
    }
}
