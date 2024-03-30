using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;


using var db = new BloggingContext();

db.Add(new Blog{Url="http://whatever"});
db.SaveChanges();


var blog = db.Blogs.Include(b=>b.Posts).First();
Console.WriteLine(blog.Posts.Count);
Console.WriteLine(blog.Url);
blog.Posts.Add(new Post {Title="Hello", Content="Whatver"});
db.SaveChanges();