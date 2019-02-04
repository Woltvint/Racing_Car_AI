using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronLayer
{
    public float newNeuronChance = 0.001f;

    public List<Neuron> n = new List<Neuron>();


    public void inicLayer(int c)
    {
        for (int i = 0; i < c; i++)
        {
            n.Add(new Neuron());
            n[i].inicNeuron();
        }
    }


    public float[] calcLayer(float[] input)
    {
        float[] output = new float[n.Count];

        for (int i = 0; i < n.Count; i++)
        {
            output[i] = n[i].calcNeuron(input);
        }

        return output;
    }


    public NeuronLayer mutateLayer()
    {
        NeuronLayer output = new NeuronLayer();
        output.n = new List<Neuron>();

        for (int i = 0; i < n.Count; i++)
        {
            output.n.Add(n[i].mutateNeuron());
        }

        if (Random.Range(0, 100) <= newNeuronChance)
        {
            Neuron newNeuron = new Neuron();
            newNeuron.inicNeuron();
            output.n.Add(newNeuron);
        }
        

        return output;
    }

}
