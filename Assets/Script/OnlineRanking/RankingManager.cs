using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _clientElem;

    private Client client;

    // Start is called before the first frame update
    void Start()
    {
        client = _clientElem.GetComponent<Client>();
        client.ConnectServer();

        client.SendServer("hello");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
