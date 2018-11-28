using System.Collections;
using System.Collections.Generic;
using System;

namespace Lemon
{
    /// <summary>
    /// 等待接口
    /// </summary>
    public interface IWait
    {
        /// <summary>
        /// 每帧检测是否等待结束
        /// </summary>
        /// <returns></returns>
        bool Tick();
    }

    public class Time
    {
        //每帧时间(秒)
        public static float deltaTime
        { get { return (float)deltaMilliseconds / 1000; } }
        //每帧时间（毫秒）
        public static int deltaMilliseconds
        { get { return 20; } }
    }

    /// <summary>
    /// 按秒等待
    /// </summary>
    public class WaitForSeconds : IWait
    {
        float _seconds = 0f;

        public WaitForSeconds(float seconds)
        {
            _seconds = seconds;
        }

        public bool Tick()
        {
            _seconds -= Time.deltaTime;
            return _seconds <= 0;
        }
    }

    /// <summary>
    /// 按帧等待
    /// </summary>
    public class WaitForFrames : IWait
    {
        private int _frames = 0;
        public WaitForFrames(int frames)
        {
            _frames = frames;
        }

        public bool Tick()
        {
            _frames -= 1;
            return _frames <= 0;
        }
    }
}
