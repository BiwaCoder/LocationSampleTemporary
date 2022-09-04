using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GoogleMapController : MonoBehaviour
{
    public Image MapImage;
    //現在位置の表示
    public void OnClickAdjustCurrentPos()
    {
        if(LocationDataLogic.Instance.Location.longitude == 0 && LocationDataLogic.Instance.Location.latitude == 0){
            PopupManger.Instance.CreateLocationStatePopup("位置情報を更新できませんでした。");
        }

        GoogleMapLogic.Build(this,MapImage);
    }

    // Start is called before the first frame update
    void Start()
    {
        GoogleMapLogic.Build(this,MapImage);
    }


}
