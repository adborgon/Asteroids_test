using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointSetter : MonoBehaviour
{
    private readonly int offset = 20;

    //It will work even if you change the aspectRatio on Play, this should be in Start
    public void SetPosition(BoundarySetter boundaryRelation)
    {
        switch (boundaryRelation.boundaryPosition)
        {
            case BoundarySetter.Position.top:
                transform.position = (boundaryRelation.transform.position + Vector3.up * offset);
                break;

            case BoundarySetter.Position.left:
                transform.position = boundaryRelation.transform.position + Vector3.left * offset;
                break;

            case BoundarySetter.Position.right:
                transform.position = boundaryRelation.transform.position + Vector3.right * offset;
                break;

            case BoundarySetter.Position.bottom:
                transform.position = boundaryRelation.transform.position + Vector3.down * offset;
                break;

            default:
                break;
        }
    }
}