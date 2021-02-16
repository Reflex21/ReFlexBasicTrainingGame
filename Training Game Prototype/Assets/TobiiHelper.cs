using System.Collections;
using System.Collections.Generic;
using Unity.Engine;
using Tobii.Gaming;

namespace Utilities
{
	public class TobiiHelper : MonoBehaviour
	{
		private GazePoint gazePoint;
		public TobiiHelper()
		{
		}

		// Checks to see if user gaze is within a certain range of a given 2D Vector
		public bool isWithinRange(Vector2 position)
		{
			float distance = distanceFromGaze(position);
			if (distance <= range)
				return true;
			return false;
		}

		// Checks to see if user gaze is within a certain range of a given (x,y)
		public bool isWithinRange(float x, float y, float range)
		{
			float distance = distanceFromGaze(x, y);
			if (distance <= range)
				return true;
			return false;
		}

		// Returns distance between user gaze and given 2D vector
		public float distanceFromGaze(Vector2 position)
		{
			gazePoint = TobiiAPI.GetGazePoint();
			if (gazepoint.IsRecent())
			{
				// If coordinates aren't postive user is looking off screen
				if ((gazePoint.Screen.x > 0) || gazePoint.Screen.y > 0)){
					float x_diff = position.x - gazePoint.Screen.x;
					float y_diff = position.y - gazePoint.Screen.y;
					float distance = Math.sqrt(
						Math.Pow(x_diff, 2f) +
						Math.Pow(y_diff, 2f));

					return distance;
				}
			}
			return -1;
		}

		// Returns distance between user gaze and given (x,y)
		public float distanceFromGaze(float x, float y)
		{
			gazePoint = TobiiAPI.GetGazePoint();
			if (gazepoint.IsRecent())
			{
				// If coordinates aren't postive user is looking off screen
				if ((gazePoint.Screen.x > 0) || gazePoint.Screen.y > 0)){
					float x_diff = x - gazePoint.Screen.x;
					float y_diff = y - gazePoint.Screen.y;
					float distance = Math.sqrt(
						Math.Pow(x_diff, 2f) +
						Math.Pow(y_diff, 2f));

					return distance;
				}
			}
			return -1;
		}
	}
}