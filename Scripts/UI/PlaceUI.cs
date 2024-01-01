using System.Collections.Generic;
using Gameplay;
using Player;
using UnityEngine;

namespace UI
{
    public class PlaceUI : MonoBehaviour
    {
        public MainPanel MainPanel;

        public GameObject ActionButtonPref;
    
        private Queue<GameObject> ButtonsPool = new ();
    
        private PlayerStats _playerStats => Game.Instance.PlayerStats;
    
        public void SetPlace(Place place)
        {
            MainPanel.SetPlacesButtonsVisible(false);
            MainPanel.SetBackground(place.Background);
            foreach (var action in place.Actions)
            {
                var button = Instantiate(ActionButtonPref, MainPanel.transform);
                ButtonsPool.Enqueue(button);
                var actionButton = button.GetComponent<ActionButton>();
                var costString = action.Cost > 0 
                    ? $" ({action.Cost}р)" 
                    : "";
                actionButton.Init(action.Name + costString, () =>
                {
                    action.OnActionStart();
                });
           
            }
            var backButton = Instantiate(ActionButtonPref, MainPanel.transform);
            ButtonsPool.Enqueue(backButton);
            backButton.GetComponent<ActionButton>().Init("Назад", () =>
            {
                HidePlace();
                MainPanel.SetPlacesButtonsVisible(true);
            });
        }

        public void HidePlace()
        {
            MainPanel.SetBackground(MainPanel.MainBackground);
            while (ButtonsPool.TryDequeue(out var button))
            {
                Destroy(button);
            }
        }
    
    }
}