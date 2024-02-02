using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendRanking : MonoBehaviour
{
    [SerializeField]
    private GameObject ClientElem;

    private Client client;

    private bool isConnect = false;

    private RankingData SendData;

    // Start is called before the first frame update
    void Start()
    {
        client = ClientElem.GetComponent<Client>();
        isConnect = client.ConnectServer((SignalData receiveData) => { ReceiveFunc(receiveData); });

        SendData.Name = Name.Username;
    }

    public void SendRankingData(int Score)
    {
        if (!isConnect) return;

        SendData.Score = Score;

        client.SendServer(
            OrderList.Update,
            "Ranking",
            JsonUtility.ToJson(SendData),
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
