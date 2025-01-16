using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField] Transform moveFloor;
    [SerializeField] Transform movePosition;
    [SerializeField] float moveSpeed = 2f;

    private Vector3 firstPosition;
    private bool isLightHit = false;

    private void Start()
    {
        if(moveFloor != null)
        {
            firstPosition = moveFloor.position;
        }
    }

    private void Update()
    {
        if (moveFloor == null) 
            return;

        if (isLightHit) 
            moveFloor.position = Vector3.MoveTowards(moveFloor.position, movePosition.position, moveSpeed * Time.deltaTime);
        else
            moveFloor.position = Vector3.MoveTowards(moveFloor.position, firstPosition, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
            isLightHit = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
            isLightHit = false;
    }
}
