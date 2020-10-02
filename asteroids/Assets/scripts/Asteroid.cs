using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public static int selfDestructPadding = 5;

    public float speed;
    public float size;

    public float minSize;

    public GameObject asteroidPrefab;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
        transform.localScale = new Vector3(size, size, 1);

    }

    private void FixedUpdate()
    {
        Debug.Log(GetComponent<Collider2D>().isTrigger);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit trigger");
        GetComponent<Collider2D>().isTrigger = false;
        Debug.Log("not trigger");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("bullet")) {
            ScoreKeeper.Instance.IncrementDestroyedAsteroids();
            Destroy(collision.gameObject);
            Split();
        }
    }

    void Split() {
        if (size / 2 > minSize)
        {
            float prevRot = 1000;
            for (int i = 0; i < 2; i++)
            {
                GameObject go = Instantiate(asteroidPrefab);
                go.GetComponent<Asteroid>().size = size / 2;
                go.GetComponent<Asteroid>().speed = speed;


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

                //to avoid split asteroids sticking with eachother
                go.GetComponent<Collider2D>().isTrigger = true;
            }
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
