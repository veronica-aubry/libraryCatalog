using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Library
{
  public class Book
  {
    private int _id;
    private string _title;

    public Book(string Title, int Id = 0)
    {
      _id = Id;
      _title = Title;
    }

    public override bool Equals(System.Object otherBook)
    {
        if (!(otherBook is Book))
        {
          return false;
        }
        else
        {
          Book newBook = (Book) otherBook;
          bool idEquality = this.GetId() == newBook.GetId();
          bool titleEquality = this.GetTitle() == newBook.GetTitle();
          return (idEquality && titleEquality);
        }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetTitle()
    {
      return _title;
    }
    public void SetTitle(string newTitle)
    {
      _title = newTitle;
    }
    public static List<Book> GetAll()
    {
      List<Book> allBooks = new List<Book>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM books;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string bookTitle = rdr.GetString(1);
        Book newBook = new Book(bookTitle, bookId);
        allBooks.Add(newBook);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allBooks;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO books (title) OUTPUT INSERTED.id VALUES (@BookName);", conn);

      SqlParameter titleParameter = new SqlParameter();
      titleParameter.ParameterName = "@BookName";
      titleParameter.Value = this.GetTitle();
      cmd.Parameters.Add(titleParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM books;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Book Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM books WHERE id = @BookId;", conn);
      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = id.ToString();
      cmd.Parameters.Add(bookIdParameter);
      rdr = cmd.ExecuteReader();

      int foundBookId = 0;
      string foundBookTitle = null;

      while(rdr.Read())
      {
        foundBookId = rdr.GetInt32(0);
        foundBookTitle = rdr.GetString(1);
      }
      Book foundBook = new Book(foundBookTitle, foundBookId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundBook;
    }

    public static Book FindTitle(string title)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM books WHERE title = @BookTitle;", conn);
      SqlParameter bookTitleParameter = new SqlParameter();
      bookTitleParameter.ParameterName = "@BookTitle";
      bookTitleParameter.Value = title;
      cmd.Parameters.Add(bookTitleParameter);
      rdr = cmd.ExecuteReader();

      int foundBookId = 0;
      string foundBookTitle = null;

      while(rdr.Read())
      {
        foundBookId = rdr.GetInt32(0);
        foundBookTitle = rdr.GetString(1);
      }
      Book foundBook = new Book(foundBookTitle, foundBookId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundBook;
    }



      public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM books WHERE id = @BookId; DELETE FROM authors_books WHERE book_id = @BookId;", conn);

      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = this.GetId();

      cmd.Parameters.Add(bookIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

        public void AddAuthor(Author newAuthor)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO authors_books (book_id, author_id) VALUES (@BookId, @AuthorId)", conn);
      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = this.GetId();
      cmd.Parameters.Add(bookIdParameter);

      SqlParameter authorIdParameter = new SqlParameter();
      authorIdParameter.ParameterName = "@AuthorId";
      authorIdParameter.Value = newAuthor.GetId();
      cmd.Parameters.Add(authorIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Author> GetAuthors()
   {
     SqlConnection conn = DB.Connection();
     SqlDataReader rdr = null;
     conn.Open();

     SqlCommand cmd = new SqlCommand("SELECT author_id FROM authors_books WHERE book_id = @BookId;", conn);
     SqlParameter bookIdParameter = new SqlParameter();
     bookIdParameter.ParameterName = "@BookId";
     bookIdParameter.Value = this.GetId();
     cmd.Parameters.Add(bookIdParameter);

     rdr = cmd.ExecuteReader();

     List<int> authorIds = new List<int> {};
     while(rdr.Read())
     {
       int authorId = rdr.GetInt32(0);
       authorIds.Add(authorId);
     }
     if (rdr != null)
     {
       rdr.Close();
     }

     List<Author> authors = new List<Author> {};
     foreach (int authorId in authorIds)
     {
       SqlDataReader queryReader = null;
       SqlCommand authorQuery = new SqlCommand("SELECT * FROM authors WHERE id = @AuthorId;", conn);

       SqlParameter authorIdParameter = new SqlParameter();
       authorIdParameter.ParameterName = "@AuthorId";
       authorIdParameter.Value = authorId;
       authorQuery.Parameters.Add(authorIdParameter);

       queryReader = authorQuery.ExecuteReader();
       while(queryReader.Read())
       {
             int thisAuthorId = queryReader.GetInt32(0);
             string authorName= queryReader.GetString(1);
             Author foundAuthor = new Author(authorName, thisAuthorId);
             authors.Add(foundAuthor);
       }
       if (queryReader != null)
       {
         queryReader.Close();
       }
     }
     if (conn != null)
     {
       conn.Close();
     }
     return authors;
   }

   public static int MatchBooktoJoin(int bookId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT id FROM authors_books WHERE book_id = @BookId", conn);
      SqlParameter bookParameter = new SqlParameter();
      bookParameter.ParameterName = "@BookId";
      bookParameter.Value = bookId.ToString();
      cmd.Parameters.Add(bookParameter);
      rdr = cmd.ExecuteReader();

      int authorsBookId = 0;

      while(rdr.Read())
      {
        authorsBookId = rdr.GetInt32(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return authorsBookId;
    }

   public List<Copy> GetCopies()
  {
    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT id FROM copies WHERE authors_books_id = @AuthorsBooksId;", conn);
    SqlParameter authorsBooksIdParameter = new SqlParameter();
    authorsBooksIdParameter.ParameterName = "@AuthorsBooksId";
    authorsBooksIdParameter.Value = this.GetId();
    cmd.Parameters.Add(authorsBooksIdParameter);

    rdr = cmd.ExecuteReader();

    List<int> copyIds = new List<int> {};
    while(rdr.Read())
    {
      int copyId = rdr.GetInt32(0);
      copyIds.Add(copyId);
    }
    if (rdr != null)
    {
      rdr.Close();
    }

    List<Copy> copies = new List<Copy> {};
    foreach (int copyId in copyIds)
    {
      SqlDataReader queryReader = null;
      SqlCommand copyQuery = new SqlCommand("SELECT * FROM copies WHERE id = @CopyId;", conn);

      SqlParameter copyIdParameter = new SqlParameter();
      copyIdParameter.ParameterName = "@CopyId";
      copyIdParameter.Value = copyId;
      copyQuery.Parameters.Add(copyIdParameter);

      queryReader = copyQuery.ExecuteReader();
      while(queryReader.Read())
      {
            int thisCopyId = queryReader.GetInt32(0);
            int copyAuthorsBooksId= queryReader.GetInt32(1);
            Copy foundCopy = new Copy(copyAuthorsBooksId, thisCopyId);
            copies.Add(foundCopy);
      }
      if (queryReader != null)
      {
        queryReader.Close();
      }
    }
    if (conn != null)
    {
      conn.Close();
    }
    return copies;
  }
  }
}
