using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;


namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;


        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
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
            //Console.WriteLine(" 5) Note Management");
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
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
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

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }


        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New URL (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            Console.WriteLine("New Authors Index (blank to leave unchanged): ");
            List<Author> authors = _authorRepository.GetAll();
            for (int i = 0; i < authors.Count; i++)
            {
                Author newAuthor = authors[i];
                Console.WriteLine($" {i + 1}) {newAuthor.FullName}");
            }
            Console.Write("> ");
            string authorIndexString = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(authorIndexString))
            {
                postToEdit.Author = authors[int.Parse(authorIndexString) - 1];
            }

            Console.WriteLine("New Blog Index (blank to leave unchanged): ");
            List<Blog> blogs = _blogRepository.GetAll();
            for (int i = 0; i < blogs.Count; i++)
            {
                Blog newBlog = blogs[i];
                Console.WriteLine($" {i + 1}) {newBlog.Title}");
            }
            Console.Write("> ");
            string blogIndexString = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(blogIndexString))
            {
                postToEdit.Blog = blogs[int.Parse(blogIndexString) - 1];
            }
            postToEdit.PublishDateTime = DateTime.UtcNow;
            _postRepository.Update(postToEdit);
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

            post.Author = authors[authorIndex - 1];

            Console.WriteLine("Please select a Blog Value: ");
            List<Blog> blogs = _blogRepository.GetAll();
            for (int i = 0; i < blogs.Count; i++)
            {
                Blog newBlog = blogs[i];
                Console.WriteLine($" {i + 1}) {newBlog.Title}");
            }
            Console.Write("> ");
            int blogIndex = int.Parse(Console.ReadLine());
            post.Blog = blogs[blogIndex - 1];

            post.PublishDateTime = DateTime.UtcNow;

            _postRepository.Insert(post);
            Console.WriteLine("Post successfully added!");
            Console.WriteLine(" ");
        }

        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }

    }
}
