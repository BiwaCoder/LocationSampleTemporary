using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class GoogleMapLogic 
{
    static public string key = "";
    static public int zoom = 15;

     // Google Maps Embed API
    static string Url = @"https://maps.googleapis.com/maps/api/staticmap?";
    


    //地図情報の更新
    public static void Build(MonoBehaviour obj,Image MapImage){
        Url = "https://maps.googleapis.com/maps/api/staticmap?";
        // 中心座標 
        Url += "center=" + LocationDataLogic.Instance.Location.latitude + "," + LocationDataLogic.Instance.Location.longitude;

        // ズームレベル
        Url += "&zoom=" + zoom;

        // 地図画像のサイズ
        Url += "&size=640x640";

        if (key != null && key.Length != 0) {
            Url += "&key=" + key;
        }

        Url = Uri.EscapeUriString(Url);
        obj.StartCoroutine(Download(Url, tex => addSplatPrototype(tex,MapImage)));
    }

     /// GoogleMapsAPIから地図画像をダウンロードする
    static IEnumerator Download(string url, Action<Texture2D> callback) {
       //TODO 通信エラーのときの処理が必要


        var www = new WWW(url);
        yield return www; // Wait for download to complete

        callback(www.texture);
    }


    /// imageにテクスチャを貼り付ける
    public static void addSplatPrototype(Texture2D tex,Image MapImage) {
        MapImage.sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), Vector2.zero);
    }

}
