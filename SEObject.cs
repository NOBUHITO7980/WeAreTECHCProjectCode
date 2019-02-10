using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEObject : MonoBehaviour
{
    AudioSource seSorce;

    SoundEffectKind effectKind = SoundEffectKind.None;

    void Start()
    {
        //seSorce = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(seSorce.clip == null) { return; }
        if(seSorce.isPlaying != false) { return; }

        SEmanager.Instance.SoundEffectStopDetection(effectKind);
        seSorce.clip = null;
        effectKind = SoundEffectKind.None;
        this.gameObject.SetActive(false);
    }

    public void SoundEffectPlay(AudioClip clip, SoundEffectKind kind)
    {
        if(seSorce == null) { seSorce = GetComponent<AudioSource>(); }
        seSorce.clip = clip;
        seSorce.Play();
        effectKind = kind;
    }
}
