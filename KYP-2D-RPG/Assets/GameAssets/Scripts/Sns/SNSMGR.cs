using UnityEngine;
using System.Collections;
using System.IO;

public class SNSMGR : MonoBehaviour {

    public static SNSMGR instance = null;
    private bool isProcessing = false;
    //public Image buttonShare;
    public string mensaje;
    // Use this for initialization
    void Awake()
    {
        if (SNSMGR.instance == null)
        {
            SNSMGR.instance = this;
        }
        else
        {
            return;
        }

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShareText()
    {
#if UNITY_ANDROID

        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        // put buffer into texture
        screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
        // apply
        screenTexture.Apply();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        byte[] dataToSave = screenTexture.EncodeToPNG();
        string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
        File.WriteAllBytes(destination, dataToSave);
        if (!Application.isEditor)
        {
            // block to open the file and share it ------------START
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "" + mensaje);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");
            //intentObject.Call<AndroidJavaObject>("createChooser", );

            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            // reset
            AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
            currentActivity.Call("startActivity", jChooser);

            //currentActivity.Call("startActivity", intentObject);
        }
        isProcessing = false;

        //buttonShare.enabled = false;
        //if (!isProcessing)
        //{
        //    StartCoroutine(ShareScreenshot());
        //}
        //Application.CaptureScreenshot("Screenshot.png");


        ////instantiate the class Intent
        //AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");

        ////instantiate the object Intent
        //AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

        ////call setAction setting ACTION_SEND as parameter
        //intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

        ////instantiate the class Uri
        //AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");

        ////instantiate the object Uri with the parse of the url's file
        //AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", Application.persistentDataPath + "/Screenshot.png");

        ////call putExtra with the uri object of the file
        //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

        ////set the type of file
        //intentObject.Call<AndroidJavaObject>("setType", "image/png");

        ////instantiate the class UnityPlayer
        //AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        ////instantiate the object currentActivity
        //AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

        ////call the activity with our Intent
        //currentActivity.Call("startActivity", intentObject);
#endif
    }

    public IEnumerator ShareScreenshot()
    {
        isProcessing = true;
        // wait for graphics to render
        yield return new WaitForEndOfFrame();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        // create the texture
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        // put buffer into texture
        screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
        // apply
        screenTexture.Apply();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        byte[] dataToSave = screenTexture.EncodeToPNG();
        string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
        File.WriteAllBytes(destination, dataToSave);
        if (!Application.isEditor)
        {
            // block to open the file and share it ------------START
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "" + mensaje);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            currentActivity.Call("startActivity", intentObject);
        }
        isProcessing = false;
        //buttonShare.enabled = true;
    }

    void ShareImage()
    {
#if UNITY_ANDROID
        //Application.CaptureScreenshot("Screenshot.png");
        //Application.persistentDataPath 
        //byte[] bytes = MyImage.EncodeToPNG();
        //string path = Application.persistentDataPath + "/MyImage.png";
        //File.WriteAllBytes(path, bytes);
        //AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        //intentObject.Call("setAction", intentClass.GetStatic("ACTION_SEND"));
        //intentObject.Call("setType", "image/*");
        //intentObject.Call("putExtra", intentClass.GetStatic("EXTRA_SUBJECT"), "Media Sharing ");
        //intentObject.Call("putExtra", intentClass.GetStatic("EXTRA_TITLE"), "Media Sharing ");
        //intentObject.Call("putExtra", intentClass.GetStatic("EXTRA_TEXT"), "Media Sharing Android Demo");
        //AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        //AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");
        //AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", path);// Set Image Path Here
        //AndroidJavaObject uriObject = uriClass.CallStatic("fromFile", fileObject);
        //// string uriPath = uriObject.Call("getPath");
        //bool fileExist = fileObject.Call("exists");
        //Debug.Log("File exist : " + fileExist);
        //// Attach image to intent
        //if (fileExist)
        //    intentObject.Call("putExtra", intentClass.GetStatic("EXTRA_STREAM"), uriObject);
        //AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //AndroidJavaObject currentActivity = unity.GetStatic("currentActivity");
        //currentActivity.Call("startActivity", intentObject);
#endif
    }

}
