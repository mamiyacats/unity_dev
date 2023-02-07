using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

public class Asyncawait : MonoBehaviour
{
    private bool _end = false;

    void Start()
    {
        ASyncTests();
    }

    private void ASyncTests()
    {
        //同じスレッドで並列処理。
        //awaitで待機しない場合は、Forget()で警告回避出来る。
        AsyncTest().Forget();
        AsyncTest2().Forget();

        ThreadTests().Forget();
    }

    //基本的に型はUniTaskにする。(呼び出しの一番大本だけがasync voidにして良い)
    private async UniTask AsyncTest()
    {
        while (true)
        {
            Debug.Log("baseTest1_start.: " + Thread.CurrentThread.ManagedThreadId);
            await UniTask.Delay(5000); //5秒処理をここで停止。
            Debug.Log("baseTest1_end.: " + Thread.CurrentThread.ManagedThreadId); //上と同じスレッドで実行されている。
            await UniTask.Delay(1000); //1秒処理をここで停止。

            if (_end == true) break; //breakで抜けないとPlayを終えてもずっと処理がループされて終わらない。
        }
    }

    private async UniTask AsyncTest2()
    {
        while (true)
        {
            Debug.Log("baseTest2_start.: " + Thread.CurrentThread.ManagedThreadId);
            await UniTask.Delay(3000);
            Debug.Log("baseTest2_end.: " + Thread.CurrentThread.ManagedThreadId);
            await UniTask.Delay(500);

            if (_end == true) break;
        }
    }

    private async UniTask<string> AsyncTest_return()
    {
        while (true)
        {
            Debug.Log("returnTest1_start: " + Thread.CurrentThread.ManagedThreadId);
            await UniTask.Delay(2000);
            Debug.Log("returnTest1_end.: " + Thread.CurrentThread.ManagedThreadId);
            await UniTask.Delay(1000);

            if (_end == true) return "returnTest";
        }
    }

    [System.Obsolete]
    private async UniTask ThreadTests()
    {
        //別スレッドで行う
        await UniTask.Run(action: () => 
        {
            Debug.Log("threadTest.: " + Thread.CurrentThread.ManagedThreadId);
        });
    }


    private void OnDestroy()
    {
        _end= true;
    }
}
