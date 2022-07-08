using UnityEngine;

namespace AiDirector
{
    public class PlayerTemplate : MonoBehaviour
    {
        private int _health;
        private int _kills;
        
        public float DistanceFrom(Transform target)
        {
            return Vector2.Distance(transform.position, target.position);
        }

        public int GetHealth()
        {
            return _health;
        }

        public int GetKills()
        {
            return _kills;
        }
    }
}