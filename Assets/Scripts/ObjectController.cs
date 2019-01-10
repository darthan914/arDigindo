using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public bool enabledWireCube = true;

    private void OnDrawGizmos()
    {
        if (min == null || max == null) return;

        Gizmos.color = Color.yellow;

        if (enabledWireCube)
        {
            Gizmos.DrawLine(new Vector3(min.position.x, min.position.y, min.position.z), new Vector3(max.position.x, min.position.y, min.position.z));
            Gizmos.DrawLine(new Vector3(max.position.x, min.position.y, min.position.z), new Vector3(max.position.x, max.position.y, min.position.z));
            Gizmos.DrawLine(new Vector3(max.position.x, max.position.y, min.position.z), new Vector3(min.position.x, max.position.y, min.position.z));
            Gizmos.DrawLine(new Vector3(min.position.x, max.position.y, min.position.z), new Vector3(min.position.x, min.position.y, min.position.z));

            Gizmos.DrawLine(new Vector3(min.position.x, min.position.y, max.position.z), new Vector3(max.position.x, min.position.y, max.position.z));
            Gizmos.DrawLine(new Vector3(max.position.x, min.position.y, max.position.z), new Vector3(max.position.x, max.position.y, max.position.z));
            Gizmos.DrawLine(new Vector3(max.position.x, max.position.y, max.position.z), new Vector3(min.position.x, max.position.y, max.position.z));
            Gizmos.DrawLine(new Vector3(min.position.x, max.position.y, max.position.z), new Vector3(min.position.x, min.position.y, max.position.z));

            Gizmos.DrawLine(new Vector3(min.position.x, min.position.y, min.position.z), new Vector3(min.position.x, min.position.y, max.position.z));
            Gizmos.DrawLine(new Vector3(max.position.x, min.position.y, min.position.z), new Vector3(max.position.x, min.position.y, max.position.z));
            Gizmos.DrawLine(new Vector3(max.position.x, max.position.y, min.position.z), new Vector3(max.position.x, max.position.y, max.position.z));
            Gizmos.DrawLine(new Vector3(min.position.x, max.position.y, min.position.z), new Vector3(min.position.x, max.position.y, max.position.z));
        }
    }



    public Transform min;
    public Transform max;

    public Bounds GetBounds()
    {
        return new Bounds(GetCenter(), GetSize());
    }

    public Vector3 GetCenter()
    {
        if (min == null || max == null) return Vector3.zero;

        return Vector3.Lerp(min.position, max.position, 0.5f);
    }

    public Vector3 GetSize()
    {
        if (min == null || max == null) return Vector3.zero;

        Vector3 size;

        size.x = Mathf.Abs(max.position.x - min.position.x);
        size.y = Mathf.Abs(max.position.y - min.position.y);
        size.z = Mathf.Abs(max.position.z - min.position.z);

        return size;
    }
}
