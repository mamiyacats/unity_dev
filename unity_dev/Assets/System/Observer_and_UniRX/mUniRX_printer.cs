using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mUniRX_printer : MonoBehaviour
{
    IDisposable _disposable= null;
    UniRx.Subject<string> _subject = null;

    public void Start()
    {
        //Subject（管理者）作成
        _subject = new UniRx.Subject<string>();
        //Observer（被管理者）作成
        var observer = new mUniRX_Observer_print<string>();

        //購読
        _disposable = _subject.Subscribe(observer);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //メッセージ発行
            _subject.OnNext("Hello,");
            _subject.OnNext("World.");
        }   
        
        if(Input.GetMouseButtonDown(0))
        {
            UnSubscribe();
        }
    }

    private void UnSubscribe()
    {
        //購読解除
        _disposable.Dispose();
        //終了通知
        _subject.OnCompleted();
        //片付け
        _subject.Dispose();
    }
}
