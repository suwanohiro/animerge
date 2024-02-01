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

    [SerializeField]
    private int CreateCount = 6;

    private Client client;

    // Start is called before the first frame update
    void Start()
    {
        RankingObjectManager objectManager = GetComponent<RankingObjectManager>();
        objectManager.CreateRankingObject(CreateCount);

        client = _clientElem.GetComponent<Client>();
        client.ConnectServer((SignalData receiveData) =>
        {
            ReceiveFunc(receiveData);
        });
        client.SendServer(OrderList.Request, "Ranking", CreateCount.ToString(), ValueType.Text);
    }

    private void ReceiveFunc(SignalData receiveData)
    {
        switch (receiveData.order)
        {
            case OrderList.Response:
                OrderReceive(receiveData);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// OrderがReceiveの時の処理
    /// </summary>
    private void OrderReceive(SignalData receiveData)
    {
        switch (receiveData.message)
        {
            case "close":
                client.DisConnectServer();
                break;

            default:
                break;
        }
    }
}
