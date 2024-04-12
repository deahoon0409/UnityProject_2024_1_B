using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExRayCast : MonoBehaviour
{
    public int Point = 0;
    public GameObject TargetObject;
    public float CheckTime = 0;
    public float GameTime = 30.0f;

    public Text pointUI;
    public Text timeUI;
    void Update()
    {
        CheckTime += Time.deltaTime;
        GameTime -= Time.deltaTime;

        if(GameTime <= 0)
        {
            PlayerPrefs.SetInt("Point", Point);
            SceneManager.LoadScene("MainScene");
        }

        pointUI.text = "점수 : " + Point.ToString();

        timeUI.text = "남은 시간 : " + GameTime.ToString() + "s";

        if(CheckTime >= 0.5f)
        {
            int RandomX = Random.Range(0, 12);
            int RandomY = Random.Range(0, 12);
            GameObject temp = Instantiate(TargetObject);
            temp.transform.position = new Vector3(-6 + RandomX, -6 + RandomY, 0);
            Destroy(temp, 1.0f);
            CheckTime = 0;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Ray cast = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(cast, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.DrawLine(cast.origin, hit.point, Color.red, 2.0f);

                if(hit.collider.gameObject.tag == "Target")
                {
                    Point += 1;
                    Destroy(hit.collider.gameObject);
                }
            }


        }
        
    }
}
