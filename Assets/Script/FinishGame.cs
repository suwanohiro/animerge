using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField]
    private float MaxScale;

    private GameObject Object;

    [SerializeField]
    private AudioSource FinishSE;

    private float time;

    private float ChangeScale;

    private bool ChangeScaleFlg;

    private bool End;               //èIóπîªíË

    // Start is called before the first frame update
    void Start()
    {
        //FinishSE = GameObject.Find("FinishSE").GetComponent<AudioSource>();

        FinishSE = FinishSE.GetComponent<AudioSource>();
        Debug.Log("clip"+FinishSE.clip);

        //FINISHÇhierarchyÇ©ÇÁíTÇ∑ÅB
        Object = GameObject.Find("FINISH");
        Object.transform.localScale = new Vector3(0, 0, 0);

        time = 0;
        ChangeScale = 0;
        ChangeScaleFlg = false;

        End = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ChangeScaleFlg)
        {
            return;
        }
        time += Time.deltaTime;
        ChangeScale = time * MaxScale;
        if (ChangeScale < MaxScale)
        {
            Object.transform.localScale = new Vector3(ChangeScale, ChangeScale, ChangeScale);
        }
        else if (ChangeScale > MaxScale)
        {
            Object.transform.localScale = new Vector3(MaxScale, MaxScale, MaxScale);
            End = true;
        }
    }
    public void Finish()
    {
        ChangeScaleFlg = true;
        FinishSE.Play();
    }
    public bool IsEnd()
    {
        return End;
    }
}

