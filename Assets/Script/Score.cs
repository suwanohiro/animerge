using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Text;

    [SerializeField]
    private GameObject GameManager;

    // Start is called before the first frame update
    void Start()
    {
        Text.SetText($"Score:345");
        //Text.SetText($"Score:{GameManager.Score.ToString()}");

        // SendRanking send = GetComponent<SendRanking>();
        // send.SendRankingData(GameManager.Score);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
