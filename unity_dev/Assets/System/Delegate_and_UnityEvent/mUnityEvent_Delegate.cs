using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Events; //UnityEvent

public class mUnityEvent_Delegate : MonoBehaviour
{
    public UnityEvent _onCompleteHandler = new();

    public async void CountDown()
    {
        Debug.Log("3");
        await UniTask.Delay(1000);  
        Debug.Log("2");
        await UniTask.Delay(1000);
        Debug.Log("1");
        await UniTask.Delay(1000);
        _onCompleteHandler.Invoke(); //発火。
    }
}
