using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List all Journals");
            Console.WriteLine(" 2) Add Journal");
            Console.WriteLine(" 3) Edit Journal Entry");
/*            Console.WriteLine(" 4) Edit Author");
            Console.WriteLine(" 5) Remove Author");*/
            Console.WriteLine(" 0) Go Back");

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
                /*case "4":
                    Edit();
                    return this;
                case "5":
                    Remove();
                    return this;*/
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            foreach (Journal journal in journals)
            {
                Console.WriteLine($"Title: {journal.Title}");
                Console.WriteLine($"{journal.Content}");
                Console.WriteLine($"Date: {journal.CreateDateTime}");
            }
        }

        private void Add()
        {
            Console.WriteLine("New Journal Entry");
            Journal journal = new Journal();

            Console.Write("Title:");
            journal.Title = Console.ReadLine();

            Console.WriteLine("Content:");
            journal.Content = Console.ReadLine();

            journal.CreateDateTime = DateTime.UtcNow;

            _journalRepository.Insert(journal);
        }

        private void Edit()
        {
            List<Journal> journals = _journalRepository.GetAll();
            foreach (Journal j in journals)
            {
                Console.WriteLine($"Title: ({j.Id}) {j.Title}");
                Console.WriteLine($"{j.Content}");
                Console.WriteLine($"Date: {j.CreateDateTime}");
            }
            Journal journal = new Journal();
            Console.Write("Which journal entry would you like to edit? ");
            journal.Id = int.Parse(Console.ReadLine());
            if (journal.Id != null)
            {
                return;
            }
            Console.Write("What would you like the title to be? (Leave blank if no changes)");
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                authorToEdit.FirstName = firstName;
            }
            Console.Write
        }
    }
}
