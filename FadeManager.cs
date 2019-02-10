using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
    Image fadeImage;
    float timer = 0;

    bool fadeSwich = false;
    Coroutine coroutine;
    private enum Fade
    {
        Clear,Black
    }

    Fade fadeType = Fade.Clear; 

    void Start()
    {
        fadeImage = transform.GetChild(0).GetComponent<Image>();
    }

    public bool GetFadeSwich
    {
        get { return fadeSwich; }
    }

    void Update()
    {

    }

    // コルーチンを開始する
    public void StartFade(bool SceneMove = false, string sceneNamae = "")
    {
        coroutine = StartCoroutine(FadeCorou(SceneMove,sceneNamae));
        fadeSwich = false;
        
    }

    // Alphaしか変わらないが色の変更
    Color setAlpha(float time, Color orignalColor)
    {
        Color color = orignalColor;
        float alpha = color.a;
        alpha = time;
        color.a = alpha;
        return color;
    }

    // フェードイン、アウトのコルーチン
    private IEnumerator FadeCorou(bool SceneMove, string sceneName)
    {
        if(fadeType == Fade.Clear && !fadeSwich)
        {
            while(timer < 1)
            {
                fadeImage.color = setAlpha(timer, fadeImage.color);
                timer += Time.deltaTime;
                yield return null;
            }
            fadeType = Fade.Black;
            if (SceneMove) { SceneLoadManager.Instance.LoadScene(sceneName); }
        }

        while(SceneLoadManager.Instance.GetLoadState)
        {
            yield return null;
        }

        if (fadeType == Fade.Black && !fadeSwich)
        {
            while (timer > 0.01f)
            {
                fadeImage.color = setAlpha(timer, fadeImage.color);
                timer -= Time.deltaTime;
                if (timer < 0.01f)
                {
                    fadeImage.color = setAlpha(0, fadeImage.color);
                }
                yield return null;
            }
            fadeType = Fade.Clear;

        }
        coroutine = null;
    }

}
