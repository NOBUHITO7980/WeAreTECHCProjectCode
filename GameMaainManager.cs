using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaainManager : SingletonMonoBehaviour<GameMaainManager>
{

    private float timer = 0;
    private string lastTimeSceneName = "";
    private bool haveNewPickel = false;
    private bool haveOldPickel = false;
    private bool haveNewHelmet = false;
    private bool haveOldHelmet = false;

    private byte blockTouchCount = 0;
    private byte blockNoiseArea = 0;

    private int score = 0;

    private float lightRange = 0;

    private bool clearDecision = false;

    // 初期化関数
    public void ResetMainGameValues()
    {
        score = 0;
        clearDecision = false;
        timer = 0;
        lastTimeSceneName = "";
    }

    public void ResetValues()
    {
        timer = 0;
        lastTimeSceneName = "";

        haveNewPickel = false;
        haveOldPickel = false;
        haveNewHelmet = false;
        haveOldHelmet = false;

        blockTouchCount = 0;
        blockNoiseArea = 0;

        score = 0;
        lightRange = 0;
        clearDecision = false;

    }

    void Start()
    {

    }

    // ノイズの追加範囲
    public byte BlockNoiseArea
    {
        set { blockNoiseArea = value; }
        get { return blockNoiseArea; }
    }

    // ブロックタッチの回数追加
    public byte BlockTochCount
    {
        set { blockTouchCount = value; }
        get { return blockNoiseArea; }
    }

    // ライトの追加範囲
    public float LightRange
    {
        set { lightRange = value; }
        get { return lightRange; }
    }

    // ゲームクリア時間
    public float Timer
    {
        set { timer = value; }
        get { return timer; }
    }

    // アイテムを保持
    public bool NewPickel {
        get { return haveNewPickel; }
        set {
            if(haveNewPickel) { return; }
            haveNewPickel = value;
        }
    }

    // アイテムを保持
    public bool OldPickel {
        get { return haveOldPickel; }
        set {
            if (haveOldPickel) { return; }
            haveOldPickel = value;
        }
    }

    // アイテムを保持
    public bool NewHelmet {
        get { return haveNewHelmet; }
        set {
            if (haveNewHelmet) { return; }
            haveOldHelmet = value;
        }
    }

    // アイテムを保持
    public bool OldHelmet {
        get { return haveOldHelmet; }
        set {
            if (haveOldHelmet) { return; }
            haveOldHelmet = value;
        }
    }

    // シーン名
    public string LastTimeSceneName
    {
        set { lastTimeSceneName = value; }
        get { return lastTimeSceneName; }
    }

    // ゲームスコア
    public int GetScore
    {
        get { return score; }
    }

    // スコアの加点
    public void AddScore(int addValue)
    {
        score += addValue;
    }

    // ゲームクリアしているかの有無
    public bool ClearDecision
    {
        set { clearDecision = value;}
        get { return clearDecision; }
    }

    void Update()
    {

    }
}
