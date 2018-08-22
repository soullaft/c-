-- phpMyAdmin SQL Dump
-- version 4.7.7
-- https://www.phpmyadmin.net/
--
-- Хост: localhost
-- Время создания: Июн 19 2018 г., 11:07
-- Версия сервера: 5.7.21-20-beget-5.7.21-20-1-log
-- Версия PHP: 5.6.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `soullaqd_terent`
--

-- --------------------------------------------------------

--
-- Структура таблицы `Clients`
--
-- Создание: Май 25 2018 г., 22:05
-- Последнее обновление: Май 30 2018 г., 09:20
--

DROP TABLE IF EXISTS `Clients`;
CREATE TABLE `Clients` (
  `ID` int(11) NOT NULL,
  `Name` varchar(55) NOT NULL,
  `Adress` varchar(255) NOT NULL,
  `Phone` varchar(55) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `Clients`
--

INSERT INTO `Clients` (`ID`, `Name`, `Adress`, `Phone`) VALUES
(7, 'ООО \"Семья\"', 'Садовая улица, 329', '88007759075'),
(8, 'ООО \"Комус\"', 'улица Куйбышева, 78', '88002003383');

-- --------------------------------------------------------

--
-- Структура таблицы `History`
--
-- Создание: Май 26 2018 г., 00:32
-- Последнее обновление: Май 30 2018 г., 09:21
--

DROP TABLE IF EXISTS `History`;
CREATE TABLE `History` (
  `ID` int(11) NOT NULL,
  `IDUser` int(11) NOT NULL,
  `IDClient` int(11) NOT NULL,
  `Text` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `History`
--

INSERT INTO `History` (`ID`, `IDUser`, `IDClient`, `Text`) VALUES
(21, 1, 7, 'Менеджер Терентьев Игорь Ильшатович продал Крем \"Absolut\" (300 шт.) ООО \"Семья\" на сумму 15000руб. \n 30.05.2018 13:21:08'),
(22, 1, 7, 'Менеджер Терентьев Игорь Ильшатович продал Мыло \"Весна\" (500 шт.) ООО \"Семья\" на сумму 17500руб. \n 30.05.2018 13:21:32'),
(23, 1, 8, 'Менеджер Терентьев Игорь Ильшатович продал Шампунь \"Весна\" (400 шт.) ООО \"Комус\" на сумму 30000руб. \n 30.05.2018 13:21:48');

-- --------------------------------------------------------

--
-- Структура таблицы `Sklad`
--
-- Создание: Май 18 2018 г., 19:16
-- Последнее обновление: Май 30 2018 г., 09:21
--

DROP TABLE IF EXISTS `Sklad`;
CREATE TABLE `Sklad` (
  `ID` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Count` int(11) NOT NULL,
  `Cost` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `Sklad`
--

INSERT INTO `Sklad` (`ID`, `Name`, `Count`, `Cost`) VALUES
(6, 'Крем \"Absolut\"', 700, 50),
(7, 'Мыло \"Весна\"', 500, 35),
(8, 'Шампунь \"Весна\"', 600, 75),
(9, 'Детское мыло', 700, 60);

-- --------------------------------------------------------

--
-- Структура таблицы `Users`
--
-- Создание: Май 18 2018 г., 19:14
-- Последнее обновление: Май 30 2018 г., 09:09
--

DROP TABLE IF EXISTS `Users`;
CREATE TABLE `Users` (
  `ID` int(11) NOT NULL,
  `FIO` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Phone` varchar(55) NOT NULL,
  `Login` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Role` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `Users`
--

INSERT INTO `Users` (`ID`, `FIO`, `Email`, `Phone`, `Login`, `Password`, `Role`) VALUES
(1, 'Терентьев Игорь Ильшатович', 'soullaft@gmail.com', '89991584716', 'root', 'root', 1),
(9, 'Иванов Иван Иванович', 'ivanov@mail.ru', '89999999999', 'qwerty', '123', 2);

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Clients`
--
ALTER TABLE `Clients`
  ADD PRIMARY KEY (`ID`);

--
-- Индексы таблицы `History`
--
ALTER TABLE `History`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `IDClient` (`IDClient`),
  ADD KEY `IDUser` (`IDUser`);

--
-- Индексы таблицы `Sklad`
--
ALTER TABLE `Sklad`
  ADD PRIMARY KEY (`ID`);

--
-- Индексы таблицы `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Clients`
--
ALTER TABLE `Clients`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT для таблицы `History`
--
ALTER TABLE `History`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT для таблицы `Sklad`
--
ALTER TABLE `Sklad`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT для таблицы `Users`
--
ALTER TABLE `Users`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `History`
--
ALTER TABLE `History`
  ADD CONSTRAINT `History_ibfk_1` FOREIGN KEY (`IDClient`) REFERENCES `Clients` (`ID`),
  ADD CONSTRAINT `History_ibfk_2` FOREIGN KEY (`IDUser`) REFERENCES `Users` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
