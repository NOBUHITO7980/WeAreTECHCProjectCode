using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSceneTexture : MonoBehaviour
{
    Material[] childMaterials = new Material[2];

    bool shaderSwich = false;

    float timer = 0;

    [SerializeField]
    [Range(60,1)]
    float timerSpeed;

    void Start()
    {

    }


    void Update()
    {
        if (!shaderSwich) { return; }

        timer -= Time.deltaTime / timerSpeed;

        for (int u = 0; u < childMaterials.Length; u++)
        {
            childMaterials[u].SetFloat("_Threshold", timer);
        }

        if(timer <= 0)
        {
            shaderSwich = false;
            timer = 1;
        }
    }

    void OnEnable()
    {

        for (int u = 0; u < childMaterials.Length; u++)
        {
            //Debug.Log(transform.GetChild(u).gameObject.GetComponent<Image>().material);
            childMaterials[u] = transform.GetChild(0).gameObject.GetComponent<Image>().material;
            childMaterials[u].SetFloat("_Threshold", 1);
        }
        timer = 1;
        shaderSwich = true;
    }

}
