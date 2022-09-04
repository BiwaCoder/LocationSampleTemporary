using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Text;

public class JsonFileSaveSystem
{
    public const string SAVE_FILE_BASE = "/Assets/Resources/Location/";

    public static DirectoryInfo SafeCreateDirectory(string path)
    {
        //ディレクトリが存在しているかの確認 なければ生成
        if (Directory.Exists(path))
        {
            return null;
        }
        return Directory.CreateDirectory(path);
    }

    
    public static void Save<T>(string fileName,T listContainer){
      
            string json = JsonConvert.SerializeObject(listContainer);
            StreamWriter writer ;
            #if UNITY_EDITOR
                string path = Directory.GetCurrentDirectory();
            #else
                //string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
                string path =  UnityEngine.Application.persistentDataPath;
                SafeCreateDirectory(path  + SAVE_FILE_BASE);   
            #endif

            path +=  (SAVE_FILE_BASE + fileName+".json");
            var fs = new FileStream( path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite );
            writer = new StreamWriter (fs,Encoding.GetEncoding("utf-8"));

            writer.WriteLine (json);
            writer.Flush ();
            writer.Close ();
    }

    public static T Load<T>(string fileName){
        T retValue;
   
        #if UNITY_EDITOR
            string path = Directory.GetCurrentDirectory();
        #else
            //string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            string path =  UnityEngine.Application.persistentDataPath;
        #endif
        path +=  (SAVE_FILE_BASE + fileName+".json");

       
        if (File.Exists(path))
        {
             //FileInfo info = new FileInfo(path + SAVE_FILE_BASE+ fileName+".json");
            using( var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite )){
                StreamReader reader = new StreamReader (fs,Encoding.GetEncoding("utf-8"));
                string json = reader.ReadToEnd ();
                retValue = JsonConvert.DeserializeObject<T>(json);
            }
            
            return retValue;
        }
        else{
            return default(T);
        }
      
    }

    //セーブデータが存在する時true
    public static bool IsExistFile(string fileName){
        #if UNITY_EDITOR
            string path = Directory.GetCurrentDirectory();
        #else
            //string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            string path =  UnityEngine.Application.persistentDataPath;
        #endif
        
        FileInfo info = new FileInfo(path + SAVE_FILE_BASE+ fileName+".json");
        if (info.Exists)
        {
            return true;
        }
        else{
            return false;
        }
    }
}