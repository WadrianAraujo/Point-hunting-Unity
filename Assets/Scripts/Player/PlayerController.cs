using UnityEngine;
using Unity.Netcode;
using Network;

namespace Player
{
    [RequireComponent(typeof(NetworkObject))]
    public class PlayerController : NetworkBehaviour
    {
        public enum PlayerState
        {
            Idle,
            Walk,
            Run,
            ReverseWalk,
        }
        
        [SerializeField] private float speedPlayer;
        [SerializeField] private Vector2 spawnPositionRange = new Vector2(-4,4);
        
        [SerializeField]
        private float walkSpeed = 3.5f;

        [SerializeField]
        private float runSpeedOffset = 2.0f;

        [SerializeField]
        private float rotationSpeed = 3.5f;
        
        [SerializeField]
        private NetworkVariable<Vector3> networkPositionDirection = new NetworkVariable<Vector3>();

        [SerializeField]
        private NetworkVariable<Vector3> networkRotationDirection = new NetworkVariable<Vector3>();

        [SerializeField]
        private NetworkVariable<PlayerState> networkPlayerState = new NetworkVariable<PlayerState>();
        
        // client caching
        private Vector3 oldInputPosition;
        private Vector3 oldInputRotation;
        
        private float inputX;
        private float inputY;

        private CharacterController characterController;
        private Animator animator;

        private int inputXHash = Animator.StringToHash("inputX");
        private int inputYHash = Animator.StringToHash("inputY");
        private PlayerState oldPlayerState = PlayerState.Idle;
        
        private void Start()
        {
            if (IsClient && IsOwner)
            {
                SpawnPlayerInGame();
                PlayerManager.Instance.AddPlayer(GetComponent<NetworkObject>());
            }
        }
        
        void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (IsClient && IsOwner)
            {
                ClientInput();
            }
            
            ClientMoveAndRotate();
            ClientVisuals();
            
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
            
            animator.SetFloat(inputXHash, inputX);
            animator.SetFloat(inputYHash, inputY);

            //characterController.Move(transform.TransformDirection(movePlayer).normalized * Time.deltaTime * speedPlayer);
            //characterController.Move(Vector3.down * Time.deltaTime);
        }
        
        private void SpawnPlayerInGame()
        {
            transform.position = new Vector3(Random.Range(spawnPositionRange.x, spawnPositionRange.y), 0,
                Random.Range(spawnPositionRange.x, spawnPositionRange.y) );
        }
        
        private void ClientMoveAndRotate()
        {
            if (networkPositionDirection.Value != Vector3.zero)
            {
                characterController.SimpleMove(networkPositionDirection.Value);
            }
            if (networkRotationDirection.Value != Vector3.zero)
            {
                transform.Rotate(networkRotationDirection.Value, Space.World);
            }
        }

        private void ClientVisuals()
        {
            if (oldPlayerState != networkPlayerState.Value)
            {
                oldPlayerState = networkPlayerState.Value;
                animator.SetTrigger($"{networkPlayerState.Value}");
            }
        }
        
        private static bool ActiveRunningActionKey()
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        }

        private void ClientInput()
        {
            // left & right rotation
            Vector3 inputRotation = new Vector3(0, Input.GetAxis("Horizontal"), 0);
            
            Vector3 inputDirection = new Vector3(inputX, 0, inputY);
            
            // forward & backward direction
            Vector3 direction = transform.TransformDirection(inputDirection);
            float forwardInput = Input.GetAxis("Vertical");
            Vector3 inputPosition = direction;

            // change animation states
            if (forwardInput == 0)
                UpdatePlayerStateServerRpc(PlayerState.Idle);
            else if (!ActiveRunningActionKey() && forwardInput > 0 && forwardInput <= 1)
                UpdatePlayerStateServerRpc(PlayerState.Walk);
            else if (ActiveRunningActionKey() && forwardInput > 0 && forwardInput <= 1)
            {
                inputPosition = direction * runSpeedOffset;
                UpdatePlayerStateServerRpc(PlayerState.Run);
            }
            else if (forwardInput < 0)
                UpdatePlayerStateServerRpc(PlayerState.ReverseWalk);

            // let server know about position and rotation client changes
            if (oldInputPosition != inputPosition)
            {
                oldInputPosition = inputPosition;
                UpdateClientPositionAndRotationServerRpc(inputPosition * walkSpeed);
            }
        }

        [ServerRpc]
        public void UpdateClientPositionAndRotationServerRpc(Vector3 newPosition)
        {
            networkPositionDirection.Value = newPosition;
            //networkRotationDirection.Value = newRotation;
        }

        [ServerRpc]
        public void UpdatePlayerStateServerRpc(PlayerState state)
        {
            networkPlayerState.Value = state;
        }
    }
}


