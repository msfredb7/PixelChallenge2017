using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Debug.Log("Initialisation des evenements");
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

        // Position vehicule initiale

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
        List<Quest.ItemQuest> questItemCharles10km = new List<Quest.ItemQuest>();
        questItemCharles10km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(14), 5));
        questItemCharles10km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(16), 8));
        questItemCharles10km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(17), 8));

        stopRoad1.Add(marc17km);
        stopRoad1.Add(monique57km);

        List<Quest> questRoad1 = new List<Quest>();
        List<Quest.ItemQuest> questItemFIRSTS = new List<Quest.ItemQuest>();
        foreach (Item item in GameManager.instance.startItems)
        {
            questItemFIRSTS.Add(new Quest.ItemQuest(item, 5));
        }
        Quest firstQuest = new Quest("Deposer Marise et ses valises a Montreal", 0, 0, GameManager.instance.startPerson, new Quest.Destination(destination, null), questItemFIRSTS);
        firstQuest.spawnPerson = false;
        firstQuest.items = GameManager.instance.startItems;
        questRoad1.Add(firstQuest);
        questRoad1.Add(new Quest("Deposer Charles et son equipement de hockey a Montreal", 10, 0, PersonneBank.GetItemByIndex(5), new Quest.Destination(destination, null), questItemCharles10km));
        questRoad1.Add(new Quest("Deposer Marc a la prochaine station service", 17, 5, PersonneBank.GetItemByIndex(1), new Quest.Destination(null, station40km)));
        questRoad1.Add(new Quest("Deposer Monique a Montreal", 57, 5, PersonneBank.GetItemByIndex(3), new Quest.Destination(destination, null)));


        // Loteries
        List<ItemEvent> itemEventRoad1 = new List<ItemEvent>();
        ItemEvent item5km = new ItemEvent(5, 0, (Item)lotteryFood.Pick());
        ItemEvent item29km = new ItemEvent(29, 0, (Item)lotteryFood.Pick());
        ItemEvent item35km = new ItemEvent(35, 0, (Item)lotteryEssence.Pick());
        ItemEvent item52km = new ItemEvent(52, 0, (Item)lotteryUtility.Pick());
        ItemEvent item67km = new ItemEvent(67, 10, (Item)lotteryCollectable.Pick());

        itemEventRoad1.Add(item5km);
        itemEventRoad1.Add(item29km);
        itemEventRoad1.Add(item35km);
        itemEventRoad1.Add(item52km);
        itemEventRoad1.Add(item67km);

        List<SpecialEvent> specialEventList = new List<SpecialEvent>();

        // Evennement de Bulle
        specialEventList.Add(new SpecialEvent(2, delegate ()
        {
            GameObject obj = GameObject.Instantiate(GameManager.instance.bulle, GameManager.instance.canvasUI.transform);
            obj.transform.position = GameManager.instance.conducteurUI.transform.position;
            foreach (Transform child in obj.transform)
            {
                child.GetComponent<Text>().text = "T'avais-tu vraiment besoin d'ammener 12 valises pour un voyage de 1 semaine Marise?";
            }
            DelayManager.CallTo(delegate ()
            {
                GameObject.Destroy(obj);
            }, 5);
        }));

        specialEventList.Add(new SpecialEvent(9, delegate ()
        {
            GameObject obj = GameObject.Instantiate(GameManager.instance.bulle, GameManager.instance.canvasUI.transform);
            obj.transform.position = GameManager.instance.conducteurUI.transform.position;
            foreach (Transform child in obj.transform)
            {
                child.GetComponent<Text>().text = "On devrait l'embarquer pour economiser, quitte a se debarasser de quelques objets...";
            }
            DelayManager.CallTo(delegate ()
            {
                GameObject.Destroy(obj);
            }, 5);
        }));

        specialEventList.Add(new SpecialEvent(20, delegate ()
        {
            GameObject obj = GameObject.Instantiate(GameManager.instance.bulle, GameManager.instance.canvasUI.transform);
            obj.transform.position = GameManager.instance.conducteurUI.transform.position;
            foreach (Transform child in obj.transform)
            {
                child.GetComponent<Text>().text = "Nice un depanneur, on va pouvoir acheter quelques provisions";
            }
            DelayManager.CallTo(delegate ()
            {
                GameObject.Destroy(obj);
            }, 5);
        }));

        specialEventList.Add(new SpecialEvent(30, delegate ()
        {
            GameObject obj = GameObject.Instantiate(GameManager.instance.bulle, GameManager.instance.canvasUI.transform);
            obj.transform.position = GameManager.instance.conducteurUI.transform.position;
            foreach (Transform child in obj.transform)
            {
                child.GetComponent<Text>().text = "Tu peux changer de poste de radio, genes toi pas !";
            }
            DelayManager.CallTo(delegate ()
            {
                GameObject.Destroy(obj);
            }, 5);
        }));

        specialEventList.Add(new SpecialEvent(40, delegate ()
        {
            GameObject obj = GameObject.Instantiate(GameManager.instance.bulle, GameManager.instance.canvasUI.transform);
            obj.transform.position = GameManager.instance.conducteurUI.transform.position;
            foreach (Transform child in obj.transform)
            {
                child.GetComponent<Text>().text = "On devrait peut-etre acheter quelques barils de plus pour eviter d'en manquer";
            }
            DelayManager.CallTo(delegate ()
            {
                GameObject.Destroy(obj);
            }, 5);
        }));

        specialEventList.Add(new SpecialEvent(45, delegate ()
        {
            GameObject obj = GameObject.Instantiate(GameManager.instance.bulle, GameManager.instance.canvasUI.transform);
            obj.transform.position = GameManager.instance.conducteurUI.transform.position;
            foreach (Transform child in obj.transform)
            {
                child.GetComponent<Text>().text = "Une epicerie, c'est le moment d'acheter de plus grandes quantitees de nourriture";
            }
            DelayManager.CallTo(delegate ()
            {
                GameObject.Destroy(obj);
            }, 5);
        }));

        specialEventList.Add(new SpecialEvent(60, delegate ()
        {
            GameObject obj = GameObject.Instantiate(GameManager.instance.bulle, GameManager.instance.canvasUI.transform);
            obj.transform.position = GameManager.instance.conducteurUI.transform.position;
            foreach (Transform child in obj.transform)
            {
                child.GetComponent<Text>().text = "Enfin un garage, on va pouvoir acheter des pieces de rechange";
            }
            DelayManager.CallTo(delegate ()
            {
                GameObject.Destroy(obj);
            }, 5);
        }));

        float eventCountdown = 0;
        int IhateWhiletrue = 0;
        while (eventCountdown < 150 && IhateWhiletrue < 1000)
        {
            Random.InitState((int)eventCountdown);

            eventCountdown += Random.Range(25.00f, 50.00f);
            int evenType = Random.Range(0, 1);

            specialEventList.Add(new SpecialEvent(eventCountdown, delegate ()
            {
                if (evenType == 0)
                    AccidentManager.instance.flatEvent();
                else
                    AccidentManager.instance.PanneMoteur();
            }));
            IhateWhiletrue++;
        }
   

        // Initialisation de la route
        Road newRoad1 = new Road(depart, destination, stopRoad1, specialEventList, itemEventRoad1, questRoad1, 70);
        RoadManager.instance.SetRoad(newRoad1);
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
                Stop monsieur5km = new Stop(1, LieuType.arretBus);
                Stop gary10km = new Stop(10, LieuType.arretBus);
                Stop mariepier30km = new Stop(30, LieuType.arretBus);
                Stop bob49km = new Stop(49, LieuType.arretBus);
                Stop maxime62km = new Stop(62, LieuType.arretBus);
                Stop jeremy100km = new Stop(100, LieuType.arretBus);


                stopRoad2.Add(monsieur5km);
                List<Quest.ItemQuest> questItemMonsieur5km= new List<Quest.ItemQuest>();
                questItemMonsieur5km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(13), 4));
                questItemMonsieur5km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(13), 4));
                questItemMonsieur5km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(13), 4));
                questItemMonsieur5km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(13), 4));
                questItemMonsieur5km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(13), 4));

                stopRoad2.Add(gary10km);
                List<Quest.ItemQuest> questItemGary10km = new List<Quest.ItemQuest>();
                questItemGary10km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(22), 20));
                questItemGary10km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(23), 10));

                stopRoad2.Add(mariepier30km);
                List<Quest.ItemQuest> questItemMariePier30km = new List<Quest.ItemQuest>();
                questItemGary10km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(15), 8));
                questItemGary10km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(15), 8));

                stopRoad2.Add(bob49km);

                stopRoad2.Add(maxime62km);
                List<Quest.ItemQuest> questItemMaxime62km = new List<Quest.ItemQuest>();
                questItemGary10km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(18), 10));
                questItemGary10km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(18), 10));
                questItemGary10km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(21), 15));

                stopRoad2.Add(jeremy100km);

                List<Quest> questRoad2 = new List<Quest>();
                questRoad2.Add(new Quest("Deposer le mysterieux monsieur et ses nombreux chapeaux a Trois-Riviere", 1, 10, PersonneBank.GetItemByIndex(2), new Quest.Destination(destination, null),questItemMonsieur5km));
                questRoad2.Add(new Quest("Deposer Justine et son materiel informatique a Trois-Riviere", 10, 0, PersonneBank.GetItemByIndex(3), new Quest.Destination(destination, null), questItemGary10km));
                questRoad2.Add(new Quest("Deposer Annushka et ses bagages jusqu'a la prochaine epicerie", 30, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(null, restaurant70km)));
                questRoad2.Add(new Quest("Deposer Bob au prochain garage", 49, 0, PersonneBank.GetItemByIndex(5), new Quest.Destination(null, garage91km)));
                questRoad2.Add(new Quest("Deposer Maxime et ses meubles a Trois-Riviere", 62, 0, PersonneBank.GetItemByIndex(5), new Quest.Destination(destination,null)));
                questRoad2.Add(new Quest("Deposer Jeremy a la prochaine epicerie", 100, 0, PersonneBank.GetItemByIndex(5), new Quest.Destination(null, restaurant112km)));


                // Loteries
                List<ItemEvent> itemEventRoad2 = new List<ItemEvent>();
                ItemEvent item14km = new ItemEvent(14, 0, (Item)lotteryFood.Pick());
                ItemEvent item25km = new ItemEvent(25, 10, (Item)lotteryCollectable.Pick());
                ItemEvent item52km = new ItemEvent(42, 0, (Item)lotteryEssence.Pick());
                ItemEvent item74km = new ItemEvent(74, 10, (Item)lotteryCollectable.Pick());
                ItemEvent item86km = new ItemEvent(86, 0, (Item)lotteryFood.Pick());
                ItemEvent item95km = new ItemEvent(95, 0, (Item)lotteryUtility.Pick());
                ItemEvent item106km = new ItemEvent(106, 0, (Item)lotteryEssence.Pick());

                itemEventRoad2.Add(item14km);
                itemEventRoad2.Add(item25km);
                itemEventRoad2.Add(item52km);
                itemEventRoad2.Add(item74km);
                itemEventRoad2.Add(item86km);
                itemEventRoad2.Add(item95km);
                itemEventRoad2.Add(item106km);

                Road newRoad2 = new Road(depart, destination, stopRoad2, null, itemEventRoad2, questRoad2, 139);
                RoadManager.instance.SetRoad(newRoad2);
                currentEvent++;


                break;
            case 1:
                Ville depart1 = new Ville("Trois-Riviere");
                Ville destination1 = new Ville("Quebec");
                // 129 km


                // Objets des magasins
                List<ItemAVendre> itemDepanneur12km = new List<ItemAVendre>();
                itemDepanneur12km.Add(new ItemAVendre(ItemBank.GetItemByIndex(4), 1, 9));
                itemDepanneur12km.Add(new ItemAVendre(ItemBank.GetItemByIndex(4), 1, 9));
                itemDepanneur12km.Add(new ItemAVendre(ItemBank.GetItemByIndex(4), 1, 9));

                List<ItemAVendre> itemDepanneur30km = new List<ItemAVendre>();
                itemDepanneur30km.Add(new ItemAVendre(ItemBank.GetItemByIndex(4), 1, 9));
                itemDepanneur30km.Add(new ItemAVendre(ItemBank.GetItemByIndex(4), 1, 9));
                itemDepanneur30km.Add(new ItemAVendre(ItemBank.GetItemByIndex(4), 1, 9));

                List<ItemAVendre> itemRestaurant50km = new List<ItemAVendre>();
                itemRestaurant50km.Add(new ItemAVendre(ItemBank.GetItemByIndex(0), 8, 5));
                itemRestaurant50km.Add(new ItemAVendre(ItemBank.GetItemByIndex(1), 7, 0));

                List<ItemAVendre> itemStation75km = new List<ItemAVendre>();
                itemStation75km.Add(new ItemAVendre(ItemBank.GetItemByIndex(5), 10, 3));
                itemStation75km.Add(new ItemAVendre(ItemBank.GetItemByIndex(6), 10, 2));
                itemStation75km.Add(new ItemAVendre(ItemBank.GetItemByIndex(7), 10, 1));

                List<ItemAVendre> itemGarage80km = new List<ItemAVendre>();
                itemGarage80km.Add(new ItemAVendre(ItemBank.GetItemByIndex(8), 20, 1));
                itemGarage80km.Add(new ItemAVendre(ItemBank.GetItemByIndex(9), 20, 1));
                itemGarage80km.Add(new ItemAVendre(ItemBank.GetItemByIndex(10), 7, 3));

                List<ItemAVendre> itemDepanneur100km = new List<ItemAVendre>();
                itemDepanneur100km.Add(new ItemAVendre(ItemBank.GetItemByIndex(2), 4, 5));
                itemDepanneur100km.Add(new ItemAVendre(ItemBank.GetItemByIndex(3), 4, 5));
                itemDepanneur100km.Add(new ItemAVendre(ItemBank.GetItemByIndex(4), 5, 5));

                List<ItemAVendre> itemCostco119km = new List<ItemAVendre>();
                itemCostco119km.Add(new ItemAVendre(ItemBank.GetItemByIndex(0), 2, 3));
                itemCostco119km.Add(new ItemAVendre(ItemBank.GetItemByIndex(5), 2, 3));
                itemCostco119km.Add(new ItemAVendre(ItemBank.GetItemByIndex(8), 10, 1));
                itemCostco119km.Add(new ItemAVendre(ItemBank.GetItemByIndex(9), 10, 1));

                List<ItemAVendre> itemStation123km = new List<ItemAVendre>();
                itemStation123km.Add(new ItemAVendre(ItemBank.GetItemByIndex(5), 15, 0));
                itemStation123km.Add(new ItemAVendre(ItemBank.GetItemByIndex(6), 15, 0));
                itemStation123km.Add(new ItemAVendre(ItemBank.GetItemByIndex(7), 15, 3));


                // Magasins
                List<Stop> stopRoad3 = new List<Stop>();
                Stop depanneur12km = new Stop(12, LieuType.depaneur, itemDepanneur12km);
                Stop depanneur30km = new Stop(30, LieuType.depaneur, itemDepanneur30km);
                Stop restaurant50km = new Stop(50, LieuType.restaurant, itemRestaurant50km);
                Stop station75km = new Stop(75, LieuType.stationEssence, itemStation75km);
                Stop garage80km = new Stop(80, LieuType.garage, itemGarage80km);
                Stop depanneur100km = new Stop(100, LieuType.depaneur, itemDepanneur100km);
                Stop costco119km = new Stop(119, LieuType.costco, itemCostco119km);
                Stop station123km = new Stop(123, LieuType.stationEssence, itemStation123km);

                stopRoad3.Add(depanneur12km);
                stopRoad3.Add(depanneur30km);
                stopRoad3.Add(restaurant50km);
                stopRoad3.Add(station75km);
                stopRoad3.Add(garage80km);
                stopRoad3.Add(depanneur100km);
                stopRoad3.Add(costco119km);
                stopRoad3.Add(station123km);


                // Quetes
                Stop ami5km = new Stop(5, LieuType.arretBus);
                Stop marc15km = new Stop(15, LieuType.arretBus);
                Stop marcos23km = new Stop(23, LieuType.arretBus);
                Stop marcus40km = new Stop(40, LieuType.arretBus);
                Stop marco45km = new Stop(45, LieuType.arretBus);
                Stop marcandre67km = new Stop(67, LieuType.arretBus);
                Stop mohamed90km = new Stop(90, LieuType.arretBus);
                Stop natasha110km = new Stop(110, LieuType.arretBus);

                stopRoad3.Add(marc15km);
                List<Quest.ItemQuest> questItemAmi5km = new List<Quest.ItemQuest>();
                questItemAmi5km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(19), 20));
                questItemAmi5km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(20), 20));


                stopRoad3.Add(marcos23km);
                List<Quest.ItemQuest> questItemMarcos23km = new List<Quest.ItemQuest>();
                questItemMarcos23km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(14), 5));
                questItemMarcos23km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(16), 8));
                questItemMarcos23km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(17), 8));

                stopRoad3.Add(marcus40km);
                List<Quest.ItemQuest> questItemMarcus40km = new List<Quest.ItemQuest>();
                questItemMarcus40km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(14), 5));
                questItemMarcus40km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(16), 8));
                questItemMarcus40km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(17), 8));

                stopRoad3.Add(marco45km);
                List<Quest.ItemQuest> questItemMarcos45km = new List<Quest.ItemQuest>();
                questItemMarcos45km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(14), 5));
                questItemMarcos45km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(16), 8));
                questItemMarcos45km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(17), 8));

                stopRoad3.Add(marcandre67km);
                List<Quest.ItemQuest> questItemMarcAndre67km = new List<Quest.ItemQuest>();
                questItemMarcAndre67km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(14), 5));
                questItemMarcAndre67km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(16), 8));
                questItemMarcAndre67km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(17), 8));

                stopRoad3.Add(mohamed90km);
                List<Quest.ItemQuest> questItemMohamed90km = new List<Quest.ItemQuest>();
                questItemMohamed90km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(22), 20));
                questItemMohamed90km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(23), 10));


                stopRoad3.Add(natasha110km);
                List<Quest.ItemQuest> questItemNatasha110km= new List<Quest.ItemQuest>();
                questItemNatasha110km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(15), 10));
                questItemNatasha110km.Add(new Quest.ItemQuest(ItemBank.GetItemByIndex(15), 10));


                List<Quest> questRoad3 = new List<Quest>();
                questRoad3.Add(new Quest("Deposer votre viel ami et ses meubles a Quebec", 5, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(destination1, null)));
                questRoad3.Add(new Quest("Deposer Marc et son equipement de hockey au prochain garage ", 15, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(null, garage80km)));
                questRoad3.Add(new Quest("Deposer Marcos et son equipement de hockey au prochain garage ", 23, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(null, garage80km)));
                questRoad3.Add(new Quest("Deposer Marcus et son equipement de hockey au prochain garage ", 40, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(null, garage80km)));
                questRoad3.Add(new Quest("Deposer Marco et son equipement de hockey au prochain garage ", 45, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(null, garage80km)));
                questRoad3.Add(new Quest("Deposer Marc-Andre et son equipement de hockey au prochain garage ", 67, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(null, garage80km)));
                questRoad3.Add(new Quest("Deposer Mohamed et son equipement equipement a Quebec ", 90, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(destination1,null)));
                questRoad3.Add(new Quest("Deposer Natasha et ses valises a Quebec ", 110, 0, PersonneBank.GetItemByIndex(0), new Quest.Destination(destination1, null)));


                // Loteries
                List<ItemEvent> itemEventRoad3 = new List<ItemEvent>();
                ItemEvent item6km = new ItemEvent(6, 0, (Item)lotteryFood.Pick());
                ItemEvent item18km = new ItemEvent(18, 0, (Item)lotteryEssence.Pick());
                ItemEvent item27km = new ItemEvent(27, 0, (Item)lotteryFood.Pick());
                ItemEvent item35km = new ItemEvent(35, 0, (Item)lotteryFood.Pick());
                ItemEvent item56km = new ItemEvent(56, 10, (Item)lotteryCollectable.Pick());
                ItemEvent item61km = new ItemEvent(61, 0, (Item)lotteryFood.Pick());
                ItemEvent item71km = new ItemEvent(71, 0, (Item)lotteryUtility.Pick());
                ItemEvent item75km = new ItemEvent(75, 0, (Item)lotteryEssence.Pick());
                ItemEvent item85km = new ItemEvent(85, 10, (Item)lotteryCollectable.Pick());
                ItemEvent item96km = new ItemEvent(96, 0, (Item)lotteryUtility.Pick());
                ItemEvent item107km = new ItemEvent(107, 0, (Item)lotteryFood.Pick());
                ItemEvent item115km = new ItemEvent(115, 0, (Item)lotteryEssence.Pick());

                itemEventRoad3.Add(item6km);
                itemEventRoad3.Add(item18km);
                itemEventRoad3.Add(item27km);
                itemEventRoad3.Add(item35km);
                itemEventRoad3.Add(item56km);
                itemEventRoad3.Add(item61km);
                itemEventRoad3.Add(item71km);
                itemEventRoad3.Add(item75km);
                itemEventRoad3.Add(item85km);
                itemEventRoad3.Add(item96km);
                itemEventRoad3.Add(item107km);
                itemEventRoad3.Add(item115km);


                Road newRoad3 = new Road(depart1, destination1, stopRoad3, null, itemEventRoad3, questRoad3, 129);
                RoadManager.instance.SetRoad(newRoad3);
                currentEvent++;
                break;


            case 2:
                Ville depart2 = new Ville("Quebec");
                Ville destination2 = new Ville("Saguenay");

                // Evenement a faire...

                Road newRoad4 = new Road(depart2, destination2, null, null, null, null, 5);
                RoadManager.instance.SetRoad(newRoad4);
                currentEvent++;
                break;


            case 3:
                Ville depart3 = new Ville("Saguenay");
                Ville destination3 = new Ville("Sept-Iles");

                // Evenement a faire...

                Road newRoad5 = new Road(depart3, destination3, null, null, null, null, 5);
                RoadManager.instance.SetRoad(newRoad5);
                currentEvent++;
                break;
        }
    }
}
