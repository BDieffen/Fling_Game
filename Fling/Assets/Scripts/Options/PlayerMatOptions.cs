using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMatOptions : MonoBehaviour
{

    public Material[] ballMats = new Material[8];
    public int ballSelectSpot;
    public Material selectedBall;
    public Material[] paddleMats = new Material[8];
    public int paddleSelectSpot;
    public Material selectedPaddle;
    public Image[] backgroundImages = new Image[5];

    void Start()
    {


    }

    void Update()
    {

    }

    public void scrollRight(Material[] array)
    {
        if (array == ballMats)
        {
            if (ballSelectSpot == 7)
            {
                ballSelectSpot = 0;
            }
            else
            {
                ballSelectSpot++;
            }
            selectedBall = ballMats[ballSelectSpot];
        }
        else if (array == paddleMats)
        {
            if (paddleSelectSpot == 7)
            {
                paddleSelectSpot = 0;
            }
            else
            {
                paddleSelectSpot++;
            }
            selectedPaddle = paddleMats[paddleSelectSpot];
        }
    }

    public void scrollLeft(Material[] array)
    {
        if (array == ballMats)
        {
            if (ballSelectSpot == 0)
            {
                ballSelectSpot = 7;
            }
            else
            {
                ballSelectSpot--;
            }
            selectedBall = ballMats[ballSelectSpot];
        }
        else if (array == paddleMats)
        {
            if (paddleSelectSpot == 0)
            {
                paddleSelectSpot = 7;
            }
            else
            {
                paddleSelectSpot--;
            }
            selectedPaddle = paddleMats[paddleSelectSpot];
        }
    }
}
