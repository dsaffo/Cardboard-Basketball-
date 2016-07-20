using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Shoot : MonoBehaviour
{
    bool increase = true;
    bool throwball = false;
    int color = 0;
    public double chargetime = 0;
    private int count = 0;
    public GameObject ball;
    private GameObject currentBall;
    public Vector3 throwSpeed = new Vector3(0, 7.901f, 7.901f);
    public Vector3 ballPos;
    private bool ballThrown = false;
    private GameObject ballClone;
    private GameObject staticBall;
    public Text charge;
    public Text score;
  
    private double passedTime;
    private int totalScore;
    private int roundScore;
    
    private float power;
    int weight = 0;
    int counter;

    static Color c1 = new Color(0, 0, 0, 1);
    static Color c2 = new Color(1, 0, 1, 1);
    static Color c3 = new Color(1, 0, 0, 1);
  
    Color[] colors = new Color[] { c1, c2, c3};
    List<Color> ColorList = new List<Color>();
    float currentTime = 0;
    static float w1 = .85f;
    static float w2 = 1f;
    static float w3 = 1.15f;
  
    float[] weights= new float[] { w1, w2, w3};
    List<float>  WeightList = new List<float>();



    
    

    CardboardHead head = null;

   
    void Start()
    {

        Application.targetFrameRate = 60;
        head = Camera.main.GetComponent<StereoController>().Head;
        currentBall = ball;
        ShuffleColor(colors);
        ShuffleWeight(weights);
        NewBall(ballThrown);
        Physics.gravity = new Vector3(0, -9.8f, 0);

        
    }

    void NewBall(bool thrown)
    {
        if (!thrown)
        {
            BallColor();
            staticBall = Instantiate(currentBall, new Vector3(2.28f, 2.83f, -3), Quaternion.identity) as GameObject;
            staticBall.GetComponent<Rigidbody>().useGravity = false;
        }
        if (thrown)
        {
            Destroy(staticBall);
        }
    }

    void ShuffleColor(Color[] picker)
    {
        ColorList.AddRange(picker);
        for (int i = 0; i < ColorList.Count; i++)
        {
            Color temp = ColorList[i];
            int randomIndex = UnityEngine.Random.Range(i, ColorList.Count);
            ColorList[i] = ColorList[randomIndex];
            ColorList[randomIndex] = temp;
        }
    }

    void ShuffleWeight(float [] weight)
    {
        WeightList.AddRange(weight);
        for (int i = 0; i < WeightList.Count; i++)
        {
            float temp = WeightList[i];
            int randomIndex = UnityEngine.Random.Range(i, WeightList.Count);
            WeightList[i] = WeightList[randomIndex];
            WeightList[randomIndex] = temp;
        }
    }

   
    void BallColor()
    {
        
      
        MeshRenderer gameObjectRenderer = currentBall.GetComponent<MeshRenderer>();


        Material newMaterial = new Material(Shader.Find("Standard"));

        if (color <= 2)
        {
            newMaterial.color = ColorList[color];
            color++;
        }
        else
        {
            color = 0;
            newMaterial.color = ColorList[color];
            color++;
        }
        gameObjectRenderer.material = newMaterial;

    }

    float ballWeight()
    {

        if (weight <= 2)
        {
            float aux = WeightList[weight];
            weight++;
            return aux ;
            
        }
        else
        {
            weight = 0;
            float aux = WeightList[weight];
            weight++;
            return aux;
        }
        
    }
   

    void Update()
    {
        counter++;
        if (counter % 2 == 0 && increase == true && !ballThrown)
        {
            power++;
            charge.text = power.ToString() + "%";
            if (power == 100)
            {
                increase = false;
            }
            
           
        }
        else if (counter % 2 == 0 && increase == false && !ballThrown)
        {
            power--;
            charge.text = power.ToString() + "%";
            if (power == 0)
            {
                increase = true;
            }

        }

        
        if (Cardboard.SDK.Triggered)
        {
            charge.text = power.ToString() + "%";
            
            throwball = true;

        }


        
    

        if (throwball && !ballThrown)
        {
            ballThrown = true;
            
            NewBall(ballThrown);  
            ballClone = Instantiate(currentBall, ballPos, transform.rotation) as GameObject;
            float multi = ballWeight();
            throwSpeed.y = power/10 * multi;
            throwSpeed.z = power/10 * multi;
            print("weight " + ballWeight());
            ballClone.GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.Impulse);
            
        }

        if (ballClone != null && ballClone.transform.position.y < -10 )
        {
            Destroy(ballClone);
            ballThrown = false;
            throwball = false;
            NewBall(ballThrown);
           

            power = 0;
            increase = true;

        }


    }

}
