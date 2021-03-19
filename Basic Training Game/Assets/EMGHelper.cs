using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMGHelper
{
    private SerialPort sp;
    private string comPort;
    private float next_time;
    private float interval;
    private bool turnOn;

    public EMGHelper(string port)
    {
        // interval = 5; //Run loop every 5 seconds.
        // next_time = Time.time;
        comPort = port;
        sp = new SerialPort("\\\\.\\" + comPort, 115200);
    }

    public void connect()
    {
        if (!sp.IsOpen)
        {
            sp.Open();
            sp.ReadTimeout = 100;
            sp.Handshake = Handshake.None;
            if (sp.IsOpen) { Debug.Log("Serial Port Opened."); }
        }
    }

    public void disconnect()
    {
        if (sp.IsOpen)
        {
            sp.Close();
            Debug.Log("Serial Port Closed.");
        }
    }

    public void setLED(bool state)
    {
        if (state)
        {
            sp.Write("1");
        }
        else
        {
            sp.Write("0");
        }
        Debug.Log("LED Toggled.");
    }
}
