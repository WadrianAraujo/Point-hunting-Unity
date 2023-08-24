using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameUi;
    [SerializeField] private TextMeshProUGUI ScoreUi;

    public void TrackPlayer(GameObject player)
    {
        player.GetComponent<PlayerData>().name.OnValueChanged += OnNameChanged;
        player.GetComponent<PlayerData>().score.OnValueChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int previousvalue, int newvalue)
    {
        ScoreUi.text = newvalue.ToString();
    }

    private void OnNameChanged(FixedString128Bytes previousvalue, FixedString128Bytes newvalue)
    {
        NameUi.text = newvalue.ToString();
    }
}
