using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AngleState
{
    None, Expansion, Shrinking
}

public class SpotRightAngleChange : MonoBehaviour
{
    

    AngleState angleState = AngleState.None;

    [SerializeField]
    float timerSpeed;

    float timer = 0;

    float setFirstAngle;

    Light light;

    bool isChange = false;

    void Start()
    {
        light = GetComponent<Light>();
        setFirstAngle = light.spotAngle;
    }


    void Update()
    {
        AngleVariable();
    }

    private void AngleVariable()
    {
        if (!isChange) { return; }

        if(angleState == AngleState.Shrinking)
        {
            timer += Time.deltaTime * timerSpeed;
            light.spotAngle = setFirstAngle - timer;
            return;
        }
        timer += Time.deltaTime * timerSpeed;
        light.spotAngle = setFirstAngle + timer;
    }


    public void SpotLightChange(AngleState state)
    {
        if(state == AngleState.None) { return; }

        angleState = state;

        isChange = true;
    }
}
