namespace Code.Scripts.Character
{
    public interface ICharacterColliderBehaviour
    {
        float GetRadius { get; }
        float GetHeight { get; }
        
        void StateState(int state);
    }
}