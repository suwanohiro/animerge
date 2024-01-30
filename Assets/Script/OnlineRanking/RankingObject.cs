using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingObject : MonoBehaviour
{
    private GameObject rankingIcon;

    private bool isInited = false;

    [SerializeField]
    private GameObject RankingIcon;

    [SerializeField]
    private GameObject UserName;

    [SerializeField]
    private GameObject Score;

    public void Initialize(Sprite sprite, float PositionY, string name, string score)
    {
        SpriteRenderer spriteRenderer = RankingIcon.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        changeNameScoreText(ref UserName, name);

        changeNameScoreText(ref Score, score);

        Vector3 pos = gameObject.transform.position;
        pos.y = PositionY;

        gameObject.transform.position = pos;

        isInited = true;
    }

    private void changeNameScoreText(ref GameObject obj, string str)
    {
        TextMeshPro textMesh = obj.GetComponent<TextMeshPro>();
        textMesh.text = str;
    }
}
