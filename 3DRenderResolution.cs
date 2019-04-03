using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class 3DRenderResolution : MonoBehaviour {
    
    public Text RenderResText;
    public Slider RenderResSlider;

    //The RenderTexture
    public RawImage RenderTexImg;
    public Camera Cam;

    // A value between 0.0 and 1.0 indicating the percentage from the maximum resolution
    private float Percentage = 1.0f;

    //The screen's maximum resolution
    private int ScreenHeight = Screen.height;
    private int ScreenWidth = Screen.width;

    private string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    void Start () {
        if (Directory.Exists(AppData+@"\YourProjectHere"))
        {
            string GameDir = AppData + @"\YourProjectHere";

            if (File.Exists(GameDir + @"\options.ini"))
            {
                string[] SaveData = File.ReadAllLines(GameDir + @"\options.ini");
                if (float.Parse(SaveData[0]) >= 0.5f)
                {
                    if (float.Parse(SaveData[0]) <= 1f)
                    {
                        int RenderWidth = int.Parse(Mathf.Floor(ScreenWidth * float.Parse(SaveData[0])).ToString());
                        int RenderHeight = int.Parse(Mathf.Floor(ScreenHeight * float.Parse(SaveData[0])).ToString());

                        if (Cam.targetTexture != null)
                        {
                            Cam.targetTexture.Release();
                        }
                        Percentage = 0.5f;
                        RenderTexture RenderTex = new RenderTexture(RenderWidth, RenderHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
                        RenderTex.filterMode = FilterMode.Point;

                        RenderTexImg.texture = RenderTex;
                        Cam.targetTexture = RenderTex;

                        float percentTxt = Mathf.Round(RenderResSlider.value * 10) / 10;
                        if (percentTxt - Mathf.Floor(percentTxt) <= 0)
                        {
                            RenderResText.text = RenderWidth + " x " + RenderHeight + " ( " + percentTxt + ".0% )";
                        }
                        else
                        {
                            RenderResText.text = RenderWidth + " x " + RenderHeight + " ( " + percentTxt + "% )";
                        }
                    }
                    else
                    {
                        string[] l = { "1", "" };
                        File.WriteAllLines(GameDir + @"\options.ini", l);

                        if (Cam.targetTexture != null)
                        {
                            Cam.targetTexture.Release();
                        }
                        RenderTexture RenderTex = new RenderTexture(ScreenWidth, ScreenHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
                        RenderTex.filterMode = FilterMode.Point;

                        RenderTexImg.texture = RenderTex;
                        Cam.targetTexture = RenderTex;
                        RenderResText.text = Screen.width + " x " + Screen.height + " ( 100.0% )";
                    }
                }
                else{
                    string[] l = { "0.5", "" };
                    File.WriteAllLines(GameDir + @"\options.ini", l);

                    if (Cam.targetTexture != null)
                    {
                        Cam.targetTexture.Release();
                    }
                    Percentage = 0.5f;
                    RenderTexture RenderTex = new RenderTexture(ScreenWidth, ScreenHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
                    RenderTex.filterMode = FilterMode.Point;

                    RenderTexImg.texture = RenderTex;
                    Cam.targetTexture = RenderTex;
                    RenderResText.text = Screen.width + " x " + Screen.height + " ( 50.0% )";
                }
            }
            else
            {
                File.Create(GameDir + @"\options.ini");
                string[] l = { "1", "" };
                File.WriteAllLines(GameDir + @"\options.ini",l);

                if (Cam.targetTexture != null)
                {
                    Cam.targetTexture.Release();
                }

                RenderTexture RenderTex = new RenderTexture(ScreenWidth, ScreenHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
                RenderTex.filterMode = FilterMode.Point;

                RenderTexImg.texture = RenderTex;
                Cam.targetTexture = RenderTex;
                RenderResText.text = Screen.width + " x " + Screen.height + " ( 100.0% )";
            }
        }
        else
        {
            Directory.CreateDirectory(AppData + @"\YourProjectHere");
            File.Create(AppData + @"\YourProjectHere\options.ini");
            string[] l = { "1", "" };
            File.WriteAllLines(AppData + @"\YourProjectHere\options.ini", l);

            if (Cam.targetTexture != null)
            {
                Cam.targetTexture.Release();
            }

            RenderTexture RenderTex = new RenderTexture(ScreenWidth, ScreenHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
            RenderTex.filterMode = FilterMode.Point;

            RenderTexImg.texture = RenderTex;
            Cam.targetTexture = RenderTex;
            RenderResText.text = Screen.width + " x " + Screen.height + " ( 100.0% )";
        }
    }
	
	void Update () {
        ScreenHeight = Screen.height;
        ScreenWidth = Screen.width;

        //Change the RenderResolution
        string RH = Mathf.Floor(ScreenHeight * Percentage).ToString();
        string RW = Mathf.Floor(ScreenWidth * Percentage).ToString();

        int RenderHeight = int.Parse(RH);
        int RenderWidth = int.Parse(RW);

        if (Cam.targetTexture != null)
        {
            Cam.targetTexture.Release();
        }

        RenderTexture RenderTex = new RenderTexture(RenderWidth, RenderHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        RenderTex.filterMode = FilterMode.Point;

        RenderTexImg.texture = RenderTex;
        Cam.targetTexture = RenderTex;
    }

    /*
    * This method is attached to a slider's Change event
    */
    public void On3DResolutionChanged()
    {
        string RenderHeight = Mathf.Floor(ScreenHeight * RenderResSlider.value / 100).ToString();
        string RenderWidth  = Mathf.Floor(ScreenWidth * RenderResSlider.value  / 100).ToString();

        float percentTxt = Mathf.Round(RenderResSlider.value * 10) / 10;
        if (percentTxt-Mathf.Floor(percentTxt) <= 0){
            RenderResText.text = RenderWidth + " x " + RenderHeight + " ( " + percentTxt + ".0% )";
        }else{
            RenderResText.text = RenderWidth + " x " + RenderHeight + " ( " + percentTxt + "% )";
        }
    }

    public void ApplyChanges()
    {
        Percentage = RenderResSlider.value/100;
        print(Percentage);

        string[] l = { Percentage.ToString(), "" };
        File.WriteAllLines(AppData + @"\YourProjectHere\options.ini", l);

        string RH = Mathf.Floor(ScreenHeight * Percentage).ToString();
        string RW = Mathf.Floor(ScreenWidth  * Percentage).ToString();

        int RenderHeight = int.Parse(RH);
        int RenderWidth = int.Parse(RW);

        if (Cam.targetTexture != null)
        {
            Cam.targetTexture.Release();
        }

        RenderTexture RenderTex = new RenderTexture(RenderWidth, RenderHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        RenderTex.filterMode = FilterMode.Point;

        RenderTexImg.texture = RenderTex;
        Cam.targetTexture = RenderTex;
    }
}
