using System.Collections.Generic;
using System;


public class Manager
{
    static NeuralNetwork net;
    static int[] layers = new int[3] { 3, 5, 2 };
    static string[] activation = new string[2] { "leakyrelu", "leakyrelu" };

    public string Run()
    {
        net = new NeuralNetwork(layers, activation);
        for (int i = 0; i < 20000; i++)
        {
            net.BackPropagate(new float[] { 0.5f, 0.5f }, new float[] { 1f, 1f });
            net.BackPropagate(new float[] { 0f, 0.7f }, new float[] { 0.7f, 0.7f });
            net.BackPropagate(new float[] { 1f, 0f }, new float[] { 1f, 1f });
            net.BackPropagate(new float[] { 0f, 1f }, new float[] { 1f, 1f });
            net.BackPropagate(new float[] { 0.3f, 0.2f }, new float[] { 0.5f, 0.5f });
        }
        //net.Load("./MLP.txt");

        Console.WriteLine("cost: " + net.cost);
        string str = "";
        str += Convert.ToString(net.FeedForward(new float[] { 0.5f, 0.5f })[0]) + " " + Convert.ToString(net.FeedForward(new float[] { 0.5f, 0.5f })[1]) + "\n";
        str += Convert.ToString(net.FeedForward(new float[] { 0.2f, 0.3f })[0]) + " " + Convert.ToString(net.FeedForward(new float[] { 0.2f, 0.3f })[1]) + "\n";
        str += Convert.ToString(net.FeedForward(new float[] { 1f, 0f })[0]) + " " + Convert.ToString(net.FeedForward(new float[] { 1f, 0f })[1]) + "\n";
        str += Convert.ToString(net.FeedForward(new float[] { 0.2f, 0.3f })[0]) + "\n";
        str += Convert.ToString(net.FeedForward(new float[] { 1f, 0f })[0]) + "\n";
        net.Save("./MLP.txt");
        return str;
    }
}