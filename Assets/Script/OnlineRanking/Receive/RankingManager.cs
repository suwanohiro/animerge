using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class RankingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _clientElem;

    [SerializeField]
    private GameObject ConnectionFailedPanel;

    [SerializeField]
    private int CreateCount = 6;

    private Client client;

    private RankingObjectManager objectManager;

    // Start is called before the first frame update
    void Start()
    {
        ConnectionFailedPanel.SetActive(false);

        objectManager = GetComponent<RankingObjectManager>();

        client = _clientElem.GetComponent<Client>();
        bool isConnect = client.ConnectServer((SignalData receiveData) => { ReceiveFunc(receiveData); });


        if (!isConnect)
        {
            ConnectionFailedFunc();
            return;
        }

        // サーバーにランキングを返すようにメッセージを送信
        client.SendServer(
            OrderList.Request,
            "Ranking",
            CreateCount.ToString(),
            ValueType.Number
        );

        Debug.Log("データを送信しました。");
    }

    private void ConnectionFailedFunc()
    {
        ConnectionFailedPanel.SetActive(true);
        StartCoroutine(HidePanel(5));
    }

    private IEnumerator HidePanel(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ConnectionFailedPanel.SetActive(false);
    }

    private void CreateRankingObject(string jsonData)
    {
        RankingArray rankingDatas = JsonUtility.FromJson<RankingArray>(jsonData);
        objectManager.CreateRankingObject(rankingDatas.Ranking);
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
            case OrderList.Response:
                OrderResponse(receiveData);
                break;

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
            case "Ranking":
                if (receiveData.valueType != ValueType.JSON) break;
                CreateRankingObject(receiveData.value);
                break;

            case "close":
                client.DisConnectServer();
                break;

            default:
                break;
        }
    }
}
