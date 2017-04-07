using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScripting {

    CCC.Utility.RandomList<Item> randomItemFood = new CCC.Utility.RandomList<Item>(new List<Item>()
    {
        ItemBank.GetItemByIndex(2),
    });

    List<Item> randomItemBank2 = new List<Item>();
    // randomItemBank2.Add(...);

    List<Item> randomItemBank3 = new List<Item>();
    // randomItemBank3.Add(...);

    public static int currentEvent = 0;
	public static void Init(Voiture car)
    {
        Ville depart = new Ville("St-Stanislas");
        Ville destination = new Ville("Montreal");

        // Evenement a faire...

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

        List<Stop> stopRoad1 = new List<Stop>();
        Stop depanneur22km = new Stop(22, LieuType.depaneur, itemDepanneur22km);
        Stop station40km = new Stop(40, LieuType.stationEssence, itemStation40km);
        Stop restaurant47km = new Stop(47, LieuType.restaurant, itemRestaurant47km);
        Stop garage62km = new Stop(62, LieuType.garage, itemGarage62km);

        stopRoad1.Add(depanneur22km);
        stopRoad1.Add(station40km);
        stopRoad1.Add(restaurant47km);
        stopRoad1.Add(garage62km);

        List<ItemEvent> itemEventRoad1 = new List<ItemEvent>();
        // ItemEvent item29km = new ItemEvent(29)
        // ItemEvent item35km = new ItemEvent(35)
        // ItemEvent item52km = new ItemEvent(52)
        // ItemEvent item67km = new ItemEvent(67)

        // itemEventRoad1.Add(item29km);
        // itemEventRoad1.Add(item35km);
        // itemEventRoad1.Add(item52km);
        // itemEventRoad1.Add(item67km);

        // depanneur22km.Add(new ItemAVendre, )
        // stopRoad1.Add(new Stop(22,));

        List<Quest> questRoad1 = new List<Quest>();
        questRoad1.Add(new Quest("Charles-Montreal", 10, new Quest.Destination(destination,null)));
        questRoad1.Add(new Quest("Marc-Station", 17, new Quest.Destination(null,station40km)));
        questRoad1.Add(new Quest("Monique-Montreal", 57, new Quest.Destination(destination, null)));

        Road newRoad = new Road(depart, destination, stopRoad1, null,itemEventRoad1,questRoad1,70);
        RoadManager.instance.SetRoad(newRoad);
    }

    public static void NextEvents(Voiture car)
    {
        switch (currentEvent)
        {
            case 0:
                Ville depart = new Ville("Montreal");
                Ville destination = new Ville("Trois-Riviere");

                // Evenement a faire...

                Road newRoad = new Road(depart, destination, null, null, null,null, 5);
                RoadManager.instance.SetRoad(newRoad);
                currentEvent++;
                break;
            case 1:
                Ville depart1 = new Ville("Trois-Riviere");
                Ville destination1 = new Ville("Quebec");

                // Evenement a faire...

                Road newRoad1 = new Road(depart1, destination1, null, null, null,null, 5);
                RoadManager.instance.SetRoad(newRoad1);
                currentEvent++;
                break;
            case 2:
                Ville depart2 = new Ville("Quebec");
                Ville destination2 = new Ville("Saguenay");

                // Evenement a faire...

                Road newRoad2 = new Road(depart2, destination2, null, null, null,null, 5);
                RoadManager.instance.SetRoad(newRoad2);
                currentEvent++;
                break;
            case 3:
                Ville depart3 = new Ville("Saguenay");
                Ville destination3 = new Ville("Sept-Iles");

                // Evenement a faire...

                Road newRoad3 = new Road(depart3, destination3, null, null, null,null, 5);
                RoadManager.instance.SetRoad(newRoad3);
                currentEvent++;
                break;
        }
    }
}
