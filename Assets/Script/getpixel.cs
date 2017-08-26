using UnityEngine;
using System.Collections;

public class getpixel : MonoBehaviour
{
    public int resWidth = 1280;
    public int resHeight = 720;
    public Camera cam;
    private Renderer render;
    private UnityEngine.Color prova;
    public Texture2D myGUITexture= new Texture2D(1280, 720,
                                                 TextureFormat.RGB24, true);
    
    private void Start()

    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5F, 0);
        Camera cam = GetComponent<Camera>();
        Renderer render = GetComponent<Renderer>();
        //myGUITexture = (Texture2D)Resources.Load("screen.png");
        if (myGUITexture == null)
        {
            Debug.Log("Null myGUITexture");
        }
    }
    void Update()
    {
     myGUITexture.ReadPixels(new Rect(0, 0, 1280, 720), 0, 0);
     Color prova = myGUITexture.GetPixel(10,10);

        if (Input.GetMouseButtonDown(0))
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            cam.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight,
                                                 TextureFormat.RGB24, true);
            cam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);


            
            Color ciao = screenShot.GetPixel(640,700);

           // string gesu = "RGBA(0.839, 0.839, 0.098, 1.000)";
            if (ciao==prova){
                Debug.Log("cvsdc");
                Instantiate(Cube);
            }
            else { Debug.Log(ciao);
                   Debug.Log(prova);}
           
            
                
         
            cam.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors 
            Destroy(rt);
            Destroy(screenShot);
    }
}
}

// Get the color of a pixel within myBitmap.
