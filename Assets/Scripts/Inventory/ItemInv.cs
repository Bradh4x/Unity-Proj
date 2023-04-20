using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemInv", menuName = "Item Inv/Create New Item")]
public class ItemInv : ScriptableObject
{
    public int id;
    public string itemName;
    /// <summary>
    /// What displays for options when being used
    /// </summary>
    public List<string> actions;
    /// <summary>
    /// If item is usable tool for gathering.
    /// </summary>
    public bool tool;
    public bool equippable;
    /// <summary>
    /// Format:
    ///     Health
    ///     Magic
    ///     
    ///     Damage
    ///     Range
    ///     Defense
    ///     Luck
    /// </summary>
    public List<int> effects;
    public Sprite icon;
    public string iconName;
}
