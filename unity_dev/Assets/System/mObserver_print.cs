using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mObserver_print<T> : IObserver<T> 
{
    public void OnReceived(T value)
    {
        Debug.Log(value.ToString());
    }
}
