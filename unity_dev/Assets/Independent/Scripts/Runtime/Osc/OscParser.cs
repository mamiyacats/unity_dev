
using UnityEngine;
using System;
using System.Text;


namespace mCat.Osc
{

    using MessageQueue = System.Collections.Generic.Queue<Message>;

    public struct Message
    {
        public string _path;
        public object[] _data;

        public override string ToString() //Debug.Log(Message) の時に以下の値で返す
        {
            var buf = new StringBuilder(); //文字列変更のためのクラス
            buf.AppendFormat("path={0} : ", _path); //書式を設定して文字列を追加 //path=_path : 
            for (var i = 0; i < _data.Length; i++)
                buf.AppendFormat("data[{0}]={1}, ", i, _data[i]); //path=_path : data[i]=_data[i]

            return buf.ToString();
        }
    }

    public class OscParser
    {
        MessageQueue _messageBuffer; //待ち行列。入れたものが入れた順に出てくるコレクション。

        Byte[] _readBuffer;
        int _readBufferLength;
        int _readPoint;


        public OscParser()
        {
            _messageBuffer = new MessageQueue();
        }


        public int _messageCount
        {
            get { return _messageBuffer.Count; } //キューの個数。
        }
        public Message PopMessage()
        {
            return _messageBuffer.Dequeue(); //キューの先頭にあるデータを取り出す。
        }
        public void FeedData(Byte[] data, int length)
        {
            _readBuffer = data;
            _readBufferLength = length;
            _readPoint = 0;
            ReadMessage();
            _readBuffer = null;
        }

        void ReadMessage()
        {
            var path = ReadString();

            if (path == "#bundle")
            {
                ReadInt64();

                while (true)
                {
                    if (_readPoint >= _readBufferLength) return;
                    var peek = _readBuffer[_readPoint];
                    if (peek == '/' || peek == '#')
                    {
                        ReadMessage();
                        return;
                    }
                    var bundleEnd = _readPoint + ReadInt32();
                    while (_readPoint < bundleEnd) ReadMessage();
                }
            }

            var temp = new Message();
            temp._path = path;

            var types = ReadString();
            var data = new object[(types.Length > 0 ? types.Length - 1 : 0)];

            for (var i = 0; i < types.Length - 1; i++)
            {
                switch (types[i + 1])
                {
                    case 'f':
                        data[i] = ReadFloat32();
                        Debug.Log(data[i]);
                        break;
                    case 'i':
                        data[i] = ReadInt32();
                        Debug.Log(data[i]);
                        break;
                    case 's':
                        data[i] = ReadString();
                        Debug.Log(data[i]);
                        break;
                    case 'b':
                        data[i] = ReadBlob();
                        Debug.Log(data[i]);
                        break;
                }
                temp._data = data;
                _messageBuffer.Enqueue(temp); //キューの末尾に追加。
            }

        }


        float ReadFloat32()
        {
            var union32 = new OscMessageEncoder.Union32();
            union32.Unpack(_readBuffer, _readPoint);
            _readPoint += 4;
            return union32.floatdata;
        }

        int ReadInt32()
        {
            var union32 = new OscMessageEncoder.Union32();
            union32.Unpack(_readBuffer, _readPoint);
            _readPoint += 4;
            return union32.intdata;
        }

        long ReadInt64()
        {
            var union64 = new OscMessageEncoder.Union64();
            union64.Unpack(_readBuffer, _readPoint);
            _readPoint += 8;
            return union64.intdata;
        }

        string ReadString()
        {
            var offset = 0;
            while (_readBuffer[_readPoint + offset] != 0)
                offset++;
            var s = System.Text.Encoding.UTF8.GetString(_readBuffer, _readPoint, offset);
            _readPoint += (offset + 4) & ~3;
            return s;
        }

        Byte[] ReadBlob()
        {
            var length = ReadInt32();
            var temp = new Byte[length];
            Array.Copy(_readBuffer, _readPoint, temp, 0, length);
            _readPoint += (length + 3) & ~3;
            return temp;
        }


    }
}