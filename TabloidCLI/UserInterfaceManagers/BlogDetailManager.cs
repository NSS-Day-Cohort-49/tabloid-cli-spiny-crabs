using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Repositories;
using TabloidCLI.Models;
using System.Linq;

namespace TabloidCLI.UserInterfaceManagers
{
    class BlogDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private TagRepository _tagRepository;
        private int _blogId;
        public BlogDetailManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _blogId = blogId;
        }

        public IUserInterfaceManager Execute()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"{blog.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            //Console.WriteLine(" 4) View Posts");
            Console.WriteLine(" 0) Go Back");

            Console.WriteLine("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    AddTag();
                    return this;
                case "3":
                    RemoveTag();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"Title: {blog.Title}");
            Console.WriteLine($"Url {blog.Url}");
            foreach (Tag blogtag in blog.Tags)
            {
                Console.WriteLine(" " + blogtag);
            }
            Console.WriteLine();
        }

        private void AddTag()
        {
            Blog blog = _blogRepository.Get(_blogId);

            Console.WriteLine($"Which tag would you like to add to {blog.Title}?");
            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                if (blog.Tags.Any(t => t.Id == tag.Id))
                {
                    Console.WriteLine($"{blog.Title} already has the {tag} tag.");
                }
                else
                {
                    _blogRepository.InsertTag(blog, tag);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection. Won't add any tags.");
            }
        }

        private void RemoveTag()
        {
            Blog blog = _blogRepository.Get(_blogId);

            if (blog.Tags.Count == 0)
            {
                Console.WriteLine("No tags to remove.");
                return;
            }

            Console.WriteLine($"Which tag would you like to remove from {blog.Title}");

            for (int i = 0; i < blog.Tags.Count; i++)
            {
                Tag tag = blog.Tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = blog.Tags[choice - 1];
                _blogRepository.DeleteTag(blog, tag);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection. Won't remove any tags.");
            }
        }
    }
}

