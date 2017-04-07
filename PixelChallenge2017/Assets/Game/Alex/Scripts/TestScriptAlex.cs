using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptAlex : MonoBehaviour {

    public Quest myQuest;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            myQuest = new Quest("MOST EPIC QUEST EVER");
            QuestManager.instance.AddQuest(myQuest);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            QuestManager.instance.DeleteQuest(myQuest);
        }
    }
}
