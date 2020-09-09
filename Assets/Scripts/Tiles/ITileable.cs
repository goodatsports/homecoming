using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileable
{
    Vector3 Pos { get; set; }
    Vector3Int Address { get; set; }


    void Interact();
}
