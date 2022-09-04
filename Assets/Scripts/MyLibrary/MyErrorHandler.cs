using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MyErrorHandler
{
    private ReactiveProperty<string> errMessage = new ReactiveProperty<string>("");
    public IReadOnlyReactiveProperty<string> ErrorMessageCheckStr
    {
        
        get { return errMessage; }
    }

    public void SetErrorMessage(string value)
    {
        errMessage.Value = value;
    }

    public MyErrorHandler(){
        Application.logMessageReceived += LogCallback;    
    }

   /// <summary>
    /// ログを取得するコールバック
    /// </summary>
    /// <param name="condition">メッセージ</param>
    /// <param name="stackTrace">コールスタック</param>
    /// <param name="type">ログの種類</param>
    public void LogCallback(string condition, string stackTrace, LogType type) {
        SetErrorMessage($"Err:{condition} {stackTrace}");
    }
}
