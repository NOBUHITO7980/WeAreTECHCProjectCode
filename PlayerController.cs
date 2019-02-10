using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    Vector3 firstSetPosition = Vector3.zero;
    Vector3 MoveSetPosition = Vector3.zero;

    Rigidbody rb;

    [SerializeField]
    ParticleSystem deathFx;

    [SerializeField]
    ParticleSystem damageFx;

    Vector3 memoryPosition = Vector3.zero;

    [SerializeField]
    int stopFpsCount;
    int fpsCount = 0;

    [SerializeField]
    float stopTime;

    float timer = 0;

    GameObject memoryBlock = null;

    [SerializeField]
    float moveSpeed, standardAccelerator, minTouchDistance, blockToDistance;
    float accelerator, memorySpeed = 0;

    MainState state = MainState.playStart;

    Transform spot;
    bool isInSanArea;
    public bool IsInSanArea
    {
        set { isInSanArea = value; }
        get { return isInSanArea; }
    }

    bool isDontMove = false;
    bool isTouch = false;
    bool moveSwich = false;
    public bool blockTouch = false;

    Animator animator;

    [SerializeField]
    int heart;

    [SerializeField]
    float sanValue;

    Blink blink;

    PlayerState playerState;
    public enum PlayerState
    {
        Idol,
        Walk,
        Mining,
        MiningUnder,
        Damage,
        Death,
        Fall,
        FastContact,
    }

    void Start()
    {
        spot = transform.Find("SanArea");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        blink = new Blink();
        memorySpeed = moveSpeed;
        UIContllor.Instance.SetValues();
        Camera.main.GetComponent<PlayerParentMove>().SetPlayerPosition = this.gameObject; ;
        transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        StateAnimation(PlayerState.Fall);
    }

    public void ResultPlayerPlayerController()
    {
        state = MainState.playResult;
    }

    public void StopPlayerPlayerController()
    {
        state = MainState.playStop;
    }

    public void ReStartPlayerPlayerController()
    {
        state = MainState.playNow;
    }

    void Update()
    {

        DontMoveInterval();
        if (MainState.playResult == state || MainState.playStop == state) { return; }

        ObjectSearch();

        if (!isTouch)
        {
            SetPosition(ScreenTouch.GetPosition());
            return;
        }

        MovingSetPosition(ScreenTouch.GetDeltaPosition());

    }

    /// <summary>
    /// タップした箇所にオブジェクトがあったらそのオブジェクトの関数を呼ぶ
    /// </summary>
    void ObjectSearch()
    {
        GameObject block = ScreenTouch.GetRayGameObject();

        if (moveSwich || blockTouch || block == null) { return; }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, blockToDistance))
        {
            if (block.tag == "Item" && hit.transform.gameObject == block)
            {
                hit.transform.gameObject.GetComponent<item>().ItemCatch();
                blockTouch = true;
                return;
            }

            if (block.tag == "Block" && hit.transform.gameObject == block)
            {
                hit.transform.gameObject.GetComponent<Block>().Touched();
                StateAnimation(PlayerState.Mining);
                blockTouch = true;
                return;
            }
            if (hit.transform.tag == "Trap" && isTouch)
            {
                isTouch = false;
                hit.transform.gameObject.GetComponent<TrapBase>().OnTouch();
            }
        }

        ray = new Ray(transform.position, transform.up * -1.0f);
        if (Physics.Raycast(ray, out hit, blockToDistance))
        {
            if (block.tag == "Item" && hit.transform.gameObject == block)
            {
                hit.transform.gameObject.GetComponent<item>().ItemCatch();
                blockTouch = true;
                return;
            }

            if (block.tag == "Block" && hit.transform.gameObject == block)
            {
                hit.transform.gameObject.GetComponent<Block>().Touched();
                StateAnimation(PlayerState.MiningUnder);
                blockTouch = true;
                return;
            }
        }

    }

    /// <summary>
    /// 移動不可時のカウント
    /// </summary>
    void DontMoveInterval()
    {
        if (!isDontMove) { return; }

        timer += Time.deltaTime;
        if (stopTime >= timer) { return; }
        timer = 0;
        isDontMove = false;

    }

    /// <summary>
    /// アニメーション管理
    /// </summary>
    /// <param name="ps"></param>
    public void StateAnimation(PlayerState ps)
    {
        if (playerState == ps) { return; }

        string s = "";
        animator.SetBool("isIdol", false);
        animator.SetBool("isWalk", false);
        animator.SetBool("isDeath", false);
        animator.SetBool("isFall", false);
        switch (ps)
        {
            case PlayerState.Idol:
                //s = "isIdol";
                animator.SetBool("isIdol", true);
                break;
            case PlayerState.Walk:
                //s = "isWalk";
                animator.SetBool("isWalk", true);
                break;
            case PlayerState.Mining:
                s = "isMining";
                break;
            case PlayerState.MiningUnder:
                s = "isMiningUnder";
                break;
            case PlayerState.Damage:
                s = "isDamage";//animator.SetBool("isDamage_Bool", true);
                break;
            case PlayerState.Death:
                animator.SetBool("isDeath", true);
                break;
            case PlayerState.Fall:
                animator.SetBool("isFall", true);
                break;
            case PlayerState.FastContact:
                animator.SetBool("isFastContact", true);
                break;

        }
        animator.SetTrigger(s);
        playerState = ps;

    }

    /// <summary>
    /// ハートを外部で変えられるようのプロパティ
    /// </summary>
    public int SetHeart
    {
        set
        {
            heart = value;
            UIContllor.Instance.HeartDisplay(heart);
        }
        get { return heart; }
    }

    /// <summary>
    /// SUN値を外部で変えられるようのプロパティ
    /// </summary>
    public float SetSUNvalue
    {
        set { sanValue = value; }
        get { return sanValue; }
    }

    public bool SetMoveSwich
    {
        set { moveSwich = value; }
    }

    public void DeathEffect()
    {
        deathFx.transform.position = this.transform.position;
        deathFx.Play();
    }

    public void Damage(int damageValue = 1)
    {
        if (SetHeart <= 0 && isDontMove) return;
        SetHeart -= damageValue;
        isDontMove = true;
        damageFx.transform.position = this.transform.position;
        damageFx.Play();
        StartCoroutine(Blink.PlayBlink(8, gameObject));
        if (SetHeart <= 0)
        {
            StateAnimation(PlayerState.Death);
            MainController.Instance.GameOver();
            return;
        }
        StateAnimation(PlayerState.Damage);

    }

    /// <summary>
    /// Playerが移動する先を保存し向きを変える
    /// </summary>
    /// <param name="pos"></param>
    public void SetPosition(Vector3 pos)
    {
        if (pos == Vector3.zero) { return; }

        firstSetPosition = pos;
        isTouch = true;

        if (Screen.width * 0.5f < pos.x)
        {
            transform.localEulerAngles = new Vector3(0, 90.0f, 0);
            spot.localEulerAngles = new Vector3(0, 0f, 0);
            return;
        }

        transform.localEulerAngles = new Vector3(0, 270.0f, 0);
        spot.localEulerAngles = new Vector3(0, 180.0f, 0);
    }

    /// <summary>
    /// スワイプしているかの確認と移動速度計算
    /// </summary>
    /// <param name="pos"></param>
    private void MovingSetPosition(Vector3 pos)
    {

        MoveSetPosition = pos;
        if (Vector3.zero == pos || minTouchDistance > Vector3.Distance(firstSetPosition, MoveSetPosition))
        {
            moveSwich = false;
            isTouch = false;
            return;
        }

        if (MoveSetPosition.x > firstSetPosition.x)
        {
            transform.localEulerAngles = new Vector3(0, 90.0f, 0);
            spot.localEulerAngles = new Vector3(0, 0f, 0);

        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 270.0f, 0);
            spot.localEulerAngles = new Vector3(0, 180.0f, 0);

        }

        float negative = 1;

        if (firstSetPosition.x > MoveSetPosition.x)
        {
            negative *= -1;
        }

        accelerator = Vector3.Distance(firstSetPosition, MoveSetPosition) / standardAccelerator * negative;
        moveSwich = true;

    }

    void FixedUpdate()
    {
        BlockTouchInterval();

        if (MainState.playResult == state || isDontMove) { return; }

        StartFallInterval();

        if (MainState.playStop == state || MainState.playStart == state)
        {
            memoryPosition = transform.position;
            return;
        }

        FixedAnim();

        SanValueManagement();

        if (!moveSwich) { return; }

        RollBlocks.Instance.TurnBlock(accelerator);
        if (playerState != PlayerState.Walk) { return; }
        SEmanager.Instance.PlayWalk(transform.position);

    }

    /// <summary>
    /// ブロックタッチした後のカウント
    /// </summary>
    void BlockTouchInterval()
    {
        if (!blockTouch) { return; }
        fpsCount++;
        if (stopFpsCount > fpsCount) { return; }
        fpsCount = 0;
        blockTouch = false;
    }

    /// <summary>
    /// スタート時のFall維持
    /// </summary>
    void StartFallInterval()
    {
        if (memoryPosition != transform.position || MainState.playNow == state || MainController.Instance.GetState == MainState.playStop) { return; }
        state = MainState.playNow;
        MainController.Instance.StartMainGame();
    }

    /// <summary>
    /// アニメーションのアップデート
    /// </summary>
    void FixedAnim()
    {
        Ray ray;

        ray = new Ray(transform.position, -transform.up);
        if (!Physics.Raycast(ray, 1.0f, LayerMask.GetMask("Block")))
        {
            StateAnimation(PlayerState.Fall);
            return;
        }

        ray = new Ray(transform.position, transform.forward + transform.up);

        if (Physics.Raycast(ray, 0.5f, LayerMask.GetMask("Block")))
        {
            StateAnimation(PlayerState.Idol);
            moveSwich = false;
            return;
        }

        if (moveSwich)
        {
            StateAnimation(PlayerState.Walk);
            return;
        }

        StateAnimation(PlayerState.Idol);
    }

    /// <summary>
    /// playerのスピードの変更するためのプロパティ
    /// </summary>
    public float moveSpeedEdit
    {
        set { moveSpeed = value; }
        get { return moveSpeed; }
    }

    /// <summary>
    /// 変更したスピードを元に戻す
    /// </summary>
    public void ResetSpeed()
    {
        moveSpeed = memorySpeed;
    }

    public void RigidBodyVariable()
    {
        GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void SanValueManagement()
    {
        if (isInSanArea)
        {
            sanValue -= Time.deltaTime * 0.1f;
            UIContllor.Instance.SunValueDisplay(sanValue);
            SEmanager.Instance.PlayHeartBeat(transform.position);
        }
        else
        {
            if (sanValue <= 1)
            {
                sanValue += Time.deltaTime * 0.1f;
                UIContllor.Instance.SunValueDisplay(sanValue);
            }
        }

        if (sanValue <= 0)
        {
            MainController.Instance.GameOver();
            StateAnimation(PlayerState.Death);
            SEmanager.Instance.PlayDeath(transform.position);
        }
    }
}
