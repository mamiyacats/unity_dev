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
        _udpClient = new(); //�C���X�^���X�쐬
        _udpClient.Connect(hostIP, port); //�w��|�[�g�֐ڑ�
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("send!");
            var message = Encoding.UTF8.GetBytes("HelloWorld!"); //�������byte�z��ɕϊ�
            _udpClient.Send(message, message.Length); //byte�z��𑗐M
        }
    }

    private void OnDestroy()
    {
        _udpClient.Close();        
    }
}
