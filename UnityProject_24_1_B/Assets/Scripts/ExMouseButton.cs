using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExMouseButton : MonoBehaviour
{
    public int Hp = 100;
    public Text textUI;

    // Update is called once per frame
    void Update()                //매 프레임마다 호출 된다.
    {
        textUI.text = "체력 : " + Hp.ToString();

        if (Input.GetMouseButtonDown(0)) //마우스 입력이 들어왔을 때
        {
            Hp -= 10;
            Debug.Log("체력 : " + Hp);       
            if (Hp <= 0 )
            {
                textUI.text = "체력 : " + Hp.ToString();
                Destroy(gameObject);
            }
        }
    }
}
