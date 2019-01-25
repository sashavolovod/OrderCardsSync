using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.OleDb;

class Program
{
    static int  Main(string[] args)
    {
        Logger("Программа синхронизации карт заказа КИТ Омега -> TexAC");

        SortedList<int, int> OrderNumbers = DbWrapper.getOrderCodes(OrderCardsSync.Properties.Settings.Default.connStr);
        if (OrderNumbers.Count == 0)
        {
            Logger("Ошибка. Не удается составить список номеров карт заказов из TexAC");
            return -1;
        }
        
        Logger("Найдено "+OrderNumbers.Count+" в TexAC");

        List<OrderCard> cards = XmlParser.Parse(OrderNumbers);

        if (cards.Count > 0)
        {
            Logger("Выбрано " + cards.Count + " карт заказа для импорта");

            DbWrapper.saveOrders(OrderCardsSync.Properties.Settings.Default.connStr, cards);
        }
        else
        {
            Logger("Нет карт заказа для импорта");
        }


        return 0;
    }

    static public void Logger(String lines)
    {
        Console.WriteLine(DateTime.Now + " : " + lines);
        System.IO.StreamWriter file = new System.IO.StreamWriter("log.txt", true);
        file.WriteLine(DateTime.Now + " : " + lines);
        file.Close();
    }

}

