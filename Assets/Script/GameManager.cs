using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMnager : MonoBehaviour
{
	[SerializeField]
	UnityEngine.UI.Text scoreText;

	public static int score=0;
	// Start is called before the first frame update
	void Start()
	{
		score = 0;
	}

	// Update is called once per frame
	void Update()
	{
        score = ScoreCount.GetScore();
        Debug.Log(GameMnager.score);
        scoreText.text = score.ToString();
	}
}
