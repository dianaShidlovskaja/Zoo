<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Emp.aspx.cs" Inherits="Zoo.Emp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="css/css.css"/>
    <link rel="stylesheet" href="css/form.css"/>
    <%-- Проверки на ввод --%>
    <script>
        function FamEmp(){
            var reg = /^[А-Я][а-я]{1,20}$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox1");
            var errorF = document.getElementById("errorF");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorF.innerText = "❌ Неправильный ввод";
                errorF.style.color = "#e7111c";
                errorF.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorF.innerText = "Корректный ввод ✔";
                errorF.style.color = "#00a900";
                errorF.style.fontSize = '18px';
                s.disabled = false;
            }
        }
        function NameEmp() {
            var reg = /^[А-Я][а-я]{1,20}$/g; //регулярное выражение, g-все вхождения
            var duty = document.getElementById("TextBox2");
            var errorN = document.getElementById("errorN");
            var s = document.getElementById("Button1");
            var end = reg.exec(duty.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorN.innerText = "❌ Неправильный ввод";
                errorN.style.color = "#e7111c";
                errorN.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorN.innerText = "Корректный ввод ✔";
                errorN.style.color = "#00a900";
                errorN.style.fontSize = '18px';
                s.disabled = false;
            }
        }
        function Zap() {
            var reg = /^(\d){1,10}$/g; //регулярное выражение, g-все вхождения
            var duty = document.getElementById("TextBox3");
            var errorZ = document.getElementById("errorZ");
            var s = document.getElementById("Button1");
            var end = reg.exec(duty.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorZ.innerText = "❌ Неправильный ввод";
                errorZ.style.color = "#e7111c";
                errorZ.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorZ.innerText = "Корректный ввод ✔";
                errorZ.style.color = "#00a900";
                errorZ.style.fontSize = '18px';
                s.disabled = false;
            }
        }
    </script>
    <title>Сотрудники</title>
</head>
<body>
    <%-- Фон для формы --%>
    <style>
        .transparent {
            background-image: url('../Properties/form2.jpg');
        }
        select[id="DropDownList1"] {
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
                <h3>Сотрудники</h3>
                <label>Фамилия</label>
                <div  id="errorF" class="error"></div>
                <asp:TextBox ID="TextBox1" runat="server" OnChange="FamEmp()" placeholder="Фамилия"></asp:TextBox>
                <label>Имя</label>
                <div id="errorN" class="error"></div>
                <asp:TextBox ID="TextBox2" runat="server" OnChange="NameEmp()" placeholder="Имя"></asp:TextBox>
                <label>Должность</label>
                <asp:DropDownList ID="DropDownList1" runat="server" class="drop"></asp:DropDownList>
                <label>Зарплата</label>
                <div id="errorZ" class="error"> </div>
                <asp:TextBox ID="TextBox3" runat="server"  OnChange="Zap()"></asp:TextBox>
                
                <asp:Button ID="Button1" runat="server" Text="Добавить" OnClick="Button1_Click"  />
            </div>
            </div>
            <%-- Таблица --%>
            <asp:GridView ID="GridView1"  AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" runat="server" DataKeyNames="EmployeeId"
            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true" PageSize="5">
	            <PagerStyle  BackColor="gray" ForeColor="#61000d"  HorizontalAlign="Center" />
                <Columns>
                   <asp:BoundField DataField="EmployeeId" ReadOnly="true" Visible="false"/>
                   <asp:TemplateField HeaderText="Фамилия">
                       <ItemTemplate>
                           <%# Eval("FamEmp")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtFamEmp" Text='<%# Eval("FamEmp")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Имя">
                      <ItemTemplate>
                           <%# Eval("NameEmp")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtNameEmp" Text='<%# Eval("NameEmp")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Должность">
                      <ItemTemplate>
                           <%# Eval("NamePos")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:DropDownList ID="dropDown" runat="server" ></asp:DropDownList>
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="ЗП">
                      <ItemTemplate>
                           <%# Eval("ZapEmp")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtZapEmp" Width="80px" Text='<%# Eval("ZapEmp")%>' />
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
