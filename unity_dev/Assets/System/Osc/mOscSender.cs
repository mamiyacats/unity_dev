using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net.Sockets;
using System.Text;

public class mOscSender : MonoBehaviour
{
    [SerializeField] string hostIP = "192.168.10.133";
    [SerializeField] int port = 9000;
    private UdpClient _udpClient;


    void Start()
    {
        _udpClient = new(); //インスタンス作成
        _udpClient.Connect(hostIP, port); //指定ポートへ接続
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("send!");
            var message = Encoding.UTF8.GetBytes("HelloWorld!"); //文字列をbyte配列に変換
            _udpClient.Send(message, message.Length); //byte配列を送信
        }
    }

    private void OnDestroy()
    {
        _udpClient.Close();        
    }
}
