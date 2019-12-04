using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite hoverSprite;
    private bool isIdle;

    // Start is called before the first frame update
    void Start()
    {
        isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        transform.GetComponent<Image>().sprite = hoverSprite;
    }

    private void OnMouseExit()
    {
        transform.GetComponent<Image>().sprite = idleSprite;
    }
}
