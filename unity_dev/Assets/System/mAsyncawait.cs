using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class mAsyncawait : MonoBehaviour
{
    private bool _end = false;
    private float _count = 0.0f;

    void Start()
    {
        ASyncTests();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            var token = this.GetCancellationTokenOnDestroy();
            GenerateCube(token, _count).Forget();
            _count += 1.1f;
        }
    }

    private void ASyncTests()
    {
        //同じスレッドで並列処理。
        //awaitで待機しない場合は、Forget()で警告回避出来る。
        AsyncTest().Forget();
        AsyncTest2().Forget();

        ThreadTests().Forget();

        var cts = new CancellationTokenSource(); //token元を生成。
        //var token = cts.Token; //tokenを生成。
        var token = this.GetCancellationTokenOnDestroy(); //OnDestroy()時にキャンセルされるtokenを生成。
        AsyncTest_cancellation(token).Forget();
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

    private async UniTask ThreadTests()
    {
        //別スレッドで行う
        await UniTask.RunOnThreadPool(action: () =>
        {
            Debug.Log("threadTest.: " + Thread.CurrentThread.ManagedThreadId);
        });
    }

    private async UniTask AsyncTest_cancellation(CancellationToken token) //引数にtokenをもたせる。
    {
        while (true)
        {
            Debug.Log("cancellationTest1_start.: " + Thread.CurrentThread.ManagedThreadId);
            await UniTask.Delay(1000, cancellationToken: token); //tokenを引数に与える。(このawait以降は実行されない)
            Debug.Log("cancellationTest1_end.: " + Thread.CurrentThread.ManagedThreadId);
            await UniTask.Delay(500, cancellationToken: token); //同上
        }
    }


    private async UniTask GenerateCube(CancellationToken token, float posX)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(posX, 0.0f, 0.0f);

        await UniTask.Delay(3000, cancellationToken: token);

        Destroy(cube);
    }


    private void OnDestroy()
    {
        _end= true;
    }
}
