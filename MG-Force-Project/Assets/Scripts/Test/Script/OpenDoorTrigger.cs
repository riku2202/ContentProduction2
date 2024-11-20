using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] Vector3 openPosition = new Vector3(0, 5, 0);
    [SerializeField] float openSpeed = 2.0f;

    private bool isDoorOpen = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube2" && !isDoorOpen)
        {
            isDoorOpen = true;
            StartCoroutine(OpenDoor());
        }

    }
    private System.Collections.IEnumerator OpenDoor()
    {
        Vector3 initalPosition = door.transform.position;
        Vector3 targetPosition = initalPosition + openPosition;

        float elapsedTime = 0;
        while (elapsedTime < 1.0f)
        {
            door.transform.position = Vector3.Lerp(initalPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }
        door.transform.position = targetPosition;
    }
}
