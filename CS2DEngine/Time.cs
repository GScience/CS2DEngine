using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine
{
    /// <summary>
    /// 更高级的时间获取
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// 从游戏开始到当前时刻总共经理时间
        /// </summary>
        public static double TotalTime { get; private set; }

        /// <summary>
        /// 从上一次刷新道现在的时间
        /// </summary>
        public static double DeltaTime { get; private set; }

        /// <summary>
        /// 从上一次刷新道现在的时间，无缩放
        /// </summary>
        public static double DeltaTimeNoScale { get; private set; }

        /// <summary>
        /// 时间缩放
        /// </summary>
        public static double TimeScale { get; set; }

        /// <summary>
        /// 刷新时间
        /// 由引擎内部自己调用
        /// </summary>
        /// <param name="deltaTime">变化的时间</param>
        internal static void Update(double deltaTime)
        {
            TotalTime += deltaTime;
            DeltaTime = deltaTime * TimeScale;
            DeltaTimeNoScale = deltaTime;
        }
    }
}
