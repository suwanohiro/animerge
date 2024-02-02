using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalObject : MonoBehaviour
{
	//�]�E���L�������T�C���g�����C�k���l�R���E�T�M���n���X�^�[
	public enum AnimaliaGrade
	{
		Elephant,
		Giraffe,
		Rhinoceros,
		Tiger,
		Dog,
		Cat,
		Rabbit,
		Hamster
	}

	[SerializeField]
	private AnimaliaGrade animaliagrade;

	private bool DestroyFlg = false;

	private float Not_Overflow_Time;
	private float Drop_Time;
	
	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("Start");
		
		Not_Overflow_Time = 1.5f;
		SetDropTime();

		if(animaliagrade > AnimaliaGrade.Hamster)
		{
			Debug.Log("animaliagrade���A�ő�l���z���Ă��܂��B");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(DestroyFlg)
		{
			//���g���폜
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D hit)
	{
		if (DestroyFlg) { return; }

		//�n���X�^�[�́A�������Ȃ�
		if (animaliagrade >= AnimaliaGrade.Hamster)				{ return; }

		//���肪�A�����I�u�W�F�N�g�Ŗ����Ȃ�A�������Ȃ�
		if (!hit.gameObject.CompareTag("Animal"))				{ return; }

		AnimalObject Partner = hit.gameObject.GetComponent<AnimalObject>();

		//�Փˑ���Ɠ����K���Ŗ����Ȃ�A�������Ȃ�
		if (GetAnimaliaGrade() != Partner.GetAnimaliaGrade())	{ return; }
		
		//Debug.Log("�Փ�");

		//GameManager�Ƃ����I�u�W�F�N�g��T���A�擾����
		GameObject obj = GameObject.Find("GameManager");

		AnimalMerger gameManager = obj.GetComponent<AnimalMerger>();

		Vector2 pos = new Vector2(0, 0);
		pos.x = (gameObject.transform.position.x + hit.gameObject.transform.position.x) / 2.0f;
		pos.y = (gameObject.transform.position.y + hit.gameObject.transform.position.y) / 2.0f;

		Debug.Log($"�n�� {gameObject.transform.position.x} {gameObject.transform.position.y} {hit.gameObject.transform.position.x} {hit.gameObject.transform.position.y}");
		gameManager.SetGenerateBooking(new AnimalMerger.GenerateBooking(pos, (animaliagrade + 1)));

		DestroyFlg = true;
	}

	public AnimaliaGrade GetAnimaliaGrade()
	{
		return animaliagrade;
	}

	public void SetDropTime()
	{
		Debug.Log("SetDropTime");
		Drop_Time = Time.time + Not_Overflow_Time;
	}

	public bool Enable_Collision_Flg()
	{
		Debug.Log($"{Drop_Time} {Time.time}");
		return Drop_Time <= Time.time;
	}
}
