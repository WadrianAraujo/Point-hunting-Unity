using System;
using UnityEngine;
using Cinemachine;
using Unity.Netcode;
using Utils;

namespace Player
{
    
    public class PlayerCamera : Singleton<PlayerCamera>
    {
        [SerializeField]
        private float amplitudeGain = 0.5f;
        [SerializeField]
        private float frequencyGain = 0.5f;
        
        private Camera m_mainCamera;
        private CinemachineVirtualCamera m_virtualCam;

        private void Awake()
        {
            m_virtualCam = GetComponent<CinemachineVirtualCamera>();
        }
        
        public void FollowPlayer(Transform transform)
        {
            // not all scenes have a cinemachine virtual camera so return in that's the case
            if (m_virtualCam == null) return;
            m_virtualCam.Follow = transform;
            
            var perlin = m_virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            perlin.m_AmplitudeGain = amplitudeGain;
            perlin.m_FrequencyGain = frequencyGain;
        }
    }
}