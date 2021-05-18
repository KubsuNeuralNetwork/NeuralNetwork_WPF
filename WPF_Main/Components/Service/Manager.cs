using System.Collections.Generic;
using System;

public class Manager
{
    static NeuralNetwork net;
    static int[] layers = new int[3] { 3, 5, 1 };
    static string[] activation = new string[2] { "leakyrelu", "leakyrelu" };

    public void Run()
    {
        net = new NeuralNetwork(layers, activation);
        for (int i = 0; i < 20000; i++)
        {
            net.BackPropagate(new float[] { 0.5f, 0.5f }, new float[] { 1f });
            net.BackPropagate(new float[] { 0f, 0.7f }, new float[] { 0.7f });
            net.BackPropagate(new float[] { 1f, 0f }, new float[] { 1f });
            net.BackPropagate(new float[] { 0f, 1f }, new float[] { 1f });
            net.BackPropagate(new float[] { 0.3f, 0.2f }, new float[] { 0.5f });

        }
        //net.Load("./MLP.txt");

        Console.WriteLine("cost: " + net.cost);

        Console.WriteLine(net.FeedForward(new float[] { 0.5f, 0.5f })[0]);
        Console.WriteLine(net.FeedForward(new float[] { 0.2f, 0.3f })[0]);
        Console.WriteLine(net.FeedForward(new float[] { 1f, 0f})[0]);
        net.Save("./MLP.txt");
        Console.Read();

        //We want the gate to simulate 3 input or gate (A or B or C)
        // 0 0 0    => 0
        // 1 0 0    => 1
        // 0 1 0    => 1
        // 0 0 1    => 1
        // 1 1 0    => 1
        // 0 1 1    => 1
        // 1 0 1    => 1
        // 1 1 1    => 1
    }
}
