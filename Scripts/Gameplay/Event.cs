using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using Player;
using UnityEngine;

[Serializable]
public class Event
{
    public PlayerStats PlayerStats;
    
    [TextArea]
    public string Text;

    public List<Choice> Choices;
    
    private PlayerStats _playerStats => Game.Instance.PlayerStats;

    public void OnEventStart()
    {
        if (_playerStats.Money > -PlayerStats.Money) 
        {
            DialogUI.Instance.OpenDialog(this);
            Game.Instance.PlayerStats.ChangeStats(PlayerStats);
        }
        else
        {
            DialogUI.Instance.OpenNotEnoughtMoney();
        }
    }
}
