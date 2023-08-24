using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace Network
{
    public class PlayerHud : NetworkBehaviour
    {
        private NetworkVariable<NetworkString> playerName = new NetworkVariable<NetworkString>();
        
        private bool overlaySet = false;

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                playerName.Value = $"Player {OwnerClientId}";
            }
        }

        public void SetOverlay()
        {
            var localPlayerOverlay = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            localPlayerOverlay.text = playerName.Value;
        }

        private void Update()
        {
            if (!overlaySet && !string.IsNullOrEmpty(playerName.Value))
            {
                SetOverlay();
                overlaySet = true;
            }
        }
    }
}
