using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleShiftsSignalR.DataRunTime
{
    public class ConnectedList
    {
        private static ConnectedList _instance;
        private static object _syncRoot = new object();
        private static object _syncAddToDictionary = new object();
        private static object _syncGetUserName = new object();
        private static object _syncGetConnectionId = new object();
        private static Dictionary<string, string> userConnectionsId;

        private ConnectedList()
        {
            userConnectionsId = new Dictionary<string, string>();
        }

        public static ConnectedList GetInstance()
        {
            if(_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new ConnectedList();
                }
            }

            return _instance;
        }

        public void AddToDictionary(string connectionId, string userName)
        {
            lock (_syncAddToDictionary)
            {
                userConnectionsId.Add(connectionId, userName);
            }
        }

        public void RemoveFromDictionary(string connectionId)
        {
            userConnectionsId.Remove(connectionId);
        }

        public string GetUserName(string connectionId)
        {
            lock (_syncGetUserName)
            {
                return userConnectionsId[connectionId];
            }
        }
    }
}
