using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentTools : MonoBehaviour {

	public enum AlignmentType
    {
        Center = 5,
        Top = 8,
        Bottom = 2,
        Left = 4,
        Right = 6,
        TopLeft = 7,
        TopRight = 9,
        BottomLeft = 1,
        BottomRight = 3
    }

    public AlignmentType alignment = AlignmentType.Center;

    public Transform target;

    public List<GameObject> alignmentHelper = new List<GameObject>();

    Bounds bounds;

    private void Awake()
    {
        if (target == null) target = GameObject.Find("Alignment Helper").transform;

        RecalculateBoundsMesh();
        CreateHelper();
        Snap(alignment);
        RecalculateBoundsMesh();
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

        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        Collider[] cols = GetComponentsInChildren<Collider>();

        foreach (Collider collider in cols)
        {
            if (collider.bounds.min.x < min.x) min.x = collider.bounds.min.x;
            if (collider.bounds.min.y < min.y) min.y = collider.bounds.min.y;
            if (collider.bounds.min.z < min.z) min.z = collider.bounds.min.z;

            if (collider.bounds.max.x > max.x) max.x = collider.bounds.max.x;
            if (collider.bounds.max.y > max.y) max.y = collider.bounds.max.y;
            if (collider.bounds.max.z > max.z) max.z = collider.bounds.max.z;
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
        }

        Vector3 min = bounds.center;
        Vector3 max = bounds.center;

        MeshFilter[] mfs = GetComponentsInChildren<MeshFilter>();

        foreach (MeshFilter mf in mfs)
        {
            Vector3 pos = mf.transform.localPosition;
            Bounds child_bounds = mf.sharedMesh.bounds;
            child_bounds.center += pos;
            bounds.Encapsulate(child_bounds);
        }
    }

    public void Snap()
    {

        switch (alignment)
        {
            case AlignmentType.Center:
                Alignment("center");
                break;
            case AlignmentType.Top:
                Alignment("top");
                break;
            case AlignmentType.Bottom:
                Alignment("bottom");
                break;
            case AlignmentType.Left:
                Alignment("left");
                break;
            case AlignmentType.Right:
                Alignment("right");
                break;
            case AlignmentType.TopLeft:
                Alignment("top left");
                break;
            case AlignmentType.TopRight:
                Alignment("top right");
                break;
            case AlignmentType.BottomLeft:
                Alignment("bottom left");
                break;
            case AlignmentType.BottomRight:
                Alignment("bottom right");
                break;
        }

    }

    public void Snap(AlignmentType alignmentTo)
    {

        switch (alignmentTo)
        {
            case AlignmentType.Center:
                Alignment("center");
                break;
            case AlignmentType.Top:
                Alignment("top");
                break;
            case AlignmentType.Bottom:
                Alignment("bottom");
                break;
            case AlignmentType.Left:
                Alignment("left");
                break;
            case AlignmentType.Right:
                Alignment("right");
                break;
            case AlignmentType.TopLeft:
                Alignment("top left");
                break;
            case AlignmentType.TopRight:
                Alignment("top right");
                break;
            case AlignmentType.BottomLeft:
                Alignment("bottom left");
                break;
            case AlignmentType.BottomRight:
                Alignment("bottom right");
                break;
        }

        alignment = alignmentTo;
        
    }

    public void Snap(int alignmentTo)
    {
        switch (alignmentTo)
        {
            case 5:
                Alignment("center");
                alignment = AlignmentType.Center;
                break;
            case 8:
                Alignment("top");
                alignment = AlignmentType.Top;
                break;
            case 2:
                Alignment("bottom");
                alignment = AlignmentType.Bottom;
                break;
            case 4:
                Alignment("left");
                alignment = AlignmentType.Left;
                break;
            case 6:
                Alignment("right");
                alignment = AlignmentType.Right;
                break;
            case 7:
                Alignment("top left");
                alignment = AlignmentType.TopLeft;
                break;
            case 9:
                Alignment("top right");
                alignment = AlignmentType.TopRight;
                break;
            case 1:
                Alignment("bottom left");
                alignment = AlignmentType.BottomLeft;
                break;
            case 3:
                Alignment("bottom right");
                alignment = AlignmentType.BottomRight;
                break;
        }
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

    void Alignment(string alignment)
    {
        alignmentHelper.Find(x => (x.name == alignment)).transform.parent = transform.parent;
        transform.parent = alignmentHelper.Find(x => (x.name == alignment)).transform;

        alignmentHelper.Find(x => (x.name == alignment)).transform.position = target.Find(alignment).transform.position;

        transform.parent = transform.parent.parent;
        alignmentHelper.Find(x => (x.name == alignment)).transform.parent = transform;
    }

    void CreateHelper()
    {
        //current alignment object
        // center
        GameObject centerCurrent = new GameObject("center");
        centerCurrent.transform.position = new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
        centerCurrent.transform.parent = transform;
        alignmentHelper.Add(centerCurrent);

        // top
        GameObject topCurrent = new GameObject("top");
        topCurrent.transform.position = new Vector3(bounds.center.x, bounds.min.y, bounds.max.z);
        topCurrent.transform.parent = transform;
        alignmentHelper.Add(topCurrent);

        // bottom
        GameObject bottomCurrent = new GameObject("bottom");
        bottomCurrent.transform.position = new Vector3(bounds.center.x, bounds.min.y, bounds.min.z);
        bottomCurrent.transform.parent = transform;
        alignmentHelper.Add(bottomCurrent);

        // left
        GameObject leftCurrent = new GameObject("left");
        leftCurrent.transform.position = new Vector3(bounds.min.x, bounds.min.y, bounds.center.z);
        leftCurrent.transform.parent = transform;
        alignmentHelper.Add(leftCurrent);

        // right
        GameObject rightCurrent = new GameObject("right");
        rightCurrent.transform.position = new Vector3(bounds.max.x, bounds.min.y, bounds.center.z);
        rightCurrent.transform.parent = transform;
        alignmentHelper.Add(rightCurrent);

        // top left
        GameObject topLeftCurrent = new GameObject("top left");
        topLeftCurrent.transform.position = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        topLeftCurrent.transform.parent = transform;
        alignmentHelper.Add(topLeftCurrent);

        // top right
        GameObject topRightCurrent = new GameObject("top right");
        topRightCurrent.transform.position = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        topRightCurrent.transform.parent = transform;
        alignmentHelper.Add(topRightCurrent);

        // bottom left
        GameObject bottomLeftCurrent = new GameObject("bottom left");
        bottomLeftCurrent.transform.position = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        bottomLeftCurrent.transform.parent = transform;
        alignmentHelper.Add(bottomLeftCurrent);

        // bottom right
        GameObject bottomRightCurrent = new GameObject("bottom right");
        bottomRightCurrent.transform.position = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        bottomRightCurrent.transform.parent = transform;
        alignmentHelper.Add(bottomRightCurrent);
    }

}
