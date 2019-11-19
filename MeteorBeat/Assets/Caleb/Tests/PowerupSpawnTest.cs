using System.Collections;
using NUnit.Framework;
using UnityEngine;
/* Caleb Seely
 * Metero Beat Tests
 * 11/2/19
 */
namespace Tests
{
    public class PowerupSpawnTest 
    {
        [Test]
        public void PowerupSpawned()
        {
            Vector3 PowerupPosition = GameObject.Find("Powerup").transform.position;
            Assert.IsNotNull(PowerupPosition);
            //Debug.Log("Powerup did not spawn");
        }
        [Test]
        public void SpaceShipSpawned()
        {
            Vector3 SpaceShip = GameObject.Find("Spaceship").transform.position;
            Assert.IsNotNull(SpaceShip);
            //Debug.Log("Powerup did not spawn");
        }

        // Expected to fail
        [Test]
        public void PowerupSpawnBehindPlayerTest()
        {
            Vector3 PowerupPosition = GameObject.Find("Powerup").transform.position;
            Vector3 SpaceshipPosition = GameObject.Find("Spaceship").transform.position;
            Assert.IsTrue(PowerupPosition.z < SpaceshipPosition.z);
            //Debug.Log("Powerup not reachable by the player.");
        }

        /* Boundry Test
         * Checks if Powerup object was spawned and placed
         * in a reachable location for the player.
         */
        [Test]
        public IEnumerator PowerupSpawnAheadofPlayerFAILTest()
        {
            Vector3 PowerupPosition = GameObject.Find("Powerup").transform.position;
            Vector3 SpaceshipPosition = GameObject.Find("Spaceship").transform.position;
            Debug.Log("Powerup not reachable by the player. \n This should always fail!");
            Assert.IsTrue(PowerupPosition.z > SpaceshipPosition.z);
            return null;
        }
    }
}
