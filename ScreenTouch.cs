using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTouch 
{

    static readonly bool IsIOS = false;//Application.platform == RuntimePlatform.IPhonePlayer;

    static readonly bool IsEditor = !IsIOS;

    static bool isTouch = false;

    static bool isObjectTouch = false; 

    static Vector3 prebPosition;

    /// <summary>
    /// タッチ情報を取得(エディタとスマホを考慮)
    /// </summary>
    /// <returns>タッチ情報</returns>
    public static GodPhase GetPhase()
    {
        if (IsEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                prebPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                return GodPhase.Began;
            }
            else if (Input.GetMouseButton(0))
            {
                return GodPhase.Moved;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                return GodPhase.Ended;
            }
        }
        else
        {
            if (Input.touchCount > 0) return (GodPhase)((int)Input.GetTouch(0).phase);
        }
        return GodPhase.None;
    }

    /// <summary>
    /// タッチポジションを取得(エディタとスマホを考慮)
    /// </summary>
    /// <returns>タッチポジション。タッチされていない場合は (0, 0, 0)</returns>
    public static Vector3 GetPosition()
    {
        if (IsEditor)
        {
            if (GetPhase() != GodPhase.None)
            {
                isTouch = true;
                return Input.mousePosition;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                isTouch = true;
                return Input.GetTouch(0).position;
            }
        }
        return Vector3.zero;
    }

    /// <summary>
    /// タッチデルタポジションを取得(エディタとスマホを考慮)
    /// </summary>
    /// <returns>タッチポジション。タッチされていない場合は (0, 0, 0)</returns>
    public static Vector3 GetDeltaPosition()
    {
        if (IsEditor)
        {
            var phase = GetPhase();
            if (isTouch && Input.GetMouseButton(0))
            {
                return Input.mousePosition;
            }else
            {
                isTouch = false;
            }
        }
        else
        {
            if (Input.touchCount > 0 && isTouch)
            {
                return Input.GetTouch(0).position;
            }
            else
            {
                isTouch = false;
            }
        }
        return Vector3.zero;
    }

    public static GameObject GetRayGameObject()
    {

        Ray ray;
        RaycastHit hit;
        if (IsEditor)
        {
            if (Input.GetMouseButtonDown(0) && !isObjectTouch)
            {
                isObjectTouch = false;
            }
            if(Input.GetMouseButtonUp(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    isObjectTouch = true;
                    return hit.transform.gameObject;
                }
                else
                {
                    return null;
                }
            }
        }
        else
        {
            if (Input.touchCount > 0 && !isObjectTouch)
            {
                isObjectTouch = false;
            }
            if(Input.touchCount <= 0)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out hit))
                {
                    isObjectTouch = true;
                    return hit.collider.gameObject;
                }
                else
                {
                    return null;
                }
            }

        }
        return null;
    }
}

/// <summary>
/// タッチ情報。UnityEngine.TouchPhase に None の情報を追加拡張。
/// </summary>
public enum GodPhase
{
    None = -1,
    Began = 0,
    Moved = 1,
    Stationary = 2,
    Ended = 3,
    Canceled = 4
}

