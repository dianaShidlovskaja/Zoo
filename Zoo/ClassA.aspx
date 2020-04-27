<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassA.aspx.cs" Inherits="Zoo.ClassA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="css/css.css"/>
    <link rel="stylesheet" href="css/form.css"/>
    <%-- Проверки на ввод --%>
    <script>
        function Squad() {
            var reg = /^[А-Я]+[а-я\s]{1,20}$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox1");
            var errorS = document.getElementById("errorS");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorS.innerText = "❌ Неправильный ввод";
                errorS.style.color = "#e7111c";
                errorS.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorS.innerText = "Корректный ввод ✔";
                errorS.style.color = "#00a900";
                errorS.style.fontSize = '18px';
                s.disabled = false;
            }
        }
        function Family() {
            var reg = /^[А-Я][а-я]{1,20}$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox2");
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
        function Kind() {
            var reg = /^[А-Я][а-я]{1,20}$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox3");
            var errorK = document.getElementById("errorK");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorK.innerText = "❌ Неправильный ввод";
                errorK.style.color = "#e7111c";
                errorK.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorK.innerText = "Корректный ввод ✔";
                errorK.style.color = "#00a900";
                errorK.style.fontSize = '18px';
                s.disabled = false;
            }
        }
        function Сountry() {
            var reg = /^[А-Я][а-я]{1,20}$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox4");
            var errorC = document.getElementById("errorC");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
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
        function Life() {
            var reg = /^\d+$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox5");
            var errorL = document.getElementById("errorL");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorL.innerText = "❌ Неправильный ввод";
                errorL.style.color = "#e7111c";
                errorL.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorL.innerText = "Корректный ввод ✔";
                errorL.style.color = "#00a900";
                errorL.style.fontSize = '18px';
                s.disabled = false;
            }
        }
    </script>
    <title>Классификация животных</title>
</head>
<body>
     <%-- Фон для формы --%>
    <style>
       .transparent {
           background-image: url('../Properties/form3.jpg');
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
            <div class="transparent">
            <div class="form-inner ">
                <h3>Классификация животных</h3>
                <label>Отряд</label>
                <div  id="errorS" class="error"></div>
                <asp:TextBox ID="TextBox1" runat="server" OnChange="Squad()"></asp:TextBox>
                <label>Семейство</label>
                <div id="errorF" class="error"></div>
                <asp:TextBox ID="TextBox2" runat="server" OnChange="Family()"></asp:TextBox>
                <label>Название вида</label>
                <div id="errorK" class="error"> </div>
                <asp:TextBox ID="TextBox3" runat="server"  OnChange="Kind()"></asp:TextBox>
                 <label>Страна проживания</label>
                <div id="errorC" class="error"> </div>
                <asp:TextBox ID="TextBox4" runat="server"  OnChange="Сountry()"></asp:TextBox>
                <label>Продолжительность жизни</label>
                <div id="errorL" class="error"> </div>
                <asp:TextBox ID="TextBox5" runat="server"  OnChange="Life()"></asp:TextBox>
                
                <asp:Button ID="Button1" runat="server" Text="Добавить" OnClick="Button1_Click"  />
            </div>
            </div>
             <%-- Таблица --%>
            <asp:GridView ID="GridView1"  AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" runat="server" DataKeyNames="ClassificationId"
            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" AllowPaging="true" PageSize="6">
                <PagerStyle BackColor="gray" ForeColor="#61000d" HorizontalAlign="Center" />
                <Columns>
                   <asp:BoundField DataField="ClassificationId" ReadOnly="true" Visible="false"/>
                   <asp:TemplateField HeaderText="Отряд">
                       <ItemTemplate>
                           <%# Eval("Squad")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtSquad" Text='<%# Eval("Squad")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Семейство">
                      <ItemTemplate>
                           <%# Eval("Family")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtFamily" Width="100px" Text='<%# Eval("Family")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Вид">
                      <ItemTemplate>
                           <%# Eval("Kind")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtKind" Width="100px" Text='<%# Eval("Kind")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Страна">
                      <ItemTemplate>
                           <%# Eval("Сountry")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtCountry" Width="100px" Text='<%# Eval("Сountry")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Жизнь">
                      <ItemTemplate>
                           <%# Eval("Life")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtLife" Width="40px" Text='<%# Eval("Life")%>' />
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
