// default
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// my imports
using UnityEngine;
using static SRML.Console.Console;

namespace ShortcutLib
{
    /// <summary>
    /// Debugging Class [SHLIB]
    /// </summary>
    public class Debugging
    {
        private static ConsoleInstance ConsoleInstance = new ConsoleInstance("Shortcut Lib");

        public static void LogComponents(GameObject objectPrefab, bool logToFile = true)
        {
            foreach (Component components in objectPrefab.GetComponents(typeof(Component)))
                ConsoleInstance.Log(components.ToString(), logToFile);
        }

        public static void LogChildren(GameObject objectPrefab, bool logToFile = true)
        {
            foreach (Transform children in objectPrefab.transform)
                ConsoleInstance.Log(objectPrefab.name + " " + children.ToString(), logToFile);
        }

        public static void LogClassContents(HashSet<Identifiable.Id> idenClass, bool logToFile = true)
        {
            foreach (Identifiable.Id id in idenClass)
                ConsoleInstance.Log(id.ToString(), logToFile);
        }

        public static void LogFindChildren(GameObject objectPrefab, string childName, bool logToFile = true)
        {
            foreach (Transform children in objectPrefab.transform.Find(childName))
                ConsoleInstance.Log(objectPrefab.name + " " + children.ToString(), logToFile);
        }

        /*public static void LogObjectSize(GameObject objectPrefab, bool logToFile = true)
        {
            foreach (Transform size in objectPrefab.transform)
            {
                ConsoleInstance.Log(objectPrefab.name + " (" + size.localScale.x + ", " + size.localScale.y + ", " + size.localScale.z + ")", logToFile);
            }
        }

        public static void LogObjectPosition(GameObject objectPrefab, bool logToFile = true)
        {
            foreach (Transform size in objectPrefab.transform)
            {
                ConsoleInstance.Log(objectPrefab.name + " (" + size.localPosition.x + ", " + size.localPosition.y + ", " + size.localPosition.z + ")", logToFile);
            }
        }*/

        /// <summary>
        /// Logs to the console (CTRL + TAB), parameters can determine what kind of log. Logging to a file will log to the srml.log file in your AppData.
        /// </summary>
        /// <param name="toLog">The <see cref="string"/> of what is to be logged.</param>
        /// <param name="logToFile">The <see cref="bool"/> of if not/to log to the srml.log file.</param>
        /// <param name="isInfo">The <see cref="bool"/> on if its to be logged as regular Info.</param>
        /// <param name="isError">The <see cref="bool"/> on if its to be logged as an error.</param>
        /// <param name="isSuccess">The <see cref="bool"/> on if its to be logged as successful log.</param>
        public static void Log(string toLog, bool logToFile = true, bool isInfo = true, bool isError = false, bool isSuccess = false)
        {
            if (isInfo)
                ConsoleInstance.Log(toLog, logToFile);
            else if (isError)
                ConsoleInstance.LogError(toLog, logToFile);
            else if (isSuccess)
                ConsoleInstance.LogSuccess(toLog, logToFile);
            else
                ConsoleInstance.Log(toLog, logToFile);
        }

        /// <summary>
        /// Logs to the srml.log file, parameters can determine what kind of log.
        /// </summary>
        /// <param name="toLog">The <see cref="string"/> of what is to be logged.</param>
        /// <param name="isInfo">The <see cref="bool"/> on if its to be logged as regular Info.</param>
        /// <param name="isError">The <see cref="bool"/> on if its to be logged as an error.</param>
        /// <param name="isWarning">The <see cref="bool"/> on if its to be logged as successful log.</param>
        public static void LogFile(string toLog, bool isInfo = true, bool isError = false, bool isWarning = false)
        {
            if (isInfo)
                ConsoleInstance.LogToFile(toLog);
            else if (isError)
                ConsoleInstance.LogErrorToFile(toLog);
            else if (isWarning)
                ConsoleInstance.LogWarningToFile(toLog);
            else
                ConsoleInstance.LogToFile(toLog);
        }

        public static void Parent(GameObject obj, GameObject parent, bool worldPosition = true)
        { obj.transform.SetParent(parent.transform, worldPosition); }

        public static void Position(GameObject obj, Vector3 position)
        { obj.transform.position = position; }
    }
}
