using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
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

    enum CreateElemFlgs
    {
        Null,
        Create,
        Created
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

    private CreateElemFlgs createFlag = CreateElemFlgs.Null;
    private RankingData[] RankingArray;

    public void CreateRankingObject(RankingData[] rankingArray)
    {
        createFlag = CreateElemFlgs.Create;
        RankingArray = rankingArray;
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

    void Update()
    {
        if (createFlag != CreateElemFlgs.Create) return;

        for (int cnt = RankingArray.Length - 1; cnt >= 0; cnt--)
        {
            RankingData work = RankingArray[cnt];
            RankObjInitData initData;

            initData.Ranking = cnt + 1;
            initData.SpriteData = GetRankIconSprite(cnt);
            initData.PositionY = TopLimitPos;
            initData.Name = work.Name;
            initData.Score = work.Score;

            GameObject obj = Instantiate(RankingObj);
            obj.GetComponent<RankingObject>().Initialize(initData);
        }

        createFlag = CreateElemFlgs.Created;
    }
}
