using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{

    public static int selfDestructPadding = 5;

    public float speed;
    public float size;

    public float minSize;

    public GameObject asteroidPrefab;

    public bool readyToCollideWithSibling = true;
    public Asteroid sibling;
    public Collider2D siblingCollider;


    // Start is called before the first frame update
    void Start()
    {
        siblingCollider = gameObject.AddComponent<PolygonCollider2D>();
        siblingCollider.isTrigger = true;
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
        transform.localScale = new Vector3(size, size, 1);

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("bullet")) {
            ScoreKeeper.Instance.IncrementDestroyedAsteroids();
            Destroy(collision.gameObject);
            Split();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (readyToCollideWithSibling)
        {
            return;
        }
        
        if (sibling == null || other.GetComponent<Asteroid>() == sibling)
        {
            readyToCollideWithSibling = true;
            Destroy(siblingCollider);
            if (sibling != null)
            {
                Destroy(sibling.siblingCollider);
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), sibling.GetComponent<Collider2D>(), false);
            }
        }
    }

    void Split() {
        if (size / 2 > minSize)
        {
            float prevRot = 1000;
            GameObject[] splitAsteroids = new GameObject[2];
            for (int i = 0; i < 2; i++)
            {
                GameObject go = Instantiate(asteroidPrefab);
                go.GetComponent<Asteroid>().size = size / 2;
                go.GetComponent<Asteroid>().speed = speed;
                splitAsteroids[i] = go;

                //force the rotation of the 2nd object to be at least 10 degrees different to the 1st
                float rot;
                do
                {
                    rot = Random.Range(0f, 360f);
                } while (Mathf.Abs(rot - prevRot) < 10);
                go.transform.rotation = Quaternion.Euler(0, 0, rot);
                prevRot = rot;


                int shapeNum = Random.Range(0, AsteroidShapeDatabase.Instance.normalSprites.Length);
                go.GetComponent<SpriteRenderer>().sprite = AsteroidShapeDatabase.Instance.normalSprites[shapeNum];
            }
            Physics2D.IgnoreCollision(splitAsteroids[0].GetComponent<Collider2D>(),splitAsteroids[1].GetComponent<Collider2D>());
            //the first asteroid has the duty to re-enable collision with its sibling
            splitAsteroids[0].GetComponent<Asteroid>().readyToCollideWithSibling = false;
            splitAsteroids[0].GetComponent<Asteroid>().sibling = splitAsteroids[1].GetComponent<Asteroid>();
            splitAsteroids[1].GetComponent<Asteroid>().sibling = splitAsteroids[0].GetComponent<Asteroid>();
            Destroy(gameObject);
        }
        else
        {
            Disintegrate();
        }
    }

    public void Disintegrate() {
        Destroy(gameObject);
    }
}
