using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScripting
{

    // Initialisation des lotteries
    static CCC.Utility.Lottery lotteryFood;

    static CCC.Utility.Lottery lotteryEssence;

    static CCC.Utility.Lottery lotteryUtility;

    static CCC.Utility.Lottery lotteryCollectable;

    public static int currentEvent = 0;


    public static void Init(Voiture car)
    {
        lotteryFood = new CCC.Utility.Lottery(new CCC.Utility.Lottery.LotteryItem[]
        {
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(0),1),
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(1),1),
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(2),2),
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(3),3),
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(4),3),
        });
        lotteryEssence =  new CCC.Utility.Lottery(new CCC.Utility.Lottery.LotteryItem[]
        {
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(5),2),
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(6),3),
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(7),5),
        });
        lotteryUtility = new CCC.Utility.Lottery(new CCC.Utility.Lottery.LotteryItem[]
        {
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(8),2),
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(9),2),
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(10),6),
        });
        lotteryCollectable = new CCC.Utility.Lottery(new CCC.Utility.Lottery.LotteryItem[]
        {
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(11),1),
        new CCC.Utility.Lottery.LotteryItem(ItemBank.GetItemByIndex(12),2),
        });

        // Chemin St-Stanilas a Montreal
        Ville depart = new Ville("St-Stanislas");
        Ville destination = new Ville("Montreal");


        // Objets des magasins
        List<ItemAVendre> itemDepanneur22km = new List<ItemAVendre>();
        itemDepanneur22km.Add(new ItemAVendre(ItemBank.GetItemByIndex(2), 5, 3));
        itemDepanneur22km.Add(new ItemAVendre(ItemBank.GetItemByIndex(3), 3, 3));
        itemDepanneur22km.Add(new ItemAVendre(ItemBank.GetItemByIndex(4), 2, 3));

        List<ItemAVendre> itemStation40km = new List<ItemAVendre>();
        itemStation40km.Add(new ItemAVendre(ItemBank.GetItemByIndex(5), 7, 6));
        itemStation40km.Add(new ItemAVendre(ItemBank.GetItemByIndex(6), 4, 6));
        itemStation40km.Add(new ItemAVendre(ItemBank.GetItemByIndex(7), 3, 6));

        List<ItemAVendre> itemRestaurant47km = new List<ItemAVendre>();
        itemRestaurant47km.Add(new ItemAVendre(ItemBank.GetItemByIndex(0), 7, 3));
        itemRestaurant47km.Add(new ItemAVendre(ItemBank.GetItemByIndex(1), 6, 3));

        List<ItemAVendre> itemGarage62km = new List<ItemAVendre>();
        itemGarage62km.Add(new ItemAVendre(ItemBank.GetItemByIndex(8), 25, 1));
        itemGarage62km.Add(new ItemAVendre(ItemBank.GetItemByIndex(9), 20, 1));
        itemGarage62km.Add(new ItemAVendre(ItemBank.GetItemByIndex(10), 5, 5));


        // Magasins
        List<Stop> stopRoad1 = new List<Stop>();
        Stop depanneur22km = new Stop(22, LieuType.depaneur, itemDepanneur22km);
        Stop station40km = new Stop(40, LieuType.stationEssence, itemStation40km);
        Stop restaurant47km = new Stop(47, LieuType.restaurant, itemRestaurant47km);
        Stop garage62km = new Stop(62, LieuType.garage, itemGarage62km);

        stopRoad1.Add(depanneur22km);
        stopRoad1.Add(station40km);
        stopRoad1.Add(restaurant47km);
        stopRoad1.Add(garage62km);


        // Quetes
        Stop charles10km = new Stop(10, LieuType.arretBus);
        Stop marc17km = new Stop(17, LieuType.arretBus);
        Stop monique57km = new Stop(57, LieuType.arretBus);

        stopRoad1.Add(charles10km);
        stopRoad1.Add(marc17km);
        stopRoad1.Add(monique57km);

        List<Quest.ItemQuest> itemQuestCharle = new List<Quest.ItemQuest>();
        itemQuestCharle.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(5), 5));

        List<Quest> questRoad1 = new List<Quest>();
        questRoad1.Add(new Quest("Déposer Marise et ses valises à Montréal", 0, 0, null, new Quest.Destination(destination, null)));
        questRoad1.Add(new Quest("Déposer Charles et son équipement de hockey à Montreal", 10, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(destination, null), itemQuestCharle));
        questRoad1.Add(new Quest("Déposer Marc à la prochaine station service", 17, 999, PersonneBank.GetItemByIndex(0), new Quest.Destination(null, station40km)));
        questRoad1.Add(new Quest("Déposer Monique à Montreal", 57, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(destination, null)));


        // Loteries
        List<ItemEvent> itemEventRoad1 = new List<ItemEvent>();
        ItemEvent item5km = new ItemEvent(5, 0, ItemBank.GetItemByIndex(17));
        ItemEvent item29km = new ItemEvent(29, 0, (Item)lotteryFood.Pick());
        ItemEvent item35km = new ItemEvent(35, 0, (Item)lotteryEssence.Pick());
        ItemEvent item52km = new ItemEvent(52, 0, (Item)lotteryUtility.Pick());
        ItemEvent item67km = new ItemEvent(67, 0, (Item)lotteryCollectable.Pick());

        itemEventRoad1.Add(item5km);
        itemEventRoad1.Add(item29km);
        itemEventRoad1.Add(item35km);
        itemEventRoad1.Add(item52km);
        itemEventRoad1.Add(item67km);


        // Initialisation de la route
        Road newRoad = new Road(depart, destination, stopRoad1, null, itemEventRoad1, questRoad1, 70);
        RoadManager.instance.SetRoad(newRoad);
    }

    public static void NextEvents(Voiture car)
    {
        switch (currentEvent)
        {
            case 0:
                Ville depart = new Ville("Montreal");
                Ville destination = new Ville("Trois-Riviere");

                // Objets des magasins
                List<ItemAVendre> itemCostco5km = new List<ItemAVendre>();
                itemCostco5km.Add(new ItemAVendre(ItemBank.GetItemByIndex(0), 5, 3));
                itemCostco5km.Add(new ItemAVendre(ItemBank.GetItemByIndex(5), 5, 3));
                itemCostco5km.Add(new ItemAVendre(ItemBank.GetItemByIndex(8), 10, 3));

                List<ItemAVendre> itemGarage21km = new List<ItemAVendre>();
                itemGarage21km.Add(new ItemAVendre(ItemBank.GetItemByIndex(8), 27, 1));
                itemGarage21km.Add(new ItemAVendre(ItemBank.GetItemByIndex(9), 21, 1));
                itemGarage21km.Add(new ItemAVendre(ItemBank.GetItemByIndex(10), 6, 5));

                List<ItemAVendre> itemStation37km = new List<ItemAVendre>();
                itemStation37km.Add(new ItemAVendre(ItemBank.GetItemByIndex(5), 8, 6));
                itemStation37km.Add(new ItemAVendre(ItemBank.GetItemByIndex(6), 5, 6));
                itemStation37km.Add(new ItemAVendre(ItemBank.GetItemByIndex(7), 4, 6));

                List<ItemAVendre> itemDepanneur56km = new List<ItemAVendre>();
                itemDepanneur56km.Add(new ItemAVendre(ItemBank.GetItemByIndex(2), 6, 3));
                itemDepanneur56km.Add(new ItemAVendre(ItemBank.GetItemByIndex(3), 4, 3));
                itemDepanneur56km.Add(new ItemAVendre(ItemBank.GetItemByIndex(4), 3, 3));

                List<ItemAVendre> itemRestaurant70km = new List<ItemAVendre>();
                itemRestaurant70km.Add(new ItemAVendre(ItemBank.GetItemByIndex(0), 7, 3));
                itemRestaurant70km.Add(new ItemAVendre(ItemBank.GetItemByIndex(1), 6, 3));

                List<ItemAVendre> itemGarage91km = new List<ItemAVendre>();
                itemGarage91km.Add(new ItemAVendre(ItemBank.GetItemByIndex(8), 28, 1));
                itemGarage91km.Add(new ItemAVendre(ItemBank.GetItemByIndex(9), 22, 1));
                itemGarage91km.Add(new ItemAVendre(ItemBank.GetItemByIndex(10), 7, 5));

                List<ItemAVendre> itemStation100km = new List<ItemAVendre>();
                itemStation100km.Add(new ItemAVendre(ItemBank.GetItemByIndex(5), 9, 6));
                itemStation100km.Add(new ItemAVendre(ItemBank.GetItemByIndex(6), 6, 6));
                itemStation100km.Add(new ItemAVendre(ItemBank.GetItemByIndex(7), 5, 6));

                List<ItemAVendre> itemRestaurant112km = new List<ItemAVendre>();
                itemRestaurant112km.Add(new ItemAVendre(ItemBank.GetItemByIndex(0), 7, 3));
                itemRestaurant112km.Add(new ItemAVendre(ItemBank.GetItemByIndex(1), 6, 3));

                List<ItemAVendre> itemCostco135km = new List<ItemAVendre>();
                itemCostco135km.Add(new ItemAVendre(ItemBank.GetItemByIndex(0), 5, 3));
                itemCostco135km.Add(new ItemAVendre(ItemBank.GetItemByIndex(5), 5, 3));
                itemCostco135km.Add(new ItemAVendre(ItemBank.GetItemByIndex(8), 10, 3));
                itemGarage91km.Add(new ItemAVendre(ItemBank.GetItemByIndex(9), 22, 1));


                // Magasins
                List<Stop> stopRoad2 = new List<Stop>();
                Stop costco5km = new Stop(5, LieuType.costco, itemCostco5km);
                Stop garage21km = new Stop(21, LieuType.garage, itemGarage21km);
                Stop station37km = new Stop(21, LieuType.stationEssence, itemStation37km);
                Stop depanneur56km = new Stop(21, LieuType.depaneur, itemDepanneur56km);
                Stop restaurant70km = new Stop(21, LieuType.restaurant, itemRestaurant70km);
                Stop garage91km = new Stop(21, LieuType.garage, itemGarage91km);
                Stop station100km = new Stop(21, LieuType.stationEssence, itemStation100km);
                Stop restaurant112km = new Stop(21, LieuType.restaurant, itemRestaurant112km);
                Stop costco135km = new Stop(21, LieuType.costco, itemCostco135km);

                stopRoad2.Add(costco5km);
                stopRoad2.Add(garage21km);
                stopRoad2.Add(station37km);
                stopRoad2.Add(depanneur56km);
                stopRoad2.Add(restaurant70km);
                stopRoad2.Add(garage91km);
                stopRoad2.Add(station100km);
                stopRoad2.Add(restaurant112km);
                stopRoad2.Add(costco135km);


                // Quetes
                Stop gary10km = new Stop(10, LieuType.arretBus);
                Stop mariepier30km = new Stop(30, LieuType.arretBus);
                Stop bob49km = new Stop(49, LieuType.arretBus);
                Stop maxime62km = new Stop(62, LieuType.arretBus);
                Stop jeremy100km = new Stop(100, LieuType.arretBus);

                stopRoad2.Add(gary10km);
                stopRoad2.Add(mariepier30km);
                stopRoad2.Add(bob49km);
                stopRoad2.Add(maxime62km);
                stopRoad2.Add(jeremy100km);

                List<Quest> questRoad1 = new List<Quest>();
                questRoad1.Add(new Quest("Déposer le mystérieux monsieur et ses nombreux chapeaux à Trois-Rivière", 0, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(destination, null)));
                questRoad1.Add(new Quest("Déposer Gary et son matériel informatique à Trois-Rivière", 10, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(destination, null)));
                questRoad1.Add(new Quest("Déposer Marie-Pier et ses bagages jusqu'à la prochaine épicerie", 30, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(null, restaurant70km)));
                questRoad1.Add(new Quest("Déposer Bob au prochain garage", 49, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(null, garage91km)));
                questRoad1.Add(new Quest("Déposer Maxime et ses meubles à Trois-Rivière", 62, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(destination,null)));
                questRoad1.Add(new Quest("Déposer Jeremy à la prochaine épicerie", 100, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(null, restaurant112km)));


                Road newRoad = new Road(depart, destination, null, null, null, null, 5);
                RoadManager.instance.SetRoad(newRoad);
                currentEvent++;


                break;
            case 1:
                Ville depart1 = new Ville("Trois-Riviere");
                Ville destination1 = new Ville("Quebec");

                // Evenement a faire...

                Road newRoad1 = new Road(depart1, destination1, null, null, null, null, 5);
                RoadManager.instance.SetRoad(newRoad1);
                currentEvent++;
                break;
            case 2:
                Ville depart2 = new Ville("Quebec");
                Ville destination2 = new Ville("Saguenay");

                // Evenement a faire...

                Road newRoad2 = new Road(depart2, destination2, null, null, null, null, 5);
                RoadManager.instance.SetRoad(newRoad2);
                currentEvent++;
                break;
            case 3:
                Ville depart3 = new Ville("Saguenay");
                Ville destination3 = new Ville("Sept-Iles");

                // Evenement a faire...

                Road newRoad3 = new Road(depart3, destination3, null, null, null, null, 5);
                RoadManager.instance.SetRoad(newRoad3);
                currentEvent++;
                break;
        }
    }
}
