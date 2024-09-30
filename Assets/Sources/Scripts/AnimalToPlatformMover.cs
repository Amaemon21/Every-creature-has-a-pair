using DG.Tweening;
using UnityEngine;
using Zenject;

public class AnimalToPlatformMover : MonoBehaviour
{   
    [Inject] private readonly EventBus _eventBus;

    [SerializeField] private Transform _arenaPoint;
    
    [SerializeField] private Transform _leftPlatform;
    [SerializeField] private Transform _rightPlatform;

    [Space(10)] 
    [SerializeField] private float _durationMove = 0.4f;
    [SerializeField] private float _durationRotate = 0.4f;
    [SerializeField] private float _durationScale = 0.4f;

    [SerializeField] private float _targetScale;
    
    private AnimalClick _animalLeftPlatform;
    private AnimalClick _animalRightPlatform;
    
    private const float YPosition = 0.3f;

    private void OnEnable()
    {
        _eventBus.ClickAnimal.AddListener(OnAnimalClick);
    }

    private void OnDisable()
    {
        _eventBus.ClickAnimal.RemoveListener(OnAnimalClick);
    }

    private void OnAnimalClick(AnimalClick animal)
    {
        if (_animalLeftPlatform == null)
        {
            AssignAnimalToPlatform(animal, ref _animalLeftPlatform, _leftPlatform);
        }
        else if (_animalRightPlatform == null)
        {
            AssignAnimalToPlatform(animal, ref _animalRightPlatform, _rightPlatform);
        }
        else
        {
            MoveAnimalToArena(animal);
        }
    }

    private void MoveAnimalToArena(AnimalClick animal)
    {
        if (animal == _animalLeftPlatform)
        {
            ResetAnimalPosition(animal, _arenaPoint);
            _animalLeftPlatform = null;
        }
        else if (animal == _animalRightPlatform)
        {
            ResetAnimalPosition(animal, _arenaPoint);
            _animalRightPlatform = null;
        }
    }

    private void AssignAnimalToPlatform(AnimalClick animal, ref AnimalClick platformAnimal, Transform platform)
    {
        if (platformAnimal != null)
            return;
        
        if (animal == _animalLeftPlatform || animal == _animalRightPlatform)
        {
            MoveAnimalToArena(animal);
            return;
        }
        
        animal.transform.DOMove(platform.position + new Vector3(0, YPosition, 0), _durationMove).OnComplete(CheckForComplaint);
        
        float targetRotationY = platform == _leftPlatform ? 0 : 180;
        animal.transform.DORotate(new Vector3(0, targetRotationY, 0), _durationRotate).OnComplete(() =>{animal.Rigidbody.isKinematic = true;});

        platformAnimal = animal;
    }

    private void ResetAnimalPosition(AnimalClick animal, Transform newParent)
    {
        animal.transform.DOMove(newParent.position + new Vector3(0, 5, 0), _durationMove).OnComplete(() =>
        {
            animal.transform.DORotate(new Vector3(0, 90, 0), _durationRotate).OnComplete(() =>
            {
                animal.Rigidbody.isKinematic = false;
            });
        });
    }

    private void CheckForComplaint()
    {
        if (_animalLeftPlatform == null || _animalRightPlatform == null)
            return;
        
        if (_animalLeftPlatform.AnimalType == _animalRightPlatform.AnimalType)
        {
            _animalLeftPlatform.transform.DOScale(new Vector3(_targetScale, _targetScale, _targetScale), _durationScale);
            _animalRightPlatform.transform.DOScale(new Vector3(_targetScale, _targetScale, _targetScale), _durationScale).OnComplete(() =>
                {
                    Destroy(_animalLeftPlatform.gameObject);
                    Destroy(_animalRightPlatform.gameObject);

                    _animalLeftPlatform = null;
                    _animalRightPlatform = null;

                    Debug.Log("Match found and animals destroyed!");
                });
        }
    }
}
