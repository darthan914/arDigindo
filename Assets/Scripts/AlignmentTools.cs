using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectController))]
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

        bounds = GetComponent<ObjectController>().GetBounds();

        CreateHelper();
        Snap(alignment);

        
    }

    public void Snap()
    {

        switch (alignment)
        {
            case AlignmentType.Center:
                Alignment("Center");
                break;
            case AlignmentType.Top:
                Alignment("Top");
                break;
            case AlignmentType.Bottom:
                Alignment("Bottom");
                break;
            case AlignmentType.Left:
                Alignment("Left");
                break;
            case AlignmentType.Right:
                Alignment("Right");
                break;
            case AlignmentType.TopLeft:
                Alignment("Top Left");
                break;
            case AlignmentType.TopRight:
                Alignment("Top Right");
                break;
            case AlignmentType.BottomLeft:
                Alignment("Bottom Left");
                break;
            case AlignmentType.BottomRight:
                Alignment("Bottom Right");
                break;
        }

    }

    public void Snap(AlignmentType alignmentTo)
    {

        switch (alignmentTo)
        {
            case AlignmentType.Center:
                Alignment("Center");
                break;
            case AlignmentType.Top:
                Alignment("Top");
                break;
            case AlignmentType.Bottom:
                Alignment("Bottom");
                break;
            case AlignmentType.Left:
                Alignment("Left");
                break;
            case AlignmentType.Right:
                Alignment("Right");
                break;
            case AlignmentType.TopLeft:
                Alignment("Top Left");
                break;
            case AlignmentType.TopRight:
                Alignment("Top Right");
                break;
            case AlignmentType.BottomLeft:
                Alignment("Bottom Left");
                break;
            case AlignmentType.BottomRight:
                Alignment("Bottom Right");
                break;
        }

        alignment = alignmentTo;
        
    }

    public void Snap(int alignmentTo)
    {
        switch (alignmentTo)
        {
            case 5:
                Alignment("Center");
                alignment = AlignmentType.Center;
                break;
            case 8:
                Alignment("Top");
                alignment = AlignmentType.Top;
                break;
            case 2:
                Alignment("Bottom");
                alignment = AlignmentType.Bottom;
                break;
            case 4:
                Alignment("Left");
                alignment = AlignmentType.Left;
                break;
            case 6:
                Alignment("Right");
                alignment = AlignmentType.Right;
                break;
            case 7:
                Alignment("Top Left");
                alignment = AlignmentType.TopLeft;
                break;
            case 9:
                Alignment("Top Right");
                alignment = AlignmentType.TopRight;
                break;
            case 1:
                Alignment("Bottom Left");
                alignment = AlignmentType.BottomLeft;
                break;
            case 3:
                Alignment("Bottom Right");
                alignment = AlignmentType.BottomRight;
                break;
        }
    }

    void Alignment(string alignment)
    {
        alignmentHelper.Find(x => (x.name == alignment)).transform.parent = transform.parent;
        transform.parent = alignmentHelper.Find(x => (x.name == alignment)).transform;

        if (target.Find(alignment) == null) return;
        alignmentHelper.Find(x => (x.name == alignment)).transform.position = target.Find(alignment).transform.position;

        transform.parent = transform.parent.parent;
        alignmentHelper.Find(x => (x.name == alignment)).transform.parent = transform;
    }

    void CreateHelper()
    {
        //current alignment object
        // center
        GameObject centerCurrent = new GameObject("Center");
        centerCurrent.transform.position = new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
        centerCurrent.transform.parent = transform;
        alignmentHelper.Add(centerCurrent);

        // top
        GameObject topCurrent = new GameObject("Top");
        topCurrent.transform.position = new Vector3(bounds.center.x, bounds.min.y, bounds.max.z);
        topCurrent.transform.parent = transform;
        alignmentHelper.Add(topCurrent);

        // bottom
        GameObject bottomCurrent = new GameObject("Bottom");
        bottomCurrent.transform.position = new Vector3(bounds.center.x, bounds.min.y, bounds.min.z);
        bottomCurrent.transform.parent = transform;
        alignmentHelper.Add(bottomCurrent);

        // left
        GameObject leftCurrent = new GameObject("Left");
        leftCurrent.transform.position = new Vector3(bounds.min.x, bounds.min.y, bounds.center.z);
        leftCurrent.transform.parent = transform;
        alignmentHelper.Add(leftCurrent);

        // right
        GameObject rightCurrent = new GameObject("Right");
        rightCurrent.transform.position = new Vector3(bounds.max.x, bounds.min.y, bounds.center.z);
        rightCurrent.transform.parent = transform;
        alignmentHelper.Add(rightCurrent);

        // top left
        GameObject topLeftCurrent = new GameObject("Top Left");
        topLeftCurrent.transform.position = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        topLeftCurrent.transform.parent = transform;
        alignmentHelper.Add(topLeftCurrent);

        // top right
        GameObject topRightCurrent = new GameObject("Top Right");
        topRightCurrent.transform.position = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        topRightCurrent.transform.parent = transform;
        alignmentHelper.Add(topRightCurrent);

        // bottom left
        GameObject bottomLeftCurrent = new GameObject("Bottom Left");
        bottomLeftCurrent.transform.position = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        bottomLeftCurrent.transform.parent = transform;
        alignmentHelper.Add(bottomLeftCurrent);

        // bottom right
        GameObject bottomRightCurrent = new GameObject("Bottom Right");
        bottomRightCurrent.transform.position = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        bottomRightCurrent.transform.parent = transform;
        alignmentHelper.Add(bottomRightCurrent);
    }

}
