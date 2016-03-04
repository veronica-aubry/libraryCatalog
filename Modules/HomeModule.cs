using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Library
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Author> allAuthors = Author.GetAll();
      List<Book> allBooks = Book.GetAll();
      model.Add("authors", allAuthors);
      model.Add("books", allBooks);
      return View["index.cshtml", model];
      };

      Post["/author_search"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Author foundAuthor = Author.FindName(Request.Form["author-name"]);
        List<Book> AuthorBooks = foundAuthor.GetBooks();
        model.Add("author", foundAuthor);
        model.Add("authorBooks", AuthorBooks);
        return View["search_results.cshtml", model];
      };

      Post["/title_search"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Book foundBook = Book.FindTitle(Request.Form["book-name"]);
        List<Author> AuthorBooks = foundBook.GetAuthors();
        model.Add("book", foundBook);
        model.Add("authorBooks", AuthorBooks);
        return View["search_results_title.cshtml", model];
      };


      Post["/book/new"] = _ => {
        Book newBook = new Book(Request.Form["book-title"]);
        newBook.Save();
        Copy newCopy = new Copy(newBook.GetId());
        newCopy.Save();
        Author newAuthor = new Author(Request.Form["book-author"]);
        newAuthor.Save();
        newBook.AddAuthor(newAuthor);


        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Author> allAuthors = Author.GetAll();
        List<Book> allBooks = Book.GetAll();
        model.Add("authors", allAuthors);
        model.Add("books", allBooks);

        return View["index.cshtml", model];
      };

      Get["/books/{id}/"] = parameters => {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Book SelectedBook = Book.Find(parameters.id);
      List<Author> allAuthors = SelectedBook.GetAuthors();
      List<Copy> allCopies = SelectedBook.GetCopies();
      model.Add("book", SelectedBook);
      model.Add("copies", allCopies);
      model.Add("authors", allAuthors);
      return View["book.cshtml", model];
      };

      Patch["/books/{id}/return/{patron_id}/{copy_id}"] = parameters => {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Book SelectedBook = Book.Find(parameters.id);
      Checkout SelectedCheckout = Checkout.Find(parameters.patron_id, parameters.copy_id);
      SelectedCheckout.Returned();
      List<Author> allAuthors = SelectedBook.GetAuthors();
      List<Copy> allCopies = SelectedBook.GetCopies();
      model.Add("book", SelectedBook);
      model.Add("copies", allCopies);
      model.Add("authors", allAuthors);
      return View["book.cshtml", model];
      };

      Post["/books/{id}/newCopy"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Book SelectedBook = Book.Find(parameters.id);
        Copy newCopy = new Copy(parameters.id);
        newCopy.Save();
        List<Author> allAuthors = SelectedBook.GetAuthors();
        List<Copy> allCopies = SelectedBook.GetCopies();
        model.Add("book", SelectedBook);
        model.Add("copies", allCopies);
        model.Add("authors", allAuthors);
        return View["book.cshtml", model];
        };

      Post["/books/{id}/newCheckout"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Book SelectedBook = Book.Find(parameters.id);
        Patron newPatron = new Patron(Request.Form["patron-name"]);
        newPatron.Save();
        Copy selectedCopy = Copy.Find(Request.Form["copy-id"]);
        Checkout newCheckout = new Checkout(selectedCopy.GetId(), newPatron.GetId(), Request.Form["duedate"]);
        newCheckout.Save();
        List<Author> allAuthors = SelectedBook.GetAuthors();
        List<Copy> allCopies = SelectedBook.GetCopies();
        model.Add("book", SelectedBook);
        model.Add("copies", allCopies);
        model.Add("authors", allAuthors);
        return View["book.cshtml", model];
        };

        Get["/wipe"] = _ => {
        Book.DeleteAll();
        Author.DeleteAll();
        Copy.DeleteAll();
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Author> allAuthors = Author.GetAll();
        List<Book> allBooks = Book.GetAll();
        model.Add("authors", allAuthors);
        model.Add("books", allBooks);
        return View["index.cshtml", model];
        };

    }
  }
}
