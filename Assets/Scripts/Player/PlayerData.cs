using Unity.Netcode;
using Unity.Collections;

namespace Player
{
    public class PlayerData : NetworkBehaviour
    {
        public NetworkVariable<int> score = new NetworkVariable<int>();
        public NetworkVariable<FixedString128Bytes> name = new NetworkVariable<FixedString128Bytes>();
    }
}
