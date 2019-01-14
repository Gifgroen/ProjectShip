using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "NewGame", menuName = "Game/Data", order = 1)]
    public class Data : ScriptableObject
    {
        private enum State
        {
            Alive,
            Died,
            Transcended
        };

        [SerializeField]
        private State state = State.Alive;

        public bool IsAlive
        {
            get { return state == State.Alive; }
        }

        public void SetTranscended()
        {
            state = State.Transcended;
        }

        public void Kill()
        {
            state = State.Died;
        }

        public void Init()
        {
            state = State.Alive;
        }
    }
}
