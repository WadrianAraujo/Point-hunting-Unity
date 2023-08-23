using System;
using UnityEngine;
using Cinemachine;
using Unity.Netcode;

namespace Player
{
    
    public class PlayerCamera : NetworkBehaviour
    {
        [SerializeField] private AxisState xAxis;
        [SerializeField] private AxisState yAxis;
        
        [SerializeField] private Transform lookAt;
        
        private Camera m_mainCamera;
        private CinemachineVirtualCamera m_virtualCam;

        private void Awake()
        {
            m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            m_virtualCam = m_mainCamera.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        }
        
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (!IsClient)
            {
                enabled = false;
                return;
            }
            
            m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            m_virtualCam = m_mainCamera.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
            m_virtualCam.LookAt = lookAt;
            m_virtualCam.Follow = lookAt;
        }

        void Update()
        {
            xAxis.Update(Time.deltaTime);
            yAxis.Update(Time.deltaTime);

            lookAt.eulerAngles = new Vector3 (yAxis.Value, xAxis.Value, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, xAxis.Value, 0), 5 * Time.deltaTime);
        }
    }
}