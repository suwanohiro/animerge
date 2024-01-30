using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingObject : MonoBehaviour
{
    private GameObject rankingIcon;

    private bool isInited = false;

    private struct ChildLabel
    {
        public const string RankingIcon = "RankingIcon";
        public const string UserName = "UserName";
        public const string Score = "Score";
    }

    public void Initialize(float RankingOrder, string UserName, string Score)
    {
        Debug.Log($"PosY : {RankingOrder}");

        for (int cnt = 0; cnt < gameObject.transform.childCount; cnt++)
        {
            Transform child = gameObject.transform.GetChild(cnt);

            if (child.name == ChildLabel.UserName)
            {
                Debug.Log("log");
                Transform textMeshPro = child.GetComponent<Transform>();

                Debug.Log(textMeshPro.position.y);

                // textMeshPro.text = UserName;
                // changeText(child.gameObject, UserName);
            }

            // childElemChange(child.gameObject.transform, UserName, Score);
        }

        Vector3 pos = gameObject.transform.position;
        pos.y = RankingOrder;

        gameObject.transform.position = pos;

        isInited = true;
    }

    private void childElemChange(Transform child, string UserName, string Score)
    {
        switch (child.name)
        {
            case ChildLabel.UserName:
                changeText(child.gameObject, UserName);
                break;

            case ChildLabel.Score:
                changeText(child.gameObject, Score);
                break;

            default:
                break;
        }
    }

    private void changeText(GameObject targetObj, string str)
    {
        TextMeshProUGUI textMeshPro = targetObj.GetComponent<TextMeshProUGUI>();

        textMeshPro.text = str;
    }

    // Start is called before the first frame update
    void Start()
    {
        // if (!isInited) Debug.LogError("インスタンスが初期化されていません。");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
