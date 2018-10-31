using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {

    public string nameProject;
    public string nameGameObject;
    public string assetBundle;

    public void GoTo(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void GoTo(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void GotoFromData()
    {
        ArModels sceneMaster = FindObjectOfType<ArModels>();
        sceneMaster.nameProject = nameProject;
        sceneMaster.nameGameObject = nameGameObject;
        sceneMaster.assetBundle = assetBundle;

        SceneManager.LoadScene("Importer");
    }
}
