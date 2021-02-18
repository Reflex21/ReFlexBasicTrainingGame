using System;
using UnityEngine;

public class Analytics
{
    private int MAX_DATA_POINTS;
    private int num;
    private double[,] data_points;
    private int[] data_pointer;
    private double[] speeds;
    
    // Variations is the number of different data types we are collecting
    // Data points is the max number of data points we collect for each
    public Analytics(int variations, int num_data)
    {
        MAX_DATA_POINTS = num_data;
        data_points = new double[variations, num_data];
        data_pointer = new int[variations];
        speeds = new double[variations];
        num = variations;
    }

    public void setSpeed(int variation, double speed)
    {
        speeds[variation] = speed;
    }

    public void addDataPoint(int variation, double data_point)
    {
        int index = data_pointer[variation];
        if(index < MAX_DATA_POINTS)
        {
            data_points[variation, index] = data_point;
            data_pointer[variation] += 1;
        } else
        {
            data_points[variation, 0] = data_point;
            data_pointer[variation] = 0;
        }
    }

    public void printDataPoints()
    {
        for(int i = 0; i < num; i++)
        {
            for (int j = 0; j < MAX_DATA_POINTS; j++)
            {
                Debug.Log(data_points[i, j]);
                Debug.Log(" ");
            }
            Debug.Log("\r\n");
        }
    }

    public void saveData(String filename)
    {
        double sum = 0;
        double cnt = 0;
        string p = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'));
        string fn = p+@"\"+ filename;
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@fn))
        {

            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < MAX_DATA_POINTS; j++)
                {
                    sum += data_points[i, j];
                    cnt++;
                    file.Write(data_points[i, j]);
                    if(j != MAX_DATA_POINTS - 1)
                    {
                        file.Write(",");
                    }
                    
                }
                file.Write("\r\n");
            }

            file.WriteLine("Avg. Reflex Time was: " + sum / cnt);
        }
    }
}
