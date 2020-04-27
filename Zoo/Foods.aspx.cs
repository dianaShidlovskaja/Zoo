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
    public partial class Foods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TextBox4.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                Table();
            }
        }

        #region Отображение данных в таблице
        private void Table()
        {
            string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
            SqlConnection con = new SqlConnection(strConn);// строка подключения
            con.Open();
            string sql = @"SELECT 
                                 [FoodId]
                                ,[TypeFood]
                                ,[CountFood]
                                ,[PriceFood]
                                ,[DateFood]
                            FROM [Zoo].[dbo].[Foods] 
                            ORDER BY DateFood desc";
            SqlCommand com = new SqlCommand(sql, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();
        }
        #endregion

        #region Добавление
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!(TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == ""))
            {
                string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                SqlConnection con = new SqlConnection(strConn);// строка подключения
                SqlCommand cmd = new SqlCommand("INSERT INTO [Zoo].[dbo].[Foods](TypeFood, CountFood,PriceFood,DateFood) VALUES (@TypeFood, @CountFood, @PriceFood,@DateFood)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@TypeFood", TextBox1.Text);
                cmd.Parameters.AddWithValue("@CountFood", TextBox2.Text);
                cmd.Parameters.AddWithValue("@PriceFood", TextBox3.Text);
                cmd.Parameters.AddWithValue("@DateFood", TextBox4.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Table();

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                Label1.Text = "";
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

            TextBox txtTypeFood = (TextBox)row.FindControl("txtTypeFood");
            TextBox txtCountFood = (TextBox)row.FindControl("txtCountFood");
            TextBox txtPriceFood = (TextBox)row.FindControl("txtPriceFood");
            TextBox txtDateFood = (TextBox)row.FindControl("txtDateFood");

            int FoodId = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

            string TypeFood = txtTypeFood.Text;
            string CountFood = txtCountFood.Text;
            string PriceFood = txtPriceFood.Text;
            DateTime DateFood = Convert.ToDateTime(txtDateFood.Text);

            Regex regex = new Regex(@"^[А-Я]+[а-я\s-]{1,20}$");
            MatchCollection matches = regex.Matches(TypeFood);

            Regex regex1 = new Regex(@"^[0-9]*[.,]?[0-9]$");
            MatchCollection matches1 = regex1.Matches(CountFood);

            Regex regex2 = new Regex(@"^(\d)+$");
            MatchCollection matches2 = regex2.Matches(PriceFood);

            if (matches.Count > 0 && matches1.Count > 0 && matches2.Count > 0)
            {
                Update(FoodId, TypeFood, Convert.ToDecimal(CountFood), Convert.ToInt32(PriceFood), DateFood);
            }
            else
            {
                Label1.Text = "Неправильный ввод - разрешены только кириллица, пробелы, тире и цифры";
            }
        }

        private void Update(int foodId, string typeFood, decimal countFood, int priceFood, DateTime DateFood)
        {
            try
            {
                string constr = @"Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                string query = "UPDATE Foods SET TypeFood = @TypeFood, CountFood = @CountFood,PriceFood = @PriceFood,DateFood = @DateFood WHERE FoodId = @FoodId";

                SqlConnection con = new SqlConnection(constr);
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@TypeFood", SqlDbType.NVarChar).Value = typeFood;
                com.Parameters.Add("@CountFood", SqlDbType.Real).Value = countFood;
                com.Parameters.Add("@PriceFood", SqlDbType.Int).Value = priceFood;
                com.Parameters.Add("@DateFood", SqlDbType.Date).Value = DateFood;
                com.Parameters.Add("@FoodId", SqlDbType.Int).Value = foodId;

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
            try
            {
                int FoodId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
                string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                SqlConnection con = new SqlConnection(strConn);// строка подключения
                SqlCommand cmd = new SqlCommand("DELETE FROM [Zoo].[dbo].[Foods] WHERE FoodId=@FoodId");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.Add("@FoodId", SqlDbType.Int).Value = FoodId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Table();
                Label1.Text = "";
            }
            catch
            {
                Label1.Text = "Нельзя удалить корм - информация о нем используется в другой таблице";
            }

        }
        #endregion
    }
}