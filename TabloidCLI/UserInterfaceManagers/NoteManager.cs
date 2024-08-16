using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private PostDetailManager _postDetailManager;
        private NoteManager _noteManager;
        private string _connectionString;
        private int _postId;
        public NoteManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _postDetailManager = new PostDetailManager(connectionString);
            _noteManager =  new NoteManager(connectionString);
            _connectionString = connectionString;
            _postId = postId;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Note Menu");
            Console.WriteLine(" 1) List Notes");
            Console.WriteLine(" 2) Add Notes");
            Console.WriteLine(" 3) Remove");
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
            List<Note> notes = _noteRepository.GetAll();
            foreach (Note note in notes)
            {
                Console.WriteLine(note);
            }
        }
        private Note Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Note:";
            }

            Console.WriteLine(prompt);

            List<Note> notes = _noteRepository.GetAll();

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
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
        private void Add()
        {
            Console.WriteLine("New Note");
            Note note = new Note();

            Console.Write("Tile: ");
            note.Title = Console.ReadLine();

            Console.Write("Content: ");
            note.Content = Console.ReadLine();  

            Console.Write("Create Date & Time: ");
            note.CreateDateTime = Console.ReadLine();

            _noteRepository.InsertNote(note);
        }
        private void Remove()
        {
            Note notetoDelete = Choose("Which author would you like to remove?");
            if(notetoDelete != null)
            {
                _noteRepository.DeleteNote(notetoDelete.Id);
            }
        }
    }
}
