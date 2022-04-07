using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public Transform centerPos;//中心点
    public float maxDis = 1.25f;//最大拖拽距离 
    private bool isClick = false;//是否点击
    private SpringJoint2D sp; //SpringJoint2D（弹簧）组件的变量
    private Rigidbody2D rg;//Rigidbody2D（刚体）组件的变量
    public LineRenderer right, left;//左右树枝的线条渲染器
    public Transform rightPos, leftPos;//左右树枝的拉线点
    public GameObject mainCamera;
    public GameObject birdCamera;
    private bool isStop;
    private bool isfly;
    private Rigidbody2D playerRb;
    public GameObject GameMg;
    public GameObject birdMode;
    private GameManager gameManager;
    public Vector3 startPosition;
    private AudioSource flyAudio;
    public AudioClip collideAudio;
    private void Awake()//在游戏开始时调用
    {
        
        sp = GetComponent<SpringJoint2D>();
        //获取到脚本绑定物体上的SpringJoint2D组件
        rg = GetComponent<Rigidbody2D>();//放在Awake()中获取到脚本绑定物体的Rigidbody2D（刚体）组件
        mainCamera.SetActive(true);
        birdCamera.SetActive(false);
        playerRb = GetComponent<Rigidbody2D>();
        isfly = false;
        gameManager = GameMg.GetComponent<GameManager>();
        flyAudio = gameObject.GetComponent<AudioSource>();
    }
    public void ResetBird()
    {
        Vector3 positionbird = transform.position;
        Quaternion rotatebird = transform.rotation;
        transform.position = startPosition;//回到初始位置
        sp.enabled = true;
        mainCamera.SetActive(true);
        birdCamera.SetActive(false);
        mainCamera.transform.position = new Vector3(13.08f, 0, -10);
        rg.isKinematic = false;
        isfly = false;
        Instantiate(birdMode, positionbird, rotatebird);
        isStop = false;

    }

    private void OnMouseDown()//鼠标按下
    {
        isClick = true;
        //是否点击状态为flase


  }
        private void OnMouseUp()//鼠标松开
    {
        isClick = false;//是否点击状态为flase
        rg.isKinematic = false;
        //把刚体组件上的是否使用动力学设置为不用（让弹簧把鸟弹出去）
        Invoke("Fly", 0.1f);
        //0.1S后调用Fly方法，为了让小鸟在出发的时候有个动力

    }
    private void Update()//游戏每一帧都会调用一次
    {
        if (!isfly)
        {
            Line();
        }
        if (isClick)//如果鼠标按下,控制拖拽距离并画线
        {
            //调整坐标
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, 10);
            if (Vector3.Distance(transform.position,centerPos.position)>maxDis)
            {
                Vector3 pos = (transform.position - centerPos.position).normalized;
                pos *= maxDis;
                transform.position = pos + centerPos.position;

            }


        }
        if (!sp.enabled&&playerRb.velocity == new Vector2(0,0)&&!isStop)//小鸟停下之后重置
        {
            Invoke("ResetBird", 0.5f);
            isStop=true;
        }
        
    }
    void Fly()//小鸟飞出时调用的方法
    {
        flyAudio.Play();
        sp.enabled = false;//将SpringJoint2D组件关闭（释放小鸟）
        mainCamera.SetActive(false);
        birdCamera.SetActive(true );
        right.enabled = false;//把右树枝线条渲染器关闭
        left.enabled = false;//把左树枝线条渲染器关闭
        isfly = true;
    }
    void Line() //连线方法
    {
        right.enabled = true;
        left.enabled = true;
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);
        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        flyAudio.PlayOneShot(collideAudio);
    }
}
