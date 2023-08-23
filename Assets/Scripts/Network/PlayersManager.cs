using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Network
{
    public class PlayerManager : NetworkBehaviour
    {
        public static PlayerManager Instance;

        private List<NetworkObject> players = new List<NetworkObject>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void AddPlayer(NetworkObject player)
        {
            players.Add(player);
        }

        public void RemovePlayer(NetworkObject player)
        {
            players.Remove(player);
        }

        public void ListPlayers()
        {
            Debug.Log("Players in the host:");
            foreach (var player in players)
            {
                Debug.Log(player.OwnerClientId); 
            }
        }
    }
}