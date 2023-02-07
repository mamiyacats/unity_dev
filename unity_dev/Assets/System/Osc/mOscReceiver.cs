
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net; //IPEndPoint等
using System.Net.Sockets; //udpCliant
using System.Text;
using UniRx;


namespace mCat.Osc
{

    public class mOscReceiver : MonoBehaviour
    {

        [Range(1, 65535)] public int _networrkPort = 9000;

        private UdpClient _udpClient;
        private Subject<string> _subject = new();

        private OscParser _parser;


        private void Start()
        {
            InitializeReceiver();
        }

        private void InitializeReceiver()
        {
            _udpClient = new(_networrkPort); //自分のポートを指定（静的ポート）
            _udpClient.BeginReceive(OnReceived, _udpClient); //受信開始

            _subject
                .ObserveOnMainThread()
                .Subscribe(msg => { OnReceivedMessage(msg); }).AddTo(this); //_subjectにデリゲートを登録

            _parser = new();
        }

        private void OnReceived(System.IAsyncResult result)
        {
            UdpClient getUdp = (UdpClient)result.AsyncState;
            IPEndPoint ipEnd = null;

            byte[] getByte = getUdp.EndReceive(result, ref ipEnd);

            var message = Encoding.UTF8.GetString(getByte); //受信したbyte配列を文字列に変換
            _subject.OnNext(message); //購読しているデリゲートを実行（ここではDebug.Log()）

            getUdp.BeginReceive(OnReceived, getUdp);

            _parser.FeedData(getByte, getByte.Length);

        }

        private void OnDestroy()
        {
            _udpClient.Close();
            _parser = null;
        }

        private void OnReceivedMessage(string message)
        {
            //Debug.Log(message);
        }
    }


    /*
    public static class Identifier
    {
        public const string Bundle = "#bundle";

        public const char Int = 'i';
        public const char Float = 'f';
        public const char String = 's';
        public const char Blob = 'b';
        public const char True = 'T';
        public const char False = 'F';
        public const char Null = 'N';
        public const char Inpulse = 'I';
        public const char OscTime = 't';
    }
    */
}