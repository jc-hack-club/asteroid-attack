using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHit : MonoBehaviour
{

    public GameObject youDiedCanvas;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("asteroid"))
        {
            ScoreKeeper.Instance.enabled = false;
            youDiedCanvas.SetActive(true);
            Debug.LogError("rip");
            GetComponent<Animator>().SetTrigger("explode");

        }
    }
    
}
