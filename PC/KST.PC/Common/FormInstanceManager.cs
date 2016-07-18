using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST
{
    /// <summary>
    /// 窗体单例管理器
    /// </summary>
    public class FormInstanceManager
    {
        private static FormInstanceManager instance;
        private FormMain mainForm;

        private static readonly object instanceLocker = new object();
        private static readonly object mainFormLocker = new object();


        /// <summary>
        /// 隐藏默认的构造函数，避免外部直接 new 出对象
        /// </summary>
        private FormInstanceManager() { }

        /// <summary>
        /// 获取实例
        /// </summary>
        public static FormInstanceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLocker)
                    {
                        if (instance == null)
                        {
                            instance = new FormInstanceManager();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// 获取主窗体实例
        /// </summary>
        public FormMain FormMain
        {
            get
            {
                if (mainForm == null)
                {
                    lock (mainFormLocker)
                    {
                        if (mainForm == null)
                        {
                            mainForm = new FormMain();
                        }
                    }
                }
                return mainForm;
            }
        }

    }
}
