using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox :TrapBase {

    bool countStart = false;

    GameObject child;

    [SerializeField]
    float limtTime;

    float timer = 0;

    [SerializeField]
    float soundRange;

    GameObject seObj;

    void Start ()
    {        
        child = transform.Find("Sphere").gameObject;
        child.GetComponent<SphereCollider>().radius = soundRange;
    }
	
	
	void Update ()
    {
        if (!countStart) { return; }

        timer += Time.deltaTime;
        if(child.GetComponent<Musicarea>().stay == true && countStart == true) {
            child.GetComponent<Musicarea>().e.GetComponent<Enemy>().PicSoundToMove();
        }
        if (limtTime > timer) { return; }
        
        countStart = false;
        timer = 0;
	}

    public override void OnTouch()
    {
        countStart = true;
        SEmanager.Instance.PlayMusicBox(transform.position);
        seObj = SEmanager.Instance.MusicBox;
    }
}
