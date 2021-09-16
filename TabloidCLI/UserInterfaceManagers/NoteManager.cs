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
                case "2":
                    AddNote();
                    return this;
                case "3":
                    Remove();
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

        private Note Choose(string prompt = null)
        {
            //if (prompt == null)
            //{
            //    prompt = "Please choose a note: ";
            //}

            Console.WriteLine("Please choose a note: ");

            List<Note> notes = _noteRepository.GetPostNotes(_postId);

            for (int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i];
                Console.WriteLine($" {i + 1}) {note.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return notes[choice - 1];
            }
            catch
            {
                Console.WriteLine("Invalid selection. Please choose another option");
                return null;
            }


        }

        private void AddNote()
        {
            Console.WriteLine("New Note");
            Note note = new Note();

            Console.Write("Title: ");
            note.Title = Console.ReadLine();

            Console.WriteLine("Content: ");
            note.Content = Console.ReadLine();

            note.CreateDateTime = DateTime.UtcNow;

            _noteRepository.Insert(note, _postId);
        }

        private void Remove()
        {
            Note noteToRemove = Choose("Which note would you like to remove? ");
            if (noteToRemove != null)
            {
                _noteRepository.Delete(noteToRemove.Id);
            }
        }
    }
}
