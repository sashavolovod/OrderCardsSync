using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


public class XmlParser
{
    public static List<OrderCard> Parse(SortedList<int, int> Codes)
    {
        List<OrderCard> orderCards = new List<OrderCard>();

        XmlDocument xmlDoc = new XmlDocument();
        string strFilename = OrderCardsSync.Properties.Settings.Default.pathToXml;

        if (File.Exists(strFilename))
            xmlDoc.Load(strFilename);
        else
            Program.Logger("Файл '" + strFilename + "' не найден");

        XmlNodeList NodeList = xmlDoc.GetElementsByTagName("KZ");

        foreach (XmlNode node in NodeList)
        {
            //Console.WriteLine(node.InnerText);
            OrderCard card = new OrderCard();
            foreach (XmlNode ChildNode in node.ChildNodes)
            {
                //Console.WriteLine("{0} : {1}", ChildNode.Name, ChildNode.InnerText);
                switch (ChildNode.Name)
                {
                    case "CODE":
                        card.CODE = int.Parse(ChildNode.InnerText);
                        break;
                    case "KZNAME":
                        card.KZNAME = ChildNode.InnerText;
                        break;
                    case "OSNOVANIE":
                        card.OSNOVANIE = ChildNode.InnerText;
                        break;
                    case "DESIGNPLAN_DATE":
                        if (ChildNode.InnerText != "")
                            card.DESIGNPLAN_DATE = DateTime.Parse(ChildNode.InnerText);
                        break;
                    case "MAKEPLAN_DATE":
                        if (ChildNode.InnerText != "")
                            card.MAKEPLAN_DATE = DateTime.Parse(ChildNode.InnerText);
                        break;
                    case "QUANTITY":
                        card.QUANTITY = int.Parse(ChildNode.InnerText);
                        break;
                    case "DSESIGN":
                        card.DSESIGN = ChildNode.InnerText;
                        break;
                    case "DSENAME":
                        card.DSENAME = ChildNode.InnerText;
                        break;
                    case "OSNSIGN":
                        card.OSNSIGN = ChildNode.InnerText;
                        break;
                    case "OSNNAME":
                        card.OSNNAME = ChildNode.InnerText;
                        break;
                    case "ZAKAZCIKSIGN":
                        card.ZAKAZCIKSIGN = ChildNode.InnerText;
                        break;
                    case "ZAKAZCIKNAME":
                        card.ZAKAZCIKNAME = ChildNode.InnerText;
                        break;
                    case "IZGOTOVITELSIGN":
                        card.IZGOTOVITELSIGN = ChildNode.InnerText;
                        break;
                    case "IZGOTOVITELNAME":
                        card.IZGOTOVITELNAME = ChildNode.InnerText;
                        break;
                }

            }
            // выбираем заказы для Инц. которые отсутствуют в TexAC
            if ((card.IZGOTOVITELSIGN == "ИнЦ") && Codes.ContainsKey(card.CODE)==false)
                orderCards.Add(card);
        }
        return orderCards;
    }
}
