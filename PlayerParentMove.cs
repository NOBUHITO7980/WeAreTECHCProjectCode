using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParentMove : MonoBehaviour
{
    GameObject playerPosition = null;

    void Start()
    {
        
    }


    void Update()
    {
        if(playerPosition == null){
            playerPosition = PlayerController.Instance.gameObject;
            return;
        }
        if( MainController.Instance.GetState == MainState.playStart)
        {
            transform.position = new Vector3(playerPosition.transform.position.x + 5.0f, this.transform.position.y, playerPosition.transform.position.z + 5f);
            return;
        }
        
        transform.position = new Vector3(playerPosition.transform.position.x + 5.0f, playerPosition.transform.position.y + 1.5f, playerPosition.transform.position.z + 3f);
    }

    public GameObject SetPlayerPosition
    {
        set { playerPosition = value; }
    }
}
