using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject PopupRoot;

    public void Start(){
        //LocationDataManager.Instance.Init();
    }

    public void OnClickMenuButton()
    {
        //エラーログ確認用のポップアップ表示
        PopupManger.Instance.CreateErrorLogPopup(ErrLogStorage.Instance.GetAllErrMessageString());
    }

    public void OnClickNewPosSaveButton(){
         SceneManager.LoadScene("PosSave");
    }

    public void OnClickPosEditButton(){
         SceneManager.LoadScene("PosEdit");
    }
    

}
