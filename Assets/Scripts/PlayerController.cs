using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;

    public GameObject appleprefab;

    private bool isMoving;

    public Vector2 input;

    private Animator animator;

    public LayerMask solidObjectsLayer;

    public LayerMask interactableLayer;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()  
    {
        if (Input.GetKeyDown("mouse 0")) {
            Debug.Log("Fire");
            GameObject apple = Instantiate(appleprefab, transform.position, Quaternion.identity);
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; // zero z
            Vector2 shootingDirection = new Vector2(mouseWorldPos.x - transform.position.x, mouseWorldPos.y - transform.position.y);
            shootingDirection.Normalize();
            apple.GetComponent<Rigidbody2D>().velocity = 5.0f*shootingDirection;
            Destroy(apple, 3.0f);
        }

        if(!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //Debug.Log("This is input.x" + input.x);
            //Debug.Log("This is input.y" + input.y);

            //if (input.x != 0) input.y = 0; //denies diagonal movement

            if (input != Vector2.zero)
            {
                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }

            }

        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
        var interactPos = transform.position + facingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.red, 1f) //for debugging, keep commented otherwise! check which direction the character is facing

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);

        if (collider != null)
        {
            Debug.Log("There is an NPC here!");

            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos) 
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) != null )
        {
            return false;
        }
        return true;
    }

}
