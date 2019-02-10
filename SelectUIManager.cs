using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUIManager : SingletonMonoBehaviour<SelectUIManager>
{
    string sceneName;

    [SerializeField]
    GameObject textureParent;

    [SerializeField]
    GameObject nextSceneToButton;

    void Start()
    {
        textureParent.SetActive(false);
        nextSceneToButton.SetActive(false);
    }

    public string GetSceneName
    {
       get { return sceneName; }
    } 

    void Update()
    {

    }

    public void SetSceneName(string setNextSceneName)
    {
        sceneName = "Main";
        textureParent.SetActive(true);
        nextSceneToButton.SetActive(true);

    }
}
