using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoEmptyObject : MonoBehaviour {

    public Color colorGizmos = Color.yellow;
    [Range(0.01f, 3f)] public float radiusSphere = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = colorGizmos;
        Gizmos.DrawSphere(transform.position, radiusSphere);
    }
}
