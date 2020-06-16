using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private bool _isInputEnabled = true;

        private Player _player;
        private CharacterController _characterController;
        private Camera _camera;

        private void Awake()
        {
            _player = this.GetComponent<Player>();
            _characterController = this.GetComponent<CharacterController>();
            _camera = Camera.main;
        }

        private void Start()
        {
            Player.OnDeath += Respawn;
        }

        private void Update()
        {
            if (_isInputEnabled)
            {
                Move();
                Turn();
            }
        }

        private void Move()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");

            Vector3 moveDirecion = new Vector3(inputX, 0, inputZ);
            Vector3 moveMotion = moveDirecion * _speed * Time.deltaTime;

            _characterController.Move(moveMotion);
        }

        private void Turn()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Plane ground = new Plane(Vector3.up, Vector3.zero);

            if (ground.Raycast(ray, out float distance))
            {
                Vector3 groundPoint = ray.GetPoint(distance);
                Vector3 lookPoint = new Vector3(groundPoint.x, this.transform.position.y, groundPoint.z);

                Debug.DrawLine(ray.origin, groundPoint, Color.red);

                this.transform.LookAt(lookPoint);
            }
        }

        public void Respawn()
        {
            _characterController.enabled = false;
            _characterController.transform.position = _player.PlayerData.RespawnPosition;
            _characterController.enabled = true;
        }
    }
}