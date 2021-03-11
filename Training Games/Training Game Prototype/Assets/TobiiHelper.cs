using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

namespace Utilities
{
    public class TobiiHelper
    {
        private Camera cam;
        public TobiiHelper()
        {
            cam = Camera.main;
        }

        // Checks to see if user gaze is within a certain range of a given 2D Vector
        // Input vector should be in World Point Coordinates
        public bool isWithinRange(Vector2 position, double range)
        {
            // Scale the given range (in Unity screen coordinates) into pixels
            //double pixelRange = Screen.dpi * range;
            double distance = distanceFromGaze(position);
            if (distance <= range)
                return true;
            return false;
        }

        // Checks to see if user gaze is within a certain range of a given (x,y)
        // Input should be in World Point Coordinates
        public bool isWithinRange(double x, double y, double range)
        {
            // Scale the given range (in Unity screen coordinates) into pixels
            //double pixelRange = Screen.dpi * range;
            double distance = distanceFromGaze(x, y);
            if (distance <= range)
                return true;
            return false;
        }

        // Returns distance between user gaze and given 2D vector
        // Input vector should be in World Point Coordinates
        public double distanceFromGaze(Vector2 position)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            if (gazePoint.IsRecent())
            {

                // Convert Tobii Coordinates to Viewport
                Vector3 original = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, 0);
                Vector3 tobii_world_coordinates = cam.ScreenToWorldPoint(original);
                Vector3 tobii_viewport_coordinates = cam.WorldToViewportPoint(tobii_world_coordinates);

                // Convert given World Point Coordinates to Viewport
                Vector3 given_coordinates = new Vector3(position.x, position.y, 0);
                Vector3 give_viewport_coordinates = cam.WorldToViewportPoint(given_coordinates);

                double x_diff = give_viewport_coordinates.x - tobii_viewport_coordinates.x;
                double y_diff = give_viewport_coordinates.y - tobii_viewport_coordinates.y;
                double distance = Math.Sqrt(
                    Math.Pow(x_diff, 2f) +
                    Math.Pow(y_diff, 2f));

                return distance;

            }
            return -1;
        }

        // Returns distance between user gaze and given (x,y)
        // Input should be in World Point Coordinates
        public double distanceFromGaze(double x, double y)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            if (gazePoint.IsRecent())
            {

                // Convert Tobii Coordinates to Viewport
                Vector3 original = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, 0);
                Vector3 tobii_world_coordinates = cam.ScreenToWorldPoint(original);
                Vector3 tobii_viewport_coordinates = cam.WorldToViewportPoint(tobii_world_coordinates);

                // Convert given World Point Coordinates to Viewport
                Vector3 given_coordinates = new Vector3((float)x, (float)y, 0);
                Vector3 give_viewport_coordinates = cam.WorldToViewportPoint(given_coordinates);

                double x_diff = give_viewport_coordinates.x - tobii_viewport_coordinates.x;
                double y_diff = give_viewport_coordinates.y - tobii_viewport_coordinates.y;
                double distance = Math.Sqrt(
                    Math.Pow(x_diff, 2f) +
                    Math.Pow(y_diff, 2f));

                return distance;

            }
            return -1;
        }
    }
}