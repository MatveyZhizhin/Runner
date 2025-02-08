using System;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace HealthOfObjects
{
    public class Health : MonoBehaviour, ITextUser
    {
        [SerializeField] protected int _startHealth;
        protected int _currentHealth;

        [SerializeField] private UnityEvent OnDeath;
        public event Action<string> Changed;

        private void Start()
        {
            AddHealth(_startHealth);
        }

        public virtual void AddHealth(int additionalHealth)
        {
            _currentHealth += additionalHealth;
            Changed?.Invoke(_currentHealth.ToString());
        }

        public virtual void TakeDamage(int damage)
        {                    
            _currentHealth -= damage; 
            
            if (_currentHealth < 0)
                _currentHealth = 0;

            Changed?.Invoke(_currentHealth.ToString());

            if (_currentHealth <= 0)
                OnDeath?.Invoke();            
        }
    }
}

