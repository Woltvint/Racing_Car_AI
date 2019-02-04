using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject s;
    public GameObject carP;
    public int gen = 0;
    public int carspergen;

    void Start()
    {
        s = GameObject.FindGameObjectWithTag("Start");

        for (int i = 0; i < carspergen; i++)
        {
            Instantiate(carP, s.transform.position, s.transform.rotation, s.transform);
        }

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Follow>().cameraInic();

    }

    private void FixedUpdate()
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

        bool state = false;

        foreach (GameObject car in cars)
        {
            state |= car.GetComponent<Car_Control>().alive;
        }

        if (!state)
        {
            List<NeuralNet> nets = new List<NeuralNet>();
            for (int i = 0; i < cars.Length; i++)
            {
                nets.Add(cars[i].GetComponent<Car_Control>().net);
            }


            nets.Sort(new NetComp());
            nets.Reverse();


            
            Debug.Log("-----------------");
            Debug.Log(nets[0] == nets[1]);
            Debug.Log(nets[0].score);
            Debug.Log(nets[1].score);
            Debug.Log(nets[2].score);

            Debug.Log(nets[0].nl[0].n.Count);
            Debug.Log(nets[1].nl[0].n.Count);
            Debug.Log(nets[2].nl[0].n.Count);

            Debug.Log(nets[0].nl[0].n[0].b);
            Debug.Log(nets[1].nl[0].n[0].b);
            Debug.Log(nets[2].nl[0].n[0].b);
            Debug.Log("-----------------");
            


            NeuralNet[] newNets = new NeuralNet[cars.Length];
            for (int i = 0; i < newNets.Length; i++)
            {
                while (true)
                {
                    int r = Random.Range(0, 5);
                    if (Random.Range(0, nets[0].score) < Mathf.Abs(nets[r].score)+10)
                    {
                        newNets[i] = nets[r];
                        newNets[i].score = 0;
                        break;
                    }
                }
            }

            newNets[0] = nets[0];
            newNets[0].score = 0;
            newNets[1] = nets[0];
            newNets[1].score = 0;
            newNets[1] = nets[1];
            newNets[1].score = 0;
            newNets[1] = nets[1];
            newNets[1].score = 0;

            for (int i = 0; i < cars.Length; i++)
            {
                Car_Control c = cars[i].GetComponent<Car_Control>();
                c.transform.position = s.transform.position;
                c.transform.rotation = s.transform.rotation;
                if (i < 4)
                {
                    c.net = newNets[Mathf.FloorToInt(i / 2)];
                }
                else
                {
                    if (i > cars.Length - 5)
                    {
                        NeuralNet nn = new NeuralNet();
                        nn.inicNet(Random.Range(1, 10), Random.Range(1, 10), 2);
                        c.net = nn;
                    }
                    else
                    {
                        c.net = newNets[Random.Range(0, newNets.Length)].mutateNet();
                    }
                }

                c.alive = true;
                c.net.score = 0;

            }

            gen++;

        }
    }


}





public class NetComp : IComparer<NeuralNet>
{
    public int Compare(NeuralNet x, NeuralNet y)
    {
        if (x == null)
        {
            if (y == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }
        else
        {
            if (y == null)
            {
                return -1;
            }
            else
            {
                int difference = x.score.CompareTo(y.score);

                if (difference != 0)
                {
                    return difference;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}

