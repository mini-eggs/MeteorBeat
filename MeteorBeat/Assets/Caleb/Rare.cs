using UnityEngine;

public class Rare : MonoBehaviour
{
    void Update()
    {
        int x = GetRandom.GetRand(1000000);
        if(x == 1)
        {
            Debug.Log("One in a million!");
            var super = SuperPowerup.Instance;
            super.AddSuperPowerup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Name:" +this.name);
        GameObject superP = GameObject.Find("BallofPower");
        Destroy(superP);
    }
}

public sealed class SuperPowerup
{
    public void AddSuperPowerup()
    {
        DrawSuperPowerup();
    }

    private void DrawSuperPowerup()
    {
        //Debug.Log("MADE");
        GameObject power = GameObject.Find("BallofPower");
        if (power)
        {
            Vector3 Location = GetRandom.NewCoordinates();
            Vector3 SpaceshipPosition = GameObject.Find("Spaceship").transform.position;
            power.transform.position += Location+SpaceshipPosition;
            power.tag = "Super";
        }

    }
    /* Singletong pattern for this one in a million special event
     * 
     * 
     */
    private SuperPowerup() { }
    public static SuperPowerup Instance
    {
        get
        {
            return Nested.instance;
        }
    }

    public GameObject Insantiate { get; private set; }

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested() { }
        internal static readonly SuperPowerup instance = new SuperPowerup();
    }
}
