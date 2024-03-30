using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class BloggingContext : DbContext 
{
    public DbSet<Blog> Blogs{get; set;}

    public DbSet<Post> Posts {get; set;}

    public DbSet<Comment> Comments {get; set;}

    public string DbPath {get;}
    
    public BloggingContext() 
    {
        //eventually will be a parameter
        DbPath = "blogging.db";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}


public class Blog
{
    public int BlogId {get; set;}

    public string Url {get; set;}

    public List<Post> Posts {get;} = new();
}


//normally make this in a separate file
public class Post
{
    public int PostId {get; set;}

    public string Title {get; set;}

    public string Content {get; set;}

    public int BlogId {get; set;}

    public List<Comment> Comments {get;} = new();

    public Blog Blog {get; set;}
}


public class Comment
{
    public int CommentId {get; set;}

    public string Content {get; set;}

    public int PostId {get; set;}

    public Post Post {get; set;}

}