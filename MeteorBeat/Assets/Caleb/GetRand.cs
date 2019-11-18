using UnityEngine;
using System;

/* Caleb Seely
 * Random number generation for new coordinates
 * 10/15/19
 */

public class GetRandom : MonoBehaviour
{
    private Vector3 position;

    /* Provides a random vector of 3 variables 
     * between a given range. 
     * (x,y,z) coordinates.
     */
    public static Vector3 NewCoordinates()
    {
        int max = 90;
        int x = 0, y = 0, z = 0;
        x = GetRand(max);
        y = GetRand(max);
        max = 100;
        z = Math.Abs(GetRand(max));  //Needed to not move backwards
        Vector3 coordinates = new Vector3(x, y, z);
        return coordinates;
    }

    private static System.Random rnd = new System.Random();

    /* Returns one random number within max range.
     * Taken from StackOverflow and protected under the CC-BY-SA
     * Ok to leave in if this was to be used commercially. 
     */
    public static int GetRand(int max)
    {
        if (rnd.Next() % 2 == 0)
        {
            return -rnd.Next() % max;
        }
        else
            return rnd.Next() % max;
    }
}