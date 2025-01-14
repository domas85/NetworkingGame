using Unity.Netcode;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
namespace NetcodeDemo
{
    public class ClientPlayerMove : NetworkBehaviour
    {
        private GameObject _mainCamera;

        [SerializeField]
        CharacterController m_CharacterController;
        [SerializeField]
        PlayerInput m_PlayerInput;
        [SerializeField]
        StarterAssetsInputs m_StarterAssetsInputs;
        [SerializeField]
        ThirdPersonController m_ThirdPersonController;
        [SerializeField]
        Transform m_CameraRoot;
        private void Awake()
        {

            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }

        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            enabled = IsClient; // Enable if this is a client.
            if (IsOwner)
            {
                // Enable if this is an owner
                m_PlayerInput.enabled = true;
                m_CharacterController.enabled = true;
                m_ThirdPersonController.enabled = true;
                m_StarterAssetsInputs.enabled = true;

                var virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
                virtualCamera.Follow = m_CameraRoot;
            }
            else if (IsServer)
            {
                m_PlayerInput.enabled = false;
            }
            else
            {
                // Disable if this is not the owner
                enabled = false;
                m_PlayerInput.enabled = false;
                m_CharacterController.enabled = false;
                m_ThirdPersonController.enabled = false;
                m_StarterAssetsInputs.enabled = false;
            }
        }

        [Rpc(SendTo.Server)]
        private void UpdateInputServerRpc(Vector2 move, Vector2 look, bool jump, bool sprint, Vector3 angles)
        {
            m_StarterAssetsInputs.MoveInput(move);
            m_StarterAssetsInputs.LookInput(look);
            m_StarterAssetsInputs.JumpInput(jump);
            m_StarterAssetsInputs.SprintInput(sprint);
            m_StarterAssetsInputs.CameraRotationInput(angles);
        }

        private void LateUpdate()
        {
            if (!IsOwner)
            {
                return;
            }

            UpdateInputServerRpc(m_StarterAssetsInputs.move, m_StarterAssetsInputs.look, m_StarterAssetsInputs.jump, m_StarterAssetsInputs.sprint, _mainCamera.transform.eulerAngles);
        }
    }
}