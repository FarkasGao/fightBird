using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMode : MonoBehaviour
{
    private bool iscollide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish")&&!iscollide)
        {
            GameObject gameMg = GameObject.FindGameObjectsWithTag("GameController")[0];
            gameMg.GetComponent<GameManager>().GetBird1P();//playerType Ϊ1
            gameObject.GetComponent<BirdMode>().enabled = false;
            iscollide = true;
        }
    }
}
