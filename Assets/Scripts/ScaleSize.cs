using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleSize : MonoBehaviour {

    public bool autoBounds = true;

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

    private void Update()
    {
        DebugBounds(bounds, Color.yellow);
    }

    public void GetSizeBounds()
    {
        originDimension = (bounds.size * transform.parent.parent.localScale.x * 100 * transform.localScale.x);
    }

    public void RecalculateBoundsCollider()
    {
        if (GetComponent<Collider>() == null)
        {
            bounds = new Bounds(Vector3.zero, Vector3.zero);
        }
        else
        {
            bounds = GetComponent<Collider>().bounds;
        }

        Vector3 min = bounds.center;
        Vector3 max = bounds.center;

        Collider[] cols = GetComponentsInChildren<Collider>();

        foreach (Collider collider in cols)
        {
            print(collider.name);
            if (collider.bounds.min.x < min.x) min.x = collider.bounds.min.x;
            if (collider.bounds.min.y < min.y) min.y = collider.bounds.min.y;
            if (collider.bounds.min.z < min.z) min.z = collider.bounds.min.z;

            if (collider.bounds.max.x > min.x) max.x = collider.bounds.max.x;
            if (collider.bounds.max.y > min.y) max.y = collider.bounds.max.y;
            if (collider.bounds.max.z > min.z) max.z = collider.bounds.max.z;
        }

        bounds = new Bounds(((min + max) / 2f), (max - min));
    }

    public void RecalculateBoundsMesh()
    {

        if (GetComponent<MeshFilter>() == null)
        {
            bounds = new Bounds(Vector3.zero, Vector3.zero);
        }
        else
        {
            bounds = GetComponent<MeshFilter>().mesh.bounds;
            //bounds.center = transform.localPosition;
        }

        Vector3 min = bounds.center;
        Vector3 max = bounds.center;

        MeshFilter[] mfs = GetComponentsInChildren<MeshFilter>();

        foreach (MeshFilter mf in mfs)
        {
            Vector3 pos = mf.transform.localPosition;
            Bounds child_bounds = mf.sharedMesh.bounds;
            //child_bounds.center += pos;
            bounds.Encapsulate(child_bounds);
        }
        // bounds.size = new Vector3(bounds.size.x * transform.localScale.x, bounds.size.y * transform.localScale.y, bounds.size.z * transform.localScale.z);
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
        if (autoBounds)
        {
            RecalculateBoundsMesh();
            GetSizeBounds();
        }

        modifiedDimension = originDimension;
        modifiedDimensionPercent = Vector3.one * 100;
        initScale = transform.localScale;
        RecalculateBoundsMesh();

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
