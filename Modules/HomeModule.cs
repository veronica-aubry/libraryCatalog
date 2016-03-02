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

        Author newAuthor = new Author(Request.Form["book-author"]);

        if (Author.FindName(newAuthor.GetName()) == null) {
          newAuthor.Save();
          newBook.AddAuthor(newAuthor);
        } else {
          Author existingAuthor = Author.FindName(newAuthor.GetName());
          existingAuthor.AddBook(newBook);
        }



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
