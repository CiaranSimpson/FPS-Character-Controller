using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : DamageableEntity
{
    public enum Mode
    {
        walking,frozen
    }
    public Mode movementMode;
}
