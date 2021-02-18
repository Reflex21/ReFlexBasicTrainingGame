using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

namespace Utilities
{
    public class TobiiHelper
    {
        public TobiiHelper()
        {
        }

        // Checks to see if user gaze is within a certain range of a given 2D Vector
        public bool isWithinRange(Vector2 position, double range)
        {
            // Scale the given range (in Unity screen coordinates) into pixels
            double pixelRange = Screen.dpi * range;
            double distance = distanceFromGaze(position);
            if (distance <= pixelRange)
                return true;
            return false;
        }

        // Checks to see if user gaze is within a certain range of a given (x,y)
        public bool isWithinRange(double x, double y, double range)
        {
            // Scale the given range (in Unity screen coordinates) into pixels
            double pixelRange = Screen.dpi * range;
            double distance = distanceFromGaze(x, y);
            if (distance <= pixelRange)
                return true;
            return false;
        }

        // Returns distance between user gaze and given 2D vector
        public double distanceFromGaze(Vector2 position)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            if (gazePoint.IsRecent())
            {

                double x_diff = position.x - gazePoint.Screen.x;
                double y_diff = position.y - gazePoint.Screen.y;
                double distance = Math.Sqrt(
                    Math.Pow(x_diff, 2f) +
                    Math.Pow(y_diff, 2f));

                return distance;

            }
            return -1;
        }

        // Returns distance between user gaze and given (x,y)
        public double distanceFromGaze(double x, double y)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            if (gazePoint.IsRecent())
            {

                double x_diff = x - gazePoint.Screen.x;
                double y_diff = y - gazePoint.Screen.y;
                double distance = Math.Sqrt(
                    Math.Pow(x_diff, 2f) +
                    Math.Pow(y_diff, 2f));

                return distance;

            }
            return -1;
        }
    }
}