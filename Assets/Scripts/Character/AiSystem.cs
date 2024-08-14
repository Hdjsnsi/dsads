using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AiSystem 
{
    public enum LineUpDirection 
    {
        Forward,
        Back,
        Right,
        Left
    }
    public static Vector3 WhereToMove(Vector3 shelterPos,LineUpDirection direction)
    {
        Vector3 goToThatLocation = new Vector3();
        switch(direction)
        {
            case LineUpDirection.Forward:
                goToThatLocation = new Vector3(shelterPos.x - 7, shelterPos.y, shelterPos.z + 5f);
                break;
            case LineUpDirection.Back:
                goToThatLocation = new Vector3(shelterPos.x - 7, shelterPos.y, shelterPos.z - 5f);
                break;
            case LineUpDirection.Right:
                goToThatLocation = new Vector3(shelterPos.x + 5f, shelterPos.y, shelterPos.z - 7);
                break;
            case LineUpDirection.Left:
                goToThatLocation = new Vector3(shelterPos.x - 5f, shelterPos.y, shelterPos.z - 7);
                break;
        }
        return goToThatLocation;
    }
    
}
