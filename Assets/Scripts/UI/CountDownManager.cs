using System;
using TMPro;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace UI
{
    public class CountDownManager : MonoBehaviour
    {
        public GameObject panelCountDown;
        public TextMeshProUGUI countdownText;
        public float countdownDuration = 5.0f;

        private void Start()
        {
            countdownText.text = "";
        }
        
        public void StartCountDown()
        {
            panelCountDown.SetActive(true);
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            for (int i = (int)countdownDuration; i >= 0; i--)
            {
                this.countdownText.text= i.ToString();
                //string countdownText = i == 0 ? "Go!" : i.ToString();
                yield return new WaitForSeconds(1.0f);
            }
            panelCountDown.SetActive(false);
        }
    }
}
