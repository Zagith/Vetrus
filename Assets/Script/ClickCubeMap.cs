using UnityEngine;
using System.Collections;

public class ClickCubeMap : MonoBehaviour
{
    public int resWidth = 1280;
    public int resHeight = 720;
    public Camera cam;
    private Renderer render;
    private void Start()
    {
        Camera cam = GetComponent<Camera>();
        Renderer render = GetComponent<Renderer>();
    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            cam.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight,
                                                 TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            cam.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors 
            Destroy(rt);

            Debug.Log(screenShot.GetPixel(640, 360));
            Color myColor = new Color(0.243f, 0.192f, 0.129f, 1f);

            if (screenShot.GetPixel(640, 360) == myColor)
            {
                Debug.Log("Purple!!!!!");
            }
        }

    }
}