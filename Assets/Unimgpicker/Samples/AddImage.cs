using Kakera;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

// WebGLで使うときはIPointerDownHandlerを継承する必要がある点に注意
public class AddImage : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Unimgpicker imagePicker;
    [SerializeField] private Image ssImage;
    public Texture2D texture;
    public Sprite texture2;

    private void Start()
    {
        if (!ssImage)
        {
            ssImage = gameObject.GetComponent<Image>();
        }
        ssImage.preserveAspect = true;
    }

#if !UNITY_WEBGL
    private void Awake()
    {
        //イメージピッカーの選択処理が終わった時のコールバック
        imagePicker.Completed += path => StartCoroutine(LoadImage(path, ssImage));
    }

    //カメラコントローラーの設定
    public void OnPressShowPicker()
    {
        imagePicker.Show("Select Image", "unimgpicker");//1024→512に変更
    }

    private IEnumerator LoadImage(string path, Image output)
    {
        string url = "file://" + path;
        WWW www = new WWW(url);
        yield return www;

        texture = www.texture;


        Debug.Log("Original"+texture.width);
        Debug.Log("Original"+texture.height);
        
        // まずリサイズ
        int _CompressRate = TextureCompressionRate.TextureCompressionRatio(texture.width, texture.height);
        TextureScale.Bilinear(texture, texture.width / _CompressRate, texture.height / _CompressRate);


        // 次に圧縮(縦長・横長すぎると使えない場合があるようです。) -> https://forum.unity.com/threads/strange-error-message-miplevel-m_mipcount.441907/
        //texture.Compress(false);

        // Spriteに変換して使用する
        texture2 = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        output.overrideSprite = texture2;
        //画像のサイズのリサイズ
        ssImage.rectTransform.sizeDelta = new Vector2(texture.width,texture.height);

        //見せ方検討する

        //シテイパスにファイルを保存する。
        //画像を保存、パスを記録する
        ConvertToPngAndSave(GetSavePath("LocalPNGData"),texture2);
    }

        ///  /// <summary>
    /// 保存先のパス取得
    /// </summary>
    /// <param name="folderName">区切りのフォルダ名</param>
    /// <returns>保存先のパス</returns>
    private string GetSavePath(string folderName)
    {
        string directoryPath = Application.persistentDataPath + "/" + folderName + "/";

        if (!Directory.Exists(directoryPath))
        {
            //まだ存在してなかったら作成
            Directory.CreateDirectory(directoryPath);
            return directoryPath + "paint.png";
        }

        return directoryPath + "paint.png";
    }



    /// <summary>
    /// 画像に変換＆保存
    /// </summary>
    private void ConvertToPngAndSave(string path, Sprite localSprite)
    {
        //Pngに変換
        byte[] bytes = localSprite.texture.EncodeToPNG();
        //保存
        File.WriteAllBytes(path, bytes);
    }

    public void LoadSavedImage(){
        ConvertToTextureAndLoad(GetSavePath("LocalPNGData"));
    }

    //セーブデータが存在する時true
    public static bool IsExistFile(string filePath){
        FileInfo info = new FileInfo(filePath);
        if (info.Exists)
        {
            return true;
        }
        else{
            return false;
        }
    }

    /// <summary>
    /// テクスチャに変換＆読み込み
    /// </summary>
    public void ConvertToTextureAndLoad(string path)
    {
        if(!IsExistFile(path)){
            return;
        }
        //読み込み
        byte[] bytes = File.ReadAllBytes(path);
        //画像をテクスチャに変換
        Texture2D loadTexture = new Texture2D(2, 2); 
        loadTexture.LoadImage(bytes);
        //テクスチャをスプライトに変換
        //output.sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), Vector2.zero);
        ssImage.overrideSprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), Vector2.zero);
    }


    // WebGLを使わない場合はこの関数(OnPointerDown)は不要
    public void OnPointerDown(PointerEventData eventData) { }

#elif UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData)
    {
        UploadFile(gameObject.name, "OnFileUpload", ".png, .PNG, .jpg, .jpeg", false);
    }

    // Called from browser
    public void OnFileUpload(string url)
    {
        StartCoroutine(OutputRoutine(url));
    }

    private IEnumerator OutputRoutine(string url)
    {
        var www = new WWW(url);
        yield return www;

        texture = www.texture;
        // まずリサイズ
        int _CompressRate = TextureCompressionRate.TextureCompressionRatio(texture.width, texture.height);
        TextureScale.Bilinear(texture, texture.width / _CompressRate, texture.height / _CompressRate);
        // 次に圧縮(縦長・横長すぎると使えない場合があるようです。) -> https://forum.unity.com/threads/strange-error-message-miplevel-m_mipcount.441907/
        //texture.Compress(false);
        // Spriteに変換して使用する
        texture2 = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        ssImage.overrideSprite = texture2;
    }
#endif
}


public static class TextureCompressionRate
{
    /// <summary>
    /// Textureが500x500に収まるようにリサイズします
    /// </summary>
    public static int TextureCompressionRatio(int width, int height)
    {
        if (width >= height)
        {
            if (width / 200 > 0) return (width / 200);
            else return 1;
        }
        else if (width < height)
        {
            if (height / 266 > 0) return (height / 266);
            else return 1;
        }
        else return 1;
        /*
        if (width >= height)
        {
            if (width / 500 > 0) return (width / 500);
            else return 1;
        }
        else if (width < height)
        {
            if (height / 500 > 0) return (height / 500);
            else return 1;
        }
        else return 1;*/
    }
}