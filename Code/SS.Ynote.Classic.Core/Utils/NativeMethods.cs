using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace SS.Ynote.Classic.Core
{
	/// <summary>
	/// Native Methods
	/// </summary>
	public static class NativeMethods
	{
		public const int WM_CREATE = 0x1;
		/// <summary>
		/// Set the Window Theme
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="pszSubAppName"></param>
		/// <param name="pszSubIdList"></param>
		/// <returns></returns>
		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
		public static extern int SetWindowTheme(HandleRef hWnd, string pszSubAppName, string pszSubIdList);
		
		 private const int SwShow = 5;
		 private const uint Seemaskinvokeidlist = 12;

		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		private static extern bool ShellExecuteEx(ref Shellexecuteinfo lpExecInfo);

		/// <summary>
		/// Shows the Properties window of a file
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		internal static bool ShowFileProperties(string filename)
		{
			var info = new Shellexecuteinfo();
			info.cbSize = Marshal.SizeOf(info);
			info.lpVerb = "properties";
			info.lpFile = filename;
			info.nShow = SwShow;
			info.fMask = Seemaskinvokeidlist;
			return ShellExecuteEx(ref info);
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct Shellexecuteinfo
		{
			public int cbSize;
			public uint fMask;
			private readonly IntPtr hwnd;

			[MarshalAs(UnmanagedType.LPTStr)] public string lpVerb;

			[MarshalAs(UnmanagedType.LPTStr)] public string lpFile;

			[MarshalAs(UnmanagedType.LPTStr)] private readonly string lpParameters;

			[MarshalAs(UnmanagedType.LPTStr)] private readonly string lpDirectory;

			public int nShow;
			private readonly IntPtr hInstApp;
			private readonly IntPtr lpIDList;

			[MarshalAs(UnmanagedType.LPTStr)] private readonly string lpClass;

			private readonly IntPtr hkeyClass;
			private readonly uint dwHotKey;
			private readonly IntPtr hIcon;
			private readonly IntPtr hProcess;
		}
	}
}
