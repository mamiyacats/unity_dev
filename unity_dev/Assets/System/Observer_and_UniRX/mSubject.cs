using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mSubject<T> : IDisposable
{
    private List<ImObserver<T>> _observers = new();

    
    //Observerを登録（購読）する
    public void RegisterObserver(ImObserver<T> observer)
    {
        _observers.Add(observer);
    }

    //Observerを登録（購読）解除する
    public void UnRegisterObserver(ImObserver<T> observer)
    {
        _observers.Remove(observer);
    }

    //メッセージを発行する。
    public void SendMessage(T message)
    {
        foreach (var observer in _observers)
        {
            observer.OnReceived(message);
        }
    }

    //破棄
    public void Dispose()
    {
        _observers.Clear();
    }

}
