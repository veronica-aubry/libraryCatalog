using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class AuthorTest : IDisposable
  {


    public void Dispose()
    {
      Author.DeleteAll();
      Book.DeleteAll();
    }

    public AuthorTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

        [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Author.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

        [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAreTheSame()
    {
      //Arrange, Act
      Author firstAuthor = new Author("Veronica Alley");
      Author secondAuthor = new Author("Veronica Alley");

      //Assert
      Assert.Equal(firstAuthor, secondAuthor);
    }

        [Fact]
    public void Test_Find_FindsAuthorInDatabase()
    {
      //Arrange
      Author testAuthor = new Author("Veronica Alley");
      testAuthor.Save();

      //Act
      Author foundAuthor = Author.Find(testAuthor.GetId());

      //Assert
      Assert.Equal(testAuthor, foundAuthor);
    }

        [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Author testAuthor = new Author("Veronica Alley");

      //Act
      testAuthor.Save();
      List<Author> result = Author.GetAll();
      List<Author> testList = new List<Author>{testAuthor};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      //Arrange
      Author testAuthor = new Author("Veronica Alley");
      testAuthor.Save();

      //Act
      Author savedAuthor = Author.GetAll()[0];

      int result = savedAuthor.GetId();
      int testId = testAuthor.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
   public void Test_FindFindsAuthorInDatabase()
   {
     //Arrange
     Author testAuthor = new Author("Veronica Alley");
     testAuthor.Save();

     //Act
     Author foundAuthor = Author.Find(testAuthor.GetId());

     //Assert
     Assert.Equal(testAuthor, foundAuthor);
   }

   [Fact]
    public void Test_AddBook_AddsBookToAuthor()
    {
      //Arrange
      Author testAuthor = new Author("Veronica Alley");
      testAuthor.Save();

      Book testBook = new Book("Home stuff");
      testBook.Save();

      //Act
      testAuthor.AddBook(testBook);

      List<Book> result = testAuthor.GetBooks();
      List<Book> testList = new List<Book>{testBook};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetBooks_ReturnsAllAuthorBooks()
    {
      //Arrange
      Author testAuthor = new Author("Veronica Alley");
      testAuthor.Save();

      Book testBook1 = new Book("Home stuff");
      testBook1.Save();

      Book testBook2 = new Book("Work stuff");
      testBook2.Save();

      //Act
      testAuthor.AddBook(testBook1);
      List<Book> result = testAuthor.GetBooks();
      List<Book> testList = new List<Book> {testBook1};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Delete_DeletesAuthorAssociationsFromDatabase()
    {
      //Arrange
      Book testBook = new Book("Home stuff");
      testBook.Save();

      string testName = "Veronica Alley";
      Author testAuthor = new Author(testName);
      testAuthor.Save();

      //Act
      testAuthor.AddBook(testBook);
      testAuthor.Delete();

      List<Author> resultBookAuthors = testBook.GetAuthors();
      List<Author> testBookAuthors = new List<Author> {};

      //Assert
      Assert.Equal(testBookAuthors, resultBookAuthors);
    }
  }
}
