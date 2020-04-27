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
    public partial class Emp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
                                [EmployeeId]
                               ,[FamEmp]
                               ,[NameEmp]
                               ,[NamePos]
                               ,[ZapEmp]
                           FROM [Zoo].[dbo].[Employee]
                           LEFT JOIN[Zoo].[dbo].[Position]
                           ON Employee.PositionId=Position.PositionId";
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
        public void cmb()
        {
            using (SqlConnection conn = new SqlConnection(@"Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd"))
            {
                DropDownList1.Items.Clear();
                string query = "SELECT PositionId, NamePos FROM Position";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Position");
                DropDownList1.DataSource = ds.Tables["Position"];
                DropDownList1.DataTextField = "NamePos";
                DropDownList1.DataValueField = "PositionId";
                DropDownList1.DataBind();
            }
        }
        #endregion

        #region Добавление
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!(TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == ""))
            {
                string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                SqlConnection con = new SqlConnection(strConn);// строка подключения
                SqlCommand cmd = new SqlCommand("INSERT INTO [Zoo].[dbo].[Employee](FamEmp,NameEmp,PositionId,ZapEmp) VALUES (@FamEmp,@NameEmp,@PositionId,@ZapEmp)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@FamEmp", TextBox1.Text);
                cmd.Parameters.AddWithValue("@NameEmp", TextBox2.Text);
                cmd.Parameters.AddWithValue("@PositionId", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@ZapEmp", TextBox3.Text);
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

            TextBox txtFamEmp = (TextBox)row.FindControl("txtFamEmp");
            TextBox txtNameEmp = (TextBox)row.FindControl("txtNameEmp");
            DropDownList dropDown = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("dropDown");
            TextBox txtZapEmp = (TextBox)row.FindControl("txtZapEmp");

            int EmployeeId = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

            string FamEmp = txtFamEmp.Text;
            string NameEmp = txtNameEmp.Text;
            int PositionId = Convert.ToInt32(dropDown.SelectedValue);
            int ZapEmp = Convert.ToInt32(txtZapEmp.Text);

            Regex regex = new Regex(@"^[А-Я][а-я]{1,20}$");
            MatchCollection matches = regex.Matches(FamEmp);

            Regex regex1 = new Regex(@"^[А-Я][а-я]{1,20}$");
            MatchCollection matches1 = regex1.Matches(NameEmp);

            Regex regex2 = new Regex(@"^(\d){1,10}$");
            MatchCollection matches2 = regex2.Matches(ZapEmp.ToString());

            if (matches.Count > 0 && matches1.Count > 0 && matches2.Count > 0)
            {
                Update(EmployeeId, FamEmp, NameEmp, PositionId, ZapEmp);
            }
            else
            {
                Label1.Text = "Неправильный ввод - разрешены только кириллица и цифры";
            }

        }
        private void Update(int EmployeeId, string FamEmp, string NameEmp, int PositionId, int ZapEmp)
        {
            try
            {
                string constr = @"Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                string query = "UPDATE Employee SET FamEmp=@FamEmp,NameEmp=@NameEmp,PositionId=@PositionId,ZapEmp=@ZapEmp WHERE EmployeeId = @EmployeeId";

                SqlConnection con = new SqlConnection(constr);
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@FamEmp", SqlDbType.NVarChar).Value = FamEmp;
                com.Parameters.Add("@NameEmp", SqlDbType.NVarChar).Value = NameEmp;
                com.Parameters.Add("@PositionId", SqlDbType.Int).Value = PositionId;
                com.Parameters.Add("@ZapEmp", SqlDbType.Int).Value = ZapEmp;
                com.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;

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
                int EmployeeId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
                string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                SqlConnection con = new SqlConnection(strConn);// строка подключения
                SqlCommand cmd = new SqlCommand("DELETE FROM [Zoo].[dbo].[Employee] WHERE EmployeeId=@EmployeeId");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Table();
                Label1.Text = "";
            }
            catch
            {
                Label1.Text = "Нельзя удалить сотрудника - информация о нем используется в другой таблице";
            }

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
                        DropDownList drop = (e.Row.FindControl("dropDown") as DropDownList);
                        drop.DataSource = GetData("SELECT PositionId, NamePos FROM Position");
                        drop.DataTextField = "NamePos";
                        drop.DataValueField = "PositionId";
                        drop.DataBind();
                        string selected = DataBinder.Eval(e.Row.DataItem, "NamePos").ToString();
                        drop.Items.FindByText(selected).Selected = true;
                    }
                }
            }
        }
        #endregion

    }
}