using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PosSaveController : MonoBehaviour
{
    public GameObject MapSaveButton;
    public GameObject ImageSelectButton;

    public Image MapImage;

    // Start is called before the first frame update
    void Start()
    {
          GoogleMapLogic.Build(this,MapImage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBackButton(){
          SceneManager.LoadScene("ViewScene");
    }


    public void OnClickSaveLocation(){
     
         float lat = LocationDataLogic.Instance.Location.latitude;
         float lng = LocationDataLogic.Instance.Location.longitude;
        


        if(LocationDataLogic.Instance.Location.longitude != 0 && LocationDataLogic.Instance.Location.latitude != 0){
            LocationRepository.Instance.IsAlreadySaveSameData(lat,lng);
            LocationRepository.Instance.SaveLocation(lat,lng);
        }
        else{
            LocationRepository.Instance.SaveLocation(lat,lng);
            PopupManger.Instance.CreateLocationStatePopup("位置情報を更新できませんでした。");
        }
        MapSaveButton.SetActive(false);
        ImageSelectButton.SetActive(true);
        MapImage.gameObject.SetActive(false);
      
    }


}
