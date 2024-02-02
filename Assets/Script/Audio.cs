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
        { // すでにロードされていたら
            Destroy(gameObject); // 自分自身を破棄して終了
            return;
        }
        isLoad = true;
        // Sceneを遷移してもオブジェクトが消えないようにする
        DontDestroyOnLoad(this);
    }
}
