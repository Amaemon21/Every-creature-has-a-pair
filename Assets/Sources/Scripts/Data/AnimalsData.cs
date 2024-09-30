using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalsData", menuName = "Data/AnimalsData")]
public class AnimalsData : ScriptableObject
{
    [SerializeField] private AnimalClick[] _animalClicks;

    public AnimalClick GetRandomAnimal()
    {
        int rand = Random.Range(0, _animalClicks.Length);
        return _animalClicks[rand];
    }
}