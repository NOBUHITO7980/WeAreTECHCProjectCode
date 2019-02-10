using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTrigger : MonoBehaviour
{
    float time;
    bool isEnable;
    SphereCollider sc;
    bool coroutine;

    private void Start()
    {
        sc = GetComponent<SphereCollider>();
    }

    public bool Coroutine
    {
        get { return coroutine; }
        set { coroutine = value; }
    }

    private void Update()
    {
        if (sc.enabled)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }
        if(time >= 3.0f)
        {
            sc.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy") { return; }
        // ここにエネミーが入ったときの関数を呼ぶ
        other.gameObject.GetComponent<Enemy>().PicSoundToMove();
    }

    public IEnumerator EnableNoise()
    {
        if (coroutine) yield break;
        coroutine = true;
        yield return new WaitForSeconds(3.0f);
        GetComponent<SphereCollider>().enabled = false;
        coroutine = false;
    }

}
