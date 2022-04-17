using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePaintBallColor : MonoBehaviour
{
    public GameObject scope;
    

    public Color[] colors = new Color[] { Color.red, Color.blue, Color.black, Color.white, Color.yellow };
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        scope.GetComponent<Image>().color = gameObject.transform.GetComponent<Renderer>().sharedMaterial.color;//Set the scope color to be the same as the paintball color

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            gameObject.transform.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", colors[index]);//change material according to colors arr
            

            scope.GetComponent<Image>().color = colors[index];//change the scope color as well

            index += 1;
            if (index == colors.Length)//if index variable got out of the index reset it back to 0
            {
                index = 0;
            }
        }
    }
}
