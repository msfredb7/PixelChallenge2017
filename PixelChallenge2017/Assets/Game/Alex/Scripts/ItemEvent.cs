using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvent {

    public float distance;

    public Item item;

    public float reward;

    private Quest.ItemQuest questItem = null;

    public ItemEvent(float distance, float reward, Item item)
    {
        this.distance = distance;
        this.item = item;
        this.reward = reward;
    }

    public void StartEvent()
    {
        Debug.Log("Un item apparait sur le bord de la route");

        if (this.item == null)
            return;

        item = GameObject.Instantiate(item.gameObject).GetComponent<Item>();

        //obj.transform.localScale = new Vector3(obj.transform.localScale.x*5, obj.transform.localScale.y, obj.transform.localScale.z*5);

        item.gameObject.transform.localPosition = new Vector3(110, 0, Random.Range(-65, -20));

        GlobalAnimator.AddFloatingItem(item.gameObject);
        
        item.onDeath.AddListener(RemoveListeners);
        item.onEnterCar.AddListener(OnEnterCar);
        item.onExitCar.AddListener(OnExitCar);
    }

    void RemoveListeners()
    {
        item.onEnterCar.RemoveListener(OnEnterCar);
        item.onExitCar.RemoveListener(OnExitCar);
    }

    void OnEnterCar()
    {
        if(questItem == null)
            questItem = new Quest.ItemQuest(item, reward);
        GameManager.instance.car.listSpecialItems.Add(questItem);
    }
    void OnExitCar()
    {
        GameManager.instance.car.listSpecialItems.Remove(questItem);
    }
}
