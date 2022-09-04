using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationData 
{
    public float latitude;
    public float longitude;

    public string DateMonthDay;

    public string DetailDate;

    public string stateMessage;

    public string ImageFileName;


    public LocationData(float latitude,float longitude,string DateMonthDay,string DetailDate,string stateMessage,string _ImageFileName)
    {
        this.latitude = latitude;
        this.longitude = longitude;
        this.DateMonthDay = DateMonthDay;
        this.DetailDate = DetailDate;
        this.stateMessage = stateMessage;
        this.ImageFileName = _ImageFileName;
    }

    public void UpdateText(string text){
        this.stateMessage = text;
    }

    public bool IsAlreadySaveSameData(float latitude,float longitude,string DateMonthDay){
        if(this.latitude == latitude && this.longitude == longitude && this.DateMonthDay == DateMonthDay){
            Debug.Log("すでに同じデータがあります  {this.latitude} {this.longitude} {this.DateMonthDay}");
            return true;
        }
        else{
            return false;
        }

    }
}
