using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;

        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            score = 0;
        }

        private void Update()
        {
            _text.text = score.ToString();

            if (score >= 1000 && score <= 2000)
            {
                _text.color = Color.Lerp(Color.white, Color.yellow, 5f);
            } else if (score > 2000)
            {
                _text.color = Color.Lerp(Color.yellow, Color.green, 5f);
            }
        }
    }
}
