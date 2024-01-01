using System;
using System.Collections.Generic;
using Player;
using Random = UnityEngine.Random;

namespace Gameplay
{
   [Serializable]
   public class Action
   {
      public string Name;
   
      public List<Event> Events;

      public int Cost;
      private PlayerStats _playerStats => Game.Instance.PlayerStats;

      public void OnActionStart()
      {
         if (_playerStats.Money >= Cost)
         {
            var random = GetRandomEvent();
            DialogUI.Instance.OpenDialog(random);
            _playerStats.ChangeStats(new PlayerStats(0,0,0,0,-Cost));
         }
         else
         {
            DialogUI.Instance.OpenNotEnoughtMoney();
         }
      }

      private Event GetRandomEvent() =>
         Events[Random.Range(0, Events.Count)];
   }
}
