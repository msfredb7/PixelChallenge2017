using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class GameManager : PublicSingleton<GameManager>
{

    public Voiture car;

    public float startCash;
    public float startGas;
    public float startFood;

    public float TowingCost;
    public float TowingGas;

    public Text cashText;

    public GameObject grille;

    public List<GameObject> waypoints = new List<GameObject>();

    void Start()
    {
        car = new Voiture(startCash, startGas);

        car.IsRunning = true;

        // Ajouts de la route initial
        EventScripting.Init(car);
        RoadManager.instance.onDestinationReached.AddListener(OnDestinationReached);
    }

    void Update()
    {
        // Condition de defaite ?
        //if (car.gas <= 0)
        //{
        //    if (car.cash < TowingCost)
        //    {
        //        // End of the game
        //        print("GAME OVER");
        //        car.IsRunning = false;
        //    }
        //    // Towing
        //    car.ChangeCash(-TowingCost);
        //    car.ChangeGas(TowingGas);
        //}
        cashText.text = car.cash + "$";
    }

    public void OnDestinationReached()
    {

        for (int i = 0; i < car.listSpecialItems.Count; i++)
        {
            car.ChangeCash(car.listSpecialItems[i].reward);
            car.listItems.Remove(car.listSpecialItems[i].item);
        }
        car.listSpecialItems.Clear();

        EventScripting.NextEvents(car);

        car.IsRunning = true;
    }

    public void CreateStop(LieuType lieu, List<ItemAVendre> items, UnityAction onContinue = null)
    {
        print("On arrete a " + lieu);
        // Gere le spaw du prefab stop et set toute le reste
        GlobalAnimator.StopAt(lieu, delegate ()
        {
            if (onContinue != null)
                onContinue.Invoke();
            print("On repart!");
        },
        delegate()
        {
            if(items.Count > 0)
                PewDiePieUI.instance.shop.Init(items);
        });
    }

    // Spawn des items important avec le personnage
    public void SpawnItems(List<Quest.ItemQuest> items)
    {
        Debug.Log("Spawn Item");

        int nbItems = 1;
        for (int i = 0; i < items.Count; i++)
        {
            if (i > (waypoints.Count - 1))
                return; // Fuck off
            GameObject obj = Instantiate(items[i].item.gameObject);
            obj.transform.position = waypoints[nbItems].transform.position;
            nbItems++;
        }
    }

    // Spawn un personnage
    public void SpawnPersonne(Personne personne)
    {
        GameObject obj = Instantiate(personne.gameObject);
        obj.transform.position = waypoints[0].transform.position;
    }
}
