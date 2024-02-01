using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMnager : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text scoreText;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        int score = ScoreCount.GetScore();

		scoreText.text = score.ToString();
    }
}
