using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] CircleObject;
    public Transform GenTransform;
    public float TimeCheck;
    public bool isGen;
    // Start is called before the first frame update
    void Start()
    {
        GenObject();
    }

    // Update is called once per frame
    void Update()
    {
       if(!isGen)
        {
            TimeCheck -= Time.deltaTime;
            if(TimeCheck <= 0 )
            {
                int RandNumber = Random.Range(0, 3);
                GameObject Temp = Instantiate(CircleObject[RandNumber]);
                Temp.transform.position = GenTransform.position;
                isGen = true;
            }
        }
    }
    public void GenObject()
    {
        isGen = false;
        TimeCheck = 1.0f;
    }

    public void MergeObject(int index, Vector3 position)   //Merge 함수는 과일번호(int) 과 생성 위치값(Vector3)을 전달 받는다
    {
        GameObject Temp = Instantiate(CircleObject[index]); //index를 그대로 쓴다
        Temp.transform.position = position;                 //위치는 전달 받은 값으로 사용
        Temp.GetComponent<CircleObject>().Used();
    }
}
