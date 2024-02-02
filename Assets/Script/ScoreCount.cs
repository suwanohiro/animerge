using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
	static public int GetScore()
	{
		int score = 0;

		GameObject[] Animals = GameObject.FindGameObjectsWithTag("Animal");

		foreach (var animal in Animals)
		{
			AnimalObject animal_Obj = animal.gameObject.GetComponent<AnimalObject>();
			score += ScoreCalculation(animal_Obj.GetAnimaliaGrade());
		}

		score -= GameObject.FindWithTag("Player").GetComponent<Player>().GetHoldScore();

		return score;
	}

	static public int ScoreCalculation(AnimalObject.AnimaliaGrade animaliaGrade)
	{
		return (int)Math.Pow(2.5, (double)animaliaGrade);
	}
}
