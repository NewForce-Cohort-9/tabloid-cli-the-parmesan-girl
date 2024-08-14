using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUi;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUi, string connectionString)
        {
            _parentUi = parentUi;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 2) View Journal Entry");
            Console.WriteLine(" 3) Add Journal Entry");
            Console.WriteLine(" 4) Edit Journal Entry");
            Console.WriteLine(" 5) Remove Journal Entry");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Journal journal = Choose();
                    if (journal == null)
                    {
                        return this;
                    }
                    else
                    {
                        Console.WriteLine($"Id: {journal.Id}");
                        Console.WriteLine($"Created On: {journal.CreateDateTime}");
                        Console.WriteLine($"Title: {journal.Title}");
                        Console.WriteLine($"Content: {journal.Content}");

                        Console.Write("Press any key to continue");
                        Console.ReadLine();
                    }
                    return this;
                case "3":
                    Add();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
                    Remove();
                    return this;
                case "0":
                    return _parentUi;
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
                Console.WriteLine(journal.Title);
            }
        }

        private Journal Choose(string prompt = null)
        {
            if ( prompt == null)
            {
                prompt = "Please choose a journal entry";
            }

            Console.WriteLine(prompt);

            List<Journal> journals = _journalRepository.GetAll();
            for ( int i = 0; i < journals.Count; i++ )
            {
                Journal journal = journals[i];
                Console.WriteLine($"{i+1}) {journal.Title}");
            }
            Console.Write("> ");
            string input = Console.ReadLine();

            try
            {
                int choice = int.Parse(input);
                return journals[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Journal Entry");
            Journal journal = new Journal();

            Console.Write("Title: ");
            journal.Title = Console.ReadLine();

            Console.Write("Content: ");
            journal.Content = Console.ReadLine();

            journal.CreateDateTime = DateTime.Now;

            _journalRepository.Insert(journal);
        }

        private void Edit()
        {
            Journal journalToEdit = Choose("Which journal entry would you like to edit?");
            if( journalToEdit == null )
            {
                return;
            }
            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if(!string.IsNullOrEmpty(title) )
            {
                journalToEdit.Title = title;
            }
            Console.Write("New Content (blank to leave unchaned): ");
            string content = Console.ReadLine();
            if(!string.IsNullOrEmpty(content) )
            {
                journalToEdit.Content = content;
            }

            _journalRepository.Update(journalToEdit);
        }

        private void Remove()
        {
            Journal journalToDelete = Choose("Which journal entry would you like to delete?");
            if ( journalToDelete != null )
            {
                _journalRepository.Delete(journalToDelete.Id);
            }
        }
    }
}
