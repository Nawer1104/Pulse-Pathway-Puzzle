using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Sun sun;

    public bool canMove = false;

    private Vector3 startPos;

    public int moveIndex = 0;

    public Vector3[] positions;

    public float speed = 5f;

    public GameObject vfxSuccess;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "box")
        {
            sun.canDraw = false;
            StartCoroutine(Success(collision.gameObject));
        }
    }
    IEnumerator Success(GameObject box)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        box.SetActive(false);
        GameObject explosion = Instantiate(vfxSuccess, transform.position, transform.rotation);
        Destroy(explosion, .75f);

        yield return new WaitForSeconds(1f);

        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].boxes.Remove(box);

    }

    private void Update()
    {
        if (!canMove)
        {
            transform.RotateAround(sun.transform.position, new Vector3(0f, 0f, 1f), 90f * Time.deltaTime);
        }
        else
        {
            Vector2 currentPos = positions[moveIndex];
            transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);

            float distance = Vector2.Distance(currentPos, transform.position);
            if (distance <= 0.05f)
            {
                moveIndex++;
            }

            if (moveIndex > positions.Length - 1)
            {
                ResetPos();
            }
        }

    }

    private void ResetPos()
    {
        canMove = false;

        transform.position = startPos;

        moveIndex = 0;
    }
}
