using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScripting {

    public static int currentEvent = 0;
	public static void Init(Voiture car)
    {
        Ville depart = new Ville("St-Stanislas");
        Ville destination = new Ville("Montreal");

        // Evenement a faire...

        List<Stop> stopRoad1 = new List<Stop>();
        List<ItemAVendre> depanneur22km = new List<ItemAVendre>();
        List<ItemAVendre> station40km = new List<ItemAVendre>();
        List<ItemAVendre> restaurant47km = new List<ItemAVendre>();
        List<ItemAVendre> garage62km = new List<ItemAVendre>();

        List<Item> randomItemBank1 = new List<Item>();
        // randomItemBank1.Add(...);

        List<Item> randomItemBank2 = new List<Item>();
        // randomItemBank2.Add(...);

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

        QuestManager.instance.AddQuest(new Quest("Charles-Montreal"));
        QuestManager.instance.AddQuest(new Quest("Marc-Station"));
        QuestManager.instance.AddQuest(new Quest("Monique-Montreal"));

        Road newRoad = new Road(depart, destination, stopRoad1, null,itemEventRoad1,70);
        RoadManager.instance.SetRoad(newRoad);
    }

    public static void NextEvents(Voiture car)
    {
        switch (currentEvent)
        {
            case 0:
                Ville depart = new Ville("Montreal");
                Ville destination = new Ville("Trois-Riviere");

                // Evennement a faire...

                Road newRoad = new Road(depart, destination, null, null, null, 5);
                RoadManager.instance.SetRoad(newRoad);
                currentEvent++;
                break;
            case 1:
                Ville depart1 = new Ville("Trois-Riviere");
                Ville destination1 = new Ville("Quebec");

                // Evennement a faire...

                Road newRoad1 = new Road(depart1, destination1, null, null, null, 5);
                RoadManager.instance.SetRoad(newRoad1);
                currentEvent++;
                break;
            case 2:
                Ville depart2 = new Ville("Quebec");
                Ville destination2 = new Ville("Saguenay");

                // Evennement a faire...

                Road newRoad2 = new Road(depart2, destination2, null, null, null, 5);
                RoadManager.instance.SetRoad(newRoad2);
                currentEvent++;
                break;
            case 3:
                Ville depart3 = new Ville("Saguenay");
                Ville destination3 = new Ville("Sept-Iles");

                // Evennement a faire...

                Road newRoad3 = new Road(depart3, destination3, null, null, null, 5);
                RoadManager.instance.SetRoad(newRoad3);
                currentEvent++;
                break;
        }
    }
}
