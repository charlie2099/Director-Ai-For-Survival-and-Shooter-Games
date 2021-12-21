using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    #region Properties
        private int _health = 0;
        private int _damage = 0;
    #endregion

    #region Unity Event Functions
        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }
        
        private void Update()
        {
            
        }
    #endregion

    #region IDamageable
        public int Health
        {
            get => _health;
            set => _health = value;
        }
        
        public void Damage(int damage)
        {
            _damage = damage;
        }
    #endregion 
}
