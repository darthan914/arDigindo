using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArModels : MonoBehaviour {

    public string oldInput;
    public string nameProject;
    public string nameGameObject;
    public string assetBundle;

    private void Awake()
    {
        if(FindObjectsOfType<ArModels>().Length > 1)
        {
            FindObjectsOfType<ArModels>()[1].oldInput = FindObjectsOfType<ArModels>()[0].oldInput;
            DestroyImmediate(FindObjectsOfType<ArModels>()[0].gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }
}
