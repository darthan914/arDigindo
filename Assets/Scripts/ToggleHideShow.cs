using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHideShow : MonoBehaviour {

    public GameObject target;

    private void Start()
    {
        if (!target) target = gameObject;
    }

    public void Toggle()
    {
        target.SetActive(!target.activeSelf);
    }
}
