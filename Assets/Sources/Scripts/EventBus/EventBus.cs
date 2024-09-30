using UnityEngine.Events;

public class EventBus
{
    public UnityEvent<AnimalClick> ClickAnimal { get; } = new UnityEvent<AnimalClick>();

    public void TriggerClickAnimal(AnimalClick animalClick) => ClickAnimal?.Invoke(animalClick);
}