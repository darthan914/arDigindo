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

        StartCoroutine(WaitForRequest(ArModels.assetBundle));
	}
	
	IEnumerator WaitForRequest(string url)
    {
        while (!Caching.ready)
            yield return null;

        using (WWW www = WWW.LoadFromCacheOrDownload(url, 0))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                messages.text = www.error;
                yield return null;
            }
            

            var assetBundle = www.assetBundle;
            

            messages.enabled = false;

            importTo.name = ArModels.nameProject;
            GameObject go = Instantiate((GameObject)assetBundle.LoadAsset(ArModels.nameGameObject));

            go.transform.position = Vector3.zero;

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

            assetBundle.Unload(false);

        }
    }
}
