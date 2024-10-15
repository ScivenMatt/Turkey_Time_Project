using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileApple : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 velocity = new Vector2(0.0f,0.0f);
    public GameObject turkey;
    void Update() {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits) {
            Debug.Log(hit.collider.gameObject);
        }

        transform.position = newPosition;
        
    }
}
