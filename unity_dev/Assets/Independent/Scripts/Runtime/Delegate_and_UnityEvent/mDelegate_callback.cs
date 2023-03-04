using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mDelegate_callback : MonoBehaviour
{

    mDelegate _mDelegate;

    private void Start()
    {
        _mDelegate = new mDelegate();
        
        //delegate(onComplete)にメソッドを登録。
        //※delegateの場合はメソッドの後に()はいらない。
        _mDelegate._onComplete += Alarm;
        _mDelegate._onAction += Alarm2;
        _mDelegate._onAction += () => { Debug.Log("rambdaAction."); }; //ラムダ式
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
             _mDelegate.CountDown();
        }
    }

    //登録するメソッド
    public void Alarm()
    {
        Debug.Log("Bang.");
    }
    public void Alarm2()
    {
        Debug.Log("Dong.");
    }
}
