using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 枚举小鸟状态
/// </summary>
public enum BirdState
{
    Waitting,
    BeforShoot,
    AfterShoot,
    WaittingForDead
}
public class Bird : MonoBehaviour
{
    //小鸟状态：等待、发射、发射后
    public BirdState state = BirdState.BeforShoot;
    public bool isMouseDown = false;
    //小鸟能够拖动的半径
    public float maxDistance = 2.4f;
    public float flySpeed = 15;
    protected Rigidbody2D rigidbody2d;
    public bool isFlying = true;
    public bool useSkill = false;
    //收集鸟身上的collider组件，因为大嘴鸟身上有两个collider组件
    private Collider2D[] colliders;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        colliders = GetComponents<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BirdState.Waitting:
                WaitControl();
                break;
            case BirdState.BeforShoot:
                MoveControl();
                break;
            case BirdState.AfterShoot:
                StopControl();
                SkillControl();
                break;
            case BirdState.WaittingForDead:
                break;
            default:
                break;
        }
    }
    private void SetCollidersEnable(bool enabled)
    {
        if (colliders == null)
        {
            return;
        }
        foreach (Collider2D collider in colliders)
        {
            if (collider != null)
            {
                collider.enabled = enabled;
            }
        }
    }
    private void OnMouseDown()
    {
        if (state == BirdState.BeforShoot && EventSystem.current.IsPointerOverGameObject() == false)
        {
            isMouseDown = true;
            //绘制皮筋
            SlingShoot.Instance.StartDraw(transform);
            AudioManerger.Instance.PlayBirdSelect(transform.position);
        }
    }
    private void OnMouseUp()
    {
        if (state == BirdState.BeforShoot && EventSystem.current.IsPointerOverGameObject() == false)
        {
            isMouseDown = false;
            SlingShoot.Instance.EndDraw();
            Fly();
        }
    }
    private void MoveControl()
    {
        SetCollidersEnable(true);
        if (isMouseDown)
        {
            transform.position = GetMousePosition();
        }
    }
    /// <summary>
    /// 获取鼠标世界坐标位置
    /// </summary>
    private Vector3 GetMousePosition()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.z = 0;
        //计算判断是否超出最大范围
        float distance = (mp - SlingShoot.Instance.GetCenterPoint()).magnitude;
        //如果超出范围，强制限制范围
        if (distance > maxDistance)
        {
            mp = (mp - SlingShoot.Instance.GetCenterPoint()).normalized * maxDistance + SlingShoot.Instance.GetCenterPoint();
        }
        return mp;
    }
    public void Fly()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2d.velocity = (SlingShoot.Instance.GetCenterPoint() - transform.position).normalized * flySpeed;
        AudioManerger.Instance.PlayBirdFlying(transform.position);
        state = BirdState.AfterShoot;
    }
    public void GoStage(Vector3 position)
    {
        state = BirdState.BeforShoot;
        transform.position = position;
        isMouseDown = false;
        isFlying = true;
        useSkill = false;


        rigidbody2d.velocity = Vector2.zero;
        rigidbody2d.angularVelocity = 0f;
        rigidbody2d.bodyType = RigidbodyType2D.Static;

        SetCollidersEnable(true);
    }
    public void StopControl()
    {
        if (rigidbody2d.velocity.magnitude < 0.1f)
        {
            state = BirdState.WaittingForDead;
            Invoke("LoadNextBird", 1f);
        }
    }
    protected void LoadNextBird()
    {
        Destroy(gameObject);
        GameObject.Instantiate(Resources.Load("Boom1"), transform.position, Quaternion.identity);
        GameManerger.Instance.loadNextBird();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        isFlying = false;
        if (other.relativeVelocity.magnitude > 5 && state == BirdState.AfterShoot)
        {
            AudioManerger.Instance.PlayBirdCollision(transform.position);
        }
    }
    public void SkillControl()
    {
        if (!Input.GetMouseButtonDown(0) || useSkill)
        {
            return;
        }
        if (isFlying == true && Input.GetMouseButton(0) && !useSkill)
        {
            FlyingSkill();
        }
        else
        {
            FullTimeSkill();
        }
        useSkill = true;
    }
    public virtual void FlyingSkill()
    {

    }
    public virtual void FullTimeSkill()
    {

    }
    public void WaitControl()
    {
        SetCollidersEnable(false);
    }
    public void GoWait()
    {
        state = BirdState.Waitting;
        isMouseDown = false;
        isFlying = true;
        useSkill = false;
        rigidbody2d.velocity = Vector2.zero;
        rigidbody2d.angularVelocity = 0f;
        rigidbody2d.bodyType = RigidbodyType2D.Static;
        SetCollidersEnable(false);
    }
}
