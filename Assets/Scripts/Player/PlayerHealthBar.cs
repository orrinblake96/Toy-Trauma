using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealthBar : MonoBehaviour
    {
        public Slider playerHealthSlider;
        public Gradient healthBarGradient;
        public Image fill;

        public void SetHealth(int currentHealth)
        {
            playerHealthSlider.value = currentHealth;
            fill.color = healthBarGradient.Evaluate(playerHealthSlider.normalizedValue);
        }

        public void SetMaxHealth(int maxHealth)
        {
            playerHealthSlider.maxValue = maxHealth;
            playerHealthSlider.value = maxHealth;
        }
    }
}
