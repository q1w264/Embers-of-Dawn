// using Scripts;
// using UnityEngine;
//
// namespace Controllers
// {
//     public class PlayerController : MonoBehaviour
//     {
//             [SerializeField] private float moveSpeed = 5f; // 玩家移动速度
//             
//             private DefaultInputSystem _inputSystem; // 输入系统实例
//             private DefaultInputSystem.PlayerActions _playerActions; // 玩家输入动作映射
//             
//             private Rigidbody2D _rb; // 玩家 Rigidbody2D 组件
//             private Vector2 _movement; // 玩家输入的移动方向
//     
//             private void Awake()
//             {
//                 _rb = GetComponent<Rigidbody2D>(); // 获取 Rigidbody2D 组件
//                 _inputSystem = new DefaultInputSystem(); // 创建输入系统实例
//                 _playerActions = _inputSystem.Player; // 获取玩家输入动作映射
//             }
//
//             private void OnEnable()
//             {
//                 _playerActions.Enable(); // 启用玩家输入
//             }
//
//             private void OnDisable()
//             {
//                 _playerActions.Disable();
//             }
//
//             private void OnDestroy()
//             {
//                 _playerActions.Disable(); // 禁用玩家输入，确保资源释放
//             }
//
//             private void Update()
//             {
//                 // 获取玩家输入
//                 _movement = _playerActions.Move.ReadValue<Vector2>(); // 读取移动输入 
//                 _movement = _movement.normalized;
//             }
//     
//             private void FixedUpdate()
//             {
//                 // 根据输入移动玩家
//                 _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
//             }
//     }
// }
