using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System; //Action, Func

public class mDelegate : MonoBehaviour
{
    public delegate void OnCompleteDelegate(); //delegateを用いて型を定義する。
    //public OnCompleteDelegate _onComplete; //定義した型でメソッドを登録する用の変数を定義する。
    public event OnCompleteDelegate _onComplete; //event修飾子をつけることで、他クラスから発火させなくすることが出来る。基本つけた方が良い。

    public event Action _onAction; //戻り値無しの汎用デリゲート(1行で良い)

    public async void CountDown()
    {
        Debug.Log("3");
        await UniTask.Delay(1000);  
        Debug.Log("2");
        await UniTask.Delay(1000);
        Debug.Log("1");
        await UniTask.Delay(1000);
        _onComplete(); //発火（呼び出し）。
        _onAction(); //発火。
    }　
}
