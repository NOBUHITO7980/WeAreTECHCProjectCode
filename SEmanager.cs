using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffectKind
{
    Death,EnemyCry,EnemyRoar,HeartBeat,Explosion,MusicBox,Picking,BlockBreak,BlockFall,Walk,None,CountDown
}

public class SEmanager : SingletonMonoBehaviour<SEmanager>
{

    [SerializeField]
    AudioClip death;

    [SerializeField]
    AudioClip[] enemyCry;

    [SerializeField]
    AudioClip[] enemyRoar;

    [SerializeField]
    AudioClip[] heartBeat;
    bool isHeartBeatPlaying = false;

    [SerializeField]
    AudioClip explosion;

    [SerializeField]
    AudioClip tntCountDown;
    
    [SerializeField]
    AudioClip musicBox;

    [SerializeField]
    AudioClip picking;

    [SerializeField]
    AudioClip blockBreak;

    [SerializeField]
    AudioClip blockFall;

    [SerializeField]
    AudioClip walk;
    bool isWalkPlaying = false;

    ObjectPool pool;

    [SerializeField]
    GameObject SEObject;

    [SerializeField]
    int instanceCount;


    void Start()
    {
        pool = GetComponent<ObjectPool>();
        pool.CreatePool(SEObject, instanceCount);
    }

    /// <summary>
    /// エネミーの鳴き声
    /// </summary>
    /// <param name="pos"></param>
    public void PlayEnemyCry(Vector3 pos)
    {
        int random = Random.Range(0, enemyCry.Length);
        MusicSetObject(pos, enemyCry[random], SoundEffectKind.EnemyCry);

    }

    /// <summary>
    /// エネミーの唸り声
    /// </summary>
    /// <param name="pos"></param>
    public void PlayEnemyRoar(Vector3 pos)
    {
        int random = Random.Range(0, enemyCry.Length);
        MusicSetObject(pos, enemyRoar[random], SoundEffectKind.EnemyRoar);

    }

    /// <summary>
    /// 心臓音
    /// </summary>
    /// <param name="pos"></param>
    public void PlayHeartBeat(Vector3 pos)
    {
        if (isHeartBeatPlaying) { return; }
        isHeartBeatPlaying = true;
        int random = Random.Range(0, heartBeat.Length);
        MusicSetObject(pos, heartBeat[random], SoundEffectKind.HeartBeat);
    }

    /// <summary>
    /// ブロック破壊音
    /// </summary>
    /// <param name="pos"></param>
    public void PlayBreackSoundEffect(Vector3 pos)
    {
        MusicSetObject(pos, blockBreak, SoundEffectKind.BlockBreak);
    }

    /// <summary>
    /// 爆発音
    /// </summary>
    /// <param name="pos"></param>
    public void PlayExplosion(Vector3 pos)
    {
        MusicSetObject(pos, explosion, SoundEffectKind.Explosion);
    }

    public void PlayCountDown(Vector3 pos)
    {
        MusicSetObject(pos, tntCountDown, SoundEffectKind.CountDown);
    }

    GameObject coundown = null;
    public GameObject CounDown
    {
        get { return coundown; }
    }

    /// <summary>
    /// オルゴール音
    /// </summary>
    /// <param name="pos"></param>
    public void PlayMusicBox(Vector3 pos)
    {
        MusicSetObject(pos, musicBox, SoundEffectKind.MusicBox);
    }

    /// <summary>
    /// ピッキング音
    /// </summary>
    /// <param name="pos"></param>
    public void PlayPicking(Vector3 pos)
    {
        MusicSetObject(pos, picking, SoundEffectKind.Picking);
    }

    /// <summary>
    /// 岩が落ちる音
    /// </summary>
    /// <param name="pos"></param>
    public void PlayBlockFall(Vector3 pos)
    {
        MusicSetObject(pos, blockFall, SoundEffectKind.BlockFall);
    }

    /// <summary>
    /// 歩く音
    /// </summary>
    /// <param name="pos"></param>
    public void PlayWalk(Vector3 pos)
    {
        if (isWalkPlaying) { return; }
        isWalkPlaying = true;
        MusicSetObject(pos, walk, SoundEffectKind.Walk);
    }

    public void PlayDeath(Vector3 pos)
    {
        MusicSetObject(pos, death, SoundEffectKind.Death);
    }
    /// <summary>
    /// 効果音に共通するコードのまとめ
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="clip"></param>
    private void MusicSetObject(Vector3 pos, AudioClip clip, SoundEffectKind kind)
    {
        GameObject obj = pool.GetObject();
        obj.transform.position = pos;
        switch(kind)
        {
            case SoundEffectKind.MusicBox:
                musicBoxSe = obj;
                break;
            case SoundEffectKind.CountDown:
                coundown = obj;
                break;
        }
        obj.GetComponent<SEObject>().SoundEffectPlay(clip, kind);
    }

    GameObject musicBoxSe = null;
    public GameObject MusicBox
    {
        get { return musicBoxSe; }
    }


    public void SoundEffectStopDetection(SoundEffectKind kind)
    {
        switch(kind)
        {
            case SoundEffectKind.Walk:
                isWalkPlaying = false;
                break;
            case SoundEffectKind.HeartBeat:
                isHeartBeatPlaying = false;
                break;
        }
    }
}
