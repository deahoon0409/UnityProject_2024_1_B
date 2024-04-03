using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExPlayer : MonoBehaviour
{
    public Rigidbody m_Rigidbody;
    public int Force = 100;
    public int point = 0;
    public float checkTime = 0;
    public Text m_Text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Rigidbody.AddForce(transform.up * Force);
        }

        checkTime += Time.deltaTime;
        if (checkTime >= 1.0f)
        {
            point += 1;
            checkTime = 0.0f;
        }

        m_Text.text = point.ToString();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            point = 0;
            gameObject.transform.position = new Vector3(0.0f, 3.0f, 0.0f);
            Debug.Log(collision.gameObject.tag);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Item"))
        {
            Debug.Log("아이템과 충돌함");
            point += 10;
            Destroy(other.gameObject);
        }

    }
    
       

        


}
