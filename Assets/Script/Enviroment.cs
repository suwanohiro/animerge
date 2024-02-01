using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enviroment : MonoBehaviour
{
    [SerializeField] private Sun_Move timer;
    GameObject[] childObject;
    SpriteRenderer[] childSprite;
    byte alpha =255;
    // Start is called before the first frame update
    void Start()
    {
        childObject = new GameObject[transform.childCount];
        childSprite=new SpriteRenderer[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            childObject[i] = childTransform.gameObject;
            childSprite[i]=childTransform.GetComponent<SpriteRenderer>();
            Debug.Log(childObject[i]);
            Debug.Log(childSprite[i]);
        }
        childObject[0].SetActive(true);
    }
    void Update()
    {
        if (timer.GetTime() >= timer.DurationTime)
        {
            Fade(2);
        }
        else if (timer.GetTime()>timer.DurationTime * 2/ 3)
        {
            Fade(1);
        }
        else if(timer.GetTime()>timer.DurationTime /3)
        {
            Fade(0);
        }
    }
    /*n”Ô–Ú‚Ì”wŒi‚Ìfade*/
    void Fade(int n)
    {
        alpha--;
        if (alpha <= 0)
        {
            alpha = 0;
            childObject[n].SetActive(false);
            childObject[n+1].SetActive(true);
            alpha = 255;
        }
        else
        {
            childSprite[n].color = new Color32(255, 255, 255, alpha);
        }
    }
}
