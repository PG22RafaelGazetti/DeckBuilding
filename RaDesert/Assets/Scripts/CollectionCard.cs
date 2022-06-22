using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CollectionCard : ScriptableObject
{
    public string cardName;

    public string cardDescription;

    public int cardAttack;

    public int cardHealth;

    public int cardCost;

    public CardType cardType;

    public CardArchetype primaryCardArchetype;

    public Color imageColor;

    //public CardArchetype secondaryCardArchetype;


    public enum CardType
    {
        Dragon,
        Plant,
        Beast,
        Elemental,
        Spirit,
        Mech,
        Demon,
    }

    public enum CardArchetype
    {
        Aggro,
        MidRange,
        Controll,
        All,
    }
}
