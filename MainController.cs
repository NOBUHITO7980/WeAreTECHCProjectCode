using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainState
{
    playNow, playStop, playResult, playStart
}

public class MainController : SingletonMonoBehaviour<MainController>
{
    enum gameJudgment
    {
        unknown, clear,over
    }

    gameJudgment judgment = gameJudgment.unknown;

    private MainState state = MainState.playNow;

    private float timer = 0;

    [SerializeField]
    float stopTime;

    private void Start()
    {
        GameMaainManager.Instance.ResetValues();
        GameMaainManager.Instance.LastTimeSceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        TimeCount();
        GameFinish();
        
    }

    /// <summary>
    /// 時間経過を加算
    /// </summary>
    void TimeCount()
    {
        if (state == MainState.playNow)
        {
            timer += Time.deltaTime;
            GameMaainManager.Instance.Timer = timer;
        }
    }

    void GameFinish()
    {
        if (state != MainState.playResult) { return; }
        if (judgment == gameJudgment.clear)
        {
            timer += Time.deltaTime;
            Camera.main.GetComponent<PostEffect>().SepiaTrue(timer);
            if (timer < 1f) { return; }
            Time.timeScale = 1;
            FadeManager.Instance.StartFade(true, "Result");
            GameMaainManager.Instance.LastTimeSceneName = SceneManager.GetActiveScene().name;
        }
        else if (judgment == gameJudgment.over)
        {
            timer += Time.deltaTime;
            Camera.main.GetComponent<PostEffect>().SepiaTrue(timer);
            if (timer < 1f) { return; }
            Time.timeScale = 1;
            FadeManager.Instance.StartFade(true, "Result");
            GameMaainManager.Instance.LastTimeSceneName = SceneManager.GetActiveScene().name;
        }
    }

    /// <summary>
    /// ゲームクリア時に表示するタイマーを渡すためのプロパティ
    /// </summary>
    public float GetTimer { get { return timer; } }

    /// <summary>
    /// ゲームを止めるときまたはゲームを再開するときに呼ぶ関数
    /// </summary>
    public void ChangeGameState()
    {
        if (state == MainState.playStop && state != MainState.playResult)
        {
            state = MainState.playNow;

            // メニュー画面のUI非表示

            // プレイヤーの動かせる
            PlayerController.Instance.ReStartPlayerPlayerController();
            // エネミーの動かせる

            return;
        }
        if(state == MainState.playNow && state != MainState.playResult)
        {
            state = MainState.playStop;

            // プレイヤーの動きを止める
            PlayerController.Instance.StopPlayerPlayerController();
            // エネミーの動きを止める

            // メニュー画面のUI表示

            return;
        }
    }

    /// <summary>
    /// プレイヤーが体力またはSUN値が0になったら呼ぶゲームオーバー関数
    /// </summary>
    public void GameOver()
    {
        if (PlayerController.Instance.SetHeart > 0 && PlayerController.Instance.SetSUNvalue > 0) { return; }

        // ゲームオーバーの処理
        PlayerController.Instance.ResultPlayerPlayerController();
        GameMaainManager.Instance.ClearDecision = false;
        StartCoroutine(GameOverCoroutines());
        
    }

    IEnumerator GameOverCoroutines()
    {
        yield return new WaitForSeconds(stopTime);
        PlayerController.Instance.DeathEffect();
        state = MainState.playResult;
        judgment = gameJudgment.over;
        GameObject.Find("SpotLight").GetComponent<SpotRightAngleChange>().SpotLightChange(AngleState.Shrinking);
        PlayerController.Instance.RigidBodyVariable();
        Time.timeScale = 0.1f;
        timer = 0;
        Camera.main.GetComponent<PostEffect>().ResetMaterial();
        Camera.main.GetComponent<PostEffect>().enabled = true;
    }

    /// <summary>
    /// プレイヤーがゴールについたら呼ぶゲームクリア関数
    /// </summary>
    public void GameClear()
    {
        if (PlayerController.Instance.SetHeart <= 0 && PlayerController.Instance.SetSUNvalue <= 0) { return; }

        PlayerController.Instance.ResultPlayerPlayerController();
        GameObject.Find("SpotLight").GetComponent<SpotRightAngleChange>().SpotLightChange(AngleState.Expansion);
        state = MainState.playResult;
        judgment = gameJudgment.clear;
        GameMaainManager.Instance.ClearDecision = true;

        PlayerController.Instance.RigidBodyVariable();
        Time.timeScale = 0.1f;
        // ゲームクリアの処理
        timer = 0;
        Camera.main.GetComponent<PostEffect>().ResetMaterial();
        Camera.main.GetComponent<PostEffect>().enabled = true;

        // リザルト表示

    }

    /// <summary>
    /// プレイヤー操作可能に
    /// </summary>
    public void StartMainGame()
    {
        state = MainState.playNow;
    }

    public MainState GetState
    {
        get { return state; }
    }
}
