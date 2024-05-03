using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject CircleObject;
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
                GameObject Temp = Instantiate(CircleObject);
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
}
