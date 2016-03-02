using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class BookTest : IDisposable
  {

    public void Dispose()
    {
      Author.DeleteAll();
      Book.DeleteAll();
    }

    public BookTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_GetAuthors_RetrievesAllAuthorsWithBook()
    {
      //Arrange
      Book testBook = new Book("Household chores");
      testBook.Save();

      Author firstAuthor = new Author("Veronica Alley");
      firstAuthor.Save();
      firstAuthor.AddBook(testBook);
      Author secondAuthor = new Author("Alison Vu");
      secondAuthor.Save();
      secondAuthor.AddBook(testBook);

      //Act
      List<Author> testAuthorList = new List<Author> {firstAuthor, secondAuthor};
      List<Author> resultAuthorList = testBook.GetAuthors();

      //Assert
      Assert.Equal(testAuthorList, resultAuthorList);
    }

    [Fact]
    public void Test_Delete_DeletesBookFromDatabase()
    {
      //Arrange
      string name1 = "Ask the Passengers";
      Book testBook1 = new Book(name1);
      testBook1.Save();

      string name2 = "Baron in the Trees";
      Book testBook2 = new Book(name2);
      testBook2.Save();

      //Act
      testBook1.Delete();
      List<Book> resultBook = Book.GetAll();
      List<Book> testBookList = new List<Book> {testBook2};

      Assert.Equal(testBookList, resultBook);
    }

      [Fact]
    public void Test_Delete_DeletesBookAssociationsFromDatabase()
    {
      //Arrange
      Author testAuthor = new Author("Veronica Alley");
      testAuthor.Save();

      string testName = "Home stuff";
      Book testBook = new Book(testName);
      testBook.Save();

      //Act
      testBook.AddAuthor(testAuthor);
      testBook.Delete();

      List<Book> resultAuthorBooks = testAuthor.GetBooks();
      List<Book> testAuthorBooks = new List<Book> {};

      //Assert
      Assert.Equal(testAuthorBooks, resultAuthorBooks);
    }

    [Fact]
    public void Test_BooksEmptyAtFirst()
    {
      //Arrange, Act
      int result = Book.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

      [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Book firstBook = new Book("Ask the Passengers");
      Book secondBook = new Book("Ask the Passengers");

      //Assert
      Assert.Equal(firstBook, secondBook);
    }

    [Fact]
    public void Test_Save_SavesBookToDatabase()
    {
      //Arrange
      Book testBook = new Book("Household chores");
      testBook.Save();

      //Act
      List<Book> result = Book.GetAll();
      List<Book> testList = new List<Book>{testBook};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToBookObject()
    {
      //Arrange
      Book testBook = new Book("Household chores");
      testBook.Save();

      //Act
      Book savedBook = Book.GetAll()[0];

      int result = savedBook.GetId();
      int testId = testBook.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
  public void Test_Find_FindsBookInDatabase()
  {
    //Arrange
    Book testBook = new Book("Household chores");
    testBook.Save();

    //Act
    Book foundBook = Book.Find(testBook.GetId());

    //Assert
    Assert.Equal(testBook, foundBook);
  }



  }
}
