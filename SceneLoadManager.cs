using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadManager : SingletonMonoBehaviour<SceneLoadManager>
{
    private AsyncOperation async;

    [SerializeField]
    private GameObject loadUI;

    [SerializeField]
    Image logo;

    [SerializeField]
    float fadeSpeed;

    Coroutine coroutine;

    bool isLoadNow = false;

    void Start()
    {
        
    }


    void Update()
    {

    }

    public void LoadScene(string scene)
    {
        isLoadNow = true;
        if(coroutine == null){
            //　ロード画面UIをアクティブにする
            loadUI.SetActive(true);
            //logo.gameObject.SetActive(true);
            //　コルーチンを開始
            coroutine = StartCoroutine(LoadData(scene));
        }
    }

    IEnumerator LoadData(string scene)
    {
        float timer = 0;
        Color cr = logo.color;

        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync(scene);
        
        ////　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone )
        {
            timer += Time.deltaTime * fadeSpeed;
            logo.color = new Color(cr.r, cr.g, cr.b, Mathf.Sin(Mathf.Abs(timer)));

            yield return null;
        }
        if(loadUI.activeSelf)
        {
            loadUI.SetActive(false);
        }
        isLoadNow = false;
        coroutine = null;
    }

    public bool GetLoadState
    {
        get { return isLoadNow; }
    }
}
