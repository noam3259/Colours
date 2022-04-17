using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Renderer renderer;
    private static int BlackCounter;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        BlackCounter=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColor(Color color)
    {//change the color of the target to the given color (will be paintBall color) 
     //if (color == Color.black)
     //{
     //    if (BlackCounter < 2)//if there are less than 2 black objects you can change color
     //    {
     //        if (renderer.material.color == Color.black)//if you shot black color on black object
     //        {
     //            BlackCounter--;//-1 because you shot on black object


        //        }
        //        renderer.material.SetColor("_Color", color);
        //        BlackCounter++;
        //    }

        //}
        //else
        //{
        //    if (renderer.material.color == Color.black)
        //    {
        //        BlackCounter--;
        //    }
        //    renderer.material.SetColor("_Color", color);
        //}

        renderer.material.SetColor("_Color", color);

    }
}
