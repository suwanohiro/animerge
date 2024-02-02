using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        //Text.SetText($"Score:345");
        Debug.Log("score "+GameMnager.score);
        Text.SetText($"Score:{GameMnager.score}");

        SendRanking send = GetComponent<SendRanking>();
        send.SendRankingData(GameMnager.score);
    }
}
