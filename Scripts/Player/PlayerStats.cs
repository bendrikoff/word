using System;

namespace Player
{
    [Serializable]
    public class PlayerStats
    {
        public Action<int> OnChangeAuthority;
        public Action<int> OnChangeHealth;
        public Action<int> OnChangeFood;
        public Action<int> OnChangePolice;
        public Action<int> OnChangeMoney;
    
        public int Authority;
        public int Health;
        public int Food;
        public int Police;
        public int Money;


        public PlayerStats(int authority, int health, int food, int police, int money)
        {
            Authority = authority;
            Health = health;
            Police = police;
            Money = money;
        }

        public void ChangeStats(PlayerStats playerStats)
        {
            Authority += playerStats.Authority;
            Health += playerStats.Health;
            Food += playerStats.Food;
            Police += playerStats.Police;
            Money += playerStats.Money;
        
            InvokeActions();
        }

        private void InvokeActions()
        {
            OnChangeAuthority?.Invoke(Authority);
            OnChangeHealth?.Invoke(Health);
            OnChangeFood?.Invoke(Food);
            OnChangePolice?.Invoke(Police);
            OnChangeMoney?.Invoke(Money);
        }
    }
}
