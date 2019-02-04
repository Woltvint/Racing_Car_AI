using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Control : MonoBehaviour
{

    public bool alive = true;
    public float maxTurnRange = 5;
    public float maxVelRange = 5;

    public Rigidbody2D rb;
    public NeuralNet net;
    public GameObject start;

    public List<Vector2> pos = new List<Vector2>();

    public bool a = true;

    public Sensor sf;
    public Sensor sl;
    public Sensor sr;
    public Sensor sls;
    public Sensor srs;
    

    void Start()
    {
        net = new NeuralNet();
        net.inicNet(1,4,2);
        rb = gameObject.GetComponent<Rigidbody2D>();
        start = GameObject.FindGameObjectWithTag("Start");
    }

    int step = 150;

    void FixedUpdate()
    {
        if (a)
        {
            if (alive)
            {

                float[] input = new float[5];

                if (sf.touch) { input[0] = 1; } else { input[0] = 0; }
                if (sr.touch) { input[1] = 1; } else { input[1] = 0; }
                if (sl.touch) { input[2] = 1; } else { input[2] = 0; }
                if (srs.touch) { input[3] = 1; } else { input[3] = 0; }
                if (sls.touch) { input[4] = 1; } else { input[4] = 0; }

                float[] control = net.calcNet(input);

                Turn(control[1]*5);
                Vel(control[0]*2);
                
            }
            a = false;
        }
        else
        {
            a = true;
        }

        if (step > 0)
        {
            step--;
        }
        else
        {
            pos.Add(transform.position);
            step = 150;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Car")
        {
            alive = false;
            rb.velocity = new Vector3(0,0,0);
            net.score = getScore();
        }
    }

    float getScore()
    {
        float output = 0;

        for (int i = 0; i < pos.Count; i++)
        {
            if (i < pos.Count-1)
            output += Vector2.Distance(pos[i],pos[i+1]);
        }

        pos.Clear();

        return output;
    }

    void Vel(float amount)
    {
        amount = sigmoid(amount, maxVelRange);
        rb.velocity = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad + (Mathf.PI/2)), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad + (Mathf.PI / 2))) * amount;
    }

    void Turn(float amount)
    {
        amount = sigmoid(amount, maxTurnRange*2) - maxTurnRange;
        transform.Rotate(Vector3.back,amount);
    }

    float sigmoid(float x,float range)
    {
        return range / (1.0f + Mathf.Exp(-x));
    }
}
