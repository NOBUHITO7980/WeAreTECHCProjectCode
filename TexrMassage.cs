using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TexrMassage : MonoBehaviour
{

    Text massageText;

    [SerializeField]
    string[] massage;

    int textSetLength;

    int nowTextNum = 0;

    int nowLine = 0;

    [SerializeField]
    [Range(0, 1)]
    float textSpeed = 0.05f;

    float elapsedTime = 0f;

    bool oneMessage = false;

    bool endMessage = false;

    void Start()
    {
        massageText = GetComponentInChildren<Text>();
        massageText.text = "";
        textSetLength = massage[nowLine].Length;
    }


    void Update()
    {
        if(endMessage|| massage == null) { return; }

        if (!oneMessage)
        {
            if (elapsedTime >= textSpeed)
            {
                massageText.text += massage[0][nowTextNum];

                nowTextNum++;
                elapsedTime = 0f;

                if (nowTextNum >= textSetLength)
                {
                    oneMessage = true;
                }
            }

            elapsedTime += Time.deltaTime;
        }
    }

}
