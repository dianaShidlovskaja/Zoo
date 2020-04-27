<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Clean.aspx.cs" Inherits="Zoo.Clean" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="css/css.css"/>
    <link rel="stylesheet" href="css/form.css"/>
    <title>Уборка клеток</title>
</head>
<body>
     <%-- Фон для формы --%>
    <style>
        .transparent {
            background-image: url('../Properties/form8.jpg');
        }
        select[id="DropDownList1"], select[id="DropDownList2"]{
            width: 100%;
            padding: 10px;
            margin: 10px 0 15px;
            border-width: 0;
            line-height: 40px;
            border-radius: 20px;
            color: white;
            background: rgba(255,255,255,.3);
            font-family: 'Roboto', sans-serif;
            font-size: 16px;
            opacity: .9;
            font-weight: bold;
        }
        .drop option{
            background: black;
        }
    </style>
    <%-- Горизонтальное меню--%>
    <nav class="top-menu">
        <ul class="menu-main">
            <li><a href="Index.aspx" class="current">Главная</a></li>
            <li><a href="#">Таблицы</a>
                <ul class="second">
                    <li><a href="Position.aspx">Должность</a></li>
                    <li><a href="Emp.aspx">Сотрудники</a></li>
                    <li><a href="ClassA.aspx">Классификация</a></li>
                    <li><a href="Cells.aspx">Клетки</a></li>
                    <li><a href="Animals.aspx">Животные</a></li>
                    <li><a href="Foods.aspx">Корм</a></li>
                    <li><a href="Feed.aspx">Кормление</a></li>
                    <li><a href="Clean.aspx">Уборка клеток</a></li>
                </ul>
            </li>
            <li><a href="Reports.aspx">Отчеты</a></li>
        </ul>
    </nav>
    <div class="ErrorText">
          <asp:Label ID="Label1" runat="server"></asp:Label><br />
    </div>
    <form id="form1" runat="server">
         <div class="dis">
            <div class="transparent">
            <div class="form-inner ">
                <h3>Уборка клеток</h3>
                <label>Клетка</label>
                <asp:DropDownList ID="DropDownList1" runat="server" class="drop"></asp:DropDownList>
                <label>Сотрудник</label>
                <asp:DropDownList ID="DropDownList2" runat="server" class="drop"></asp:DropDownList>
                <label>Дата</label>
                <asp:TextBox ID="TextBox1" runat="server" TextMode="Date" ></asp:TextBox>
                
                <asp:Button ID="Button1" runat="server" Text="Добавить" OnClick="Button1_Click"  />
            </div>
            </div>
            <%-- Таблица --%>
            <asp:GridView ID="GridView1"  AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" runat="server" DataKeyNames="CleanId"
            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true" PageSize="5">
	            <PagerStyle  BackColor="gray" ForeColor="#61000d"  HorizontalAlign="Center" />
                <Columns>
                   <asp:BoundField DataField="CleanId" ReadOnly="true" Visible="false"/>
                   <asp:TemplateField HeaderText="Клетка">
                       <ItemTemplate>
                           <%# Eval("NameCell")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:DropDownList ID="dropDown1" runat="server" ></asp:DropDownList>
                       </EditItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Сотрудник">
                      <ItemTemplate>
                           <%# Eval("FamEmp")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:DropDownList ID="dropDown2" runat="server" ></asp:DropDownList>
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Дата">
                      <ItemTemplate>
                           <%# Eval("DateClean","{0:dd.MM.yyyy}")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox ID="txtDateClean" runat="server" Text='<%# Bind("DateClean","{0:yyyy-MM-dd}")%>' TextMode="Date"></asp:TextBox>
                       </EditItemTemplate>
                   </asp:TemplateField>

                   <asp:commandfield ControlStyle-CssClass="button" ButtonType="Button" ShowEditButton="true" ShowCancelButton="true" HeaderText="Изменить" EditText="Изменить" UpdateText="Изменить" CancelText="Отменить"/>
                   <asp:commandfield ControlStyle-CssClass="button" ButtonType="Button" ShowDeleteButton="true"  HeaderText="Удалить" EditText="Удалить"/>
               </Columns>
             </asp:GridView>
        </div>
    </form>
</body>
</html>
