using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float interval = 10;
    public float multiplier = 0.9f;

    public float timer;

    public float minPaddingFromCameraBounds, maxPaddingfromCameraBounds, minSpawnAngle = 10,maxSpawnAngle = 80,minSpeed, maxSpeed, minSize, maxSize;

    public GameObject asteroid;
    // Start is called before the first frame update
    void Start()
    {
        timer = interval;     
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        while (timer > interval)
        {
            timer -= interval;
            Spawn();
            interval *= multiplier;
            if (interval < 1)
                interval = 1;
            
        }
        Bounds cameraBounds = Utils.getCameraBounds();

    }

    void Spawn() {

        Bounds cameraBounds = Utils.getCameraBounds();

        GameObject go = Instantiate(asteroid);
        Asteroid asteroidComponent = go.GetComponent<Asteroid>();


        string upOrDown = Random.Range(0,2)==0?"up":"down";
        string leftOrRight = Random.Range(0,2)==0?"left":"right";


        float posX;
        if (leftOrRight == "left")
        {
            posX = Random.Range(cameraBounds.min.x - maxPaddingfromCameraBounds, cameraBounds.min.x - minPaddingFromCameraBounds);
        }
        else {
            posX = Random.Range(cameraBounds.max.x + maxPaddingfromCameraBounds, cameraBounds.max.x + minPaddingFromCameraBounds);
        }

        float posY;
        if (upOrDown == "down")
        {
            posY = Random.Range(cameraBounds.min.y - maxPaddingfromCameraBounds, cameraBounds.min.y - minPaddingFromCameraBounds);
        }
        else
        {
            posY = Random.Range(cameraBounds.max.y + maxPaddingfromCameraBounds, cameraBounds.max.y + minPaddingFromCameraBounds);
        }

        go.transform.position = new Vector3(posX, posY);

        float rot = Random.Range(minSpawnAngle, maxSpawnAngle);

        if (upOrDown == "up")
            rot += 90;


        if (leftOrRight == "left")
            rot *= -1;

        go.transform.rotation = Quaternion.Euler(0,0,rot);

        asteroidComponent.speed = Random.Range(minSpeed, maxSpeed);
        asteroidComponent.size = Random.Range(minSize, maxSize);

        int shapeNum = Random.Range(0, AsteroidShapeDatabase.Instance.normalSprites.Length);
        go.GetComponent<SpriteRenderer>().sprite = AsteroidShapeDatabase.Instance.normalSprites[shapeNum];
        go.AddComponent<PolygonCollider2D>();

        Debug.Log("spawned asteroid of size " + asteroidComponent.size + " and speed " + asteroidComponent.speed + " at position " + go.transform.position + " and rotation " + go.transform.rotation.eulerAngles);
        
    }
}
