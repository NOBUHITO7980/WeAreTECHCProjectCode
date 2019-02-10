using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTextures : MonoBehaviour
{
    [SerializeField]
    GameObject[] images;

    Material[] imageMaterial;

    [SerializeField]
    [Range(1,100)]
    float timerSpeed;

    float timer = 0;

    bool shaderSwicth = false;

    void Start()
    {
        imageMaterial = new Material[images.Length];

        for (int u = 0; u < images.Length; u++)
        {
            imageMaterial[u] =  images[u].GetComponent<Image>().material;
        }
    }

    void Update()
    {
        if (!shaderSwicth) { return; }
        timer = Time.deltaTime / timerSpeed;

        for (int u = 0; u < images.Length; u++)
        {
            imageMaterial[u].SetFloat("_Threshold", timer);
        }

        if(timer >= 1) { shaderSwicth = false; }
    }

    public void DisplayTexture()
    {
        timer = 0;
        shaderSwicth = true;
    }
}
