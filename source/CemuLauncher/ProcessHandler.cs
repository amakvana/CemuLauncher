using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CemuLauncher
{

    /// <summary>
    /// https://stackoverflow.com/a/13109774
    /// Modified to accept pName rather than pID
    /// </summary>
    /// 
    public static class ProcessHandler
    {
        [Flags]
        private enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport("kernel32.dll")] private static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")] private static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")] private static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)] private static extern bool CloseHandle(IntPtr handle);

        public static void SuspendProcess(string pName)
        { 
            //var process = Process.GetProcessById(pid); // throws exception if process does not exist
            var processes = Process.GetProcessesByName(pName); // throws exception if process does not exist

            if (processes.Length > 0)
            {
                foreach (Process p in processes)
                {
                    foreach (ProcessThread pT in p.Threads)
                    {
                        IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                        if (pOpenThread == IntPtr.Zero)
                        {
                            continue;
                        }

                        SuspendThread(pOpenThread);
                        CloseHandle(pOpenThread);
                    }
                }
            }
        }

        public static void ResumeProcess(string pName)
        {
            //var process = Process.GetProcessById(pid); // throws exception if process does not exist
            var processes = Process.GetProcessesByName(pName); // throws exception if process does not exist

            if (processes.Length > 0)
            {
                foreach (Process p in processes)
                {
                    if (p.MainModule.FileName == string.Empty) return;

                    foreach (ProcessThread pT in p.Threads)
                    {
                        IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                        if (pOpenThread == IntPtr.Zero)
                        {
                            continue;
                        }

                        var suspendCount = 0;
                        do
                        {
                            suspendCount = ResumeThread(pOpenThread);
                        } while (suspendCount > 0);

                        CloseHandle(pOpenThread);
                    }
                }
            }
        }
    }
}
