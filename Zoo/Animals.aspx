<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Animals.aspx.cs" Inherits="Zoo.Animals" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="css/css.css"/>
    <link rel="stylesheet" href="css/form.css"/>
    <script>
        function NameAnimal() {
            var reg = /^[А-Я][а-я]{1,20}$/g; //регулярное выражение, g-все вхождения
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
        function Age() {
            var reg = /^(\d){1,2}$/g; //регулярное выражение, g-все вхождения
            var duty = document.getElementById("TextBox2");
            var errorA = document.getElementById("errorA");
            var s = document.getElementById("Button1");
            var end = reg.exec(duty.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorA.innerText = "❌ Неправильный ввод";
                errorA.style.color = "#e7111c";
                errorA.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorA.innerText = "Корректный ввод ✔";
                errorA.style.color = "#00a900";
                errorA.style.fontSize = '18px';
                s.disabled = false;
            }
        }
        function Weight() {
            var reg = /^(\d)+$/g; //регулярное выражение, g-все вхождения
            var duty = document.getElementById("TextBox3");
            var errorW = document.getElementById("errorW");
            var s = document.getElementById("Button1");
            var end = reg.exec(duty.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
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
    </script>
    <title>Животные</title>
</head>
<body>
     <%-- Фон для формы --%>
    <style>
       .transparent {
           background-image: url('../Properties/form5.jpg');
       }
        select[id="DropDownList1"], select[id="DropDownList2"], select[id="DropDownList3"] {
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
        .drop option {
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
                <h3>Животные</h3>
                <label>Классификация</label>
                <asp:DropDownList ID="DropDownList1" runat="server" class="drop"></asp:DropDownList>
                <label>Кличка</label>
                <div id="errorN" class="error"></div>
                <asp:TextBox ID="TextBox1" runat="server" OnChange="NameAnimal()"></asp:TextBox>
                <label>Возраст</label>
                <div id="errorA" class="error"> </div>
                <asp:TextBox ID="TextBox2" runat="server"  OnChange="Age()"></asp:TextBox>
                <label>Пол</label>
                <asp:DropDownList ID="DropDownList2" runat="server" class="drop">
                    <asp:ListItem>Самец</asp:ListItem> 
                    <asp:ListItem>Самка</asp:ListItem>
                </asp:DropDownList>
                <label>Масса в кг.</label>
                <div id="errorW" class="error"> </div>
                <asp:TextBox ID="TextBox3" runat="server"  OnChange="Weight()"></asp:TextBox>
                <label>Клетка</label>
                <asp:DropDownList ID="DropDownList3" runat="server" class="drop"></asp:DropDownList>
                
                <asp:Button ID="Button1" runat="server" Text="Добавить" OnClick="Button1_Click"  />
            </div>
            </div>
            <%-- Таблица --%>
            <asp:GridView ID="GridView1"  AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" runat="server" DataKeyNames="AnimalId"
            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true" PageSize="6">
                <PagerStyle BackColor="gray" ForeColor="#61000d" HorizontalAlign="Center" />
                <Columns>
                   <asp:BoundField DataField="AnimalId" ReadOnly="true" Visible="false"/>
                   <asp:TemplateField HeaderText="Вид">
                       <ItemTemplate>
                           <%# Eval("Kind")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                            <asp:DropDownList ID="dropDown1" runat="server" ></asp:DropDownList>
                       </EditItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Кличка">
                      <ItemTemplate>
                           <%# Eval("NameAnimal")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtNameAnimal" width="80px" Text='<%# Eval("NameAnimal")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Возраст">
                      <ItemTemplate>
                           <%# Eval("Age")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                            <asp:TextBox runat="server" ID="txtAge" Width="40px" Text='<%# Eval("Age")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Пол">
                      <ItemTemplate>
                           <%# Eval("Gender")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                            <asp:DropDownList ID="dropDown2" runat="server" >
                                <asp:ListItem>Самец</asp:ListItem> 
                                <asp:ListItem>Самка</asp:ListItem>
                            </asp:DropDownList>
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Масса">
                      <ItemTemplate>
                           <%# Eval("Weight")%>
                       </ItemTemplate>
                       
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtWeight" Width="60px" Text='<%# Eval("Weight")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                     <asp:TemplateField HeaderText="Клетка">
                      <ItemTemplate>
                           <%# Eval("NameCell")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:DropDownList ID="dropDown3" runat="server" ></asp:DropDownList>
                       </EditItemTemplate>
                   </asp:TemplateField>

                   <asp:commandfield ControlStyle-CssClass="button" ButtonType="Button" ShowEditButton="true" ShowCancelButton="true" HeaderText="Изменить" EditText="Изменить" CancelText="Отменить" UpdateText="Изменить"/>
                   <asp:commandfield ControlStyle-CssClass="button" ButtonType="Button" ShowDeleteButton="true"  HeaderText="Удалить" EditText="Удалить"/>
               </Columns>
             </asp:GridView>
        </div>
        <asp:ValidationSummary runat="server" ID="Val1" ShowSummary="true" ShowMessageBox="true" />
    </form>
</body>
</html>
