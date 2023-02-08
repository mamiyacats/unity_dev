using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//interfaceはpublicにしておかないと、インターフェースを引数として使う関数を実装出来ない。
public interface IObserver<T>
{
    //受信通知用
    void OnReceived(T value);
}
