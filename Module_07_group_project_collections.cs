
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module7DictionaryAssignment // Gerson Henriquez & Devonte Pandohie
{
    internal class Program
    {
        // The key is the book genre, and the value is a list of books under that genre.
        // List<string> allows us to store multiple books under one genre.
        // IgnoreCase allows users to type "fiction" or "Fiction" without any issues.
        private static readonly Dictionary<string, List<string>> bookGenres =
            new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        private static void Main()
        {
            bool running = true;

            // Keeps the program running until the user chooses to exit.
            while (running)
            {
                ShowMenu();
                string choice = (Console.ReadLine() ?? "").Trim();

                // The switch statement allows the user to select what they want to do.
                switch (choice)
                {
                    case "1":
                        PopulateDictionary();
                        break;
                    case "2":
                        DisplayDictionary();
                        break;
                    case "3":
                        RemoveKey();
                        break;
                    case "4":
                        AddNewKeyAndValue();
                        break;
                    case "5":
                        AddValueToExistingKey();
                        break;
                    case "6":
                        SortAndDisplayKeys();
                        break;
                    case "7":
                        SearchForBook();
                        break;
                    case "8":
                        running = false;
                        Console.WriteLine("Thanks for using the Book Genre Dictionary!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please enter a number from 1 to 8.");
                        break;
                }
            }
        }

        // Displays all six assignment tasks, an additional search feature, and an exit option.
        private static void ShowMenu()
        {
            Console.WriteLine("\n===== BOOK GENRE DICTIONARY =====");
            Console.WriteLine("1. Populate the Dictionary");
            Console.WriteLine("2. Display Dictionary Contents");
            Console.WriteLine("3. Remove a Key");
            Console.WriteLine("4. Add a New Key and Value");
            Console.WriteLine("5. Add a Value to an Existing Key");
            Console.WriteLine("6. Sort the Keys");
            Console.WriteLine("7. Search for a Book");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option (1-8): ");
        }

        // Populates the dictionary with premade genres and books.
        private static void PopulateDictionary()
        {
            // Checks if the dictionary already has data before replacing anything.
            if (bookGenres.Count > 0)
            {
                Console.Write("This will replace existing entries. Continue? (y/n): ");
                string answer = (Console.ReadLine() ?? "").Trim();

                // If the user does not enter y, the dictionary will remain unchanged.
                if (!answer.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Operation cancelled.");
                    return;
                }
            }

            // Clears the old entries before adding the premade ones.
            bookGenres.Clear();

            bookGenres.Add("Fiction", new List<string>
            {
                "Star Wars: The Last Jedi",
                "Cars 2"
            });

            bookGenres.Add("Murder Mystery", new List<string>
            {
                "One of Us Is Lying",
                "The Silent Patient"
            });

            bookGenres.Add("Fantasy Comedy", new List<string>
            {
                "Timmy Failure: Mistakes Were Made",
                "Timmy Failure: Now Look What You've Done"
            });

            Console.WriteLine($"Dictionary populated with {bookGenres.Count} genres.");
        }

        // Displays every genre along with all the books stored under it.
        private static void DisplayDictionary()
        {
            // Checks if there is anything to display before going through the dictionary.
            if (bookGenres.Count == 0)
            {
                Console.WriteLine("The dictionary is empty. Choose option 1 or 4.");
                return;
            }

            Console.WriteLine("\n===== DICTIONARY CONTENTS =====");

            // Goes through each key and its values, then displays them together.
            foreach (KeyValuePair<string, List<string>> entry in bookGenres)
            {
                Console.WriteLine($"{entry.Key}: {string.Join(", ", entry.Value)}");
            }

            // Counts all the books stored in the dictionary across every genre.
            int totalBooks = bookGenres.Values.Sum(books => books.Count);

            Console.WriteLine($"\nTotal Genres: {bookGenres.Count}");
            Console.WriteLine($"Total Books: {totalBooks}");
        }

        // Removes a genre and all the books associated with it.
        private static void RemoveKey()
        {
            // Makes sure the dictionary is not empty before trying to remove a key.
            if (bookGenres.Count == 0)
            {
                Console.WriteLine("The dictionary is empty.");
                return;
            }

            string genre = ReadRequiredInput("Enter the genre to remove: ");

            // Remove() returns true if the genre was found and removed.
            if (bookGenres.Remove(genre))
            {
                Console.WriteLine($"{genre} was removed successfully.");
            }
            else
            {
                Console.WriteLine($"{genre} was not found. Please try again.");
            }
        }

        // Adds a completely new genre and its first book to the dictionary.
        private static void AddNewKeyAndValue()
        {
            // Gets the new genre and book title from the user.
            string genre = ReadRequiredInput("Enter the new genre to add: ");
            string book = ReadRequiredInput("Enter the new book for the genre: ");

            // Checks if the genre already exists to avoid adding duplicate keys.
            if (bookGenres.ContainsKey(genre))
            {
                Console.WriteLine($"{genre} already exists. Use option 5 instead.");
                return;
            }

            // Adds the new genre as a key and the book as its first value.
            bookGenres.Add(genre, new List<string> { book });

            Console.WriteLine($"Added genre '{genre}' with book '{book}'.");
        }

        // Adds another book to a genre that is already in the dictionary.
        private static void AddValueToExistingKey()
        {
            string genre = ReadRequiredInput("Enter the existing genre: ");

            // Checks if the genre exists and gets the list of books under it.
            if (!bookGenres.TryGetValue(genre, out var books))
            {
                Console.WriteLine($"{genre} was not found. Please try again.");
                return;
            }

            string book = ReadRequiredInput("Enter the book title to add: ");

            // Checks the existing books to make sure the new book is not a duplicate.
            // IgnoreCase makes sure capitalization does not affect the comparison.
            bool duplicate = books.Any(existingBook =>
                existingBook.Equals(book, StringComparison.OrdinalIgnoreCase));

            if (duplicate)
            {
                Console.WriteLine($"{book} already exists under {genre}.");
                return;
            }

            // If the book is not a duplicate, it gets added to the genre's list.
            books.Add(book);

            Console.WriteLine($"Added '{book}' to '{genre}'.");
        }

        // Sorts the dictionary keys alphabetically from A to Z.
        private static void SortAndDisplayKeys()
        {
            // Checks if the dictionary is empty before trying to sort anything.
            if (bookGenres.Count == 0)
            {
                Console.WriteLine("The dictionary is empty. Nothing to sort.");
                return;
            }

            // Takes all the genre keys and puts them in alphabetical order.
            // ToList() stores the sorted keys in a separate list.
            List<string> sortedKeys = bookGenres.Keys
                .OrderBy(key => key, StringComparer.OrdinalIgnoreCase)
                .ToList();

            Console.WriteLine("\n===== GENRES (A TO Z) =====");

            // Displays each sorted genre along with the books belonging to it.
            foreach (string key in sortedKeys)
            {
                Console.WriteLine($"{key}: {string.Join(", ", bookGenres[key])}");
            }
        }

        // Extra feature that allows the user to search for a book by its title.
        private static void SearchForBook()
        {
            // Prevents searching when there are no books in the dictionary.
            if (bookGenres.Count == 0)
            {
                Console.WriteLine("The dictionary is empty.");
                return;
            }

            string search = ReadRequiredInput("Enter a book title to search: ");
            bool found = false;

            // Goes through each genre and checks every book stored under it.
            foreach (var entry in bookGenres)
            {
                foreach (string book in entry.Value)
                {
                    // Checks if the book title contains what the user searched for.
                    // This allows partial searches and ignores uppercase or lowercase letters.
                    if (book.Contains(search, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Found: {book} | Genre: {entry.Key}");
                        found = true;
                    }
                }
            }

            // If no matching book was found, the user will be notified.
            if (!found)
            {
                Console.WriteLine("No matching book was found.");
            }
        }

        // Makes sure the user enters something instead of leaving the input blank.
        private static string ReadRequiredInput(string prompt)
        {
            // Keeps asking for input until the user types something.
            while (true)
            {
                Console.Write(prompt);

                // Reads the user's input and removes unnecessary spaces.
                string input = (Console.ReadLine() ?? "").Trim();

                // If the input is not empty, it gets returned to the method that called it.
                if (input.Length > 0)
                {
                    return input;
                }

                // If the input was blank, the user will be asked to try again.
                Console.WriteLine("Input cannot be blank. Please try again.");
            }
        }
    }
}
