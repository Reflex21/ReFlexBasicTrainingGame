using System;
using UnityEngine;

public class Analytics
{
    private int NUM_DATA_POINTS;
    private int NUM_VARIATIONS;
    private double[,] data_points;
    private int[] data_pointer;
    private String[] titles;
    private bool calculate_avg;
    
    // Variations are the number of different data types we are collecting (i.e reaction time, distance from gaze, etc)
    // Data points is the max number of data points we collect for each of the different data types (i.e. collected reaction time 10 times)
    public Analytics(int datapoints, int variations, bool calculate_avg)
    {
        NUM_DATA_POINTS = datapoints;
        NUM_VARIATIONS = variations;
        data_points = new double[datapoints, variations];
        data_pointer = new int[datapoints];
        titles = new String[variations];
        
        this.calculate_avg = calculate_avg;
    }

    // Sets the name for given data type
    public void setName(int variation, String name)
    {
        titles[variation] = name;
    }

    // Adds a new datapoint 
    public void addDataPoint(int datapoint_num, double data_point)
    {
        int index = data_pointer[datapoint_num];
        if(index < NUM_VARIATIONS)
        {
            data_points[datapoint_num, index] = data_point;
            data_pointer[datapoint_num] += 1;
        } else
        {
            data_points[datapoint_num, 0] = data_point;
            data_pointer[datapoint_num] = 0;
        }
    }

    // Saves the data to a csv file
    public void saveData(String filename)
    {
        double sum = 0;
        double cnt = 0;
        string p = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'));
        string fn = p+@"\"+ filename;
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@fn))
        {
            for(int i = 0; i < NUM_VARIATIONS - 1; i++)
            {
                file.Write(titles[i] + ",");
            }
            file.Write(titles[NUM_VARIATIONS - 1] + "\r\n");
            for (int i = 0; i < NUM_DATA_POINTS; i++)
            {
                for (int j = 0; j < NUM_VARIATIONS; j++)
                {
                    file.Write(data_points[i, j]);
                    if(j != NUM_VARIATIONS - 1)
                    {
                        file.Write(",");
                    }
                    
                }
                file.Write("\r\n");
            }
        }
    }
}
