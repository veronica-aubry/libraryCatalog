using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Library
{
  public class Copy
  {
    private int _id;
    private int _authors_books_id;

    public Copy(int AuthorsBooksId, int Id = 0)
    {
      _id = Id;
      _authors_books_id = AuthorsBooksId;
    }

    public override bool Equals(System.Object otherCopy)
    {
        if (!(otherCopy is Copy))
        {
          return false;
        }
        else
        {
          Copy newCopy = (Copy) otherCopy;
          bool idEquality = this.GetId() == newCopy.GetId();
          bool authors_books_idEquality = this.GetAuthorsBooksId() == newCopy.GetAuthorsBooksId();
          return (idEquality && authors_books_idEquality);
        }
    }

    public int GetId()
    {
      return _id;
    }
    public int GetAuthorsBooksId()
    {
      return _authors_books_id;
    }
    public void SetAuthorsBooksId(int newAuthorsBooksId)
    {
      _authors_books_id = newAuthorsBooksId;
    }
    public static List<Copy> GetAll()
    {
      List<Copy> allcopies = new List<Copy>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM copies;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int CopyId = rdr.GetInt32(0);
        int CopyAuthorsBooksId = rdr.GetInt32(1);
        Copy newCopy = new Copy(CopyAuthorsBooksId, CopyId);
        allcopies.Add(newCopy);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allcopies;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO copies (authors_books_id) OUTPUT INSERTED.id VALUES (@CopyAuthorsBooksId);", conn);

      SqlParameter authorsBooksIdParameter = new SqlParameter();
      authorsBooksIdParameter.ParameterName = "@CopyAuthorsBooksId";
      authorsBooksIdParameter.Value = this.GetAuthorsBooksId();
      cmd.Parameters.Add(authorsBooksIdParameter);
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
      SqlCommand cmd = new SqlCommand("DELETE FROM copies;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Copy Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM copies WHERE id = @CopyId;", conn);
      SqlParameter CopyIdParameter = new SqlParameter();
      CopyIdParameter.ParameterName = "@CopyId";
      CopyIdParameter.Value = id.ToString();
      cmd.Parameters.Add(CopyIdParameter);
      rdr = cmd.ExecuteReader();

      int foundCopyId = 0;
      int foundCopyAuthorsBooksId = 0;

      while(rdr.Read())
      {
        foundCopyId = rdr.GetInt32(0);
        foundCopyAuthorsBooksId = rdr.GetInt32(1);
      }
      Copy foundCopy = new Copy(foundCopyAuthorsBooksId, foundCopyId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCopy;
    }

    public static Copy FindAuthorsBooksId(int authors_books_id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM copies WHERE authors_books_id = @CopyAuthorsBooksId;", conn);
      SqlParameter CopyAuthorsBooksIdParameter = new SqlParameter();
      CopyAuthorsBooksIdParameter.ParameterName = "@CopyAuthorsBooksId";
      CopyAuthorsBooksIdParameter.Value = authors_books_id;
      cmd.Parameters.Add(CopyAuthorsBooksIdParameter);
      rdr = cmd.ExecuteReader();

      int foundCopyId = 0;
      int foundCopyAuthorsBooksId = 0;

      while(rdr.Read())
      {
        foundCopyId = rdr.GetInt32(0);
        foundCopyAuthorsBooksId = rdr.GetInt32(1);
      }
      Copy foundCopy = new Copy(foundCopyAuthorsBooksId, foundCopyId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCopy;
    }



      public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM copies WHERE id = @CopyId; DELETE FROM checkouts WHERE copy_id = @CopyId;", conn);

      SqlParameter CopyIdParameter = new SqlParameter();
      CopyIdParameter.ParameterName = "@CopyId";
      CopyIdParameter.Value = this.GetId();

      cmd.Parameters.Add(CopyIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

        public void AddPatron(Patron newPatron)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO checkouts (copy_id, patron_id) VALUES (@CopyId, @PatronId)", conn);
      SqlParameter CopyIdParameter = new SqlParameter();
      CopyIdParameter.ParameterName = "@CopyId";
      CopyIdParameter.Value = this.GetId();
      cmd.Parameters.Add(CopyIdParameter);

      SqlParameter patronIdParameter = new SqlParameter();
      patronIdParameter.ParameterName = "@PatronId";
      patronIdParameter.Value = newPatron.GetId();
      cmd.Parameters.Add(patronIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Patron> GetPatrons()
   {
     SqlConnection conn = DB.Connection();
     SqlDataReader rdr = null;
     conn.Open();

     SqlCommand cmd = new SqlCommand("SELECT patron_id FROM checkouts WHERE copy_id = @CopyId;", conn);
     SqlParameter CopyIdParameter = new SqlParameter();
     CopyIdParameter.ParameterName = "@CopyId";
     CopyIdParameter.Value = this.GetId();
     cmd.Parameters.Add(CopyIdParameter);

     rdr = cmd.ExecuteReader();

     List<int> patronIds = new List<int> {};
     while(rdr.Read())
     {
       int patronId = rdr.GetInt32(0);
       patronIds.Add(patronId);
     }
     if (rdr != null)
     {
       rdr.Close();
     }

     List<Patron> patrons = new List<Patron> {};
     foreach (int patronId in patronIds)
     {
       SqlDataReader queryReader = null;
       SqlCommand patronQuery = new SqlCommand("SELECT * FROM patrons WHERE id = @PatronId;", conn);

       SqlParameter patronIdParameter = new SqlParameter();
       patronIdParameter.ParameterName = "@PatronId";
       patronIdParameter.Value = patronId;
       patronQuery.Parameters.Add(patronIdParameter);

       queryReader = patronQuery.ExecuteReader();
       while(queryReader.Read())
       {
             int thisPatronId = queryReader.GetInt32(0);
             string patronName= queryReader.GetString(1);
             Patron foundPatron = new Patron(patronName, thisPatronId);
             patrons.Add(foundPatron);
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
     return patrons;
   }
  }
}
