using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : PublicSingleton<QuestManager>
{
    public void flatEvent()
    {
        int toolPos = -1;
        for (int i = 0; i < GameManager.instance.car.listItems.Count; i++)
        {
            if (GameManager.instance.car.listItems[i].nom == "Roue de secours")
                toolPos = i;
        }





    }


    public void useTool(int toolPos)
    {
        GameObject obj = GameManager.instance.car.listItems[toolPos].gameObject;
        GameManager.instance.car.listItems.RemoveAt(toolPos);
        Destroy(obj);
    }

    public void messageFlat()
    {


    }


    public void choixRemorquage()
    {


    }

}

