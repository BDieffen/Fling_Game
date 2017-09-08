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
    public int ballSelectSpot = 0;
    public Material selectedBall;
    public Material[] paddleMats = new Material[8];
    public int paddleSelectSpot = 0;
    public Material selectedPaddle;
    public Image[] backgroundImages = new Image[5];

    public GameObject ball;
    public GameObject paddle;

    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/playerOptions.dat"))
        {
            Load();
        }

    }

    void Update()
    {

    }

    public void BallRight()
    {
        scrollRight(ballMats);
    }

    public void BallLeft()
    {
        scrollLeft(ballMats);
    }

    public void PaddleRight()
    {
        scrollRight(paddleMats);
    }

    public void PaddleLeft()
    {
        scrollLeft(paddleMats);
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
            ball.GetComponent<MeshRenderer>().material = selectedBall;
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
            paddle.GetComponent<MeshRenderer>().material = selectedPaddle;
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
            ball.GetComponent<MeshRenderer>().material = selectedBall;
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
            paddle.GetComponent<MeshRenderer>().material = selectedPaddle;
        }
    }

    private void Load()
    {
        
    }

    public void BackAndSave()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    [System.Serializable]
    public class PlayerOptions
    {
        
    }
}
