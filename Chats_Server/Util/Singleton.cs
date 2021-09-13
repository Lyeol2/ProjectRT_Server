namespace ProjectRT.Util
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        /// <summary>
        /// T 타입 객체 인스턴스
        /// </summary>
        private static T instance;
        /// <summary>
        /// 싱글턴 인스턴스를 찾거나 생성하는 과정 중 다른 스레드에서 사용중인지 판단할 객체
        /// </summary>
        private static object syncObject = new object();
        /// <summary>
        /// 외부에서 인스턴스에 접근하기 위한 프로퍼티
        /// </summary>
        public static T Instance
        {
            get
            {
                // 찾았는데 없다면
                if (instance == null)
                {
                    lock (syncObject)
                    {
                        instance = new T();
                    }
                }
                return instance;
            }
        }

        public static bool HasInstance()
        {
            return (instance != null) ? true : false;
        }
    }
}
