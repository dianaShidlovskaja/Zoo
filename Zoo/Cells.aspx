<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cells.aspx.cs" Inherits="Zoo.Cells" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="css/css.css"/>
    <link rel="stylesheet" href="css/form.css"/>
     <%-- Проверки на ввод --%>
    <script>
        function NameCell() {
            var reg = /^[А-Я]+([а-я\s]{1,20}|[а-я\s]{1,20}\s\d)$/g; //регулярное выражение, g-все вхождения
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
        function WidthCell() {
            var reg = /^\d+$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox2");
            var errorW = document.getElementById("errorW");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorW.innerText = "❌ Неправильный ввод";
                errorW.style.color = "#e7111c";
                errorW.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorW.innerText = "Корректный ввод ✔";
                errorW.style.color = "#00a900";
                errorW.style.fontSize = '18px';
                s.disabled = false;
            }
        }
        function HeightCell() {
            var reg = /^\d+$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox3");
            var errorH = document.getElementById("errorH");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorH.innerText = "❌ Неправильный ввод";
                errorH.style.color = "#e7111c";
                errorH.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorH.innerText = "Корректный ввод ✔";
                errorH.style.color = "#00a900";
                errorH.style.fontSize = '18px';
                s.disabled = false;
            }
        }
        function Places() {
            var reg = /^\d+$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox4");
            var errorP = document.getElementById("errorP");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
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
        function PriceCell() {
            var reg = /^\d+$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox5");
            var errorPr = document.getElementById("errorPr");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorPr.innerText = "❌ Неправильный ввод";
                errorPr.style.color = "#e7111c";
                errorPr.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorPr.innerText = "Корректный ввод ✔";
                errorPr.style.color = "#00a900";
                errorPr.style.fontSize = '18px';
                s.disabled = false;
            }
        }
    </script>
    <title>Клетки</title>
</head>
<body>
     <%-- Фон для формы --%>
    <style>
       .transparent {
           background-image: url('../Properties/form4.jpg');
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
                <h3>Клетки</h3>
                <label>Название</label>
                <div  id="errorN" class="error"></div>
                <asp:TextBox ID="TextBox1" runat="server" OnChange="NameCell()" placeholder="Пример: Вольер для жирафов"></asp:TextBox>
                <label>Ширина в метрах</label>
                <div id="errorW" class="error"></div>
                <asp:TextBox ID="TextBox2" runat="server" OnChange="WidthCell()"></asp:TextBox>
                <label>Высота в метрах</label>
                <div id="errorH" class="error"> </div>
                <asp:TextBox ID="TextBox3" runat="server"  OnChange="HeightCell()"></asp:TextBox>
                 <label>Вместимость</label>
                <div id="errorP" class="error"> </div>
                <asp:TextBox ID="TextBox4" runat="server"  OnChange="Places()"></asp:TextBox>
                <label>Цена</label>
                <div id="errorPr" class="error"> </div>
                <asp:TextBox ID="TextBox5" runat="server"  OnChange="PriceCell()"></asp:TextBox>
                
                <asp:Button ID="Button1" runat="server" Text="Добавить" OnClick="Button1_Click"  />
            </div>
            </div>
            <%-- Таблица --%>
            <asp:GridView ID="GridView1"  AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" runat="server" DataKeyNames="CellId"
            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" AllowPaging="true" PageSize="6">
                <PagerStyle BackColor="gray" ForeColor="#61000d" HorizontalAlign="Center" />
                <Columns>
                   <asp:BoundField DataField="CellId" ReadOnly="true" Visible="false"/>
                   <asp:TemplateField HeaderText="Название">
                       <ItemTemplate>
                           <%# Eval("NameCell")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtNameCell" Text='<%# Eval("NameCell")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Ширина">
                      <ItemTemplate>
                           <%# Eval("WidthCell")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtWidthCell" Width="50px" Text='<%# Eval("WidthCell")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Высота">
                      <ItemTemplate>
                           <%# Eval("HeightCell")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtHeightCell" Width="50px" Text='<%# Eval("HeightCell")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Места">
                      <ItemTemplate>
                           <%# Eval("Places")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtPlaces" Width="50px" Text='<%# Eval("Places")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Цена">
                      <ItemTemplate>
                           <%# Eval("PriceCell")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtPriceCell" Width="100px" Text='<%# Eval("PriceCell")%>' />
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
