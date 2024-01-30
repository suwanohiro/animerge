using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RankingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _clientElem;

    private GameObject RankingObj;

    private Client client;

    // Start is called before the first frame update
    void Start()
    {
        RankingObj = (GameObject)Resources.Load("Prefab/RankingObject");

        List<GameObject> array = new List<GameObject>();

        for (int cnt = 0; cnt < 5; cnt++)
        {
            array.Add(Instantiate(RankingObj));
        }

        Debug.Log(array.Count);

        const float posY = 1 * 1.25f;
        float totalSize = posY * array.Count;
        for (int cnt = 0; cnt < array.Count; cnt++)
        {
            GameObject obj = array[cnt];
            obj.GetComponent<RankingObject>().Initialize(((cnt + 1) * 1.25f), "mario", "555");
        }


        return;

        client = _clientElem.GetComponent<Client>();
        client.ConnectServer();

        client.SendServer(RequestLabel.Request, "Ranking");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
