using System.Collections.Generic;
using DilmerGames.Core.Singletons;
using Unity.Netcode;
using UnityEngine;

namespace Network
{
    public class PlayerManager : NetworkSingleton<PlayerManager>
    {
        NetworkVariable<int> playersInGame = new NetworkVariable<int>();

        public int PlayersInGame
        {
            get
            {
                return playersInGame.Value;
            }
        }

        void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
            {
                if(IsServer)
                    playersInGame.Value++;
            };

            NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
            {
                if(IsServer)
                    playersInGame.Value--;
            };
        }
    }
}