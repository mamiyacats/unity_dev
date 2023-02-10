using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mUnityEvent_Delegate_callback : MonoBehaviour
{

    mUnityEvent_Delegate _mUnityEvent_Delegate;

    private void Start()
    {
        _mUnityEvent_Delegate = new mUnityEvent_Delegate();

        //UnityEvent(onComplete)にメソッドを登録。
        //※delegateの場合はメソッドの後に()はいらない。
        _mUnityEvent_Delegate._onCompleteHandler.AddListener(Alarm);

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mUnityEvent_Delegate.CountDown();
        }
    }

    //登録するメソッド
    public void Alarm()
    {
        Debug.Log("Bang.");
    }
}
