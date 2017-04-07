using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecialEvent {

    public float distance;

    public UnityAction specialEvent;

    public SpecialEvent(float distance, UnityAction specialEvent)
    {
        this.distance = distance;
        this.specialEvent = specialEvent;
    }

    public void StartEvent()
    {
        specialEvent.Invoke();
    }
}
