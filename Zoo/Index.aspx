<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Zoo.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="css/css.css"/>
    <title>Зоопарк</title>
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
    <div class="text">
        <h2>Добро пожаловать</h2>
        <p>Ниже вам будут представлены животные, проживающие в нашем зоопарке, а так же интересные факты о них :</p>    
    </div>
    <%-- Блочные элементы --%>
    <div class="post-wrap">
	<div class="post-item">
		<div class="item-content">
			<div class="item-icon"><img src="Properties/giraffe.png"/></div>
			<div class="item-body">
				<h3>Жираф</h3>
				<p>Расположение и размер пятен, покрывающих шкуры жирафов, неповторимы, как отпечатки человеческих пальцев. По их оттенку можно установить, насколько стар жираф – чем пятна темнее, тем животное старше.</p>
			</div>
		</div>
	</div>
    <div class="post-item">
	    <div class="item-content">
		    <div class="item-icon"><img src="Properties/elephant.png"/></div>
		    <div class="item-body">
			    <h3>Слон</h3>
			    <p>У слона самая долгая в мире беременность. Период беременности у слона может достигать 22 месяцев. По истечении этого срока, рождается слоненок весом около 100 килограмм, покрытый волосами.</p>
		    </div>
	    </div>
    </div>
    <div class="post-item">
	    <div class="item-content">
		    <div class="item-icon"><img src="Properties/koala.png"/></div>
		    <div class="item-body">
			    <h3>Коала</h3>
			    <p>Вес мозга коалы составляет всего 0,2% от массы тела животного. Ученые выяснили, что мозг у предков коал занимал весь череп, но из-за питания листьями постепенно деградировал до нынешнего состояния.</p>
		    </div>
	    </div>
    </div>
    <div class="post-item">
	    <div class="item-content">
		    <div class="item-icon"><img src="Properties/monkey.png"/></div>
		    <div class="item-body">
			    <h3>Обезьяна</h3>
			    <p>Обезьяны, как наиболее близкие к человеку по физиологии, многократно запускались в суборбитальные и орбитальные полёты как до, так и после первого полёта человека в космос. Всего в космос слетали 32 обезьяны</p>
		    </div>
	    </div>
    </div>
    <div class="post-item">
	    <div class="item-content">
		    <div class="item-icon"><img src="Properties/tiger.png"/></div>
		    <div class="item-body">
			    <h3>Тигр</h3>
			    <p>В отличие от большинства других кошек, тигры любят воду и являются прекрасными пловцами, способными во время охоты проплывать несколько километров и пересекать реки. Нередко тигры купаются и играют в воде просто для удовольствия.</p>
		    </div>
	    </div>
    </div>
    <div class="post-item">
	    <div class="item-content">
		    <div class="item-icon"><img src="Properties/penguin.png"/></div>
		    <div class="item-body">
			    <h3>Пингвин</h3>
			    <p>Чтобы согреться, пингвины сбиваются в плотные кучи. В центре этого сборища температура может быть на 40-45 градусов выше, чем снаружи, и птицы постоянно меняются местами, чтобы тепла досталось всем.</p>
		    </div>
	    </div>
    </div>
    </div>
    <%-- Подвал --%>
    <footer>
        <hr/>
        <p>&laquo;Информационная система учета содержания животных зоопарка&raquo;</p>
    </footer>
</body>
</html>
