using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mObservePattern_printer : MonoBehaviour
{
    private mSubject<string> _mSubject;
    private mObserver_print<string> _mObserver;

    private void Start()
    {
        //Subject作成
        _mSubject = new mSubject<string>();
        //Observer作成
        _mObserver = new mObserver_print<string>();

        //購読
        _mSubject.RegisterObserver(_mObserver);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //メッセージ発行
            _mSubject.SendMessage("Hello,");
            _mSubject.SendMessage("World.");
        }
    }

    private void OnDestroy()
    {
        //購読解除
        _mSubject.UnRegisterObserver(_mObserver); 
        
        //片付け
        _mSubject.Dispose();
        _mObserver = null;
    }
}
