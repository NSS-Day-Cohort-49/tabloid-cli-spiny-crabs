using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Repositories;
using TabloidCLI.Models;
using System.Linq;

namespace TabloidCLI.UserInterfaceManagers
{
    class NoteManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private PostRepository _postRepository;
        private int _postId;


        public NoteManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _postId = postId;

        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Note Manager");
            Console.WriteLine("1) View Notes");
            Console.WriteLine("2) Add a Note");
            Console.WriteLine("3) Remove a Note");
            Console.WriteLine(" 0) Go Back");

            Console.WriteLine("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ViewNotes();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void ViewNotes()
        {
            List<Note> notes = _noteRepository.GetPostNotes(_postId);
            foreach (Note note in notes)
            {
                Console.WriteLine(note);
            }
        }
    }
}
