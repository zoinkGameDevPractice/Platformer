using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private bool isMovingRight = true;
    public Transform groundDetection;
    public float distance = 2f;

    CharacterCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D ray = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if(ray.collider == false)
        {
            if (isMovingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isMovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isMovingRight = true;
            }
        }
    }
}
