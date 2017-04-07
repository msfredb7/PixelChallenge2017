using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvent {

    public float distance;

    public Item item;

    public float reward;

    public ItemEvent(float distance, float reward, Item item)
    {
        this.distance = distance;
        this.item = item;
        this.reward = reward;
    }

    public void StartEvent()
    {
        Debug.Log("Un item apparait sur le bord de la route");

        if (item == null)
            return;

        GameObject obj = GameObject.Instantiate(item.gameObject);

        //obj.transform.localScale = new Vector3(obj.transform.localScale.x*5, obj.transform.localScale.y, obj.transform.localScale.z*5);

        obj.gameObject.transform.localPosition = new Vector3(110, 0, Random.Range(-80, -20));

        GlobalAnimator.AddFloatingItem(obj);

        DelayManager.CallTo(AfterEvent, 3);
    }

    public void AfterEvent()
    {
        // Apres l'event
        if (reward > 0) // Si l'objet pick up est sense etre un objet recompense quand on arrive a la ville
        {
            for (int i = 0; i < GameManager.instance.car.listItems.Count; i++) // on trouve
            {
                if (GameManager.instance.car.listItems[i] == item) // l'objet qui a ete pick up
                    GameManager.instance.car.listSpecialItems.Add(new Quest.ItemQuest(item, reward)); // et on le met dans la liste d'objet reward
            }
        }
    }
}
