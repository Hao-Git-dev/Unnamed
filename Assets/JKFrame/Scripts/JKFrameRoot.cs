using UnityEngine;

namespace JKFrame
{
#if UNITY_EDITOR
    using UnityEditor;
    [InitializeOnLoad]
#endif
    [DefaultExecutionOrder(-20)]
    /// <summary>
    /// 框架根节点
    /// </summary>
    public class JKFrameRoot : MonoBehaviour
    {
        private JKFrameRoot() { }
        private static JKFrameRoot Instance;
        public static Transform RootTransform { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this) // 防止Editor下的Instance已经存在，并且是自身
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            RootTransform = transform;
            DontDestroyOnLoad(gameObject);
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            InitSystems();
        }

        #region System
        private void InitSystems()
        {
            PoolSystem.Init();
#if ENABLE_LOG
            JKLog.Init(FrameSetting.LogConfig);
#endif
        }

        #endregion

        #region Editor
#if UNITY_EDITOR
        // 编辑器专属事件系统
        
        [InitializeOnLoadMethod]
        public static void InitForEditor()
        {
           
        }
#endif
        #endregion
    }

}


