using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    private List<GameObject> Text = new List<GameObject>();
    private Text text;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject Statstext = GameObject.Find("Stats");

        //for (int i = 0; i < Statstext.transform.childCount; ++i)
        //{
        //    Text.Add(Statstext.transform.GetChild(i).gameObject);

        //}
        GameObject Statstext = GameObject.Find("LevelStats");
        Player = GameObject.Find("Player");


        for (int i = 0; i < Statstext.transform.childCount; ++i)
        {
            Text.Add(Statstext.transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Text[0].GetComponent<Text>().text = Player.GetComponent<PlayerController>().IDAMAGE.ToString("F1");
        Text[1].GetComponent<Text>().text = Player.GetComponent<PlayerController>().COOL.ToString("F1");
        Text[2].GetComponent<Text>().text = Player.GetComponent<PlayerController>().LDAMAGE.ToString();
        Text[3].GetComponent<Text>().text = Player.GetComponent<PlayerController>().IEXP.ToString();
        Text[4].GetComponent<Text>().text = Player.GetComponent<PlayerController>().THROW.ToString();
        Text[5].GetComponent<Text>().text = Player.GetComponent<PlayerController>().ATKSPEED.ToString("F1");
        Text[6].GetComponent<Text>().text = "LevelPoint : " + Player.GetComponent<PlayerController>().LEVELPOINT.ToString();
    }
}
