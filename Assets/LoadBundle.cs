using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBundle : MonoBehaviour {

    public Transform importTo;

	// Use this for initialization
	void Start () {
        if (importTo == null) return;

        string url = "http://127.0.0.1/arDigindoModel/good_mood_booth";
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
	}
	
	IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        AssetBundle bundle = www.assetBundle;

        if(www.error == null)
        {
            GameObject go = Instantiate((GameObject)bundle.LoadAsset("Good_Mood_Booth"));
            go.transform.parent = importTo;
            go.AddComponent<AlignmentTools>();
            go.AddComponent<ScaleSize>();
        }
        else
        {
            Debug.LogError(www.error);
        }
    }
}
