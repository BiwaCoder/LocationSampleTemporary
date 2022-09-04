using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UniRx;

#if UNITY_ANDROID && UNITY_2018_3_OR_NEWER
using UnityEngine.Android;
#endif

public class LocationDataLogic : SingletonMonoBehaviour<LocationDataLogic> 
{
    public int IntervalMiliSeconds = 1000;

    //位置情報の取得状況
    public LocationServiceStatus Status;
    //位置情報の経度、緯度などの情報
    public LocationInfo Location;

    public void Init(){
        //位置情報サービス自体がオフになっているかをチェック
        if (!Input.location.isEnabledByUser)
        {
            PopupManger.Instance.CreateLocationStatePopup("設定-プライバシー-位置情報サービスで、位置情報サービスがオフにされています。");
        }

        
        StartCoroutine("checkPositon");
    }

    //位置情報サービスの有効化
    IEnumerator checkPositon(){
         //Androidの位置情報権限ダイアログの表示
         #if UNITY_ANDROID
            if (!Permission.HasUserAuthorizedPermission (Permission.CoarseLocation)) {
                Permission.RequestUserPermission(Permission.CoarseLocation);
            }else {
                Debug.Log("No CoarseLocation Permission");
                PopupManger.Instance.CreateLocationStatePopup("位置情報機能が無効化されています。設定から有効にしてください。");
            }
            if (!Permission.HasUserAuthorizedPermission (Permission.FineLocation) ) {
                Permission.RequestUserPermission(Permission.FineLocation);
            } else {
                Debug.Log("No FineLocation Permission");
                PopupManger.Instance.CreateLocationStatePopup("位置情報機能が無効化されています。設定から有効にしてください。");
            }
        #endif


        //位置情報の取得
        while (true)
        {
            this.Status = Input.location.status;
            //位置情報サービス自体がオフになっているかをチェック
            if (Input.location.isEnabledByUser)
            {
                switch(this.Status)
                {
                    //位置情報が無効状態なので有効に
                    case LocationServiceStatus.Stopped:
                        Input.location.Start();
                        break;
                    case LocationServiceStatus.Running:
                        //位置情報の取得、データの読み込み
                        this.Location = Input.location.lastData;
                        break;
                    case LocationServiceStatus.Failed: 
                        break;
                    default:
                        break;
                }
            }
            else{
                  this.Status = LocationServiceStatus.Failed;
            }
            yield return new WaitForSeconds(1);
        }
    }

    public LocationInfo GetCurrentPosData(){
        if(this.Status != LocationServiceStatus.Running){  
             PopupManger.Instance.CreateLocationStatePopup("位置情報機能が無効化されています。設定から有効にしてください。");
        } 
        return this.Location;
    }

    public string GetCurrentPosText()
    {
        string currentPosString="";
        if(this.Status == LocationServiceStatus.Running){
            currentPosString = Status.ToString()
                    + "\n" + "lat:" + this.Location.latitude.ToString()
                    + "\n" + "lng:" + this.Location.longitude.ToString();
        }
        else{
             PopupManger.Instance.CreateLocationStatePopup("位置情報機能が無効化されています。設定から有効にしてください。");
        }

        return currentPosString;
    }

}
