/*
* PROJECT: HergBot Logging
* PROGRAMMER: Justin
* FIRST VERSION: 04/08/2017
*/

using System;

namespace HergBot.Logging
{
    /// <summary>
    /// An interface to follow for implementing HergBot Logging classes
    /// </summary>
    public interface ILogger
    {
        void LogDebug(string debugMessage, string methodName = "");

        void LogError(string errorMessage, string methodName = "");

        void LogException(Exception exception, string methodName = "");

        void LogInfo(string infoMessage, string methodName = "");

        void LogWarning(string warningMessage, string methodName = "");
    }
}