using UnityEngine;

namespace AiDirector
{
    /*
     * [Info]
     * This class is a template for a player class that defines some common methods
     * that a player class may have. 
     * The Director's rule based system accesses these methods to help designers create rules.
     *
     * [Note]
     * If you want to use your own player class, pass it into the inspector and make sure to
     * refactor the PlayerTemplate method to pass in your own class.
     */
    
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