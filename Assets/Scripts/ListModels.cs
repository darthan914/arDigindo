using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using LitJson;

public class ListModels : MonoBehaviour {

    string url = "https://digindo.co.id/api/arModel";
    string baseurl = "https://digindo.co.id/";

    List<GameObject> listButton = new List<GameObject>();

    public GameObject buttonList;
    public Transform listPanel;

    public Text messages;
    public InputField find;

    private void Start()
    {
        ArModels sceneMaster = FindObjectOfType<ArModels>();
        find.text = sceneMaster.oldInput;
    }

    public void GetList()
    {
        messages.text = "Memuat...";
        ArModels sceneMaster = FindObjectOfType<ArModels>();

        sceneMaster.oldInput = find.text;

        StartCoroutine(SendRequest(find.text));
    }

    IEnumerator SendRequest(string findRequest)
    {
        WWWForm form = new WWWForm();
        form.AddField("find", findRequest);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                messages.text = www.error;
            }
            else
            {
                foreach(GameObject go in listButton)
                {
                    DestroyImmediate(go);
                }
                listButton.Clear();

                JsonData data = JsonMapper.ToObject(www.downloadHandler.text);

                for(int i = 0; i < data["data"]["index"].Count; i++)
                {
                    GameObject btn = Instantiate(buttonList);
                    listButton.Add(btn);
                    btn.transform.SetParent(listPanel == null ? transform : listPanel);
                    btn.transform.localScale = Vector3.one;
                    btn.transform.Find("Text").GetComponent<Text>().text = data["data"]["index"][i]["name"].GetString();
                    btn.transform.GetComponent<GoToScene>().nameProject = data["data"]["index"][i]["name"].GetString();
                    btn.transform.GetComponent<GoToScene>().nameGameObject = data["data"]["index"][i]["name_game_object"].GetString();
                    btn.transform.GetComponent<GoToScene>().assetBundle = baseurl + data["data"]["index"][i]["asset_bundle"].GetString();

                    
                }

                if(data["data"]["index"].Count == 0)
                {
                    messages.text = "Tidak Ada Data";
                }
                else
                {
                    messages.text = "Data Ditemukan. Total : " + data["data"]["index"].Count.ToString();
                }
            }
        }
    }
}