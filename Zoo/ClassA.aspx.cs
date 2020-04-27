using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Zoo
{
    public partial class ClassA : System.Web.UI.Page
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
                                [ClassificationId]
                               ,[Squad]
                               ,[Family]
                               ,[Kind]
                               ,[Сountry]
                               ,[Life]
                           FROM [Zoo].[dbo].[Classification]";
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
                SqlCommand cmd = new SqlCommand("INSERT INTO [Zoo].[dbo].[Classification](Squad,Family,Kind,Сountry,Life) VALUES (@Squad, @Family,@Kind,@Сountry,@Life)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Squad", TextBox1.Text);
                cmd.Parameters.AddWithValue("@Family", TextBox2.Text);
                cmd.Parameters.AddWithValue("@Kind", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Сountry", TextBox4.Text);
                cmd.Parameters.AddWithValue("@Life", TextBox5.Text);
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

        #region Редактирвоание
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Table();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];

            TextBox txtSquad = (TextBox)row.FindControl("txtSquad");
            TextBox txtFamily = (TextBox)row.FindControl("txtFamily");
            TextBox txtKind = (TextBox)row.FindControl("txtKind");
            TextBox txtCountry = (TextBox)row.FindControl("txtCountry");
            TextBox txtLife = (TextBox)row.FindControl("txtLife");

            int ClassificationId = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

            string Squad = txtSquad.Text;
            string Family = txtFamily.Text;
            string Kind = txtKind.Text;
            string Country = txtCountry.Text;
            string Life = txtLife.Text;

            Regex regex = new Regex(@"^[А-Я]+[а-я\s]{1,20}$");
            MatchCollection matches = regex.Matches(Squad);

            Regex regex1 = new Regex(@"^[А-Я][а-я]{1,20}$");
            MatchCollection matches1 = regex1.Matches(Family);

            Regex regex2 = new Regex(@"^[А-Я][а-я]{1,20}$");
            MatchCollection matches2 = regex2.Matches(Kind);

            Regex regex3 = new Regex(@"^[А-Я][а-я]{1,20}$");
            MatchCollection matches3 = regex3.Matches(Country);

            Regex regex4 = new Regex(@"^\d+$");
            MatchCollection matches4 = regex4.Matches(Life);

            if (matches.Count > 0 && matches1.Count > 0 && matches2.Count > 0 && matches3.Count > 0 && matches4.Count > 0)
            {
                Update(ClassificationId, Squad, Family, Kind, Country, Convert.ToInt32(Life));
            }
            else
            {
                Label1.Text = "Неправильный ввод - разрешены только кириллица, пробелы и цифры";
            }
        }

        private void Update(int classificationId, string squad, string family, string kind, string сountry, int life)
        {
            try
            {
                string constr = @"Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                string query = "UPDATE Classification SET Squad = @Squad, Family = @Family, Kind = @Kind, Сountry = @Сountry, Life = @Life WHERE ClassificationId = @ClassificationId";

                SqlConnection con = new SqlConnection(constr);
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@Squad", SqlDbType.NVarChar).Value = squad;
                com.Parameters.Add("@Family", SqlDbType.NVarChar).Value = family;
                com.Parameters.Add("@Kind", SqlDbType.NVarChar).Value = kind;
                com.Parameters.Add("@Сountry", SqlDbType.NVarChar).Value = сountry;
                com.Parameters.Add("@Life", SqlDbType.Int).Value = life;
                com.Parameters.Add("@ClassificationId", SqlDbType.Int).Value = classificationId;

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
                int ClassificationId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
                string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                SqlConnection con = new SqlConnection(strConn);// строка подключения
                SqlCommand cmd = new SqlCommand("DELETE FROM [Zoo].[dbo].[Classification] WHERE ClassificationId=@ClassificationId");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.Add("@ClassificationId", SqlDbType.Int).Value = ClassificationId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Table();
                Label1.Text = "";
            }
            catch
            {
                Label1.Text = "Нельзя удалить классификацию - информация о ней используется в другой таблице";
            }
           
        }
        #endregion
    }
}