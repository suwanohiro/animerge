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
		//相手が、動物オブジェクトで無いなら、処理しない
		if (!collision.gameObject.CompareTag("Animal")) { return; }

		Debug.Log($"{!collision.GetComponent<AnimalObject>().Enable_Collision_Flg()}");

		//溢れ無効時間中なら、帰る
		if (!collision.GetComponent<AnimalObject>().Enable_Collision_Flg())
		{ return; }

		Debug.Log("溢れ検出　Finishを実行");
		GameManager_Obj.GetComponent<FinishGame>().Finish();
	}
}
