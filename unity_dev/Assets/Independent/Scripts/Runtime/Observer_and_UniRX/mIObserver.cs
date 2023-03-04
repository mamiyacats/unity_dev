using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//interfaceはpublicにしておかないと、インターフェースを引数として使う関数を実装出来ない。
public interface ImObserver<T>
{
    //値受信用
    void OnReceived(T value);
}
