// See https://aka.ms/new-console-template for more information

Console.Clear();

List<(string Title, string Author, int Pages)> booksList = new List<(string, string, int)>();

bool addAnotherBook;

do
{
    //Get details for book from user!
    Console.Write("Enter book title: ");
    string title = Console.ReadLine();

    Console.Write("Enter author's full name: ");
    string author = Console.ReadLine();

    Console.Write("Enter number of pages: ");
    int pages;
    while(!int.TryParse(Console.ReadLine(), out pages) || pages <= 0)
    {
        Console.WriteLine("Please enter a valid, positive number for pages.");
        Console.Write("Enter page numbers: ");
    }

    //Tuple that adds things to a list
    var newBook = (title, author, pages);
    booksList.Add(newBook);

    //Shows gathered info
    Console.WriteLine("\n Book Information:");
    foreach (var book in booksList)
    {
        Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Pages: {book.Pages}");
    }

    //Saves everything into a file
    SaveBookInfoToFile(newBook);

    //See if user wants to add another book
    Console.Write("Do you want to add another book? (yes/no): ");
    addAnotherBook = Console.ReadLine()?.ToLower() == "yes";
} while (addAnotherBook);

//User can pick which book in list they want to read
if (booksList.Count > 0)
{
    Console.WriteLine("\n Choose a book to start reading: ");

    for (int i = 0; i < booksList.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {booksList[i].Title} by {booksList[i].Author}");
    }

    int selectedBookIndex;
    do
    {
        Console.Write("Enter the number of the book you want to read: ");
    } while (!int.TryParse(Console.ReadLine(), out selectedBookIndex) || selectedBookIndex < 1 || selectedBookIndex > booksList.Count);

    //Start countdown for 3 mins. Timer takes up screen still and don't know how to exit timer
    Console.WriteLine($"You will start reading {booksList[selectedBookIndex - 1].Title} in 3 minutes");
    CountdownTimer(3*60);

    //When time is up, asks user if they completed the reading. 
    Console.Write("Did you finish the reading in three minutes? (write complete if yes!)");
    string completionStatus = Console.ReadLine()?.ToLower();

    if (completionStatus == "complete")
    {
        Console.WriteLine("Congrats! You did it!");
    }

    else
    {
        Console.WriteLine("You can try again, it's okay.");
    }
}
else
{
    Console.WriteLine("No books added, exiting program.");
}

    static void SaveBookInfoToFile((string Title, string Author, int Pages) book)
    {
        string filePath = "books.txt";

        // Makes a line with the information
        string bookInfo = $"{book.Title}, {book.Author}, {book.Pages}";

        // Adds book information to the file
        File.AppendAllText(filePath, $"{bookInfo}\n");

        Console.WriteLine($"Book information saved to {filePath}");
    }

    static void CountdownTimer(int seconds)
    {
        Console.WriteLine($"Reading period: 3 minutes countdown started.");
        //shows timer and each second passing
        while (seconds > 0)
        {
            Console.WriteLine($"Time remaining: {TimeSpan.FromSeconds(seconds)}");
            Thread.Sleep(1000); 
            seconds--;
        }

        Console.WriteLine("Reading period expired.");
    }

