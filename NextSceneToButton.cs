using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneToButton : MonoBehaviour
{
    bool judgment = false;

    [SerializeField]
    AudioClip se;

    void Start()
    {

    }


    void Update()
    {

    }

    public void GotoNextScene()
    {
        GetComponent<AudioSource>().PlayOneShot(se);
        FadeManager.Instance.StartFade(true, SelectUIManager.Instance.GetSceneName);
        
    }
}
