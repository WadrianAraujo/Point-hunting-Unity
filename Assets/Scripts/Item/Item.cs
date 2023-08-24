using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Item
{
    public class Item : NetworkBehaviour
    {
        private void OnTriggerEnter(Collider col)
        {
            if (!col.CompareTag("Player")) return;
            
            if (!NetworkManager.Singleton.IsServer) return;

        }
    }
}
