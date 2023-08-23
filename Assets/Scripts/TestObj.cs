using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObj : BaseUnit
{
    private readonly Dictionary<KeyCode, Vector2> _keycodeMap = new Dictionary<KeyCode, Vector2>
    {
        {
            KeyCode.W, Vector2.up
        },
        {
            KeyCode.A, Vector2.left
        },
        {
            KeyCode.S, Vector2.down
        },
        {
            KeyCode.D, Vector2.right
        }
    };

    private float _speed = 0.5f;
    private Vector3Int _dist = new Vector3Int(12, 0, 0);
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // var moveDirection = Vector2.zero;
        // foreach (var (key, direction) in _keycodeMap)
        // {
        //     if (Input.GetKey(key))
        //     {
        //         moveDirection += direction;
        //     }
        // }

        // if (moveDirection.magnitude != 0f)
        // {
            // Vector2 displacementRaw = Time.deltaTime * _speed / moveDirection.magnitude * moveDirection;
            // Vector2 displacement = NavManager.Instance.GetMoveDisplacement(this, displacementRaw, _dist);
            // transform.Translate(displacement);
        // }
        Vector2 displacementRaw = Time.deltaTime * _speed * new Vector2(2, 1);
        Vector2 displacement = NavManager.Instance.GetMoveDisplacement(this, displacementRaw, _dist);
        transform.Translate(displacement);
    }
}
