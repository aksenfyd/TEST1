using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject ui;
    public RectTransform uiTranpos;

    public float sizeX;
    public float sizeY;

    private void Awake()
    {
        uiTranpos = ui.GetComponent<RectTransform>();
    }
    private void Start()
    {
        sizeX = 1000.0f;
        sizeY = 500.0f;


    }
    private void OnEnable()
    {
     
        StartCoroutine(EffectUI());
    }

    private void OnDisable()
    {

        uiTranpos.sizeDelta = new Vector2(5.0f, 5.0f);

    }
    IEnumerator EffectUI()
    {
        float fTime = 0.0f;
        while(uiTranpos.sizeDelta.y < sizeY)
        {
            fTime += Time.deltaTime * 4.0f;

            uiTranpos.sizeDelta = Vector2.Lerp(
                new Vector2(5.0f, 5.0f),
                new Vector2(5.0f, sizeY),
                fTime
                );
            yield return null;
        }
            fTime = 0.0f;
        while(uiTranpos.sizeDelta.x < sizeX)
        {
            fTime += Time.deltaTime * 4.0f;

            uiTranpos.sizeDelta = Vector2.Lerp(
                new Vector2(5.0f, sizeY),
                new Vector2(sizeX, sizeY),
                fTime
                );
            yield return null;
        }
    }
}
