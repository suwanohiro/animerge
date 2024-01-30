using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net.Sockets;
using System.Text;
using System;
using UnityEditor.PackageManager;

public class Client : MonoBehaviour
{
    // アドレス
    [SerializeField]
    private string Address = "127.0.0.1";

    // ポート
    [SerializeField]
    private ushort Port = 3000;

    // 受信バッファサイズ
    [SerializeField]
    private ushort ReceiveBuffSize = 1024;

    // 接続済みかどうか
    bool isConnected = false;

    private TcpClient tcpClient;
    private NetworkStream networkStream;

    public bool ConnectServer()
    {
        try
        {
            tcpClient = new TcpClient(Address, Port);
            networkStream = tcpClient.GetStream();
            isConnected = true;

            Debug.Log("接続に成功しました。");
            return true;
        }
        catch
        {
            isConnected = false;

            Debug.Log("接続に失敗しました。");
            return false;
        }
    }

    public bool SendServer(string order, string message)
    {
        RequestServer request = new RequestServer(order, message);
        return SendServer(request);
    }

    /// <summary>
    /// サーバーへメッセージを送信する処理
    /// </summary>
    /// <param name="requestData">送信するメッセージ</param>
    /// <returns>送信に成功したかどうか</returns>
    public bool SendServer(RequestServer requestData)
    {
        if (!isConnected)
        {
            Debug.LogError("サーバーへ接続されていません。");
            return false;
        }

        // サーバーへの送信処理
        try
        {
            var writeBuffer = Encoding.UTF8.GetBytes(requestData.ToJSON());
            networkStream.Write(writeBuffer, 0, writeBuffer.Length);

            Debug.Log($"サーバーへ[{requestData.ToJSON()}]の送信に成功しました。");
            return true;
        }
        catch
        {
            Debug.LogError($"サーバーへ[{requestData.ToJSON()}]の送信に失敗しました。");
            return false;
        }
    }

    /// <summary>
    /// サーバーからデータを受信する処理
    /// </summary>
    /// <returns>受信した文字列データ</returns>
    public string Receive()
    {
        if (!isConnected)
        {
            Debug.LogError("サーバーへ接続されていません。");
            return "";
        }

        string message = "";

        // サーバーからの受信処理
        try
        {
            var readBuffer = new byte[ReceiveBuffSize];

            int totalBytesRead = 0;
            int bytesRead;

            do
            {
                // バッファが足りるかどうか
                bool isEnoughBuff = totalBytesRead + ReceiveBuffSize > readBuffer.Length;

                // バッファが足りなければ拡張
                if (!isEnoughBuff)
                {
                    Array.Resize(ref readBuffer, readBuffer.Length * 2);
                }

                // データを読み取る
                bytesRead = networkStream.Read(readBuffer, totalBytesRead, ReceiveBuffSize);
                totalBytesRead += bytesRead;
            } while (bytesRead == ReceiveBuffSize);
            // bytesReadがbufferSize未満なら、すべてのデータが読み取られたと判断

            // 受信したデータを文字列に変換
            message = Encoding.UTF8.GetString(readBuffer, 0, totalBytesRead);
            Debug.Log($"受信しました。内容：{message}");
        }
        catch
        {
            Debug.LogError("受信に失敗しました。");
        }

        return message;
    }

    void OnDestroy()
    {
        tcpClient.Dispose();
        networkStream.Dispose();

        Debug.Log("切断しました。");
    }
}
