using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;      
    public bool isUsed;
    Rigidbody2D rigidbody2D;

    public int index;

    void Awake()
    {
        isUsed = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.simulated = false;
    }

    void Start()
    {
     
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
        isDrag = false;                   //드래그가 종료
        isUsed = true;                    //사용이 완료
        rigidbody2D.simulated = true;      //물리 현상 시작
    }

    public void OnCollisionEnter2D(Collision2D collision)   //2D 충돌이 일어날경우
    {
        if (index >= 7)                //준비된 과일이 최대 7개
            return;
        if (collision.gameObject.tag == "Fruit")  //충돌 물체의 TAG가 Fruit 일 경우
        {
            CircleObject temp = collision.gameObject.GetComponent<CircleObject>();  //임시로 Class temp를 선언하고 충돌체의 Class를받아온다

            if(temp.index == index)  //과일 번호가 같은경우
            {
                if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())  //유니티에서 지원하는 고유의 ID를받아와서 ID가 큰쪽에서 다음 과일 생성
                {
                    //gameManger 에서 생성함수 호출
                    GameObject Temp = GameObject.FindWithTag("GameManager");
                    if (Temp != null)
                    {
                        Temp.gameObject.GetComponent<GameManager>().MergeObject(index , gameObject.transform.position); //생성된 MerageObject 함수에 인수와 함께 전달
                    }
                    
                    Destroy(temp.gameObject);          //충돌 물체 파괴
                    Destroy(gameObject);              //자기 자신 파괴
                }
            }
        }
    }
}
