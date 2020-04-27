<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Position.aspx.cs" Inherits="Zoo.Position" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="css/css.css" />
    <link rel="stylesheet" href="css/form.css" />
    <%-- Проверки на ввод --%>
    <script>
        function NamePos() {
            var reg = /^[А-Я]+[а-я\s-]{1,20}$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox1");
            var errorN = document.getElementById("errorN");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
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
        function Duty() {
            var reg = /^[А-Я]+[а-я\s]{1,20}$/g; //регулярное выражение, g-все вхождения
            var duty = document.getElementById("TextBox2");
            var errorD = document.getElementById("errorD");
            var s = document.getElementById("Button1");
            var end = reg.exec(duty.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorD.innerText = "❌ Неправильный ввод";
                errorD.style.color = "#e7111c";
                errorD.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorD.innerText = "Корректный ввод ✔";
                errorD.style.color = "#00a900";
                errorD.style.fontSize = '18px';
                s.disabled = false;
            }
        } 
    </script>
    <title>Должность</title>
</head>
<body>
    <%-- Фон для формы --%>
    <style>
        .transparent {
            background-image: url('../Properties/form1.jpg');
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
    <%-- Форма и таблица --%>
    <form id="form1" runat="server">
        <div class="dis">
            <%-- Форма --%>
            <div class="transparent">
                <div class="form-inner ">
                    <h3>Должность</h3>
                    <label>Название должности</label>
                    <div id="errorN" class="error"></div>
                    <asp:TextBox ID="TextBox1" runat="server" OnChange="NamePos()" placeholder="Пример: Закупщик корма"></asp:TextBox>
                    <label>Обязанности</label>
                    <div id="errorD" class="error"></div>
                    <asp:TextBox ID="TextBox2" runat="server" OnChange="Duty()" placeholder="Пример: Закупать корм для животных"></asp:TextBox>

                    <asp:Button ID="Button1" runat="server" Text="Добавить" OnClick="Button1_Click" />
                </div>
            </div>
            <%-- Таблица --%>
            <asp:GridView ID="GridView1" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" runat="server" DataKeyNames="PositionId"
                OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" AllowPaging="true" PageSize="4">
                <PagerStyle BackColor="gray" ForeColor="#61000d" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="PositionId" ReadOnly="true" Visible="false" />
                    <asp:TemplateField HeaderText="Название должности">
                        <ItemTemplate>
                            <%# Eval("NamePos")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="txtNamePos" Text='<%# Eval("NamePos")%>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Обязанности">
                        <ItemTemplate>
                            <%# Eval("Duty")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="txtDuty" Text='<%# Eval("Duty")%>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ControlStyle-CssClass="button" ButtonType="Button" ShowEditButton="true" ShowCancelButton="true" HeaderText="Изменить" EditText="Изменить" UpdateText="Изменить" CancelText="Отменить" />
                    <asp:CommandField ControlStyle-CssClass="button" ButtonType="Button" ShowDeleteButton="true" HeaderText="Удалить" EditText="Удалить" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
