using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerInB : MonoBehaviour
{
    public bool SceneStarting;

    void Start()
    {
        StartCoroutine(SceneStart());
    }

    IEnumerator SceneStart()
    {
        SceneStarting = true;
        yield return GameManagerInB.instance.lightControllerInB.InitLights();
        SceneStarting = false;
    }
}
