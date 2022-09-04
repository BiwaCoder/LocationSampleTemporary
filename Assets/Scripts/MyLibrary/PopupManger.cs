using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManger : SingletonMonoBehaviour<PopupManger> 
{
    public GameObject PopupRoot;
    public bool IsDisplayPopup = false;

    public bool IsDisplayPopup2 = false;


    

    //文字のみの表示
    public void CreateLocationStatePopup(string popupText,GameObject _PopupRoot=null){
        if(IsDisplayPopup == false){
            IsDisplayPopup = true;
            GameObject PoupuResource  = (GameObject) Resources.Load("GeneralPopUp");
            GameObject InsObj = Instantiate(PoupuResource);
            InsObj.GetComponent<GeneralPopupController>().InitPopup(popupText,() =>{IsDisplayPopup=false;});
            if(_PopupRoot != null){
                PopupRoot = _PopupRoot;
            }
            InsObj.transform.SetParent(PopupRoot.transform,false);
        }
    }

    //前後ボタンで表示できる内容を変更できるポップアップ
    public void CreateErrorLogPopup(string popupText,GameObject _PopupRoot=null){
        if(IsDisplayPopup2 == false){
            IsDisplayPopup2 = true;
            GameObject PoupuResource  = (GameObject) Resources.Load("GeneralPopUp");
            GameObject InnerResource  = (GameObject) Resources.Load("LogButton");

            GameObject InsObj = Instantiate(PoupuResource);
            GameObject InnerResourceObj = Instantiate(InnerResource);

            InsObj.GetComponent<GeneralPopupController>().InitPopup("",() => {IsDisplayPopup2=false;} );
            InnerResourceObj.GetComponent<LogButtonController>().SetParentPopupController( InsObj.GetComponent<GeneralPopupController>());
            
            if(_PopupRoot != null){
                PopupRoot = _PopupRoot;
            }
            InsObj.transform.SetParent(PopupRoot.transform,false);
            InnerResourceObj.transform.SetParent(InsObj.transform,false);
        }
    }
}
