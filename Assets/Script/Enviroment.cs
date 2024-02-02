using UnityEngine;

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
            //Debug.Log(childObject[i]);
            //Debug.Log(childSprite[i]);
        }
        
    }
    void Update()
    {
        if (timer.GetTime() >= timer.GetDurationTime())
        {
            Fade(2);
        }
        else if (timer.GetTime() > timer.GetDurationTime() * 2 / 3)
        {
            Fade(1);
        }
        else if (timer.GetTime() > timer.GetDurationTime() * 1 / 3)
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
            alpha = 255;
        }
        else
        {
            childSprite[n].color = new Color32(255, 255, 255, alpha);
        }
    }
}
