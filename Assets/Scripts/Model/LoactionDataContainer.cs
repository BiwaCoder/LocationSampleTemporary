using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoactionDataContainer 
{
     public List<LocationData> dataList = new List<LocationData>();

     //マッチしたデータを返す仕組みを実装する、複数あればどれを返す？
     //チェックインポイントの作成、チェックイン処理の実装

     public bool IsAlreadySaveSameData(float latitude,float longitude,string DateMonthDay){
           foreach(LocationData data in dataList){
                if(data.IsAlreadySaveSameData(latitude,longitude,DateMonthDay)){
                     return true;
                }
           }
           return false;
     }
}
