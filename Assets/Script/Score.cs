using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        Text.SetText($"Score:345");
        
        //Text.SetText($"Score:{GameManager.Score.ToString()}");
        //Text.text += GameManager.Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
