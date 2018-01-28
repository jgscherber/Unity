using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    private Camera reference;
    private float width;
    private float height;
    private RectTransform buttonTransform;

    void Start()
    {
        reference = Camera.main;
        height = 2f * reference.orthographicSize;
        width = height * reference.aspect;

        buttonTransform = GetComponent<RectTransform>();
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
