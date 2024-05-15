using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{

    public FoodTypeSO type;

    private Animator _animator;

    [SerializeField] private float plofTime;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetInteger("foodType", type.id);
        //StartCoroutine(Disapear());

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            TileMapManager.Instance.AddPoints(type.healthyPoints);

            RevealTrueSelf();
            Debug.Log("Chocando Trigger");
            StartCoroutine(Plof());

        }


    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    RevealTrueSelf();
    //    Debug.Log("Chocando Collider");
    //    Plof();
    //}
    IEnumerator Disapear()
    {
        yield return new WaitForSeconds(20);
        DestroyThis();
    }

    IEnumerator Plof()
    {
        yield return new WaitForSeconds(plofTime);
        DestroyThis();
    }


    void DestroyThis()
    {
        Destroy(gameObject);
    }

    void RevealTrueSelf ()
    {
        _animator.SetBool("isClosed", false);

    }



}