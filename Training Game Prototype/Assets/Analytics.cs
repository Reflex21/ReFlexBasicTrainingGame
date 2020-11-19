using System;
using Utilities;

public class Analytics
{
    private int MAX_DATA_POINTS = 10;
    private double[,] data_points;
    private int[] data_pointer;
    private double[] speeds;
    private StopwatchWrapper[] sw_arr;
    
    // Variations is the number of different data types we are collecting
    // Data points is the max number of data points we collect for each
    public Analytics(int variations, int num_data)
    {
        MAX_DATA_POINTS = num_data;
        data_points = new double[variations, num_data];
        data_pointer = new int[variations];
        speeds = new double[variations];
        sw_arr = new StopwatchWrapper[variations];
    }

    void setSpeed(int variation, double speed)
    {
        speeds[variation] = speed;
    }

    void addDataPoint(int variation, double data_point)
    {
        int index = data_pointer[variation];
        if(index < MAX_DATA_POINTS)
        {
            data_points[variation, index] = data_point;
            data_pointer[variation] += 1;
        } else
        {
            data_points[variation, index] = data_point;
            data_pointer[variation] = 0;
        }
    }

    void printDataPoints(int variation)
    {
        for(int i = 0; i < variation; i++)
        {
            for (int j = 0; j < variation; j++)
            {
                Console.Write(data_points[i, j]);
                Console.Write(" ");
            }
            Console.Write("\r\n");
        }
    }
}
