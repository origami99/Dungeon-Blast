using UnityEngine;
using static Assets.Scripts.Utils.Globals;

namespace Assets.Scripts.Player
{
    public class PlayerMeleeAttack : MonoBehaviour
    {
        [SerializeField] private Animator _playerAnimator;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _playerAnimator.SetTrigger(AnimatorParameters.MeleeAttack);
            }
        }
    }
}
