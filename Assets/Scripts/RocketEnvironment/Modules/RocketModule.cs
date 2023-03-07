using UnityEngine;

namespace RocketEnvironment.Modules
{
    public abstract class RocketModule
    {
        public bool IsModuleActive { get; set; }

        protected Rocket Owner { get; private set; }
        protected Rigidbody Rigidbody { get; private set; }

        public RocketModule(Rocket owner, bool isActive = true)
        {
            IsModuleActive = isActive;
            Owner = owner;
            Rigidbody = Owner.GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            if (IsModuleActive) OnFixedUpdate();
        }

        protected virtual void OnFixedUpdate() { }
    }
}