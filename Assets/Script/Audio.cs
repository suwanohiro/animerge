using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private static bool isLoad = false;
    // Start is called before the first frame update
    void Start()
    {
        if (isLoad)
        { // ���łɃ��[�h����Ă�����
            Destroy(gameObject); // �������g��j�����ďI��
            return;
        }
        isLoad = true;
        // Scene��J�ڂ��Ă��I�u�W�F�N�g�������Ȃ��悤�ɂ���
        DontDestroyOnLoad(this);
    }
}
