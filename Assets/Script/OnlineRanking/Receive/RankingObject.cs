using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingObject : MonoBehaviour
{
    public struct RankObjInitData
    {
        public RankObjInitData(int ranking, Sprite sprite, float posY, string name, int score)
        {
            Ranking = ranking;
            SpriteData = sprite;
            PositionY = posY;
            Name = name;
            Score = score;
        }

        // 順位
        public int Ranking;

        // アイコンデータ
        public Sprite SpriteData;

        // 座標 (y)
        public float PositionY;

        // ユーザー名
        public string Name;

        // スコア
        public int Score;
    }

    [SerializeField]
    private GameObject RankingIcon;

    [SerializeField]
    private GameObject RankingNumber;

    [SerializeField]
    private GameObject UserName;

    [SerializeField]
    private GameObject Score;

    public void Initialize(RankObjInitData initData)
    {
        // ランキングアイコンを設定
        SpriteRenderer spriteRenderer = RankingIcon.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = initData.SpriteData;

        // 順位表記
        changeTMPro(ref RankingNumber, initData.Ranking.ToString());

        // ユーザー名を設定
        changeTMPro(ref UserName, initData.Name);

        // スコアを設定
        changeTMPro(ref Score, initData.Score.ToString("N0"));

        // 座標を設定
        Vector3 pos = gameObject.transform.position;
        pos.y = initData.PositionY;
        gameObject.transform.position = pos;
    }


    /// <summary>
    /// TextMeshProの文字列データを書き換える
    /// </summary>
    /// <param name="obj">書き換え対象オブジェクト</param>
    /// <param name="str">書き換え文字列</param>
    private void changeTMPro(ref GameObject obj, string str)
    {
        TextMeshPro textMesh = obj.GetComponent<TextMeshPro>();
        textMesh.text = str;
    }
}
