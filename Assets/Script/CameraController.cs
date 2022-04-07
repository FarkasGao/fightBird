using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cameraMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraMove = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right*cameraMove*Time.deltaTime*50);
        if(cameraMove>0&&transform.position.x>20)
        {
            transform.position = new Vector3(20, 0, -10);
        }
        if(cameraMove<0&&transform.position.x<-5)
        {
            transform.position = new Vector3(-5, 0, -10);
        }
    }
}
