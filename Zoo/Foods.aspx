<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Foods.aspx.cs" Inherits="Zoo.Foods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="css/css.css"/>
    <link rel="stylesheet" href="css/form.css"/>
    <script>
        function TypeFood() {
            var reg = /^[А-Я]+[а-я\s-]{1,20}$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox1");
            var errorT = document.getElementById("errorT");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorT.innerText = "❌ Неправильный ввод";
                errorT.style.color = "#e7111c";
                errorT.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorT.innerText = "Корректный ввод ✔";
                errorT.style.color = "#00a900";
                errorT.style.fontSize = '18px';
                s.disabled = false;
            }
        }
        function CountFood() {
            var reg = /^[0-9]*[.,]?[0-9]$/g; //регулярное выражение, g-все вхождения
            var duty = document.getElementById("TextBox2");
            var errorC = document.getElementById("errorC");
            var s = document.getElementById("Button1");
            var end = reg.exec(duty.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorC.innerText = "❌ Неправильный ввод";
                errorC.style.color = "#e7111c";
                errorC.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorC.innerText = "Корректный ввод ✔";
                errorC.style.color = "#00a900";
                errorC.style.fontSize = '18px';
                s.disabled = false;
            }
        }
        function PriceFood() {
            var reg = /^(\d)+$/g; //регулярное выражение, g-все вхождения
            var duty = document.getElementById("TextBox3");
            var errorP = document.getElementById("errorP");
            var s = document.getElementById("Button1");
            var end = reg.exec(duty.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorP.innerText = "❌ Неправильный ввод";
                errorP.style.color = "#e7111c";
                errorP.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorP.innerText = "Корректный ввод ✔";
                errorP.style.color = "#00a900";
                errorP.style.fontSize = '18px';
                s.disabled = false;
            }
        }
    </script>
    <title>Корм</title>
</head>
<body>
     <%-- Фон для формы --%>
    <style>
        .transparent {
            background-image: url('../Properties/form6.jpg');
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
                    <h3>Корм</h3>
                    <label>Тип корма</label>
                    <div id="errorT" class="error"></div>
                    <asp:TextBox ID="TextBox1" runat="server" OnChange="TypeFood()" placeholder="Пример: Мясо"></asp:TextBox>
                    <label>Количество в кг. </label>
                    <div id="errorC" class="error"></div>
                    <asp:TextBox ID="TextBox2" runat="server" OnChange="CountFood()"></asp:TextBox>
                    <label>Цена</label>
                    <div id="errorP" class="error"></div>
                    <asp:TextBox ID="TextBox3" runat="server" OnChange="PriceFood()" ></asp:TextBox>
                    <label>Дата покупки</label>
                    <asp:TextBox ID="TextBox4" runat="server" TextMode="Date" ></asp:TextBox>

                    <asp:Button ID="Button1" runat="server" Text="Добавить" OnClick="Button1_Click" />
                </div>
            </div>
            <%-- Таблица --%>
            <asp:GridView ID="GridView1" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" runat="server" DataKeyNames="FoodId"
                OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" AllowPaging="true" PageSize="5">
                <PagerStyle BackColor="gray" ForeColor="#61000d" HorizontalAlign="Center" />
                <Columns>
                    <asp:BoundField DataField="FoodId" ReadOnly="true" Visible="false" />
                    <asp:TemplateField HeaderText="Тип корма">
                        <ItemTemplate>
                            <%# Eval("TypeFood")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="txtTypeFood" Text='<%# Eval("TypeFood")%>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Количество">
                        <ItemTemplate>
                            <%# Eval("CountFood")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="txtCountFood" Width="50px" Text='<%# Eval("CountFood")%>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Цена">
                        <ItemTemplate>
                            <%# Eval("PriceFood")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="txtPriceFood" Width="100px" Text='<%# Eval("PriceFood")%>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Дата">
                      <ItemTemplate>
                           <%# Eval("DateFood","{0:dd.MM.yyyy}")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox ID="txtDateFood" runat="server" Text='<%# Bind("DateFood","{0:yyyy-MM-dd}")%>' TextMode="Date"></asp:TextBox>
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
