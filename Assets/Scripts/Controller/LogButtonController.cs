using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogButtonController : MonoBehaviour
{

    public Button NextButton;
    public Button BackButton;

    public GeneralPopupController ParentPopUpController;

    public void SetParentPopupController(GeneralPopupController _ParentPopUpController){
        ParentPopUpController = _ParentPopUpController;
        ParentPopUpController.PopupText.text = ErrLogStorage.Instance.GetCurrentErrString();
    }

    void Start()
    {
        NextButton.onClick.AddListener(OnClickNext);
        BackButton.onClick.AddListener(OnClickBack);

        StartCoroutine(ButtonCheckLoop(1.0f));
    }

    private IEnumerator ButtonCheckLoop(float second) {
        // ループ
        while (true) {

        
            if(ErrLogStorage.Instance.IsNext()){
                NextButton.gameObject.SetActive(true);
            }
            else{
                NextButton.gameObject.SetActive(false);
            }

            if(ErrLogStorage.Instance.IsBack()){
                BackButton.gameObject.SetActive(true);
            }
            else{
                BackButton.gameObject.SetActive(false);
            }

                        // secondで指定した秒数ループします
            yield return new WaitForSeconds(second);

        }
}

    

    public void OnClickNext()
    {
        if(ErrLogStorage.Instance.IsNext()){
            ErrLogStorage.Instance.GoNext();
        }
        ParentPopUpController.PopupText.text = ErrLogStorage.Instance.GetCurrentErrString();
    }

    public void OnClickBack()
    {
        if(ErrLogStorage.Instance.IsBack()){
            ErrLogStorage.Instance.GoBack();
        }

         ParentPopUpController.PopupText.text = ErrLogStorage.Instance.GetCurrentErrString();
    }
}
