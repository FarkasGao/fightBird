using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public Transform centerPos;//���ĵ�
    public float maxDis = 1.25f;//�����ק���� 
    private bool isClick = false;//�Ƿ���
    private SpringJoint2D sp; //SpringJoint2D�����ɣ�����ı���
    private Rigidbody2D rg;//Rigidbody2D�����壩����ı���
    public LineRenderer right, left;//������֦��������Ⱦ��
    public Transform rightPos, leftPos;//������֦�����ߵ�
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
    private void Awake()//����Ϸ��ʼʱ����
    {
        
        sp = GetComponent<SpringJoint2D>();
        //��ȡ���ű��������ϵ�SpringJoint2D���
        rg = GetComponent<Rigidbody2D>();//����Awake()�л�ȡ���ű��������Rigidbody2D�����壩���
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
        transform.position = startPosition;//�ص���ʼλ��
        sp.enabled = true;
        mainCamera.SetActive(true);
        birdCamera.SetActive(false);
        mainCamera.transform.position = new Vector3(13.08f, 0, -10);
        rg.isKinematic = false;
        isfly = false;
        Instantiate(birdMode, positionbird, rotatebird);
        isStop = false;

    }

    private void OnMouseDown()//��갴��
    {
        isClick = true;
        //�Ƿ���״̬Ϊflase


  }
        private void OnMouseUp()//����ɿ�
    {
        isClick = false;//�Ƿ���״̬Ϊflase
        rg.isKinematic = false;
        //�Ѹ�������ϵ��Ƿ�ʹ�ö���ѧ����Ϊ���ã��õ��ɰ��񵯳�ȥ��
        Invoke("Fly", 0.1f);
        //0.1S�����Fly������Ϊ����С���ڳ�����ʱ���и�����

    }
    private void Update()//��Ϸÿһ֡�������һ��
    {
        if (!isfly)
        {
            Line();
        }
        if (isClick)//�����갴��,������ק���벢����
        {
            //��������
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, 10);
            if (Vector3.Distance(transform.position,centerPos.position)>maxDis)
            {
                Vector3 pos = (transform.position - centerPos.position).normalized;
                pos *= maxDis;
                transform.position = pos + centerPos.position;

            }


        }
        if (!sp.enabled&&playerRb.velocity == new Vector2(0,0)&&!isStop)//С��ͣ��֮������
        {
            Invoke("ResetBird", 0.5f);
            isStop=true;
        }
        
    }
    void Fly()//С��ɳ�ʱ���õķ���
    {
        flyAudio.Play();
        sp.enabled = false;//��SpringJoint2D����رգ��ͷ�С��
        mainCamera.SetActive(false);
        birdCamera.SetActive(true );
        right.enabled = false;//������֦������Ⱦ���ر�
        left.enabled = false;//������֦������Ⱦ���ر�
        isfly = true;
    }
    void Line() //���߷���
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
