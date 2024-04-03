using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Policy;

public partial class BookManagement : System.Web.UI.Page
{
    string connectionString = "Server=localhost;Database=BookStoreDB;Integrated Security=SSPI;";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Update_DataGrid();
        }
    }

    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        try
        {
            if (Txt_Id.Text == "-1")
            {
                Insert_Book(
                    title: Txt_Title.Text,
                    author: Txt_Author.Text,
                    price: Txt_Price.Text,
                    publishYear: Txt_PublishedYear.Text
                );
            }
            else
            {
                Update_Book(
                    id: Txt_Id.Text,
                    title: Txt_Title.Text,
                    author: Txt_Author.Text,
                    price: Txt_Price.Text,
                    publishYear: Txt_PublishedYear.Text
                );
            }
            Reset_UI();
            Update_DataGrid();
        }
        catch (SqlTypeException)
        {
            Console.WriteLine("Invalid Data Types");
        }
        
    }

    protected void Insert_Book(string title, string author, string price, string publishYear)
    {
        string query = "INSERT INTO Books ([Title], [Author], [Price], [PublishedYear]) VALUES (@title, @author, @price, @publish)";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@author", author);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@publish", publishYear);
                bool status = command.ExecuteNonQuery() > 0;
            }
        }
    }
    protected void Update_DataGrid()
    {
        List<Book> allBooks = new List<Book>();
        string query = "Select * FROM Books";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int counter = 1;
                    while (reader.Read())
                    {
                        allBooks.Add(new Book
                        {
                            Id = Convert.ToInt32(reader["BookID"]),
                            DisplayId = counter,
                            Title = Convert.ToString(reader["Title"]),
                            Author = Convert.ToString(reader["Author"]),
                            Price = Convert.ToDecimal(reader["Price"]),
                            PublishedYear = Convert.ToInt32(reader["PublishedYear"])
                        });
                        counter += 1;
                    }
                }

            }
        }
        Tbl_Books.DataSource = allBooks;
        Tbl_Books.DataBind();
    }
    protected void Update_Book(string id, string title, string author, string price, string publishYear)
    {
        string query = "UPDATE Books SET [Title] = @title, [Author] = @author, [Price] = @price, [PublishedYear] = @publish WHERE [BookID] = @id;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@author", author);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@publish", publishYear);
                command.Parameters.AddWithValue("@id", id);
                Console.WriteLine(command.ToString());
                bool status = command.ExecuteNonQuery() > 0;
            }
        }
    }
    protected void Delete_Row(string id)
    {
        string query = "DELETE FROM Books WHERE [BookID] = @id;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                bool status = command.ExecuteNonQuery() != 0;
            }
        }
    }
    

    protected void Reset_UI()
    {
        Txt_Id.Text = "-1";
        Txt_Title.Text = null;
        Txt_Author.Text = null;
        Txt_Price.Text = null;
        Txt_PublishedYear.Text = null;
    }
    //protected void Edit_UI(string id)
    //{
    //    string query = "Select * FROM Books where [BookId] = @id;";
    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        using (SqlCommand command = new SqlCommand(query, connection))
    //        {
    //            command.Parameters.AddWithValue("BookId", id);
    //            using (SqlDataReader reader = command.ExecuteReader())
    //            {
    //                if (reader.HasRows)
    //                {
    //                    while (reader.Read())
    //                    {
    //                        Txt_Id.Text = reader["BookID"].ToString();
    //                        Txt_Title.Text = reader["Title"].ToString();
    //                        Txt_Author.Text = reader["Author"].ToString();
    //                        Txt_Price.Text = reader["Price"].ToString();
    //                        Txt_PublishedYear.Text = reader["PublishedYear"].ToString();
    //                    }
    //                }
                    
    //            }

    //        }
    //    }
    //}


    //protected void Tbl_Books_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    Console.WriteLine(Convert.ToInt32(e.CommandArgument));
    //    int index = Convert.ToInt32(e.CommandArgument);
    //    string id = Tbl_Books.DataKeys[index].Value.ToString();

    //    if (e.CommandName == "Delete")
    //    {
    //        Delete_Row(id);
    //    }
    //    else if (e.CommandName == "Edit")
    //    {
    //        Edit_UI(id);
    //    }
    //}

    protected void Tbl_Books_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Tbl_Books.EditIndex = e.NewEditIndex;
        Update_DataGrid();
        ChangeValidation(false);
    }

    private void ChangeValidation(bool validation)
    {
        RequiredFieldValidator1.Enabled = validation;
        RequiredFieldValidator2.Enabled = validation;
        RequiredFieldValidator3.Enabled = validation;
        RequiredFieldValidator4.Enabled = validation;
        RegularExpressionValidator1.Enabled = validation;
        RegularExpressionValidator2.Enabled = validation;
    }

    protected void Tbl_Books_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        ChangeValidation(true);

        GridViewRow row = Tbl_Books.Rows[e.RowIndex];
        string bookID = Tbl_Books.DataKeys[e.RowIndex].Value.ToString();
        Console.WriteLine(bookID);

        string title = e.NewValues["Title"].ToString();
        string author = e.NewValues["Author"].ToString();
        string price = e.NewValues["Price"].ToString();
        string publishedYear = e.NewValues["PublishedYear"].ToString();

        Update_Book(bookID, title, author, price, publishedYear);

        Tbl_Books.EditIndex = -1;
        Update_DataGrid();
    }

    protected void Tbl_Books_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        ChangeValidation(true);
        Tbl_Books.EditIndex = -1;
    }


    protected void Tbl_Books_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string bookID = Tbl_Books.DataKeys[e.RowIndex].Value.ToString();

        Delete_Row(bookID);

        //Reload Grid
        Update_DataGrid();
    }


}