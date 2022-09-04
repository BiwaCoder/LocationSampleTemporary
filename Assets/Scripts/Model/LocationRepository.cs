using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocationRepository : SingletonMonoBehaviour<LocationRepository> 
{
    LoactionDataContainer locationDataList;


    private const string SAVE_FILE_NAME = "currentPos";

    void Start()
    {
        locationDataList = new LoactionDataContainer();

         //位置情報の読み出し
        if(JsonFileSaveSystem.IsExistFile(SAVE_FILE_NAME)){
            locationDataList = JsonFileSaveSystem.Load<LoactionDataContainer>(SAVE_FILE_NAME);
            Debug.Log(locationDataList.dataList.Count);
            foreach(LocationData data in locationDataList.dataList){
                Debug.Log($"LocationData:lat{data.latitude}lng{data.longitude}:");
            }
        }
    }

    public void SaveLocation(float lat,float lng){
        locationDataList.dataList.Add(new LocationData(lat,lng,DateTime.Now.ToString("yyyy/MM/dd"),DateTime.Now.ToString(),"",""));
        JsonFileSaveSystem.Save<LoactionDataContainer>(SAVE_FILE_NAME,locationDataList);
    }

    public bool IsAlreadySaveSameData(float lat,float lng){
        bool ret = locationDataList.IsAlreadySaveSameData(lat,lng,DateTime.Now.ToString("yyyy/MM/dd"));
        if(ret == true){
             PopupManger.Instance.CreateLocationStatePopup("すでに同じデータが存在します。");
        }
        return ret;
    }

}
