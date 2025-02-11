using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace ClasesMotores
{
    public class CanvasManagerr : MonoBehaviour
    {

        [SerializeField]
        private TMP_Text scoreText;

        [SerializeField]
        private Image healthBar;

        public TMP_Text ScoreText { get => scoreText;}
        public Image HealthBar { get => healthBar;}

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

