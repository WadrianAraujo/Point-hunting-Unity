using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Network;
using Utils;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private Button startServerButton;
        [SerializeField] private Button startHostButton;
        [SerializeField] private Button startClientButton;
        [SerializeField] private TextMeshProUGUI playersIngameText;
        [SerializeField] private CountDownManager countDownManager;

        private bool ingame;
        private void Update()
        {
            playersIngameText.text = $"Players in game: {PlayerManager.Instance.PlayersInGame}";
            if (PlayerManager.Instance.PlayersInGame == 2 && !ingame)
            {
                Debug.Log("Partida iniciada");
                countDownManager.StartCountDown();
                ingame = true;
            }
        }

        private void Start()
        {
            startHostButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartHost();
            });
            
            startServerButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartServer();
            });
            
            startClientButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartClient();
            }); 
        }
    }
}
