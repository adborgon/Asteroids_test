using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundarySetter : MonoBehaviour
{
    public enum Position
    {
        top, left, right, bottom
    }

    public Position boundaryPosition;
    public float offset;

    public SpawnPointSetter pointSetter;

    private void Update()
    {
        //It will work even if you change the aspectRatio on Play, this should be in Start
        switch (boundaryPosition)
        {
            case Position.top:
                transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height + offset));
                break;

            case Position.left:
                transform.position = Camera.main.ScreenToWorldPoint(new Vector2(0 - offset, Screen.height / 2));
                break;

            case Position.right:
                transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width + offset, Screen.height / 2));

                break;

            case Position.bottom:
                transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0 - offset));
                break;

            default:
                break;
        }
        //Fix Camera -10 position
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        pointSetter.SetPosition(this);
    }

    /// <summary>
    /// Collision detected on the boundaries
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Player":
            case "Bullet":
            case "Enemy":
                col.transform.position = ReversePosition(boundaryPosition, col.transform);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Object has arrived to a boundary
    /// </summary>
    /// <returns></returns>
    public Vector2 ReversePosition(BoundarySetter.Position boundaryPosition, Transform objectToReverse)
    {
        Vector2 newPosition = Vector2.zero;
        switch (boundaryPosition)
        {
            case BoundarySetter.Position.top:
                newPosition = new Vector2(objectToReverse.position.x, objectToReverse.position.y * -1 + 0.1f);
                break;

            case BoundarySetter.Position.left:
                newPosition = new Vector2(objectToReverse.position.x * -1, objectToReverse.position.y - 0.1f);
                break;

            case BoundarySetter.Position.right:
                newPosition = new Vector2(objectToReverse.position.x * -1, objectToReverse.position.y + 0.1f);
                break;

            case BoundarySetter.Position.bottom:
                newPosition = new Vector2(objectToReverse.position.x, objectToReverse.position.y * -1 - 0.1f);
                break;

            default:
                break;
        }
        return newPosition;
    }
}