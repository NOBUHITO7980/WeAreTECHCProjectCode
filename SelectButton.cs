using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{


    void Start()
    {

    }


    void Update()
    {

    }

    public void GetButtonFunction()
    {
        //TitleUIController.Instance.SetNextSeneName = Resources.Load("Scenes/" + this.gameObject.name).ToString();
        TitleUIController.Instance.SetNextSeneName(Resources.Load("Scenes/Main").ToString());
    }
}
