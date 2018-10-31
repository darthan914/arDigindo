using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadBundle : MonoBehaviour {

    public Transform importTo;
    public Text messages;

    ArModels ArModels;

    // Use this for initialization
    void Awake () {
        if (importTo == null) return;

        ArModels = FindObjectOfType<ArModels>();

        //importTo.parent.gameObject.SetActive(false);

        string url = ArModels.assetBundle;
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
	}
	
	IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        AssetBundle bundle = www.assetBundle;

        if(www.error == null)
        {
            messages.enabled = false;
            //importTo.parent.gameObject.SetActive(true);
            importTo.parent.name = ArModels.nameProject;
            GameObject go = Instantiate((GameObject)bundle.LoadAsset(ArModels.nameGameObject));

            if (go.GetComponent<Renderer>()) go.GetComponent<Renderer>().enabled = false;
            if (go.GetComponent<Collider>()) go.GetComponent<Collider>().enabled = false;
            if (go.GetComponent<Canvas>()) go.GetComponent<Canvas>().enabled = false;

            var rendererComponents = go.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = go.GetComponentsInChildren<Collider>(true);
            var canvasComponents = go.GetComponentsInChildren<Canvas>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;

            go.transform.parent = importTo;
            go.AddComponent<AlignmentTools>();
            go.AddComponent<ScaleSize>();
        }
        else
        {
            messages.text = www.error;
        }
    }
}
