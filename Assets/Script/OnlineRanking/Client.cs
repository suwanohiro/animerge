using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class Client : MonoBehaviour
{
    // アドレス
    [SerializeField]
    private string Address = "127.0.0.1";

    // ポート
    [SerializeField]
    private ushort Port = 54321;

    // 受信バッファサイズ
    [SerializeField]
    private ushort ReceiveBuffSize = 1024;

    // 接続済みかどうか
    bool _isConnected = false;

    public bool isConnected
    {
        get { return _isConnected; }
    }

    private TcpClient tcpClient;
    private NetworkStream networkStream;

    public bool ConnectServer(Action<SignalData> receiveFunc)
    {
        try
        {
            tcpClient = new TcpClient(Address, Port);
            networkStream = tcpClient.GetStream();
            _isConnected = true;

            Debug.Log("接続に成功しました。");


            Task.Run(() => { ReceiveStart(receiveFunc); });
            return true;
        }
        catch
        {
            _isConnected = false;

            Debug.Log("接続に失敗しました。");
            return false;
        }
    }

    public bool SendServer(string order, string message, string value, string valueType)
    {
        SignalDataElem request = new SignalDataElem(order, message, value, valueType);
        return SendServer(request);
    }

    /// <summary>
    /// サーバーへメッセージを送信する処理
    /// </summary>
    /// <param name="requestData">送信するメッセージ</param>
    /// <returns>送信に成功したかどうか</returns>
    public bool SendServer(SignalDataElem requestData)
    {
        if (!_isConnected)
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

    private void ReceiveStart(Action<SignalData> receiveFunc)
    {
        Debug.Log("サーバーからのデータ受信待機を開始します。");
        while (_isConnected)
        {
            string receiveData = Receive();
            SignalData? data = null;

            try
            {
                data = JsonUtility.FromJson<SignalData>(receiveData);
            }
            catch
            {
                Debug.LogError($"不正な形式のデータを受信しました。\nData: {receiveData}");
            }

            if (data != null) receiveFunc((SignalData)data);
        }
        Debug.Log("サーバーからのデータ受信待機を終了しました。");
    }

    /// <summary>
    /// サーバーからデータを受信する処理
    /// </summary>
    /// <returns>受信した文字列データ</returns>
    private string Receive()
    {
        if (!_isConnected)
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
            if (_isConnected) Debug.LogError("受信に失敗しました。");
        }

        return message;
    }

    /// <summary>
    /// 接続を切断する処理
    /// </summary>
    public void DisConnectServer()
    {
        if (tcpClient == null) return;

        _isConnected = false;

        tcpClient.Dispose();
        networkStream.Dispose();

        tcpClient = null;
        networkStream = null;

        Debug.Log("切断しました。");
    }

    void OnDestroy()
    {
        DisConnectServer();
    }
}
