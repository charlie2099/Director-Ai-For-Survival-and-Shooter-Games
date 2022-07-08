using AiDirector;

namespace Rules
{
    public class KillsAtADistanceRule : IDirectorIntensityRule
    {
        private readonly int _kills;
        private readonly float _distance;
        private readonly float _intensity;

        public KillsAtADistanceRule(int kills, float distance, float intensity)
        {
            _kills = kills;
            _distance = distance;
            _intensity = intensity;
        }

        public float CalculatePerceivedIntensity(Player player, Director director)
        {
            /*if (player.killsAtADistance(_distance) > _kills)
            {
                return director.IncreaseIntensity(_intensity);
            }*/
            float intensity = 0;
            return intensity;
        }
    }
}