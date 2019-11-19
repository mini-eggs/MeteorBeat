using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    //Singleton functionality
    private static LevelGeneration _instance;

    public static LevelGeneration Instance { 
        get { 
            return _instance; 
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public GameObject ring;
    public GameObject asteroid;
    public float spawnDistance;
    //Determines how far away things should spawn. Also tied to flying speed
    //so you can see faster things coming.
    public int meteorSpawnRate;
    //This how many meteors spawn every tenth of a second.
    public float ringSpawnDelay;
    //The delay in between each ring spawn.
    

    private bool isActive;
    // Used for end of level semantics. Stop removing items from scene when
    // game complete.

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        StartCoroutine("MeteorSpawner");
        //Begin spawning meteors in parallel.
        StartCoroutine("RingSpawner");
        //Begin spawning rings in parallel.
        StartCoroutine("LevelScript");
        //Begin adjusting values in time with the music.
    }

    IEnumerator FutureDestroy(GameObject item)
    {
        //Causes objects to clean up behind you if the game is still running
        yield return new WaitForSeconds(5f);
        if(isActive)
        {
            Destroy(item, 0); 
        }
    }

    IEnumerator MeteorSpawner(){
        while(isActive){
            float sideDistance = 70;
            //Determines how wide the spread of meteors should be.
            for(int i = 0; i < meteorSpawnRate; i++){
                //Every tenth of a second, this for loop runs, one for each meteor.
                
                //This function spawns an asteroid, starting at the player position.
                //The x and y are randomized by an offset of the sideDistance variable.
                //All of them will spawn the same distance ahead of the ship, which is
                //the spawnDistance times the flyingSpeed.
                //It is also given a random rotation across all three axes. (Quaternion)
                GameObject currentAsteroid = Instantiate(
                    asteroid, 
                    this.transform.position + new Vector3(
                        Random.Range(-sideDistance, sideDistance), 
                        Random.Range(-sideDistance, sideDistance), 
                        spawnDistance * this.GetComponent<keyControls>().flyingSpeed),
                    Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

                
                currentAsteroid.name = "Asteroid"; //  for OnCollisionEnter in other scripts.
                currentAsteroid.AddComponent<AsteroidIntegration>();
                StartCoroutine(MeteorScale(currentAsteroid)); //This coroutine will spawn it gently by scaling up
                StartCoroutine(MeteorSpin(currentAsteroid)); //This coroutine will give it a permanent spin
                StartCoroutine(FutureDestroy(currentAsteroid)); //This coroutine will get rid of it after five seconds
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    //Causes the meteor to spawn gently instead of suddenly by scaling it up from 0
    IEnumerator MeteorScale(GameObject asteroid){
        //Choose a random size.
        float scale = Random.Range(0.5f, 3.0f);
        //Set start time.
        float startTime = Time.time;
        //Start scaling it at 0%.
        float scalePercent = 0f;
        while(scalePercent < 1f){
            scalePercent = (Time.time - startTime); //Determine how big we should be based on time
            //Perform the scaling by using the linear interpolation function (Lerp)
            asteroid.transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(scale,scale,scale),scalePercent);
            yield return null; //Waits one frame before looping again.
        }
        //In case we missed a frame or something, scale it to its final size and stop coroutine.
        asteroid.transform.localScale = new Vector3(scale,scale,scale);
    }

    //Creates a permanent spin effect.
    IEnumerator MeteorSpin(GameObject asteroid){
        //Maximum spinning speed allowed
        float rotationSpeed = 2;
        float x = Random.Range(-rotationSpeed, rotationSpeed);
        float y = Random.Range(-rotationSpeed, rotationSpeed);
        float z = Random.Range(-rotationSpeed, rotationSpeed);
        while(asteroid){ //While the asteroid exists, apply the same rotation each frame.
            asteroid.transform.rotation *= Quaternion.Euler(new Vector3(x,y,z));
            yield return null;
        }
    }

    IEnumerator RingSpawner(){
        while(isActive){
            //Very similar to the meteor spawner. But no rotation or spinning.
            float sideDistance = 20;
            GameObject currentRing = Instantiate(
                ring, 
                this.transform.position + new Vector3(
                    Random.Range(-sideDistance, sideDistance), 
                    Random.Range(-sideDistance, sideDistance), 
                    spawnDistance * this.GetComponent<keyControls>().flyingSpeed), 
                Quaternion.identity);
            StartCoroutine(FutureDestroy(currentRing));
            yield return new WaitForSeconds(ringSpawnDelay);
        }
    }

    //Handles the events as they happen throughout the level. Reads kind of like a movie script.
    IEnumerator LevelScript(){
        //Tutorial
        this.GetComponent<keyControls>().flyingSpeed = 15;
        this.GetComponent<keyControls>().movementSpeed = 20;
        meteorSpawnRate = 1;
        ringSpawnDelay = 2.5f;
        yield return new WaitForSeconds(20);
        //Increase rings in the middle of tutorial
        meteorSpawnRate = 3;
        ringSpawnDelay = 1.7f;
        yield return new WaitForSeconds(21);
        //Music kicks off, level starts moving
        this.GetComponent<keyControls>().flyingSpeed = 25;
        this.GetComponent<keyControls>().movementSpeed = 25;
        meteorSpawnRate = 10;
        ringSpawnDelay = 1f;
        yield return new WaitForSeconds(33);
        //Music really becomes lively
        this.GetComponent<keyControls>().flyingSpeed = 30;
        this.GetComponent<keyControls>().movementSpeed = 30;
        meteorSpawnRate = 15;
        yield return new WaitForSeconds(11);
        //Slowdown and buildup
        this.GetComponent<keyControls>().flyingSpeed = 15;
        this.GetComponent<keyControls>().movementSpeed = 15;
        meteorSpawnRate = 0;
        ringSpawnDelay = 0.5f;
        yield return new WaitForSeconds(11);
        //Very large drop
        this.GetComponent<keyControls>().flyingSpeed = 30;
        this.GetComponent<keyControls>().movementSpeed = 30;
        meteorSpawnRate = 50;
        ringSpawnDelay = 1f;
        yield return new WaitForSeconds(22);
        //Slow down a tad, music returns
        meteorSpawnRate = 15;
        yield return new WaitForSeconds(44);
        //Die off completely
        this.GetComponent<keyControls>().flyingSpeed = 15;
        this.GetComponent<keyControls>().movementSpeed = 15;
        meteorSpawnRate = 10;
        yield return new WaitForSeconds(22);
        //Build up to murder part of the level
        meteorSpawnRate = 0;
        ringSpawnDelay = 0.25f;
        yield return new WaitForSeconds(10);
        //Murder part of the level
        this.GetComponent<keyControls>().flyingSpeed = 40;
        this.GetComponent<keyControls>().movementSpeed = 30;
        meteorSpawnRate = 100;
        ringSpawnDelay = 1f;
        yield return new WaitForSeconds(21);
        //Music returns, ease off a bit
        this.GetComponent<keyControls>().flyingSpeed = 35;
        this.GetComponent<keyControls>().movementSpeed = 30;
        meteorSpawnRate = 30;
        yield return new WaitForSeconds(45);
        //Die off again
        this.GetComponent<keyControls>().flyingSpeed = 15;
        this.GetComponent<keyControls>().movementSpeed = 15;
        meteorSpawnRate = 10;
        yield return new WaitForSeconds(47);
        //Song over. End of level.
        meteorSpawnRate = 0;
        ringSpawnDelay = 60f;
        yield return null;
    }

    /* 
     * StopCoroutines
     *
     * Used for end of level semantics.
     * Stop disappearing asteroid and showing more!
     */
    public void StopCoroutines() 
    {
        isActive = false;
        StartCoroutine("MeteorSpawner");
        StartCoroutine("RingSpawner");
        StartCoroutine("LevelScript");
    }
}
