namespace Code.Scripts.Models.Common
{
    public interface IHealthBehaviour
    {
        public float CurrentHealth { get; }

        public void ApplyHeal(float value);

        public void ApplyDamage(float value);
    }
}