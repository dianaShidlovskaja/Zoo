<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feed.aspx.cs" Inherits="Zoo.Feed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="css/css.css"/>
    <link rel="stylesheet" href="css/form.css"/>
    <%-- Проверки на ввод --%>
    <script>
        function Ration(){
            var reg = /^[0-9]*[.,]?[0-9]$/g; //регулярное выражение, g-все вхождения
            var name = document.getElementById("TextBox1");
            var errorR = document.getElementById("errorR");
            var s = document.getElementById("Button1");
            var end = reg.exec(name.value) || []; //exec- поиск сопоставления регулярки, кт возвращает массив с результатами или (пустой массив вместо) нул
            if (end.length == 0) {
                errorR.innerText = "❌ Неправильный ввод";
                errorR.style.color = "#e7111c";
                errorR.style.fontSize = '18px';
                s.disabled = true;
            } else {
                errorR.innerText = "Корректный ввод ✔";
                errorR.style.color = "#00a900";
                errorR.style.fontSize = '18px';
                s.disabled = false;
            }
        }
    </script>
    <title>Кормление</title>
</head>
<body>
     <%-- Фон для формы --%>
    <style>
        .transparent {
            background-image: url('../Properties/form7.png');
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
                <h3>Кормление</h3>
                <label>Сотрудник</label>
                <asp:DropDownList ID="DropDownList1" runat="server" class="drop"></asp:DropDownList>
                <label>Животное</label>
                <asp:DropDownList ID="DropDownList2" runat="server" class="drop"></asp:DropDownList>
                <label>Тип корма</label>
                <asp:DropDownList ID="DropDownList3" runat="server" class="drop"></asp:DropDownList>
                <label>Рацион</label>
                <div id="errorR" class="error"> </div>
                <asp:TextBox ID="TextBox1" runat="server" OnChange="Ration()"></asp:TextBox>
                <label>Дата</label>
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Date" ></asp:TextBox>
                
                <asp:Button ID="Button1" runat="server" Text="Добавить" OnClick="Button1_Click"  />
            </div>
            </div>
            <%-- Таблица --%>
            <asp:GridView ID="GridView1"  AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" runat="server" DataKeyNames="FeedId"
            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true" PageSize="5">
	            <PagerStyle  BackColor="gray" ForeColor="#61000d"  HorizontalAlign="Center" />
                <Columns>
                   <asp:BoundField DataField="FeedId" ReadOnly="true" Visible="false"/>
                   <asp:TemplateField HeaderText="Сотрудник">
                       <ItemTemplate>
                           <%# Eval("FamEmp")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:DropDownList ID="dropDown1" runat="server" ></asp:DropDownList>
                       </EditItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Животное">
                      <ItemTemplate>
                           <%# Eval("NameAnimal")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:DropDownList ID="dropDown2" runat="server" ></asp:DropDownList>
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Тип корма">
                      <ItemTemplate>
                           <%# Eval("TypeFood")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:DropDownList ID="dropDown3" runat="server"  ></asp:DropDownList>
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Рацион">
                      <ItemTemplate>
                           <%# Eval("Ration")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox runat="server" ID="txtRation" Width="50px" Text='<%# Eval("Ration")%>' />
                       </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Дата">
                      <ItemTemplate>
                           <%# Eval("DateFeed","{0:dd.MM.yyyy}")%>
                       </ItemTemplate>
                       <EditItemTemplate>
                           <asp:TextBox ID="txtDateFeed" Width="130px" runat="server" Text='<%# Bind("DateFeed","{0:yyyy-MM-dd}")%>' TextMode="Date"></asp:TextBox>
                       </EditItemTemplate>
                   </asp:TemplateField>

                   <asp:commandfield ControlStyle-CssClass="button" ButtonType="Button" ShowEditButton="true" ShowCancelButton="true" HeaderText="Изменить" EditText="Изменить" UpdateText="Изменить" CancelText="Отменить"/>
                   <asp:commandfield ControlStyle-CssClass="button" ButtonType="Button" ShowDeleteButton="true"  HeaderText="Удалить" EditText="Удалить"/>
               </Columns>
             </asp:GridView>
        </div>
    </form>
    <%-- Памятка --%>
        <div class="block">
            <a href="#" class="block1">Памятка <span><img src="Properties/Памятка.png" style="width:auto; height:450px;" /></span></a>
        </div>
</body>
</html>
