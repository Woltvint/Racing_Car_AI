using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron
{
    public float mutationRate = 0.1f;
    public float mutationChance = 20;

    public float b;
    public List<float> w = new List<float>();

    public void inicNeuron()
    {
        b = 0;// Random.Range(-1.0f,1.0f);
    }


    public float calcNeuron(float[] input)
    {
        float output = b;

        for (int i = 0; i < input.Length; i++)
        {
            if (w.Count > i)
            {
                output += input[i] * w[i];
            }
            else
            {
                w.Add(0/*Random.Range(-1.0f,1.0f)*/);
            }
        }

        return sigmoid(output, 1);
    }


    public Neuron mutateNeuron()
    {
        Neuron n = new Neuron();
        n.inicNeuron();
        
        

        for (int i = 0; i < w.Count; i++)
        {
            if (Random.value * 100 < mutationChance)
            {
                n.w.Add(w[i] + Random.Range(-mutationRate, mutationRate));
            }
            else
            {
                n.w.Add(w[i]);
            }
        }

        if (Random.value * 100 < mutationChance)
        {
            n.b = b + Random.Range(-mutationRate, mutationRate);
        }
        else
        {
            n.b = b;
        }

        return n;
    }


    float sigmoid(float x, float range)
    {
        return (range / (1.0f + Mathf.Exp(-x))) - range/2;
    }
}
