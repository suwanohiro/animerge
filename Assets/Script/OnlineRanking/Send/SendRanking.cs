using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendRanking : MonoBehaviour
{
    [SerializeField]
    private GameObject ClientElem;

    private Client client;

    // Start is called before the first frame update
    void Start()
    {
        client = ClientElem.GetComponent<Client>();
        bool isConnect = client.ConnectServer((SignalData receiveData) => { ReceiveFunc(receiveData); });

        if (!isConnect) return;

        RankingData data;
        data.Name = "test player";
        data.Score = 100000;

        SendRankingData(data);
    }

    public void SendRankingData(RankingData data)
    {
        client.SendServer(
            OrderList.Update,
            "Ranking",
            JsonUtility.ToJson(data),
            ValueType.JSON
        );
    }

    ////////////////////////////////////////////////////////////////////////
    //
    // 通信関連処理
    //
    ////////////////////////////////////////////////////////////////////////

    private void ReceiveFunc(SignalData receiveData)
    {
        switch (receiveData.order)
        {
            default:
                break;
        }
    }

    /// <summary>
    /// OrderがResponseの時の処理
    /// </summary>
    private void OrderResponse(SignalData receiveData)
    {
        switch (receiveData.message)
        {
            default:
                break;
        }
    }
}
