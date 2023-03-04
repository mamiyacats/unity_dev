using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mUniRX_Observer_print<T> : IObserver<T>
{
    //受信通知用
    public void OnCompleted()
    {
        Debug.Log("OnCompleted.");
    }

    //エラー通知用
    public void OnError(Exception error)
    {
        Debug.LogError(error);
    }

    //値受信用（OnReceived()と同じ）
    public void OnNext(T value)
    {
        Debug.Log(value.ToString());
    }
}
