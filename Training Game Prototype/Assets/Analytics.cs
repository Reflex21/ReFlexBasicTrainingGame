using System;

public class Analytics
{
    private int MAX_DATA_POINTS = 10;
    private double[,] data_points;
    private int[] data_pointer;
    
    // Variations is the number of different data types we are collecting
    // Data points is the max number of data points we collect for each
    public Analytics(int variations, int data_points)
    {
        MAX_DATA_POINTS = data_points;
        data_points = new double[variations, data_points];
        data_pointer new int[variations];
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

    double[] getDataPoints(int variation)
    {
        return data_points[variation];
    }



}
