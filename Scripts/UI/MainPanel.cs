using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainPanel: MonoBehaviour
    {
        public List<Button> PlacesButton;
    
        public Image Background;

        public Sprite MainBackground;

        public void SetPlacesButtonsVisible(bool show) =>
            PlacesButton.ForEach(x => x.gameObject.SetActive(show));

        public void SetBackground(Sprite sprite) =>
            Background.sprite = sprite;

    }
}