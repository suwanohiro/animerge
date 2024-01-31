using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sun_Move : MonoBehaviour
{
	[SerializeField]
	private int MoveRange;			//�ړ��͈́@���݂̈ʒu���獶�E�ɂǂ����

	[SerializeField]
	private float CursorVerticalWidth;

	private float StartTime;

	[SerializeField]
	private float DurationTime;     //�Q�[���J�n����I���܂łɊ|���鎞��

	[SerializeField]
	Image MorningImage;

	[SerializeField]
	Image DaytimeImage;

	[SerializeField]
	Image NightImage;

	[SerializeField]
	Image CursorImage;

	// Start is called before the first frame update
	void Start()
	{
		StartTime = Time.time;

		Vector2 pos = this.transform.position;
		pos.x -= MoveRange / 2;
		
		MorningImage.transform.position = pos;
		pos.x += MoveRange / 2;

		DaytimeImage.transform.position = pos;
		pos.x += MoveRange / 2;

		NightImage.transform.position = pos;

		CursorImage.transform.position = pos;
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 Pos = this.transform.position;

		Pos.x += (MoveRange * GetRatio()) - (MoveRange / 2);
		Pos.y += CursorVerticalWidth;

		CursorImage.transform.position = Pos;
	}

	//�o�ߎ��Ԏ擾
	float GetTime()
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

	//�Q�[���̏I�����Ԃ��߂��Ă��邩
	bool IsEnd()
	{
		if(GetTime() > DurationTime)
			return true;

		return false;
	}
}
