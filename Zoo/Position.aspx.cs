using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zoo
{
    public partial class Position : System.Web.UI.Page
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
                                [PositionId]
                               ,[NamePos]
                               ,[Duty]
                           FROM [Zoo].[dbo].[Position]";
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
            if (!(TextBox1.Text == "" || TextBox2.Text == ""))
            {
                string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                SqlConnection con = new SqlConnection(strConn);// строка подключения
                SqlCommand cmd = new SqlCommand("INSERT INTO [Zoo].[dbo].[Position](NamePos, Duty) VALUES (@NamePos, @Duty)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@NamePos", TextBox1.Text);
                cmd.Parameters.AddWithValue("@Duty", TextBox2.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Table();

                TextBox1.Text = "";
                TextBox2.Text = "";
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

            TextBox txtNamePos = (TextBox)row.FindControl("txtNamePos");
            TextBox txtDuty = (TextBox)row.FindControl("txtDuty");

            int PositionId = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

            string NamePos = txtNamePos.Text;
            string Duty = txtDuty.Text;

            Regex regex = new Regex(@"^[А-Я]+[а-я\s-]{1,20}$");
            MatchCollection matches = regex.Matches(NamePos);

            Regex regex1 = new Regex(@"^[А-Я]+[а-я\s]{1,20}$");
            MatchCollection matches1 = regex1.Matches(Duty);

            if (matches.Count > 0 && matches1.Count > 0)
            {
                Update(PositionId, NamePos, Duty);
            }
            else
            {
                Label1.Text = "Неправильный ввод - разрешены только кириллица, пробел и тире";
            }
        }
        private void Update(int PositionId, string NamePos, string Duty)
        {
            try
            {
                string constr = @"Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                string query = "UPDATE Position SET NamePos = @NamePos, Duty = @Duty WHERE PositionId = @PositionId";

                SqlConnection con = new SqlConnection(constr);
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@NamePos", SqlDbType.NVarChar).Value = NamePos;
                com.Parameters.Add("@Duty", SqlDbType.NVarChar).Value = Duty;
                com.Parameters.Add("@PositionId", SqlDbType.Int).Value = PositionId;

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
                int PositionId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
                string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                SqlConnection con = new SqlConnection(strConn);// строка подключения
                SqlCommand cmd = new SqlCommand("DELETE FROM [Zoo].[dbo].[Position] WHERE PositionId=@PositionId");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.Add("@PositionId", SqlDbType.Int).Value = PositionId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Table();
                Label1.Text = "";
            }
            catch
            {
                Label1.Text= "Нельзя удалить должность - информация о ней используется в другой таблице";
            }
          
        }
        #endregion
    }
}