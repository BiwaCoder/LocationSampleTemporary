using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GeneralPopupController : MonoBehaviour
{

    public Button CloseButton;
    public Text PopupText;
    // Start is called before the first frame update

    private Action CloseCallback;

    public void InitPopup(string text,Action callback){
         PopupText.text = text;
         CloseCallback = callback;
         CloseButton.onClick.AddListener(OnclickCloseButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickCloseButton(){
        CloseCallback.Invoke();
        Destroy(this.gameObject);
    }
}
