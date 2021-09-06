using UnityEngine;

namespace Interfaces {
    
    public class Carryable : MonoBehaviour {
        
        [HideInInspector] public new Rigidbody2D rigidbody;
        [HideInInspector] public new Collider2D collider;
        [HideInInspector] public new Transform transform;
    
        private bool isCarried;
        
        public void Start() {
            isCarried = false; 
            rigidbody = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
            transform = GetComponent<Transform>();
        }

        public void Update() {
        }

        public void Pickup() {
            isCarried = true;
            collider.enabled = false;
            rigidbody.Sleep();
        }

        public void Release() {
            isCarried = false;
            collider.enabled = true;
            rigidbody.WakeUp();
        }
    }

    public interface IThrowable {
        void Throw();
    }

    public interface IBreakable {
        void Break();
    }

    public interface IStrikable {
        bool Strike(float damage, ElementType element);
    }

    public interface ISlashable {
        bool Slash(float damage, float throwForce, float weakenAmount, float weakenTime, ElementType element);
    }

    public interface IExplodable {
        //returns if the item dies from the explosion or not
        bool Explode(float damage, ElementType element);
    }

    public interface IProgressBar {
        void SetCurrentValue(float newVal);

        float GetCurrentValue();
    }

    public interface IAnimatedUI {
        void Show();

        void Hide();
    }

    public interface IInteractable {
        public void Interact();
    }
    

}