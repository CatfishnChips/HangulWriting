using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Shop Item", fileName = "Default Shop Item")]
public class ShopItem: ScriptableObject
{
   [SerializeField] private Sprite _sprite;
   [SerializeField] private int _cost;
   [SerializeField] private bool _owned = false;

   public Sprite Sprite {get{return _sprite;}}
   public int Cost {get{return _cost;}}
   public bool Owned {get{return _owned;} set{_owned = value;}}
}
