using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RankObjInitData = RankingObject.RankObjInitData;

public class RankingObjectManager : MonoBehaviour
{
    enum Rank : int
    {
        No1,
        No2,
        No3,
        Other
    }

    [SerializeField]
    private GameObject RankingObj;

    [SerializeField]
    private float TopLimitPos = 2.5f;

    [Serializable]
    struct RankingIconImages
    {
        [SerializeField]
        public Sprite No1;
        [SerializeField]
        public Sprite No2;
        [SerializeField]
        public Sprite No3;
        [SerializeField]
        public Sprite Other;
    }

    [SerializeField]
    private RankingIconImages RankingIcons;


    private string[] user_name =
        {
            "mario",
            "toad",
            "bowser",
            "link",
            "donkey",
            "AAAAAAAAAA"
        };


    public void CreateRankingObject(int CreateCount)
    {
        for (int cnt = CreateCount - 1; cnt >= 0; cnt--)
        {
            int score = (int)((CreateCount - cnt) * 123.45f) * 80;
            RankObjInitData initData;

            initData.Ranking = cnt + 1;
            initData.SpriteData = GetRankIconSprite(cnt);
            initData.PositionY = TopLimitPos;
            initData.Name = user_name[cnt];
            initData.Score = score;

            GameObject obj = Instantiate(RankingObj);
            obj.GetComponent<RankingObject>().Initialize(initData);
        }
    }

    /// <summary>
    /// ランキングアイコンを取得する
    /// </summary>
    /// <param name="ranking">順位</param>
    /// <returns>ランキングアイコン</returns>
    private Sprite GetRankIconSprite(int ranking)
    {
        Sprite sprite = null;

        switch ((Rank)ranking)
        {
            case Rank.No1:
                sprite = RankingIcons.No1;
                break;

            case Rank.No2:
                sprite = RankingIcons.No2;
                break;

            case Rank.No3:
                sprite = RankingIcons.No3;
                break;

            default:
                sprite = RankingIcons.Other;
                break;
        }

        return sprite;
    }
}
