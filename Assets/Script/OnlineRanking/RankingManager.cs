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

    private Client client;

    // Start is called before the first frame update
    void Start()
    {
        RankingObjectManager objectManager = GetComponent<RankingObjectManager>();
        objectManager.CreateRankingObject();

        // client = _clientElem.GetComponent<Client>();
        // client.ConnectServer();
        // client.SendServer(SignalLabel.Request, "Ranking", "", ValueType.Text);
    }
}
