using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public List<GameObject> targets = new List<GameObject>();
    public Transform targetRotate;

    public Text nameText;
    public Text scaleText;

    public InputField xInput;
    public InputField yInput;
    public InputField zInput;

    public InputField xInputPercent;
    public InputField yInputPercent;
    public InputField zInputPercent;

    public Scrollbar rotation;

    public InputField scaleInput;

    public Toggle proposional;

    public Dropdown list;

    private int index;
    [HideInInspector] public Vector3 initPosition;
    [HideInInspector] public Quaternion initRotation;
    [HideInInspector] public Vector3 initScale;

    private void Start()
    {
        xInput.onEndEdit.AddListener(delegate { ValueChange("x"); });
        yInput.onEndEdit.AddListener(delegate { ValueChange("y"); });
        zInput.onEndEdit.AddListener(delegate { ValueChange("z"); });

        xInputPercent.onEndEdit.AddListener(delegate { ValueChangePercent("x"); });
        yInputPercent.onEndEdit.AddListener(delegate { ValueChangePercent("y"); });
        zInputPercent.onEndEdit.AddListener(delegate { ValueChangePercent("z"); });

        scaleInput.onEndEdit.AddListener(delegate { ValueChange(""); });

        list.onValueChanged.AddListener(delegate { SetIndexController(); });

        initPosition = targetRotate.localPosition;
        initRotation = targetRotate.localRotation;
        initScale = targetRotate.localScale;

        ClearInputObject();
    }

    private void Update()
    {
        targetRotate.Rotate(Vector3.up, Mathf.Lerp(3, -3, rotation.value));

        
        //targetRotate.rotation = Quaternion.Euler(targetRotate.parent.eulerAngles.x, Mathf.Lerp(-360, 360, rotation.value), targetRotate.parent.eulerAngles.z);
    }

    public void ScreenShot()
    {
        ScreenCapture.CaptureScreenshot("arDigindo-" + System.DateTime.Now.ToString());
    }

    public void ResetScrollbar()
    {
        rotation.value = 0.5f;
    }


    public void AddToController(GameObject go)
    {
        if(targets.Count == 0)
        {
            targets.Add(go);
            index = 0;
            SyncInputObject();
        }
        else
        {
            if(!targets.Contains(go)) targets.Add(go);
        }

        UpdateOption();
    }

    public void RemoveToController(GameObject go)
    {
        if (targets.IndexOf(go) <= index)
        {
            if(index > 0)
            {
                index--;
                SyncInputObject();
            }
            else
            {
                ClearInputObject();
            }
        }

        targets.Remove(go);
        UpdateOption();
    }

    public void SetIndexController()
    {
        index = list.value;
        SyncInputObject();
    }

    void UpdateOption()
    {
        list.ClearOptions();

        List<string> nameList = new List<string>();

        foreach(GameObject target in targets)
        {
            nameList.Add(target.name);
        }

        list.AddOptions(nameList);
    }

    void SyncInputObject()
    {
        if (targets[index] == null || targets.Count == 0) return;

        nameText.text = targets[index].name;
        scaleText.text = "1 : " + targets[index].GetComponentInChildren<ScaleSize>().globalScale.ToString();

        xInput.text = targets[index].GetComponentInChildren<ScaleSize>().modifiedDimension.x.ToString();
        yInput.text = targets[index].GetComponentInChildren<ScaleSize>().modifiedDimension.y.ToString();
        zInput.text = targets[index].GetComponentInChildren<ScaleSize>().modifiedDimension.z.ToString();

        xInputPercent.text = targets[index].GetComponentInChildren<ScaleSize>().modifiedDimensionPercent.x.ToString();
        yInputPercent.text = targets[index].GetComponentInChildren<ScaleSize>().modifiedDimensionPercent.y.ToString();
        zInputPercent.text = targets[index].GetComponentInChildren<ScaleSize>().modifiedDimensionPercent.z.ToString();

        scaleInput.text = targets[index].GetComponentInChildren<ScaleSize>().globalScale.ToString();
    }

    void ClearInputObject()
    {
        if(nameText) nameText.text = "Searching...";
        if(scaleText) scaleText.text = "- : -";

        xInput.text = "";
        yInput.text = "";
        zInput.text = "";

        xInputPercent.text = "";
        yInputPercent.text = "";
        zInputPercent.text = "";

        scaleInput.text = "";
    }

    public void Alignment(int alignment)
    {
        if (targets[index] == null || targets.Count == 0) return;

        targets[index].GetComponentInChildren<AlignmentTools>().Snap(alignment);
    }

    public void ResetObject()
    {
        if (targets[index] == null || targets.Count == 0) return;

        targets[index].GetComponentInChildren<ScaleSize>().ResetScale();
        targets[index].GetComponentInChildren<AlignmentTools>().Snap(5);

        targetRotate.localPosition = initPosition;
        targetRotate.localRotation = initRotation;
        targetRotate.localScale = targets[index].GetComponentInChildren<ScaleSize>().initScale;

        SyncInputObject();
    }

    void ValueChange(string on)
    {
        if (targets[index] == null || targets.Count == 0) return;

        float x = float.Parse(xInput.text == "" ? "0" : xInput.text);
        float y = float.Parse(yInput.text == "" ? "0" : yInput.text);
        float z = float.Parse(zInput.text == "" ? "0" : zInput.text);

        float scale = float.Parse(scaleInput.text == "" ? "1" : scaleInput.text);

        Vector3 dimension = new Vector3(x,y,z);

        targets[index].GetComponentInChildren<ScaleSize>().ValueChange(dimension, scale, on, proposional.isOn);

        SyncInputObject();
    }


    void ValueChangePercent(string on)
    {
        if (targets[index] == null || targets.Count == 0) return;

        float x = float.Parse(xInputPercent.text == "" ? "0.00001" : xInputPercent.text);
        float y = float.Parse(yInputPercent.text == "" ? "0.00001" : yInputPercent.text);
        float z = float.Parse(zInputPercent.text == "" ? "0.00001" : zInputPercent.text);

        float scale = float.Parse(scaleInput.text == "" ? "1" : scaleInput.text);

        Vector3 dimension = new Vector3(x, y, z);

        targets[index].GetComponentInChildren<ScaleSize>().ValueChangePercent(dimension, scale, on, proposional.isOn);

        SyncInputObject();
    }
}
