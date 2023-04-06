using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{

    private Text text;
    private RectTransform rectTransform;

    private Color oldColor;

    private void Awake()
    {
        
        text = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();


    }
    private void Start()
    {
        text.text = transform.name;
        
    }


    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        text.color = oldColor;

        SceneManager.LoadScene(text.text);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        oldColor = text.color;
        text.color = Color.white;
    }

}
