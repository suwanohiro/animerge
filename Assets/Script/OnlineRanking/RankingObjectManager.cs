using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingObjectManager : MonoBehaviour
{
    enum Rank : int {
        No1,
        No2,
        No3,
        Other
    }

    [SerializeField]
    private GameObject RankingObj;

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
        public Sprite[] Other;
    }

    [SerializeField]
    private RankingIconImages RankingIcons;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> array = new List<GameObject>();
        string[] name =
        {
            "mario",
            "toad",
            "bowser",
            "link",
            "donkey",
            "kirby"
        };

        for (int cnt = 0; cnt < 6; cnt++)
        {
            array.Add(Instantiate(RankingObj));
        }

        const float posY = 1 * 1.25f;
        float totalSize = posY * array.Count;
        float topPos = 2.5f;
        for (int cnt = 0; cnt < array.Count; cnt++)
        {
            float pos = cnt * 1.25f;
            pos = topPos - pos;
            GameObject obj = array[cnt];
            Sprite sprite = GetRankIconSprite(cnt);
            Debug.Log(sprite != null);
            int score = (int)((float)(array.Count - cnt) * 123.45f);
            obj.GetComponent<RankingObject>().Initialize(sprite, pos, name[cnt], score.ToString());
        }
        return;
    }

    /// <summary>
    /// ランキングアイコンを取得する
    /// </summary>
    /// <param name="ranking">順位</param>
    /// <returns>ランキングアイコン</returns>
    private Sprite GetRankIconSprite(int ranking)
    {
        Sprite sprite = null;

        switch ((Rank)ranking) {
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
                int index = ranking - (int)Rank.Other;
                int otherLength = RankingIcons.Other.Length;

                // 配列の範囲外ならここで処理終了
                if (otherLength <= index) break;

                sprite = RankingIcons.Other[index];
                break;
        }

        return sprite;
    }
}
