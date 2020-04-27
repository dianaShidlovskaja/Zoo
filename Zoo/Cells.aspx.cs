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
    public partial class Cells : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
                                [CellId]
                               ,[NameCell]
                               ,[WidthCell]
                               ,[HeightCell]
                               ,[Places]
                               ,[PriceCell]
                           FROM [Zoo].[dbo].[Cells]";
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
            if (!(TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "" || TextBox4.Text == "" || TextBox5.Text == ""))
            {
                string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                SqlConnection con = new SqlConnection(strConn);// строка подключения
                SqlCommand cmd = new SqlCommand("INSERT INTO [Zoo].[dbo].[Cells](NameCell,WidthCell,HeightCell,Places,PriceCell) VALUES (@NameCell, @WidthCell,@HeightCell,@Places,@PriceCell)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@NameCell", TextBox1.Text);
                cmd.Parameters.AddWithValue("@WidthCell", TextBox2.Text);
                cmd.Parameters.AddWithValue("@HeightCell", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Places", TextBox4.Text);
                cmd.Parameters.AddWithValue("@PriceCell", TextBox5.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Table();

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox5.Text = "";
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

            TextBox txtNameCell = (TextBox)row.FindControl("txtNameCell");
            TextBox txtWidthCell = (TextBox)row.FindControl("txtWidthCell");
            TextBox txtHeightCell = (TextBox)row.FindControl("txtHeightCell");
            TextBox txtPlaces = (TextBox)row.FindControl("txtPlaces");
            TextBox txtPriceCell = (TextBox)row.FindControl("txtPriceCell");

            int CellId = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

            string NameCell = txtNameCell.Text;
            string WidthCell = txtWidthCell.Text;
            string HeightCell = txtHeightCell.Text;
            string Places = txtPlaces.Text;
            string PriceCell = txtPriceCell.Text;

            Regex regex = new Regex(@"^[А-Я]+([а-я\s]{1,20}|[а-я\s]{1,20}\s\d)$");
            MatchCollection matches = regex.Matches(NameCell);

            Regex regex1 = new Regex(@"^\d+$");
            MatchCollection matches1 = regex1.Matches(WidthCell);

            Regex regex2 = new Regex(@"^\d+$");
            MatchCollection matches2 = regex2.Matches(HeightCell);

            Regex regex3 = new Regex(@"^\d+$");
            MatchCollection matches3 = regex3.Matches(Places);

            Regex regex4 = new Regex(@"^\d+$");
            MatchCollection matches4 = regex4.Matches(PriceCell);

            if (matches.Count > 0 && matches1.Count > 0 && matches2.Count > 0 && matches3.Count > 0 && matches4.Count > 0)
            {
                Update(CellId, NameCell, Convert.ToInt32(WidthCell), Convert.ToInt32(HeightCell), Convert.ToInt32(Places), Convert.ToInt32(PriceCell));
            }
            else
            {
                Label1.Text = "Неправильный ввод - разрешены только кириллица, пробелы и цифры";
            }
        }

        private void Update(int cellId, string nameCell, int widthCell, int heightCell, int places, int priceCell)
        {
            try
            {
                string constr = @"Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                string query = "UPDATE Cells SET NameCell = @NameCell, WidthCell = @WidthCell, HeightCell = @HeightCell, Places = @Places, PriceCell = @PriceCell WHERE CellId = @CellId";

                SqlConnection con = new SqlConnection(constr);
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@NameCell", SqlDbType.NVarChar).Value = nameCell;
                com.Parameters.Add("@WidthCell", SqlDbType.Int).Value = widthCell;
                com.Parameters.Add("@HeightCell", SqlDbType.Int).Value = heightCell;
                com.Parameters.Add("@Places", SqlDbType.Int).Value = places;
                com.Parameters.Add("@PriceCell", SqlDbType.Int).Value = priceCell;
                com.Parameters.Add("@CellId", SqlDbType.Int).Value = cellId;

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
                int CellId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
                string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                SqlConnection con = new SqlConnection(strConn);// строка подключения
                SqlCommand cmd = new SqlCommand("DELETE FROM [Zoo].[dbo].[Cells] WHERE CellId=@CellId");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.Add("@CellId", SqlDbType.Int).Value = CellId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Table();
                Label1.Text = "";
            }
            catch
            {
                Label1.Text = "Нельзя удалить вольер - информация о нем используется в другой таблице";
            }
           
        }
        #endregion
    }
}