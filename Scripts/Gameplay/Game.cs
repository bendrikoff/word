using Player;

namespace Gameplay
{
    public class Game : Singleton<Game>
    {
        public PlayerStats PlayerStats;

        private void Start()
        {
            PlayerStats.ChangeStats(new PlayerStats(0,0,0,0,15));
        }
    }
}
