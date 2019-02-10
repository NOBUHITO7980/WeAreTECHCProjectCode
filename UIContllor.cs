using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContllor : SingletonMonoBehaviour<UIContllor>
{
    [SerializeField]
    GageFill gf;
    //Slider sunValue;

    //[SerializeField]
    //GameObject heartParent, heartObj;

    GameObject[] heart = new GameObject[4];

    [SerializeField]
    [Range(0.01f, 1.0f)]
    float sliderSpeed;

    int memoryHeart;
    float memorySunValue;

    [SerializeField]
    Material mt;

    void Start()
    {
        mt.SetFloat("_AlphaValue", 0);
        for (int u = 1; u <= 4; u++)
        {
            heart[u-1] = GameObject.Find("Heat_" + u); 
        }
    }


    public void SetFalse()
    {
        for(int i = 0; i < heart.Length; i++)
        {
            heart[i].SetActive(false);
        }
        gf.gameObject.SetActive(false);
    }

    public void SetValues()
    {
        PlayerController.Instance.SetHeart = 4;
        memoryHeart = PlayerController.Instance.SetHeart;
        gf.Fill(PlayerController.Instance.SetSUNvalue);
        //sunValue.maxValue = PlayerController.Instance.SetSUNvalue;
        //sunValue.value = sunValue.maxValue;
        //memorySunValue = sunValue.maxValue;
        //memoryHeart = PlayerController.Instance.SetHeart;
        //heart = new GameObject[memoryHeart];

        //for (int i = 0; i < heart.Length; i++)
        //{
        //    heart[i] = Instantiate(heartObj, heartParent.transform.position, Quaternion.identity);
        //    heart[i].transform.SetParent(heartParent.transform, false);
        //    heart[i].transform.localPosition = new Vector3(0, 0, 0);
        //    RectTransform rectTransform = heart[i].GetComponent<RectTransform>();
        //    rectTransform.localPosition = new Vector3(rectTransform.localPosition.x + (30 * i), rectTransform.localPosition.y);
        //}
    }

    void Update()
    {
        
    }

    public void HeartDisplay(int setValue)
    {
        if(memoryHeart < setValue) { return; }
        for(int u = 0; u < memoryHeart; u++)
        {
            heart[u].SetActive(true);
        }
        for(int u = memoryHeart; u > setValue; u--)
        {
            heart[u-1].SetActive(false);
        }
        //foreach (Transform childObj in heartParent.transform)
        //{
        //    if (setValue > num)
        //    {
        //        childObj.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        childObj.gameObject.SetActive(false);
        //    }
        //    num++;
        //}
    }

    /// <summary>
    /// 外部からSUN値変動を呼ぶ関数
    /// </summary>
    /// <param name="setValue"></param>
    public void SunValueDisplay(float setValue)
    {
        //StartCoroutine("SunValueSlider", setValue);
        gf.Fill(setValue);
        mt.SetFloat("_AlphaValue", Mathf.Lerp(1,0,setValue));
    }

    /// <summary>
    /// SUN値変動のコルーチン
    /// </summary>
    /// <param name="setValue"></param>
    /// <returns></returns>
    private IEnumerator SunValueSlider( float setValue)
    {
        yield return null;
        //Color sunValueColor = sunValue.GetComponent<Image>().color;
        //float Red = sunValueColor.r;
        //float Green = sunValueColor.g;
        //while (setValue < sunValue.value)
        //{
        //    sunValue.value = Mathf.MoveTowards(sunValue.value, setValue, sliderSpeed * Time.deltaTime);
            //if(sunValue.value > memorySunValue / 2)
            //{
            //    sunValueColor = new Color(1 / ((sunValue.value * 0.5f) / (memorySunValue * 0.5f)) / (memorySunValue * 0.5f))
            //}
            //Debug.Log(sunValue.value);
        //    if (setValue <= sunValue.value) { yield return null; }

            
        //}
    }


}
