using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zoo
{
    public partial class Feed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TextBox2.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                Table();
                cmb();
            }
        }

        #region Подгрузка данных в таблицу
        private void Table()
        {
            string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
            SqlConnection con = new SqlConnection(strConn);// строка подключения
            con.Open();
            string sql = @"SELECT 
                                [FeedId]
                               ,[FamEmp]
                               ,[NameAnimal]
                               ,[TypeFood]
                               ,[Ration]
                               ,[DateFeed]
                           FROM [Zoo].[dbo].[Feed]
                           LEFT JOIN[Zoo].[dbo].[Employee]
                           ON Feed.EmployeeId=Employee.EmployeeId
                           LEFT JOIN[Zoo].[dbo].[Animals]
                           ON Feed.AnimalId=Animals.AnimalId
                           LEFT JOIN[Zoo].[dbo].[Foods]
                           ON Feed.FoodId=Foods.FoodId";
            SqlCommand com = new SqlCommand(sql, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();
        }
        #endregion

        #region Подгрузка данных в DropDownList
        private void cmb()
        {
            using (SqlConnection conn = new SqlConnection(@"Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd"))
            {
                DropDownList1.Items.Clear();
                string query = "SELECT EmployeeId, FamEmp FROM Employee";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Employee");
                DropDownList1.DataSource = ds.Tables["Employee"];
                DropDownList1.DataTextField = "FamEmp";
                DropDownList1.DataValueField = "EmployeeId";
                DropDownList1.DataBind();

                DropDownList2.Items.Clear();
                string query1 = "SELECT AnimalId, NameAnimal FROM Animals";
                SqlDataAdapter da1 = new SqlDataAdapter(query1, conn);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "Animals");
                DropDownList2.DataSource = ds1.Tables["Animals"];
                DropDownList2.DataTextField = "NameAnimal";
                DropDownList2.DataValueField = "AnimalId";
                DropDownList2.DataBind();

                DropDownList3.Items.Clear();
                string query2 = "SELECT FoodId, TypeFood FROM Foods where countfood>0";
                SqlDataAdapter da2 = new SqlDataAdapter(query2, conn);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "Foods");
                DropDownList3.DataSource = ds2.Tables["Foods"];
                DropDownList3.DataTextField = "TypeFood";
                DropDownList3.DataValueField = "FoodId";
                DropDownList3.DataBind();
            }
        }
        #endregion

        #region Добавление
        protected void Button1_Click(object sender, EventArgs e)
        {
            string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
            SqlConnection con = new SqlConnection(strConn);// строка подключения
            string cmd1 = "Select CountFood From Foods where FoodId=" + DropDownList3.SelectedValue;
            SqlDataAdapter da = new SqlDataAdapter(cmd1, strConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Classification");
            string Count = ds.Tables[0].Rows[0][0].ToString();
            string Ration = TextBox1.Text;
            if (!(TextBox1.Text == "" || TextBox2.Text == ""))
            {
                if (Convert.ToDecimal(Count) > 0 && Convert.ToDecimal(Count) >= Convert.ToDecimal(Ration))
            {
                
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Zoo].[dbo].[Feed](EmployeeId,AnimalId,FoodId,Ration,DateFeed) VALUES (@EmployeeId,@AnimalId,@FoodId,@Ration,@DateFeed)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@EmployeeId", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@AnimalId", DropDownList2.SelectedValue);
                    cmd.Parameters.AddWithValue("@FoodId", DropDownList3.SelectedValue);
                    cmd.Parameters.AddWithValue("@Ration", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@DateFeed", TextBox2.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Table();

                    TextBox1.Text = "";
                    Label1.Text = "";
                }
                else
                {
                    Label1.Text = "Нельзя покормить животное - купите корм";
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                }
            }
            else
            {
                Label1.Text = "Не все поля заполнены - заполните пустые поля!";
            }
        }
        #endregion

        #region Редактирование
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Table();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];

            DropDownList dropDown1 = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("dropDown1");
            DropDownList dropDown2 = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("dropDown2");
            DropDownList dropDown3 = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("dropDown3");
            TextBox txtRation = (TextBox)row.FindControl("txtRation");
            TextBox txtDateFeed = (TextBox)row.FindControl("txtDateFeed");

            int FeedId = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

            int EmployeeId = Convert.ToInt32(dropDown1.SelectedValue);
            int AnimalId = Convert.ToInt32(dropDown2.SelectedValue);
            int FoodId = Convert.ToInt32(dropDown3.SelectedValue);
            string Ration = txtRation.Text;
            DateTime DateFeed = Convert.ToDateTime(txtDateFeed.Text);

            Regex regex = new Regex(@"^[0-9]*[.,]?[0-9]$");
            MatchCollection matches = regex.Matches(Ration);

            if (matches.Count > 0)
            {
                Update(FeedId, EmployeeId, AnimalId, FoodId, Convert.ToDecimal(Ration), DateFeed);
            }
            else
            {
                Label1.Text = "Неправильный ввод - разрешены только цифры и точка/запятая";
            }
        }

        private void Update(int feedId, int employeeId, int animalId, int foodId, decimal ration, DateTime dateFeed)
        {
            try
            {
                string constr = @"Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                string query = "UPDATE Feed SET EmployeeId=@EmployeeId,AnimalId=@AnimalId,FoodId=@FoodId,Ration=@Ration,DateFeed=@DateFeed WHERE FeedId = @FeedId";

                SqlConnection con = new SqlConnection(constr);
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employeeId;
                com.Parameters.Add("@AnimalId", SqlDbType.Int).Value = animalId;
                com.Parameters.Add("@FoodId", SqlDbType.Int).Value = foodId;
                com.Parameters.Add("@Ration", SqlDbType.Real).Value = ration;
                com.Parameters.Add("@DateFeed", SqlDbType.Date).Value = dateFeed;
                com.Parameters.Add("@FeedId", SqlDbType.Int).Value = feedId;

                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                GridView1.EditIndex = -1;
                Table();
                Label1.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Table();
        }
        #endregion

        #region Подгрузка таблицы при перемещении
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Table();
        }
        #endregion

        #region Удаление
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int FeedId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
            SqlConnection con = new SqlConnection(strConn);// строка подключения
            SqlCommand cmd = new SqlCommand("DELETE FROM [Zoo].[dbo].[Feed] WHERE FeedId=@FeedId");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.Parameters.Add("@FeedId", SqlDbType.Int).Value = FeedId;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Table();
        }
        #endregion

        #region Подгрузка данных в DropDownList при нажатии кнопки "Изменить"
        private DataSet GetData(string query)
        {
            string conString = @"Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        return ds;
                    }
                }
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                    {
                        DropDownList drop1 = (e.Row.FindControl("dropDown1") as DropDownList);
                        drop1.DataSource = GetData("SELECT EmployeeId, FamEmp FROM Employee");
                        drop1.DataTextField = "FamEmp";
                        drop1.DataValueField = "EmployeeId";
                        drop1.DataBind();
                        string selected1 = DataBinder.Eval(e.Row.DataItem, "FamEmp").ToString();
                        drop1.Items.FindByText(selected1).Selected = true;

                        DropDownList drop2 = (e.Row.FindControl("dropDown2") as DropDownList);
                        drop2.DataSource = GetData("SELECT AnimalId, NameAnimal FROM Animals");
                        drop2.DataTextField = "NameAnimal";
                        drop2.DataValueField = "AnimalId";
                        drop2.DataBind();
                        string selected2 = DataBinder.Eval(e.Row.DataItem, "NameAnimal").ToString();
                        drop2.Items.FindByText(selected2).Selected = true;

                        DropDownList drop3 = (e.Row.FindControl("dropDown3") as DropDownList);
                        drop3.DataSource = GetData("SELECT FoodId, TypeFood FROM Foods");
                        drop3.DataTextField = "TypeFood";
                        drop3.DataValueField = "FoodId";
                        drop3.DataBind();
                        string selected3 = DataBinder.Eval(e.Row.DataItem, "TypeFood").ToString();
                        drop3.Items.FindByText(selected3).Selected = true;
                    }
                }
            }
        }
        #endregion
    }
}