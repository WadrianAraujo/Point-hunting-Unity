using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvas : MonoBehaviour
{
    [SerializeField] private Transform scoreBoard;
    [SerializeField] private GameObject PlayerScoreTemplate;

    private void OnEnable()
    {
        NetworkPlayer.OnPlayerSpawn += OnPlayerSpawned;
    }

    private void OnDisable()
    {
        NetworkPlayer.OnPlayerSpawn -= OnPlayerSpawned;
    }

    private void OnPlayerSpawned(GameObject player)
    {
        GameObject PlayerUI = Instantiate(PlayerScoreTemplate, scoreBoard);
    }
}
