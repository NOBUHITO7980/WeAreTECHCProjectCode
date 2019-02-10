using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gole : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MainController.Instance.GameClear();
        }
    }
}
