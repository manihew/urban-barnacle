using System;
using System.Collections.Generic;

namespace Company1.Department1.Project1.Services.Helper.DataAccess
{
    /// <summary>
    /// Singleton Implementation for TokenCollection
    /// </summary>
    public class SingletonData
    {
        private static volatile SingletonData instance;
        private static object syncRoot = new Object();
        public Dictionary<Tuple<String, String, String>, String> TokenCollection { get; set; }
        public static SingletonData Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SingletonData();
                        }
                    }
                }

                return instance;
            }
        }

        private SingletonData()
        {
            TokenCollection = new Dictionary<Tuple<String, String, String>, String>();
        }
    }
}
