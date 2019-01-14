using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectController))]
public class ScaleSize : MonoBehaviour {

    public Vector3 originDimension = Vector3.one;
    public Vector3 modifiedDimension;
    public Vector3 modifiedDimensionPercent;
    public Vector3 initScale;

    [Range(1, 100)] public float globalScale = 1;

    Bounds bounds;

    // Use this for initialization
    void Start () {
        SetInit();
    }

    public void GetSizeBounds()
    {
        originDimension = GetComponent<ObjectController>().GetSize() * 100;
    }

    Vector3 ScaleModifier()
    {
        float x;
        float y;
        float z;

        x = initScale.x * (modifiedDimension.x / originDimension.x);
        y = initScale.y * (modifiedDimension.y / originDimension.y);
        z = initScale.z * (modifiedDimension.z / originDimension.z);

        return new Vector3(x,y,z);
    }

    public void ResetScale()
    {
        modifiedDimension = originDimension;
        transform.localScale = initScale;
        globalScale = 1;

        transform.localScale = ScaleModifier() / globalScale;
    }

    public void ValueChange(Vector3 dimension, float globalScale2, string proposionalOn, bool proposional)
    {
        Debug.Log("Value Change " + proposionalOn);
        float x = dimension.x;
        float y = dimension.y;
        float z = dimension.z;
        float global = globalScale2;

        if (proposional)
        {
            switch(proposionalOn)
            {
                case "x":
                    y = (x * modifiedDimension.y) / modifiedDimension.x;
                    z = (x * modifiedDimension.z) / modifiedDimension.x;
                    break;
                case "y":
                    x = (y * modifiedDimension.x) / modifiedDimension.y;
                    z = (y * modifiedDimension.z) / modifiedDimension.y;
                    break;
                case "z":
                    x = (z * modifiedDimension.x) / modifiedDimension.z;
                    y = (z * modifiedDimension.y) / modifiedDimension.z;
                    break;
                default:
                    break;
            }
        }

        modifiedDimension = new Vector3(x, y, z);
        modifiedDimensionPercent = new Vector3(modifiedDimension.x / originDimension.x, modifiedDimension.y / originDimension.y, modifiedDimension.z / originDimension.z) * 100;
        globalScale = global;

        transform.localScale = ScaleModifier() / globalScale;

        GetComponent<AlignmentTools>().Snap();
    }

    public void ValueChangePercent(Vector3 dimension, float globalScale2, string proposionalOn, bool proposional)
    {
        Debug.Log("Value Change " + proposionalOn);
        float x = originDimension.x * (dimension.x / 100);
        float y = originDimension.y * (dimension.y / 100);
        float z = originDimension.z * (dimension.z / 100);
        float global = globalScale2;

        if (proposional)
        {
            switch (proposionalOn)
            {
                case "x":
                    y = (x * modifiedDimension.y) / modifiedDimension.x;
                    z = (x * modifiedDimension.z) / modifiedDimension.x;
                    break;
                case "y":
                    x = (y * modifiedDimension.x) / modifiedDimension.y;
                    z = (y * modifiedDimension.z) / modifiedDimension.y;
                    break;
                case "z":
                    x = (z * modifiedDimension.x) / modifiedDimension.z;
                    y = (z * modifiedDimension.y) / modifiedDimension.z;
                    break;
                default:
                    break;
            }
        }

        modifiedDimension = new Vector3(x, y, z);
        modifiedDimensionPercent = new Vector3(modifiedDimension.x / originDimension.x, modifiedDimension.y / originDimension.y, modifiedDimension.z / originDimension.z) * 100;
        globalScale = global;

        transform.localScale = ScaleModifier() / globalScale;

        GetComponent<AlignmentTools>().Snap();
    }

    public void SetInit()
    {
        GetSizeBounds();
        modifiedDimension = originDimension;
        modifiedDimensionPercent = Vector3.one * 100;
        initScale = transform.localScale;

        // GetComponent<AlignmentTools>().Snap();
    }

    void DebugBounds(Bounds bounds)
    {
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y, bounds.min.z), new Vector3(bounds.max.x, bounds.min.y, bounds.min.z));
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.min.y, bounds.min.z), new Vector3(bounds.max.x, bounds.max.y, bounds.min.z));
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.max.y, bounds.min.z), new Vector3(bounds.min.x, bounds.max.y, bounds.min.z));
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.max.y, bounds.min.z), new Vector3(bounds.min.x, bounds.min.y, bounds.min.z));

        Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y, bounds.max.z), new Vector3(bounds.max.x, bounds.min.y, bounds.max.z));
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.min.y, bounds.max.z), new Vector3(bounds.max.x, bounds.max.y, bounds.max.z));
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.max.y, bounds.max.z), new Vector3(bounds.min.x, bounds.max.y, bounds.max.z));
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.max.y, bounds.max.z), new Vector3(bounds.min.x, bounds.min.y, bounds.max.z));

        Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y, bounds.min.z), new Vector3(bounds.min.x, bounds.min.y, bounds.max.z));
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.min.y, bounds.min.z), new Vector3(bounds.max.x, bounds.min.y, bounds.max.z));
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.max.y, bounds.min.z), new Vector3(bounds.max.x, bounds.max.y, bounds.max.z));
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.max.y, bounds.min.z), new Vector3(bounds.min.x, bounds.max.y, bounds.max.z));
    }

    void DebugBounds(Bounds bounds, Color color)
    {
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y, bounds.min.z), new Vector3(bounds.max.x, bounds.min.y, bounds.min.z), color);
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.min.y, bounds.min.z), new Vector3(bounds.max.x, bounds.max.y, bounds.min.z), color);
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.max.y, bounds.min.z), new Vector3(bounds.min.x, bounds.max.y, bounds.min.z), color);
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.max.y, bounds.min.z), new Vector3(bounds.min.x, bounds.min.y, bounds.min.z), color);

        Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y, bounds.max.z), new Vector3(bounds.max.x, bounds.min.y, bounds.max.z), color);
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.min.y, bounds.max.z), new Vector3(bounds.max.x, bounds.max.y, bounds.max.z), color);
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.max.y, bounds.max.z), new Vector3(bounds.min.x, bounds.max.y, bounds.max.z), color);
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.max.y, bounds.max.z), new Vector3(bounds.min.x, bounds.min.y, bounds.max.z), color);

        Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y, bounds.min.z), new Vector3(bounds.min.x, bounds.min.y, bounds.max.z), color);
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.min.y, bounds.min.z), new Vector3(bounds.max.x, bounds.min.y, bounds.max.z), color);
        Debug.DrawLine(new Vector3(bounds.max.x, bounds.max.y, bounds.min.z), new Vector3(bounds.max.x, bounds.max.y, bounds.max.z), color);
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.max.y, bounds.min.z), new Vector3(bounds.min.x, bounds.max.y, bounds.max.z), color);
    }
}
