using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Library
{
  public class Checkout
  {
    private int _id;
    private int _copy_id;
    private int _patron_id;
    private DateTime _duedate;
    private int _returned;

    public Checkout(int checkedCopy, int checkedPatron, DateTime dueDate, int Returned = 0, int Id = 0)
    {
      _id = Id;
      _copy_id = checkedCopy;
      _patron_id = checkedPatron;
      _duedate = dueDate;
      _returned = Returned;
    }

    public void Save()
    {

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO checkouts (copy_id, patron_id, duedate, returned) OUTPUT INSERTED.id VALUES (@CopyId, @PatronId, @Duedate, @Returned)", conn);
      SqlParameter CopyIdParameter = new SqlParameter();
      CopyIdParameter.ParameterName = "@CopyId";
      CopyIdParameter.Value = this.GetCopyId();
      cmd.Parameters.Add(CopyIdParameter);


      SqlParameter patronIdParameter = new SqlParameter();
      patronIdParameter.ParameterName = "@PatronId";
      patronIdParameter.Value = this.GetPatronId();
      cmd.Parameters.Add(patronIdParameter);

      SqlParameter duedateParameter = new SqlParameter();
      duedateParameter.ParameterName = "@Duedate";
      duedateParameter.Value = this.GetDueDate();
      cmd.Parameters.Add(duedateParameter);

      SqlParameter returnedParameter = new SqlParameter();
      returnedParameter.ParameterName = "@Returned";
      returnedParameter.Value = 0;
      cmd.Parameters.Add(returnedParameter);

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

    public static Checkout Find(int patron_id, int copy_id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM checkout WHERE patron_id = @PatronId AND copy_id = @CopyId;", conn);
      SqlParameter patronIdParameter = new SqlParameter();
      patronIdParameter.ParameterName = "@PatronId";
      patronIdParameter.Value = patron_id.ToString();
      cmd.Parameters.Add(patronIdParameter);


      SqlParameter copyIdParameter = new SqlParameter();
      copyIdParameter.ParameterName = "@CopyId";
      copyIdParameter.Value = copy_id.ToString();
      cmd.Parameters.Add(copyIdParameter);


      rdr = cmd.ExecuteReader();

      int foundcheckoutId = 0;
      int foundpatronId = 0;
      int foundcopyId = 0;
      DateTime checkoutDueDate = new DateTime (2016-02-23);
      int foundreturn = 0;

      while(rdr.Read())
      {
        foundcheckoutId = rdr.GetInt32(0);
        foundpatronId = rdr.GetInt32(1);
        foundcopyId = rdr.GetInt32(2);
        checkoutDueDate = rdr.GetDateTime(3);
        foundreturn = rdr.GetInt32(4);
      }
       Checkout foundCheckout = new Checkout(foundcopyId, foundpatronId, checkoutDueDate, foundreturn, foundcheckoutId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCheckout;
    }


    public void Returned()
  {
    SqlConnection conn = DB.Connection();
    SqlDataReader rdr;
    conn.Open();

    SqlCommand cmd = new SqlCommand("UPDATE checkouts SET returned = @Returned OUTPUT INSERTED.returned WHERE id = @CheckoutId;", conn);

    SqlParameter returnParameter = new SqlParameter();
    returnParameter.ParameterName = "@NewName";
    returnParameter.Value = 1;
    cmd.Parameters.Add(returnParameter);


    SqlParameter checkoutIdParameter = new SqlParameter();
    checkoutIdParameter.ParameterName = "@CheckoutId";
    checkoutIdParameter.Value = this.GetId();
    cmd.Parameters.Add(checkoutIdParameter);
    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      this._returned = rdr.GetInt32(4);
    }

    if (rdr != null)
    {
      rdr.Close();
    }

    if (conn != null)
    {
      conn.Close();
    }
  }

    public override bool Equals(System.Object otherCheckout)
    {
        if (!(otherCheckout is Checkout))
        {
          return false;
        }
        else
        {
          Checkout newCheckout = (Checkout) otherCheckout;
          bool idEquality = this.GetId() == newCheckout.GetId();
          bool copyIdEquality = this.GetCopyId() == newCheckout.GetCopyId();
          bool patronIdEquality = this.GetPatronId() == newCheckout.GetPatronId();
          bool dueDateEquality = this.GetDueDate() == newCheckout.GetDueDate();
          bool returnedEquality = this.GetReturned() == newCheckout.GetReturned();
          return (idEquality && copyIdEquality && patronIdEquality && dueDateEquality && returnedEquality);
        }
    }

    public int GetId()
    {
      return _id;
    }

    public int GetCopyId()
    {
      return _copy_id;
    }

    public int GetPatronId()
    {
      return _patron_id;
    }

    public DateTime GetDueDate()
    {
      return _duedate;
    }

    public int GetReturned()
    {
      return _returned;
    }
  }
}
