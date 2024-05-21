using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;      
    public bool isUsed;
    Rigidbody2D rigidbody2D;

    public int index;

    public float EndTime = 0.0f;
    public SpriteRenderer spriteRenderer;

    public GameManager gameManager;

    void Awake()
    {
        isUsed = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.simulated = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (isUsed) return;

        if(isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float leftBorder = -4f + transform.localScale.x / 2f;
            float RightBorder = 4f - transform.localScale.x / 2f;

            if(mousePos.x < leftBorder) mousePos.x = leftBorder;
            if (mousePos.x > RightBorder) mousePos.x = RightBorder;

            mousePos.y = 8;
            mousePos.z = 0;

            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);
        }

        if (Input.GetMouseButtonDown(0)) Drag();
        if (Input.GetMouseButtonUp(0)) Drop();


    }
    void Drag()
    {
        isDrag = true;
        rigidbody2D.simulated = false;
    }
    void Drop()
    {
        isDrag = false;
        isUsed = true;
        rigidbody2D.simulated = true;

        GameObject Temp = GameObject.FindWithTag("GameManager");
        if(Temp != null)
        {
            Temp.gameObject.GetComponent<GameManager>().GenObject();
        }
    }

    public void Used()
    {
        isDrag = false;                   //�巡�װ� ����
        isUsed = true;                    //����� �Ϸ�
        rigidbody2D.simulated = true;      //���� ���� ����
    }
    public void OnTriggerStay2D(Collider2D collision)   //Trigger �浹 ���� ��
    {
        if(collision.tag == "EndLine")                                   //�浹���� ��ü���� Tag �� EndLine �� ���
        {
            EndTime += Time.deltaTime;                                   //�����ӽ��۸�ŭ ���� ���Ѽ� �ʸ� �����

            if(EndTime > 1)                                               //�浹������ 1�� �Ǿ��� ���
            {
                spriteRenderer.color = new Color(0.9f, 0.2f, 0.2f);      //������ ó��
            }
            if(EndTime > 3)
            {
                gameManager.EndGame();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "EndLine")                 //�浹 ��ü�� ���� ��������
        {
            EndTime = 0.0f;
            spriteRenderer.color = Color.white;        //���� �������� ����
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)   //2D �浹�� �Ͼ���
    {
        if (index >= 7)                //�غ�� ������ �ִ� 7��
            return;
        if (collision.gameObject.tag == "Fruit")  //�浹 ��ü�� TAG�� Fruit �� ���
        {
            CircleObject temp = collision.gameObject.GetComponent<CircleObject>();  //�ӽ÷� Class temp�� �����ϰ� �浹ü�� Class���޾ƿ´�

            if(temp.index == index)  //���� ��ȣ�� �������
            {
                if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())  //����Ƽ���� �����ϴ� ������ ID���޾ƿͼ� ID�� ū�ʿ��� ���� ���� ����
                {
                    //gameManger ���� �����Լ� ȣ��
                    GameObject Temp = GameObject.FindWithTag("GameManager");
                    if (Temp != null)
                    {
                        Temp.gameObject.GetComponent<GameManager>().MergeObject(index , gameObject.transform.position); //������ MerageObject �Լ��� �μ��� �Բ� ����
                    }
                    
                    Destroy(temp.gameObject);          //�浹 ��ü �ı�
                    Destroy(gameObject);              //�ڱ� �ڽ� �ı�
                }
            }
        }
    }
}
