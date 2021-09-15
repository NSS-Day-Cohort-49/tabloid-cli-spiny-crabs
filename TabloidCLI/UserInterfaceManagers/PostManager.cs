using System;
using System.Collections.Generic;
using TabloidCLI.Models;


namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private Repositories.PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private Repositories.BlogRepository _blogRepository;
        private string _connectionString;


        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new Repositories.PostRepository(connectionString);
            _blogRepository = new Repositories.BlogRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Posts");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 5) Note Management");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;

                case "2":
                    Add();
                    return this;

                //case "4":
                //    Edit();
                //    return this;
                //case "5":
                //    Remove();
                //    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"{post.Title} - {post.Url}");

            }
        }




        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title of Post: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            Console.WriteLine("Please select Authors Index: ");
            List<Author> authors = _authorRepository.GetAll();
            for (int i = 0; i < authors.Count; i++)
            {
                Author newAuthor = authors[i];
                Console.WriteLine($" {i + 1}) {newAuthor.FullName}");
            }
            Console.Write("> ");

            int authorIndex = int.Parse(Console.ReadLine());
            
            post.Author = authors[authorIndex-1]; 

            Console.WriteLine("Please select a Blog Value: ");
            List<Blog> blogs = _blogRepository.GetAll();
            for (int i = 0; i < blogs.Count; i++)
            {
                Blog newBlog = blogs[i];
                Console.WriteLine($" {i + 1}) {newBlog.Title}");
            }
            Console.Write("> ");
            int blogIndex = int.Parse(Console.ReadLine());
            post.Blog = blogs[blogIndex-1];

            post.PublishDateTime = DateTime.UtcNow;

            _postRepository.Insert(post);
            Console.WriteLine("Post successfully added!");
            Console.WriteLine(" ");
        }


    }
}
