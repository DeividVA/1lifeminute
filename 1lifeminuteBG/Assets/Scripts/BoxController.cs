using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{

    public FoodTypeSO type;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disapear());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //RevealTrueSelf();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //RevealTrueSelf();
    }
    IEnumerator Disapear()
    {
        yield return new WaitForSeconds(20);
        DestroyThis();
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}