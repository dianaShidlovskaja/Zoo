using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zoo
{
    public partial class Animals : System.Web.UI.Page
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
                                [AnimalId]
                               ,[Kind]
                               ,[NameAnimal]
                               ,[Age]
                               ,[Gender]
                               ,[Weight]
                               ,[NameCell]
                           FROM [Zoo].[dbo].[Animals]
                           LEFT JOIN[Zoo].[dbo].[Classification]
                           ON Animals.ClassificationId=Classification.ClassificationId
                           LEFT JOIN[Zoo].[dbo].[Cells]
                           ON Animals.CellId=Cells.CellId";
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
                string query = "SELECT ClassificationId, Kind FROM Classification";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Classification");
                DropDownList1.DataSource = ds.Tables["Classification"];
                DropDownList1.DataTextField = "Kind";
                DropDownList1.DataValueField = "ClassificationId";
                DropDownList1.DataBind();

                DropDownList3.Items.Clear();
                string query1 = "SELECT CellId, NameCell FROM Cells";
                SqlDataAdapter da1 = new SqlDataAdapter(query1, conn);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "Cells");
                DropDownList3.DataSource = ds1.Tables["Cells"];
                DropDownList3.DataTextField = "NameCell";
                DropDownList3.DataValueField = "CellId";
                DropDownList3.DataBind();
            }
        }
        #endregion

        #region Добавление
        protected void Button1_Click(object sender, EventArgs e)
        {
            string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
            SqlConnection con = new SqlConnection(strConn);// строка подключения
            string cmd1 = "Select Places From Cells where CellId=" + DropDownList3.SelectedValue;
            SqlDataAdapter da = new SqlDataAdapter(cmd1, strConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Classification");
            int Place = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            if (Place > 0)
            {
                if (!(TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == ""))
                {

                    SqlCommand cmd = new SqlCommand("INSERT INTO [Zoo].[dbo].[Animals](ClassificationId,NameAnimal,Age,Gender,Weight,CellId) VALUES (@ClassificationId,@NameAnimal,@Age,@Gender,@Weight,@CellId)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ClassificationId", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@NameAnimal", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@Age", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@Gender", DropDownList2.SelectedValue);
                    cmd.Parameters.AddWithValue("@Weight", TextBox3.Text);
                    cmd.Parameters.AddWithValue("@CellId", DropDownList3.SelectedValue);
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
            else
            {
                Label1.Text = "Нельзя добавить животное - клетка занята";
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
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
            TextBox txtNameAnimal = (TextBox)row.FindControl("txtNameAnimal");
            TextBox txtAge = (TextBox)row.FindControl("txtAge");
            DropDownList dropDown2 = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("dropDown2");
            TextBox txtWeight = (TextBox)row.FindControl("txtWeight");
            DropDownList dropDown3 = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("dropDown3");

            int AnimalId = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

            int ClassificationId = Convert.ToInt32(dropDown1.SelectedValue);
            string NameAnimal = txtNameAnimal.Text;
            string Age = txtAge.Text;
            string Gender = dropDown2.SelectedValue;
            string Weigh = txtWeight.Text;
            int CellId = Convert.ToInt32(dropDown3.SelectedValue);

            Regex regex = new Regex(@"^[А-Я][а-я]{1,20}$");
            MatchCollection matches = regex.Matches(NameAnimal);

            Regex regex1 = new Regex(@"^(\d){1,2}$");
            MatchCollection matches1 = regex1.Matches(Age);

            Regex regex2 = new Regex(@"^(\d)+$");
            MatchCollection matches2 = regex2.Matches(Weigh);

            if (matches.Count > 0 && matches1.Count > 0 && matches2.Count > 0)
            {
                Update(AnimalId, ClassificationId, NameAnimal, Convert.ToInt32(Age), Gender, Convert.ToInt32(Weigh), CellId);
            }
            else
            {
                Label1.Text = "Неправильный ввод - разрешены только кириллица и цифры";
            }
        }

        private void Update(int animalId, int classificationId, string nameAnimal, int age, string gender, int weigh, int cellId)
        {
            try
            {
                string constr = @"Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                string query = "UPDATE Animals SET ClassificationId=@ClassificationId,NameAnimal=@NameAnimal,Age=@Age,Gender=@Gender,Weight=@Weight,CellId=@CellId WHERE AnimalId = @AnimalId";

                SqlConnection con = new SqlConnection(constr);
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@ClassificationId", SqlDbType.Int).Value = classificationId;
                com.Parameters.Add("@NameAnimal", SqlDbType.NVarChar).Value = nameAnimal;
                com.Parameters.Add("@Age", SqlDbType.Int).Value = age;
                com.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = gender;
                com.Parameters.Add("@Weight", SqlDbType.Int).Value = weigh;
                com.Parameters.Add("@CellId", SqlDbType.Int).Value = cellId;
                com.Parameters.Add("@AnimalId", SqlDbType.Int).Value = animalId;

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
                int AnimalId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
                string strConn = "Server=87.103.193.98;Database=Zoo;User Id=admin;Password=p@ssw0rd";
                SqlConnection con = new SqlConnection(strConn);// строка подключения
                SqlCommand cmd = new SqlCommand("DELETE FROM [Zoo].[dbo].[Animals] WHERE AnimalId=@AnimalId");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Parameters.Add("@AnimalId", SqlDbType.Int).Value = AnimalId;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Table();
                Label1.Text = "";
            }
            catch
            {
                Label1.Text = "Нельзя удалить животное - информация о нем используется в другой таблице";
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
                        DropDownList drop = (e.Row.FindControl("dropDown1") as DropDownList);
                        drop.DataSource = GetData("SELECT ClassificationId, Kind FROM Classification");
                        drop.DataTextField = "Kind";
                        drop.DataValueField = "ClassificationId";
                        drop.DataBind();
                        string selected = DataBinder.Eval(e.Row.DataItem, "Kind").ToString();
                        drop.Items.FindByText(selected).Selected = true;

                        DropDownList drop3 = (e.Row.FindControl("dropDown3") as DropDownList);
                        drop3.DataSource = GetData("SELECT CellId, NameCell FROM Cells");
                        drop3.DataTextField = "NameCell";
                        drop3.DataValueField = "CellId";
                        drop3.DataBind();
                        string selected3 = DataBinder.Eval(e.Row.DataItem, "NameCell").ToString();
                        drop3.Items.FindByText(selected3).Selected = true;
                    }
                }
            }
        }
        #endregion
    }
}