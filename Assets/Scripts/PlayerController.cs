using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5f; // get from somewhere else (i.e. stats)?

    public float arriveDistance = 0.25f;

    Vector3 targetPosition;

    Quaternion targetRotation;

    bool isMoving;
    
    // This is temporary movement logic until we get simple game loop implemented.
    // For early development will let characters run around free reign interacting with world.
    // Afterwards will constrain with the turn-based system.
	void Update () {
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButton(1))
        {
            Plane plane = new Plane(Vector3.up, transform.position); // Revisit 'inPoint' param
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit = new RaycastHit();
            //if (Physics.Raycast(ray, out hit))
            float dist = 0f;
            if(plane.Raycast(ray, out dist)) // with this opposed to RaycastHit can NOT read tags or anything...
            {
                //Debug.Log(hit.collider.gameObject.name);
                //targetPosition = hit.point;
                targetPosition = ray.GetPoint(dist);
                targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
                transform.rotation = targetRotation; // TODO Make this rotation smooth
                isMoving = true;
            }
        }

        if (isMoving)
        {
            if(Vector3.Distance(transform.position, targetPosition) > arriveDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                isMoving = false;
            }
        }
	}
}
