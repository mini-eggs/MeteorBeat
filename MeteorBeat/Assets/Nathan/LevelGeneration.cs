using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject ring;
    public GameObject asteroid;
    public float spawnDistance;
    public int meteorSpawnRate;
    public float ringSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MeteorSpawner");
        StartCoroutine("RingSpawner");
        StartCoroutine("LevelScript");
    }

    IEnumerator MeteorSpawner(){
        while(true){
            float sideDistance = 70;
            for(int i = 0; i < meteorSpawnRate; i++){
                GameObject currentAsteroid = Instantiate(asteroid, this.transform.position + new Vector3(Random.Range(-sideDistance, sideDistance), Random.Range(-sideDistance, sideDistance), spawnDistance * this.GetComponent<keyControls>().flyingSpeed), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
                StartCoroutine(MeteorScale(currentAsteroid));
                StartCoroutine(MeteorSpin(currentAsteroid));
                Destroy(currentAsteroid, 5);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MeteorScale(GameObject asteroid){
        float scale = Random.Range(0.5f, 3.0f);
        float startTime = Time.time;
        float scalePercent = 0f;
        while(scalePercent < 1f){
            scalePercent = (Time.time - startTime);
            asteroid.transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(scale,scale,scale),scalePercent);
            yield return null;
        }
        asteroid.transform.localScale = new Vector3(scale,scale,scale);
    }

    IEnumerator MeteorSpin(GameObject asteroid){
        float rotationSpeed = 2;
        float x = Random.Range(-rotationSpeed, rotationSpeed);
        float y = Random.Range(-rotationSpeed, rotationSpeed);
        float z = Random.Range(-rotationSpeed, rotationSpeed);
        while(asteroid){
            asteroid.transform.rotation *= Quaternion.Euler(new Vector3(x,y,z));
            yield return null;
        }
    }

    IEnumerator RingSpawner(){
        while(true){
            float sideDistance = 20;
            GameObject currentRing = Instantiate(ring, this.transform.position + new Vector3(Random.Range(-sideDistance, sideDistance), Random.Range(-sideDistance, sideDistance), spawnDistance * this.GetComponent<keyControls>().flyingSpeed), Quaternion.identity);
            Destroy(currentRing, 5);
            yield return new WaitForSeconds(ringSpawnTime);
        }
    }

    IEnumerator LevelScript(){
        //Tutorial
        this.GetComponent<keyControls>().flyingSpeed = 15;
        this.GetComponent<keyControls>().movementSpeed = 20;
        meteorSpawnRate = 1;
        ringSpawnTime = 2.5f;
        yield return new WaitForSeconds(20);
        //Increase rings in the middle of tutorial
        meteorSpawnRate = 3;
        ringSpawnTime = 1.7f;
        yield return new WaitForSeconds(21);
        //Music kicks off, level starts moving
        this.GetComponent<keyControls>().flyingSpeed = 25;
        this.GetComponent<keyControls>().movementSpeed = 25;
        meteorSpawnRate = 10;
        ringSpawnTime = 1f;
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
        ringSpawnTime = 0.5f;
        yield return new WaitForSeconds(11);
        //Very large drop
        this.GetComponent<keyControls>().flyingSpeed = 30;
        this.GetComponent<keyControls>().movementSpeed = 30;
        meteorSpawnRate = 50;
        ringSpawnTime = 1f;
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
        ringSpawnTime = 0.25f;
        yield return new WaitForSeconds(10);
        //Murder part of the level
        this.GetComponent<keyControls>().flyingSpeed = 40;
        this.GetComponent<keyControls>().movementSpeed = 30;
        meteorSpawnRate = 100;
        ringSpawnTime = 1f;
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
        ringSpawnTime = 60f;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
