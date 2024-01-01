using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[Serializable]
public class Choice
{
    public string Name;

    public List<Event> PossibleEvents = new ();

    public UnityEvent Action;

    public Event GetEvent()
    {
        if (PossibleEvents.Count == 0)
            return null;

        var random = Random.Range(0, PossibleEvents.Count);
        return PossibleEvents[random];
    }
    public Choice(string name,UnityEvent action)
    {
        Name = name;
        Action = action;
    }
}
