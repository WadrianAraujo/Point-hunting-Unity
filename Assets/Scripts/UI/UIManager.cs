using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Network;
namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button startServerButton;
        [SerializeField] private Button startHostButton;
        [SerializeField] private Button startClientButton;
        [SerializeField] private TextMeshProUGUI playersIngameText;

        private void Update()
        {
            PlayerManager.Instance.ListPlayers();
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
