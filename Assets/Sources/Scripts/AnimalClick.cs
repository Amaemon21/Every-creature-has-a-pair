using UnityEngine;
using Zenject;

public class AnimalClick : MonoBehaviour
{
    [Inject] private readonly EventBus _eventBus;

    [SerializeField] private AnimalType _animalType;
    
    private Rigidbody _rigidbody;

    public AnimalType AnimalType => _animalType;
    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        _eventBus.TriggerClickAnimal(this);
    }
}