using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // this namespace makes the magic, tho
 
[RequireComponent(typeof(Collider))] //A collider is needed to receive clicks
public class Syringe : MonoBehaviour
{
    [SerializeField] Visc visc;
    [SerializeField] UnityEvent anEvent; // puts an easy to setup event in the inspector and anEvent references it so you can .Invoke() it
    private void OnMouseDown()
    {
        print("You clicked the cube!");
        anEvent.Invoke(); // Triggers the events you have setup in the inspector
    }
    public void EventClick() // methods have to be public void to be used by UnityEvents, they can't really return anything either, as far as I know... At least I don't know how an event will capture the return...
    {
        visc.StartMeasuring();
    }
}
