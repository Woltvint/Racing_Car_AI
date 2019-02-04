using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNet
{
    public float score = 0;
    public List<NeuronLayer> nl = new List<NeuronLayer>();

    public void inicNet(int l, int n, int on)
    {
        for (int i = 0; i < l; i++)
        {
            nl.Add(new NeuronLayer());
            if (i < l - 1)
            {
                nl[i].inicLayer(n);
            }
            else
            {
                nl[i].inicLayer(on);
            }
        }
    }

    public float[] calcNet(float[] input)
    {
        float[] output = input;

        for (int i = 0; i < nl.Count; i++)
        {
            output = nl[i].calcLayer(output);
        }

        return output;
    }

    public NeuralNet mutateNet()
    {
        NeuralNet net = new NeuralNet();
        net.nl = new List<NeuronLayer>();

        for (int i = 0; i < nl.Count; i++)
        {
            net.nl.Add(nl[i].mutateLayer());
        }

        return net;
    }


}
