<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Zoo.Reports" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="css/css.css" />
    <link rel="stylesheet" href="css/form.css" />
    <title>Отчеты</title>
</head>
<body>
    <%-- Горизонтальное меню --%>
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
    <%-- Вертикальное меню --%>
    <div class="category-wrap">
        <h3>Таблицы</h3>
        <ul>
            <li><a href="Position.aspx">Должность</a></li>
            <li><a href="Emp.aspx">Сотрудники</a></li>
            <li><a href="ClassA.aspx">Классификация</a></li>
            <li><a href="Cells.aspx">Клетки</a></li>
            <li><a href="Animals.aspx">Животные</a></li>
            <li><a href="Foods.aspx">Корм</a></li>
            <li><a href="Feed.aspx">Кормление</a></li>
            <li><a href="Clean.aspx">Уборка клеток</a></li>
        </ul>
    </div>
    <%-- Текст --%>
    <form id="form1" runat="server">
        <div class="text" style="margin-left: 15px;">
            <h2>Сводки</h2>
            <%-- Отчет по сотрудникам --%>
            <p style="margin: 0; padding: 0;">Какую з/п получают сорудники зоопарка ежемесячно?</p>
            <%-- Таблица №1 --%>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:BoundField DataField="NamePos" HeaderText="Должность" />
                    <asp:BoundField DataField="Zap" HeaderText="Зарплата в месяц" />
                </Columns>
            </asp:GridView>
            <p style="margin:10px 0; padding: 0;">Итого: <asp:Label ID="Label1" runat="server" ForeColor="#b40129"></asp:Label></p>
            <p style="margin:10px 0; padding: 0;">В год: <asp:Label ID="Label2" runat="server" ForeColor="#b40129"></asp:Label></p>
            <%-- Диаграмма1 --%>
            <asp:Chart runat="server" ID="Chart1" ImageStorageMode="UseImageLocation" Width="500px" Height="300px" BackColor="#513335" BackSecondaryColor="Gray" BackGradientStyle="DiagonalRight" BorderlineDashStyle="Solid" BorderSkin-SkinStyle="Emboss" BorderlineColor="#999999">
                <Series>
                    <asp:Series Name="Series1" ChartType="Pie"></asp:Series>
                </Series>
                <Titles>
                    <asp:Title Text="Ежемесячная з/п сотрудникам" ForeColor="white" Font="Playfair Display SC, 16"></asp:Title>
                </Titles>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BackColor="#e3d9df">
                        <Area3DStyle Enable3D="True" Inclination="15" IsClustered="False" />
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <%-- Отчет по вольерам --%>
            <p style="margin-left: 0; padding: 0;">Сколько зоопарку обходятся вольеры?</p>
            <%-- Таблица №2 --%>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:BoundField DataField="NameCell" HeaderText="Название вольера" />
                    <asp:BoundField DataField="PriceCell" HeaderText="Цена" />
                </Columns>
            </asp:GridView>
            <p style="margin:10px 0; padding: 0;">Итого: <asp:Label ID="Label3" runat="server" ForeColor="#b40129"></asp:Label></p>
            <%-- Диаграмма2 --%>
            <asp:Chart runat="server" ID="Chart2" ImageStorageMode="UseImageLocation" Width="600px" Height="400px" BackColor="#513335" BackSecondaryColor="Gray" BackGradientStyle="DiagonalRight" BorderlineDashStyle="Solid" BorderSkin-SkinStyle="Emboss" BorderlineColor="#999999">
                <Series>
                    <asp:Series Name="Series1" ChartType="Pie"></asp:Series>
                </Series>
                <Titles>
                    <asp:Title Text="Затраты на вольеры" ForeColor="white" Font="Playfair Display SC, 16"></asp:Title>
                </Titles>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BackColor="#e3d9df">
                        <Area3DStyle Enable3D="True" Inclination="15" IsClustered="False" />
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <%-- Отчет по корму --%>
            <p style="margin-left: 0; padding: 0;">Сколько зоопарку обходится корм?</p>
            <%-- Таблица №3 --%>
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:BoundField DataField="TypeFood" HeaderText="Название корма" />
                    <asp:BoundField DataField="PriceFood" HeaderText="Цена" />
                </Columns>
            </asp:GridView>
            <p style="margin:10px 0; padding: 0;">Итого: <asp:Label ID="Label4" runat="server" ForeColor="#b40129"></asp:Label></p>
            <%-- Диаграмма3 --%>
            <asp:Chart runat="server" ID="Chart3" ImageStorageMode="UseImageLocation" Width="600px" Height="400px" BackColor="#513335" BackSecondaryColor="Gray" BackGradientStyle="DiagonalRight" BorderlineDashStyle="Solid" BorderSkin-SkinStyle="Emboss" BorderlineColor="#999999">
                <Series>
                    <asp:Series Name="Series1" ChartType="Pie"></asp:Series>
                </Series>
                <Titles>
                    <asp:Title Text="Затраты на корм" ForeColor="white" Font="Playfair Display SC, 16"></asp:Title>
                </Titles>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BackColor="#e3d9df">
                        <Area3DStyle Enable3D="True" Inclination="15" IsClustered="False" />
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
    </form>
    <%-- Каритинка смешная --%>
    <div class="text">
        <div>
            <img src="Properties/mem.png" alt="Мем" title="М" style="position: fixed; bottom: 0; right: 0; z-index: 500;" />
        </div>
    </div>
</body>
</html>
