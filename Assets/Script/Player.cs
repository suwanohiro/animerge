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

	void PlayerMove()
	{
		float x = Input.GetAxisRaw("Horizontal");

		//同一のGameObjectが持つRigidbodyコンポーネントを取得
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

		rigidbody.AddForce(new Vector2(x * 3, 0));

		Vector2 Pos = transform.position;
		if (Pos.x > BacePos + MoveRange)
		{
			Pos.x = BacePos + MoveRange;
			rigidbody.velocity = Vector2.zero;
		}
		else if (Pos.x < BacePos - MoveRange)
		{
			Pos.x = BacePos - MoveRange;
			rigidbody.velocity = Vector2.zero;
		}

		transform.position = Pos;

		if(NowAnimal != null)
			NowAnimal.transform.position = Pos;
	}

	void GenerateAnimal_Now()
	{
		//プレイヤーが保持する動物を生成
		NowAnimal = GameManager.GenerateAnimal(new GameManager.GenerateBooking(
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
		NextAnimal = GameManager.GenerateAnimal(new GameManager.GenerateBooking(
							new Vector2(NextAnimalLocation.transform.position.x,
										NextAnimalLocation.transform.position.y),
							RandomAnimaliaGrade()));
		Rigidbody2D nextRigid = NextAnimal.GetComponent<Rigidbody2D>();
		nextRigid.simulated = false;
		AnimalObject nextAnimal = NowAnimal.GetComponent<AnimalObject>();
		nextAnimal.enabled = false;
	}
}
