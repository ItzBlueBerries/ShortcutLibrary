using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SRML.Console.Console;

namespace ShortcutLib.Extras
{
    public static class Debugging
    {
        internal static readonly ConsoleInstance ShortcutConsole = new ConsoleInstance("Shortcut Library");

        /// <summary>
        /// Sets the position of the given <see cref="GameObject"/>.
        /// </summary>
        /// <param name="obj">The <see cref="GameObject"/> to have it's position set.</param>
        /// <param name="position">The <see cref="Vector3"/> position to be set to the object.</param>
        public static void Position(this GameObject obj, Vector3 position) => obj.transform.position = position;

        /// <summary>
        /// Sets the parent of the given <see cref="GameObject"/>.
        /// </summary>
        /// <param name="obj">The <see cref="GameObject"/> to have it's parent set.</param>
        /// <param name="parent">The <see cref="GameObject"/> to be set as the parent of the object.</param>
        /// <param name="worldPosition">A <see cref="bool"/> that toggles if the object keeps it's world position.</param>
        public static void Parent(this GameObject obj, GameObject parent, bool worldPosition = true) => obj.transform.SetParent(parent.transform, worldPosition);

        /// <summary>
        /// Logs the size of the given <see cref="GameObject"/>.
        /// </summary>
        /// <param name="obj">The <see cref="GameObject"/> to have it's size logged.</param>
        /// <param name="logToFile">A <see cref="bool"/> that toggles if this is also logged to the srml.log file.</param>
        public static void LogSize(this GameObject obj, bool logToFile = true) =>
            ShortcutConsole.Log(obj.name + " (" + obj.transform.localScale.x + ", " + obj.transform.localScale.y + ", " + obj.transform.localScale.z + ")", logToFile);

        /// <summary>
        /// Logs the local position of the given <see cref="GameObject"/>.
        /// </summary>
        /// <param name="obj">The <see cref="GameObject"/> to have it's local position logged.</param>
        /// <param name="logToFile">A <see cref="bool"/> that toggles if this is also logged to the srml.log file.</param>
        public static void LogPosition(this GameObject obj, bool logToFile = true) =>
            ShortcutConsole.Log(obj.name + " (" + obj.transform.localPosition.x + ", " + obj.transform.localPosition.y + ", " + obj.transform.localPosition.z + ")", logToFile);

        /// <summary>
        /// Logs the contents of the <see cref="List{T}"/>.
        /// </summary>
        /// <param name="list">The <see cref="List{T}"/> to have it's contents logged.</param>
        /// <param name="logToFile">A <see cref="bool"/> that toggles if this is also logged to the srml.log file.</param>
        public static void LogList<T>(this ICollection<T> list, bool logToFile = true)
        {
            foreach (T item in list)
                ShortcutConsole.Log(item.ToString(), logToFile);
        }

        /// <summary>
        /// Logs the <see cref="Transform"/>s nested in the given <see cref="GameObject"/>.
        /// </summary>
        /// <param name="obj">The <see cref="GameObject"/> to have it's nested <see cref="Transform"/>s logged.</param>
        /// <param name="logToFile">A <see cref="bool"/> that toggles if this is also logged to the srml.log file.</param>
        public static void LogChildren(this GameObject obj, bool logToFile = true)
        {
            foreach (Transform transform in obj.transform)
                ShortcutConsole.Log(obj.name + " | " + transform.ToString(), logToFile);
        }

        /// <summary>
        /// Logs the <see cref="Component"/>s in the given <see cref="GameObject"/>.
        /// </summary>
        /// <param name="obj">The <see cref="GameObject"/> to have it's <see cref="Component"/>s logged.</param>
        /// <param name="logToFile">A <see cref="bool"/> that toggles if this is also logged to the srml.log file.</param>
        public static void LogComponents(this GameObject obj, bool logToFile = true)
        {
            foreach (Component component in obj.GetComponents(typeof(Component)))
                ShortcutConsole.Log(component.ToString(), logToFile);
        }

        /*/// <summary>
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
        }*/
    }
}
