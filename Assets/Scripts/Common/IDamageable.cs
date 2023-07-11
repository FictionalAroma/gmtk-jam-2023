using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    void TakeDamage(int damage);
    
}
