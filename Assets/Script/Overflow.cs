using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Overflow : MonoBehaviour
{
	[SerializeField]
	GameObject GameManager_Obj;

	void OnTriggerStay2D(Collider2D collision)
	{
		//���肪�A�����I�u�W�F�N�g�Ŗ����Ȃ�A�������Ȃ�
		if (!collision.gameObject.CompareTag("Animal")) { return; }

		Debug.Log($"{!collision.GetComponent<AnimalObject>().Enable_Collision_Flg()}");

		//��ꖳ�����Ԓ��Ȃ�A�A��
		if (!collision.GetComponent<AnimalObject>().Enable_Collision_Flg())
		{ return; }

		Debug.Log("��ꌟ�o�@Finish�����s");
		GameManager_Obj.GetComponent<FinishGame>().Finish();
	}
}
