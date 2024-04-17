using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenChange : MonoBehaviour
{
    public bool isPunch = false;
 
 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isPunch)
            {
                isPunch = true;
                transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.1f, 10, 1).OnComplete(EndPuch);

            }
        }    
    }
    void EndPuch()
    {
        isPunch = false;
    }
}
