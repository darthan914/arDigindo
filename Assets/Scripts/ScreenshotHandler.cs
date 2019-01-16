using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour {


    static ScreenshotHandler instance;

    Camera myCamera;
    bool takeScreenshootNextFrame;
    string location;


    private void Awake()
    {
        instance = this;
        myCamera = GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takeScreenshootNextFrame)
        {
            takeScreenshootNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            location = Application.persistentDataPath + "/" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            System.IO.File.WriteAllBytes(location, byteArray);

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }

    void TakeScreenshot(int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshootNextFrame = true;
    }

    public static string TakeScreenshot_Static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
        return "Saved";
    }
}
