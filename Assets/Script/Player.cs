using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	float MoveRange;

	[SerializeField]
	GameObject NextAnimalLocation;

	GameObject NowAnimal;

	GameObject NextAnimal;

	float BacePos;

	float DropTime;

	[SerializeField]
	float DropWaitTime;

	[SerializeField]
	float SetWaitTime;

	// Start is called before the first frame update
	void Start()
	{
		BacePos = transform.position.x;

		DropTime = Time.time;

		GenerateAnimal_Now();

		GenerateAnimal_Next();
	}

	// Update is called once per frame
	void Update()
	{
		PlayerMove();

		if (Input.GetKeyDown(KeyCode.Space) &&
			NowAnimal != null &&
			DropTime + DropWaitTime <= Time.time)
		{
			Rigidbody2D nowRigid = NowAnimal.GetComponent<Rigidbody2D>();
			nowRigid.simulated = true;
			AnimalObject nowAnimal = NowAnimal.GetComponent<AnimalObject>();
			nowAnimal.enabled = true;

			NowAnimal = null;

			DropTime = Time.time;
		}
		else if(NowAnimal == null && DropTime + SetWaitTime <= Time.time)
		{
			NowAnimal = NextAnimal;

			GenerateAnimal_Next();
		}
	}

	AnimalObject.AnimaliaGrade RandomAnimaliaGrade()
	{
		return (AnimalObject.AnimaliaGrade)
				Random.Range((int)AnimalObject.AnimaliaGrade.Elephant,
							 (int)AnimalObject.AnimaliaGrade.Tiger);
	}

	[SerializeField]
	float Speed;
	void PlayerMove()
	{
		float x = 0;

		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			x -= Speed;
		}
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			x += Speed;
		}

		transform.Translate(x, 0, 0);

		x = 0.0f;

		if (transform.position.x > BacePos + MoveRange)
		{
			x = transform.position.x - (BacePos + MoveRange);
		}
		else if (transform.position.x < BacePos - MoveRange)
		{
			x = transform.position.x - (BacePos - MoveRange);
		}

		transform.Translate(-x, 0, 0);

		if (NowAnimal != null)
			NowAnimal.transform.position = transform.position;
	}

	void GenerateAnimal_Now()
	{
		//プレイヤーが保持する動物を生成
		NowAnimal = AnimalMerger.GenerateAnimal(new AnimalMerger.GenerateBooking(
							new Vector2(transform.position.x,
										transform.position.y),
							RandomAnimaliaGrade()));

		Rigidbody2D nowRigid = NowAnimal.GetComponent<Rigidbody2D>();
		nowRigid.simulated = false;
		AnimalObject nowAnimal = NowAnimal.GetComponent<AnimalObject>();
		nowAnimal.enabled = false;
	}

	void GenerateAnimal_Next()
	{
		//次に落とす動物を生成
		NextAnimal = AnimalMerger.GenerateAnimal(new AnimalMerger.GenerateBooking(
							new Vector2(NextAnimalLocation.transform.position.x,
										NextAnimalLocation.transform.position.y),
							RandomAnimaliaGrade()));
		Rigidbody2D nextRigid = NextAnimal.GetComponent<Rigidbody2D>();
		nextRigid.simulated = false;
		AnimalObject nextAnimal = NextAnimal.GetComponent<AnimalObject>();
		nextAnimal.enabled = false;
	}

	public int GetHoldScore()
	{
		int now = (NowAnimal == null) ? 0:
				ScoreCount.ScoreCalculation
					(NowAnimal.GetComponent<AnimalObject>().GetAnimaliaGrade());

		int next = (NextAnimal == null) ? 0 :
				ScoreCount.ScoreCalculation
					(NextAnimal.GetComponent<AnimalObject>().GetAnimaliaGrade());

		return now + next;
	}
}
