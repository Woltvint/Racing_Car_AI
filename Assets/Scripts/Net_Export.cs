using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net_Export : MonoBehaviour
{
    public Car_Control car;

    public bool dothat = false;


    private void FixedUpdate()
    {
        if (dothat)
        {
            printNet();
        }
    }

    void printNet()
    {
        if (car)
        {
            NeuralNet net = car.net;
            string output = "";

            output += "<net>\n";
            for (int l = 0; l < net.nl.Count; l++)
            {
                output += "<layer>\n";
                for (int n = 0; n < net.nl[l].n.Count; n++)
                {
                    output += "<neuron>\n";
                    output += "<w>\n";
                    for (int w = 0; w < net.nl[l].n[n].w.Count; w++)
                    {
                        output += "w: " + net.nl[l].n[n].w[w] + "\n";
                    }
                    output += "</w>\n";

                    output += "<b>\n";
                    output += "b: " + net.nl[l].n[n].b + "\n";
                    output += "</b>\n";
                    output += "</neuron>\n";
                }
                output += "</layer>\n";
            }
            output += "</net>\n";

            Debug.Log(output);
            dothat = false;
        }

        
    }

}
