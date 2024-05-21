using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject[] CircleObject;
    public Transform GenTransform;
    public float TimeCheck;
    public bool isGen;

    public int Point;                                 //점수 값 선언  (int)
    public int BestScore;                            //스코어 값 선언 (int)
    public static event Action<int> OnPointChanged;
    public static event Action<int> OnBestScoreChanged;
    // Start is called before the first frame update
    void Start()
    {
        BestScore = PlayerPrefs.GetInt("BestScore");
        GenObject();
        OnPointChanged?.Invoke(Point);
        OnBestScoreChanged?.Invoke(BestScore);
    }

    // Update is called once per frame
    void Update()
    {
       if(!isGen)
        {
            TimeCheck -= Time.deltaTime;
            if(TimeCheck <= 0 )
            {
                int RandNumber = UnityEngine.Random.Range(0, 3);
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

        Point += (int)Mathf.Pow(index, 2) * 10;   //index의 2승으로 점수 포인트 증가 Pow 함수 사용
        OnPointChanged?.Invoke(Point);            //포인트가 변경되었을때 이벤트에 변경 되었다고 알림
    }

    public void EndGame()
    {
        int BestScore = PlayerPrefs.GetInt("BestScore");    //기존에 저장된 점수를 불러온다

        if(Point > BestScore)                              //포인트와 비교한다
        {
            BestScore = Point;
           PlayerPrefs.SetInt("BestScore", BestScore);         //포인트가 더클경우 저장한다
            OnBestScoreChanged?.Invoke(Point);
        } 
    }
}
