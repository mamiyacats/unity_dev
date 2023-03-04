using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mObserver_print<T> : ImObserver<T>
{
    //値受信用
    public void OnReceived(T value)
    {
        Debug.Log(value.ToString());
    }
}
