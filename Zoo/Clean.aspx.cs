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
    public partial class Clean : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TextBox1.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
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
                                [CleanId]
                               ,[NameCell]
                               ,[FamEmp]
                               ,[DateClean]
                           FROM [Zoo].[dbo].[Clean]
                           LEFT JOIN[Zoo].[dbo].[Employee]
                           ON Clean.EmployeeId=Employee.EmployeeId
                           LEFT JOIN[Zoo].[dbo].[Cells]
                           ON Clean.CellId=Cells.CellId";
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
                string query1 = "SELECT CellId, NameCell FROM Cells";
                SqlDataAdapter da1 = new SqlDataAdapter(query1, conn);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "Cells");
                DropDownList1.DataSource = ds1.Tables["Cells"];
                DropDownList1.DataTextField = "NameCell";
                DropDownList1.DataValueField = "CellId";
                DropDownList1.DataBind();

                DropDownList2.Items.Clear();
                string query = "SELECT EmployeeId, FamEmp FROM Employee";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Employee");
                DropDownList2.DataSource = ds.Tables["Employee"];
                DropDownList2.DataTextField = "FamEmp";
                DropDownList2.DataValueField = "EmployeeId";
                DropDownList2.DataBind();


            }
        }
        #endregion

        #region Добавление
        protected void Button1_Click(object sender, EventArgs e)
        {
            string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
            SqlConnection con = new SqlConnection(strConn);// строка подключения
            if (!(TextBox1.Text == ""))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [Zoo].[dbo].[Clean](CellId,EmployeeId,DateClean) VALUES (@CellId,@EmployeeId,@DateClean)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@CellId", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@EmployeeId", DropDownList2.SelectedValue);
                cmd.Parameters.AddWithValue("@DateClean", TextBox1.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Table();
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

            DropDownList dropDown1 = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("dropDown1");
            DropDownList dropDown2 = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("dropDown2");
            TextBox txtDateClean = (TextBox)row.FindControl("txtDateClean");

            int CleanId = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

            int CellId = Convert.ToInt32(dropDown1.SelectedValue);
            int EmployeeId = Convert.ToInt32(dropDown2.SelectedValue);
            string DateClean = txtDateClean.Text;

            if (!(DateClean.ToString() == ""))
            {
                Update(CleanId, CellId, EmployeeId, Convert.ToDateTime(DateClean));
            }
            else
            {
                Label1.Text = "Заполните дату!";
            }
        }

        private void Update(int cleanId, int cellId, int employeeId, DateTime dateClean)
        {
            try
            {
                string constr = @"Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                string query = "UPDATE Clean SET CellId=@CellId,EmployeeId=@EmployeeId,DateClean=@DateClean WHERE CleanId = @CleanId";

                SqlConnection con = new SqlConnection(constr);
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@CellId", SqlDbType.Int).Value = cellId;
                com.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employeeId;
                com.Parameters.Add("@DateClean", SqlDbType.Date).Value = dateClean;
                com.Parameters.Add("@CleanId", SqlDbType.Int).Value = cleanId;

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
                        drop1.DataSource = GetData("SELECT CellId, NameCell FROM Cells");
                        drop1.DataTextField = "NameCell";
                        drop1.DataValueField = "CellId";
                        drop1.DataBind();
                        string selected1 = DataBinder.Eval(e.Row.DataItem, "NameCell").ToString();
                        drop1.Items.FindByText(selected1).Selected = true;

                        DropDownList drop2 = (e.Row.FindControl("dropDown2") as DropDownList);
                        drop2.DataSource = GetData("SELECT EmployeeId, FamEmp FROM Employee");
                        drop2.DataTextField = "FamEmp";
                        drop2.DataValueField = "EmployeeId";
                        drop2.DataBind();
                        string selected2 = DataBinder.Eval(e.Row.DataItem, "FamEmp").ToString();
                        drop2.Items.FindByText(selected2).Selected = true;
                    }
                }
            }
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
            int CleanId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
            SqlConnection con = new SqlConnection(strConn);// строка подключения
            SqlCommand cmd = new SqlCommand("DELETE FROM [Zoo].[dbo].[Clean] WHERE CleanId=@CleanId");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.Parameters.Add("@CleanId", SqlDbType.Int).Value = CleanId;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Table();
        }
        #endregion
    }
}