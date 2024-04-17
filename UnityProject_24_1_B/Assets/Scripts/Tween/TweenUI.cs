using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TweenUI : MonoBehaviour
{
    public float duration = 1f;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        image.DOFade(0f , duration)
               .SetEase(Ease.InOutQuad)
               .SetAutoKill(false)
               .Pause()
               .OnComplete(() => Debug.Log("UI ¿Ï·á"));

        image.DOPlay();
    }

  
    
}
