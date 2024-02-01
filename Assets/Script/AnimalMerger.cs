using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimalMerger : MonoBehaviour
{
	public struct GenerateBooking
	{
		public Vector2 Pos;
		public AnimalObject.AnimaliaGrade AnimaliaGrade;

		public GenerateBooking(Vector2 pos, AnimalObject.AnimaliaGrade animaliaGrade)
		{
			Debug.Log($"コンストラクタ {pos.x} {pos.y}");
			Pos = pos;
			AnimaliaGrade = animaliaGrade;
		}

		static public bool operator ==(GenerateBooking t1, GenerateBooking t2)
		{
			return ((t1.Pos == t2.Pos) && (t1.AnimaliaGrade == t2.AnimaliaGrade));
		}

		static public bool operator !=(GenerateBooking t1, GenerateBooking t2)
		{
			//(c1 != c2)とすると、無限ループ
			return !(t1 == t2);
			
		}
	}

	List<GenerateBooking> bookings = new List<GenerateBooking>();

	// Update is called once per frame
	void Update()
	{
		foreach (GenerateBooking bookingData in bookings)
		{
			GenerateAnimal(bookingData);
		}

		bookings.Clear();

		//if(Input.GetKeyDown(KeyCode.Space))
		//{
		//	GenerateAnimal(new GenerateBooking(new Vector2(1, 4), AnimalObject.AnimaliaGrade.Elephant));
		//}
	}

	//同じ階級どうしで、衝突した時の生成の予約処理
	public void SetGenerateBooking(GenerateBooking setData)
	{
		for (int i = 0; i < bookings.Count; i++)
		{
			if (bookings[i] == setData)
			//if(Equals(bookings[i], setData))
			{
				Debug.Log("拒否");
				return;
			}
		}

		//foreach (GenerateBooking bookingData in bookings)
		//{
		//	if (bookingData == setData)
		//	{
		//		return;
		//	}
		//}

		bookings.Add(setData);
	}

	static public GameObject GenerateAnimal(GenerateBooking Data)
	{
		string str = new string("");

		switch (Data.AnimaliaGrade)
		{
			case AnimalObject.AnimaliaGrade.Elephant:
				str = "AnimalObject_Elephant";
				break;

			case AnimalObject.AnimaliaGrade.Giraffe:
				str = "AnimalObject_Giraffe";
				break;

			case AnimalObject.AnimaliaGrade.Rhinoceros:
				str = "AnimalObject_Rhinoceros";
				break;

			case AnimalObject.AnimaliaGrade.Tiger:
				str = "AnimalObject_Tiger";
				break;

			case AnimalObject.AnimaliaGrade.Dog:
				str = "AnimalObject_Dog";
				break;

			case AnimalObject.AnimaliaGrade.Cat:
				str = "AnimalObject_Cat";
				break;

			case AnimalObject.AnimaliaGrade.Rabbit:
				str = "AnimalObject_Rabbit";
				break;

			case AnimalObject.AnimaliaGrade.Hamster:
				str = "AnimalObject_Hamster";
				break;
		}

		GameObject obj = (GameObject)Resources.Load(str);
		Vector3 vector3 = new Vector3(Data.Pos.x, Data.Pos.y, 0f);
		return Instantiate(obj, vector3, Quaternion.identity);
	}
}
