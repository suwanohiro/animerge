using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sun_Move : MonoBehaviour
{
	//***** �\���ʒu�֘A *****
	[SerializeField]
	private int MoveRange;			//�ړ��͈́@���݂̈ʒu���獶�E�ɂǂ����

	[SerializeField]
	private float CursorVerticalSpace;

	//***** �\���f�ފ֘A *****
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

	//***** ���Ԑ���֘A *****
	private float StartTime;

	[SerializeField]
	private float DurationTime;     //�Q�[���J�n����I���܂łɊ|���鎞��

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

	//�Q�[���̏I�����Ԃ��߂��Ă��邩
	bool IsEnd()
	{
		if(GetTime() > DurationTime)
			return true;

		return false;
	}

	//�o�ߎ��Ԏ擾
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
