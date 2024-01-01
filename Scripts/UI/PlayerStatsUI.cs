using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerStatsUI : MonoBehaviour
    {
        public TextMeshProUGUI Authority;
        public TextMeshProUGUI Health;
        public TextMeshProUGUI Food;
        public TextMeshProUGUI Police;
        public TextMeshProUGUI Money;
        public TextMeshProUGUI Status;

        public Sprite FullStar;
        public Sprite HalfStar;
    
        public List<Image> Stars;

        public Image HealthImage;
        public Image FoodImage;
    
        public List<Status> StatusList = new();
        private void Start()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            Game.Instance.PlayerStats.OnChangeAuthority += AuthorityChange;
            Game.Instance.PlayerStats.OnChangeHealth += HealthChange;
            Game.Instance.PlayerStats.OnChangeFood += FoodChange;
            Game.Instance.PlayerStats.OnChangePolice += PoliceChange;
            Game.Instance.PlayerStats.OnChangeMoney += 
                (value) => Money.text = value + "р";
        }

        private void AuthorityChange(int value)
        {
            Status.text = StatusList.Last(x => value >= x.MaxAuthority).Name;
            Authority.text = value.ToString();
        }
    
        private void PoliceChange(int value)
        {
            if (value >= 10)
            {
                DialogUI.Instance.OpenLoose("Ты проиграл. Тебя посадили в тюрьму.");
                return;
            }
            ReloadStars();
            Police.text = "Розыск: "+value + "/10";
            var starsCount = (int)Math.Ceiling(value / 2.0f);
            for (int i = 0; i < starsCount; i++)
            {
                Stars[i].gameObject.SetActive(true);
            }
            Stars[starsCount].sprite = value % 2 == 0 
                ? FullStar 
                : HalfStar;
        }

        private void ReloadStars()
        {
            foreach (var star in Stars)
            {
                star.gameObject.SetActive(false);
                star.sprite = FullStar;
            }
        }

        private void HealthChange(int value)
        {
            if (value <= 0)
            {
                DialogUI.Instance.OpenLoose("Ты проиграл. Не хватило здоровья.");
                return;
            }
            Health.text = "Здоровье: " + value;
            HealthImage.fillAmount = (float)value / 10;
        }
    
        private void FoodChange(int value)
        {
            if (value <= 0)
            {
                DialogUI.Instance.OpenLoose("Ты проиграл. Умер от голода.");
                return;
            }
            Food.text = "Сытость: " + value;
            FoodImage.fillAmount = (float)value / 10;
        }
    }
}
