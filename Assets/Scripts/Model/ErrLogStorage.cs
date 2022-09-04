using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ErrLogStorage :  SingletonMonoBehaviour<ErrLogStorage> 
{

    MyErrorHandler errHandler;
    public List<string> errMessageList = new List<string>();
    public int Index=0;

    void Start()
    {
        //エラーがあった時の受け取り
        errHandler = new MyErrorHandler();
        errHandler.ErrorMessageCheckStr.Skip(1).Subscribe(
            value => {
                 errMessageList.Add(value);
            }
         );
    }

    public string GetCurrentErrString(){
        if(0 <= Index && Index < errMessageList.Count){
           return errMessageList[Index];
        }
        else{
            return "NoData";
        }
    }

    public bool IsNext(){
        if(Index  < errMessageList.Count){
            return true;
        }
        else{
            return false;
        }
    }

    public bool IsBack(){
        if(1  <= Index){
            return true;
        }
        else{
            return false;
        }
    }

    public void GoNext(){
        ++Index;
    }



    public void GoBack(){
        --Index;
    }

    public string GetAllErrMessageString(){
        string retrunErrMessage = "";
        foreach(string errMessageData in errMessageList){
            retrunErrMessage += errMessageData + "\n";
        }
        return retrunErrMessage;
    }
}
