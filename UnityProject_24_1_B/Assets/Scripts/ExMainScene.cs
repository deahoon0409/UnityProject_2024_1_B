using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ExMainScene : MonoBehaviour
{
    public Text PointUI;
    // Start is called before the first frame update
    void Start()
    {
        PointUI.text = PlayerPrefs.GetInt("Point").ToString();

    }

    public void GotoGame()
    {
        SceneManager.LoadScene("GameScene_Shoot");
    }

}
