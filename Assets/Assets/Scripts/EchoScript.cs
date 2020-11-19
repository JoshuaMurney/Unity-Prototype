using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoScript : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer spriteRenderer;
    public Sprite smallEcho;
    public Sprite largeEcho;
    private bool isEchoLarge;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += transform.forward;
        offset = transform.position - player.transform.position;
        isEchoLarge = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isEchoLarge)
            {
                spriteRenderer.sprite = smallEcho;
                isEchoLarge = false;
            }
            else
            {
                spriteRenderer.sprite = largeEcho;
                isEchoLarge = true;
            }
        }
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
