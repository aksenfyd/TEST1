using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    private List<GameObject> Text = new List<GameObject>();
    private List<GameObject> Price = new List<GameObject>();
    public GameObject Player;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject Statstext = GameObject.Find("Stats");

        //for (int i = 0; i < Statstext.transform.childCount; ++i)
        //{
        //    Text.Add(Statstext.transform.GetChild(i).gameObject);

        //}
        
        GameObject Statstext = GameObject.Find("Stats");

        for (int i=0;i< Statstext.transform.childCount;++i)
        {
            Text.Add(Statstext.transform.GetChild(i).gameObject);
        }
        GameObject Pricetext = GameObject.Find("StatsPrice");

        for (int i=0;i< Pricetext.transform.childCount;++i)
        {
            Price.Add(Pricetext.transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Text[0].GetComponent<Text>().text = ControllerManager.GetInstance().ATKLV.ToString();
        Text[1].GetComponent<Text>().text = ControllerManager.GetInstance().CRILV.ToString();
        Text[2].GetComponent<Text>().text = ControllerManager.GetInstance().HPLV.ToString();
        Text[3].GetComponent<Text>().text = ControllerManager.GetInstance().CRIDMGLV.ToString();
        Text[4].GetComponent<Text>().text = ControllerManager.GetInstance().HPREGENLV.ToString();
        Text[5].GetComponent<Text>().text = ControllerManager.GetInstance().GOLDLV.ToString();


        Price[0].GetComponent<Text>().text = ((ControllerManager.GetInstance().ATKLV + 1) * 100).ToString() + "GOLD";
        Price[1].GetComponent<Text>().text = ((ControllerManager.GetInstance().CRILV + 1) * 100).ToString() + "GOLD";
        Price[2].GetComponent<Text>().text = ((ControllerManager.GetInstance().HPLV + 1) * 100).ToString() + "GOLD";
        Price[3].GetComponent<Text>().text = ((ControllerManager.GetInstance().CRIDMGLV + 1) * 100).ToString() + "GOLD";
        Price[4].GetComponent<Text>().text = ((ControllerManager.GetInstance().HPREGENLV + 1) * 100).ToString() + "GOLD";
        Price[5].GetComponent<Text>().text = ((ControllerManager.GetInstance().GOLDLV + 1) * 100).ToString() + "GOLD";

    }
}
