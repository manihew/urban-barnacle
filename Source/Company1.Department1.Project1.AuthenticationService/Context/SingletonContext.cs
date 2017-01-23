using System;
using System.Collections.Generic;

namespace Company1.Department1.Project1.AuthenticationService.Context
{
    /// <summary>
    /// Singleton Context
    /// </summary>
    public class SingletonContext
    {

        private static volatile SingletonContext instance;
        private static object syncRoot = new Object();
        public Dictionary<String, String> RequestLogs { get; set; }
        public Dictionary<String, String> Applications { get; set; }

        private SingletonContext() { RequestLogs = new Dictionary<String, String>(); Applications = new Dictionary<String, String>(); }

        public static SingletonContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        { 
                            instance = new SingletonContext();
                        }
                    }
                }
                return instance;
            }
        }
    }
}