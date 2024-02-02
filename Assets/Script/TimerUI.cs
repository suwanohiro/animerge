using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sun_Move : MonoBehaviour
{
	//***** 表示位置関連 *****
	[SerializeField]
	private int MoveRange;			//移動範囲　現在の位置から左右にどれ程か

	[SerializeField]
	private float CursorVerticalSpace;

	//***** 表示素材関連 *****
	[SerializeField]
	Image MorningImage;

	[SerializeField]
	Image DaytimeImage;

	[SerializeField]
	Image TwilightImage;

	[SerializeField]
	Image NightImage;

	[SerializeField]
	Image CursorImage;

	//***** 時間制御関連 *****
	private float StartTime;

	[SerializeField]
	private float DurationTime;     //ゲーム開始から終了までに掛かる時間

	[SerializeField]
	GameObject GameManager_Obj;

	// Start is called before the first frame update
	void Start()
	{
		StartTime = Time.time;

		float space = MoveRange / 3;

		Vector2 pos = this.transform.position;
		
		pos.x -= MoveRange / 2;
		MorningImage.transform.position = pos;

		pos.x += space;
		DaytimeImage.transform.position = pos;

		pos.x += space;
		TwilightImage.transform.position = pos;

		pos.x += space;
		NightImage.transform.position = pos;

		CursorImage.transform.position = pos;
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 Pos = this.transform.position;

		Pos.x += (MoveRange * GetRatio()) - (MoveRange / 2);
		Pos.y += CursorVerticalSpace;

		CursorImage.transform.position = Pos;

		if(IsEnd())
		{
			GameManager_Obj.GetComponent<FinishGame>().Finish();
		}
	}

	//ゲームの終了時間を過ぎているか
	bool IsEnd()
	{
		if(GetTime() > DurationTime)
			return true;

		return false;
	}

	//経過時間取得
	public float GetTime()
	{
		return Time.time - StartTime;
	}

	float GetRatio()
	{
		float ratio;

		if (IsEnd())
			ratio = 1.0f;
		else
			ratio = GetTime() / DurationTime;

		return ratio;
	}

	public float GetDurationTime()
	{
		return DurationTime;
	}
}
