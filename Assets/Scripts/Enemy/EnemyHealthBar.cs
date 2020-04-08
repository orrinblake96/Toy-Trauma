using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyHealthBar : MonoBehaviour
    {
        public Slider enemyHealthSlider;

        public void SetHealth(int currentHealth)
        {
            enemyHealthSlider.value = currentHealth;
        }
        
        public void SetMaxHealth(int maxHealth)
        {
            enemyHealthSlider.maxValue = maxHealth;
            enemyHealthSlider.value = maxHealth;
        }
    }
}
