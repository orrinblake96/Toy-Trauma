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
        }
    }
}
