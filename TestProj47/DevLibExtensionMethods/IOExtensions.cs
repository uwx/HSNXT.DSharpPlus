// Decompiled with JetBrains decompiler
// Type: TestProj47.IOExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;

namespace HSNXT
{
    public static partial class Extensions

    {
        /// <summary>Renames the file to "* (n).*" if file already exists.</summary>
        /// <param name="filename">The filename.</param>
        /// <returns>New file name "* (n).*" if file already exists; otherwise, the original file name.</returns>
        public static string RenameFile(this string filename)
        {
            if (!File.Exists(filename))
                return filename;
            var withoutExtension = Path.GetFileNameWithoutExtension(filename);
            var extension = Path.GetExtension(filename);
            var directoryName = Path.GetDirectoryName(Path.GetFullPath(filename));
            var regex = new Regex("^" + withoutExtension + "\\s*(\\d+)?");
            var num = Directory.GetFiles(directoryName, withoutExtension + "*" + extension)
                .Where(i => regex.IsMatch(Path.GetFileNameWithoutExtension(i))).Count();
            return withoutExtension + " (" + (num + 1) + ")" + extension;
        }

        /// <summary>
        /// Renames the folder to "* (n)" if folder already exists.
        /// </summary>
        /// <param name="path">The folder path.</param>
        /// <returns>New folder name "* (n)" if folder already exists; otherwise, the original folder name.</returns>
        public static string RenameFolder(string path)
        {
            if (!Directory.Exists(path))
                return path;
            var fileName = Path.GetFileName(path);
            var directoryName = Path.GetDirectoryName(Path.GetFullPath(path));
            var regex = new Regex("^" + fileName + "\\s*(\\d+)?");
            var num = Directory.GetDirectories(directoryName, fileName + "*")
                .Where(i => regex.IsMatch(Path.GetFileName(i))).Count();
            return fileName + " (" + (num + 1) + ")";
        }

        /// <summary>
        /// Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file.
        /// </summary>
        /// <param name="contents">The string to write to the file.</param>
        /// <param name="filename">The file to write to.</param>
        /// <param name="overwrite">Whether overwrite exists file.</param>
        /// <param name="encoding">The encoding to apply to the string.</param>
        /// <returns>Full path of the file name if write file succeeded.</returns>
        public static string WriteTextFile(this string contents, string filename, bool overwrite = false,
            Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            var fullPath = Path.GetFullPath(filename);
            var directoryName = Path.GetDirectoryName(fullPath);
            if (!overwrite && File.Exists(fullPath))
                throw new ArgumentException("The specified file already exists.", fullPath);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            if (encoding == null)
                File.WriteAllText(fullPath, contents);
            else
                File.WriteAllText(fullPath, contents, encoding);
            return fullPath;
        }

        /// <summary>
        /// Appends the specified string to the file, creating the file if it does not already exist.
        /// </summary>
        /// <param name="contents">The string to append to the file.</param>
        /// <param name="filename">The file to append the specified string to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <returns>Full path of the file name if write file succeeded.</returns>
        public static string AppendTextFile(this string contents, string filename, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            var fullPath = Path.GetFullPath(filename);
            var directoryName = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            if (encoding == null)
                File.AppendAllText(fullPath, contents);
            else
                File.AppendAllText(fullPath, contents, encoding);
            return fullPath;
        }

        /// <summary>
        /// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
        /// </summary>
        /// <param name="filename">The file to open for reading.</param>
        /// <param name="encoding">The encoding applied to the contents of the file.</param>
        /// <returns>A string containing all lines of the file.</returns>
        public static string ReadTextFile(this string filename, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            var fullPath = Path.GetFullPath(filename);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            if (encoding != null)
                return File.ReadAllText(fullPath, encoding);
            return File.ReadAllText(fullPath);
        }

        /// <summary>
        /// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
        /// </summary>
        /// <param name="filename">The file to open for reading.</param>
        /// <param name="encoding">The encoding applied to the contents of the file.</param>
        /// <returns>A string array containing all lines of the file.</returns>
        public static string[] ReadFileAllLines(this string filename, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            var fullPath = Path.GetFullPath(filename);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("The specified file does not exist.", fullPath);
            if (encoding != null)
                return File.ReadAllLines(fullPath, encoding);
            return File.ReadAllLines(fullPath);
        }

        /// <summary>
        /// Creates a new file, writes the specified byte array to the file, and then closes the file.
        /// </summary>
        /// <param name="bytes">The bytes to write to the file.</param>
        /// <param name="filename">The file to write to.</param>
        /// <param name="overwrite">Whether overwrite exists file.</param>
        /// <returns>Full path of the file name if write file succeeded.</returns>
        public static string WriteBinaryFile(this byte[] bytes, string filename, bool overwrite = false)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));
            var fullPath = Path.GetFullPath(filename);
            var directoryName = Path.GetDirectoryName(fullPath);
            if (!overwrite && File.Exists(fullPath))
                throw new ArgumentException("The specified file already exists.", fullPath);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            File.WriteAllBytes(fullPath, bytes);
            return fullPath;
        }

        /// <summary>
        /// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
        /// </summary>
        /// <param name="filename">The file to open for reading.</param>
        /// <returns>A byte array containing the contents of the file.</returns>
        public static byte[] ReadBinaryFile(this string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            var fullPath = Path.GetFullPath(filename);
            if (File.Exists(fullPath))
                return File.ReadAllBytes(fullPath);
            throw new FileNotFoundException("The specified file does not exist.", fullPath);
        }

        /// <summary>
        /// Creates a new file, writes the specified stream to the file, and then closes the file.
        /// </summary>
        /// <param name="source">The stream to write to the file.</param>
        /// <param name="filename">The file to write to.</param>
        /// <param name="overwrite">Whether overwrite exists file.</param>
        /// <returns>Full path of the file name if write file succeeded.</returns>
        public static string WriteFile(this Stream source, string filename, bool overwrite = false)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var fullPath = Path.GetFullPath(filename);
            var directoryName = Path.GetDirectoryName(fullPath);
            if (!overwrite && File.Exists(fullPath))
                throw new ArgumentException("The specified file already exists.", fullPath);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                var buffer = new byte[81920];
                int count;
                while ((count = source.Read(buffer, 0, buffer.Length)) != 0)
                    fileStream.Write(buffer, 0, count);
                return fullPath;
            }
        }

        /// <summary>Open containing folder with Windows Explorer.</summary>
        /// <param name="filename">Path or File name.</param>
        /// <returns>Full path or the file name if open folder succeeded.</returns>
        [EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
        public static string OpenContainingFolder(this string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            var fullPath = Path.GetFullPath(filename);
            Process.Start("explorer.exe", Path.GetDirectoryName(fullPath));
            return fullPath;
        }

        /// <summary>
        /// Returns the directory information for the specified path string.
        /// </summary>
        /// <param name="source">The path of a file or directory.</param>
        /// <returns>
        /// Directory information for path, or null if path denotes a root directory or is null.
        /// Returns System.String.Empty if path does not contain directory information.
        /// </returns>
        public static string GetDirectoryName(this string source)
        {
            return Path.GetDirectoryName(source);
        }

        /// <summary>
        /// Returns the absolute path for the specified path string.
        /// </summary>
        /// <param name="source">The file or directory for which to obtain absolute path information.</param>
        /// <returns>A string containing the fully qualified location of path, such as "C:\MyFile.txt".</returns>
        public static string GetFullPath(this string source)
        {
            return Path.GetFullPath(source);
        }

        /// <summary>Determines whether the specified file exists.</summary>
        /// <param name="source">The file to check.</param>
        /// <returns>
        /// true if the caller has the required permissions and path contains the name of an existing file; otherwise, false.
        /// This method also returns false if path is null, an invalid path, or a zero-length string.
        /// If the caller does not have sufficient permissions to read the specified file,
        /// no exception is thrown and the method returns false regardless of the existence of path.
        /// </returns>
        public static bool ExistsFile(this string source)
        {
            return File.Exists(source);
        }

        /// <summary>
        /// Determines whether the given path refers to an existing directory on disk.
        /// </summary>
        /// <param name="source">The path to test.</param>
        /// <returns>true if path refers to an existing directory; otherwise, false.</returns>
        public static bool ExistsDirectory(this string source)
        {
            return Directory.Exists(source);
        }

        /// <summary>
        /// Moves a specified file to a new location, providing the option to specify a new file name.
        /// </summary>
        /// <param name="sourceFileName">The name of the file to move.</param>
        /// <param name="destFileName">The new path for the file.</param>
        /// <param name="overwrite">Whether overwrite exists file.</param>
        /// <returns>Full path of the destination file name if move file succeeded.</returns>
        public static string MoveFileTo(this string sourceFileName, string destFileName, bool overwrite = false)
        {
            if (string.IsNullOrEmpty(sourceFileName))
                throw new ArgumentNullException(nameof(sourceFileName));
            if (string.IsNullOrEmpty(destFileName))
                throw new ArgumentNullException(nameof(destFileName));
            var fullPath1 = Path.GetFullPath(sourceFileName);
            var fullPath2 = Path.GetFullPath(destFileName);
            var directoryName = Path.GetDirectoryName(destFileName);
            if (File.Exists(fullPath2))
            {
                if (!overwrite)
                    throw new ArgumentException("The specified file already exists.", fullPath1);
                File.Delete(fullPath2);
            }
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            File.Move(fullPath1, fullPath2);
            return fullPath2;
        }

        /// <summary>
        /// Copies an existing file to a new file. Overwriting a file of the same name is allowed.
        /// </summary>
        /// <param name="sourceFileName">The file to copy.</param>
        /// <param name="destFileName">The name of the destination file. This cannot be a directory.</param>
        /// <param name="overwrite">true if the destination file can be overwritten; otherwise, false.</param>
        /// <returns>Full path of the destination file name if copy file succeeded.</returns>
        public static string CopyFileTo(this string sourceFileName, string destFileName, bool overwrite = false)
        {
            if (string.IsNullOrEmpty(sourceFileName))
                throw new ArgumentNullException(nameof(sourceFileName));
            if (string.IsNullOrEmpty(destFileName))
                throw new ArgumentNullException(nameof(destFileName));
            var fullPath1 = Path.GetFullPath(sourceFileName);
            var fullPath2 = Path.GetFullPath(destFileName);
            var directoryName = Path.GetDirectoryName(destFileName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            File.Copy(fullPath1, fullPath2, overwrite);
            return fullPath2;
        }

        /// <summary>
        /// Copies a directory to a new directory. Overwriting a file of the same name is allowed.
        /// </summary>
        /// <param name="sourceDirectory">The directory to copy.</param>
        /// <param name="destDirectory">The name of the destination directory.</param>
        /// <param name="overwrite">true if the destination file can be overwritten; otherwise, false.</param>
        /// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs.</param>
        /// <returns>Full path of the destination directory if copy succeeded; otherwise, String.Empty.</returns>
        public static string CopyDirectoryTo(this string sourceDirectory, string destDirectory, bool overwrite = true,
            bool throwOnError = false)
        {
            if (string.IsNullOrEmpty(sourceDirectory))
            {
                if (throwOnError)
                    throw new ArgumentNullException("sourceFileName");
                return string.Empty;
            }
            if (string.IsNullOrEmpty(destDirectory))
            {
                if (throwOnError)
                    throw new ArgumentNullException("destFileName");
                return string.Empty;
            }
            var fullPath1 = Path.GetFullPath(sourceDirectory);
            var fullPath2 = Path.GetFullPath(destDirectory);
            if (fullPath1.Equals(fullPath2, StringComparison.OrdinalIgnoreCase))
            {
                if (throwOnError)
                    throw new ArgumentException("Source directory and destination directory are the same.");
                return string.Empty;
            }
            if (!Directory.Exists(sourceDirectory))
            {
                if (throwOnError)
                    throw new DirectoryNotFoundException($"{fullPath1} does not exist.");
                return string.Empty;
            }
            try
            {
                foreach (var file in Directory.GetFiles(fullPath1, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        file.CopyFileTo(file.Replace(fullPath1, fullPath2), overwrite);
                    }
                    catch
                    {
                        if (throwOnError)
                            throw;
                    }
                }
                return fullPath2;
            }
            catch
            {
                if (!throwOnError)
                    return string.Empty;
                throw;
            }
        }

        /// <summary>
        /// Determines whether the given path is a directory or not.
        /// </summary>
        /// <param name="sourcePath">The path to test.</param>
        /// <returns>true if path is a directory; otherwise, false.</returns>
        public static bool IsDirectory(this string sourcePath)
        {
            var fileInfo = new FileInfo(sourcePath);
            return (fileInfo.Attributes & FileAttributes.Directory) != 0;
        }

        /// <summary>
        /// Determines whether the specified path is empty directory.
        /// </summary>
        /// <param name="sourcePath">The path to check.</param>
        /// <returns>true if the specified path is empty directory; otherwise, false.</returns>
        public static bool IsDirectoryEmpty(this string sourcePath)
        {
            if (Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories).Length == 0)
                return Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories).Length == 0;
            return false;
        }

        /// <summary>
        /// Determines whether the given path refers to an existing directory or a file on disk.
        /// </summary>
        /// <param name="source">The path to test.</param>
        /// <returns>true if path refers to an existing directory or a file; otherwise, false.</returns>
        public static bool ExistsFileSystem(this string source)
        {
            var fileInfo = new FileInfo(source);
            if ((fileInfo.Attributes & FileAttributes.Directory) == 0)
                return fileInfo.Exists;
            return true;
        }

        /// <summary>
        /// Deletes an empty directory and, if indicated, any subdirectories and files in the directory.
        /// </summary>
        /// <param name="source">The name of the directory to remove.</param>
        /// <param name="recursive">true to remove directories, subdirectories, and files in path; otherwise, false.</param>
        /// <returns>true if succeeded; otherwise, false.</returns>
        public static bool DeleteDirectory(this string source, bool recursive)
        {
            var directoryInfo = new DirectoryInfo(source);
            if (!directoryInfo.Exists)
                return true;
            foreach (var file in directoryInfo.GetFiles("*",
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                try
                {
                    file.Attributes = FileAttributes.Normal;
                }
                catch
                {
                }
                try
                {
                    file.Delete();
                }
                catch
                {
                }
            }
            foreach (var directory in directoryInfo.GetDirectories("*",
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                try
                {
                    directory.Attributes = FileAttributes.Normal;
                }
                catch
                {
                }
                try
                {
                    directory.Delete(recursive);
                }
                catch
                {
                }
            }
            try
            {
                directoryInfo.Attributes = FileAttributes.Normal;
            }
            catch
            {
            }
            try
            {
                directoryInfo.Delete(recursive);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Deletes the specified file.</summary>
        /// <param name="source">The name of the file to be deleted.</param>
        /// <returns>true if succeeded; otherwise, false.</returns>
        public static bool DeleteFile(this string source)
        {
            var fileInfo = new FileInfo(source);
            if (!fileInfo.Exists)
                return true;
            try
            {
                fileInfo.Attributes = FileAttributes.Normal;
            }
            catch
            {
            }
            try
            {
                fileInfo.Delete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Execute a command line.</summary>
        /// <param name="sourceCmd">A command line to execute.</param>
        /// <param name="onExited">Occurs when the associated process exits.</param>
        /// <param name="runasAdmin">true to run as Administrator; false to run as current user.</param>
        /// <param name="hidden">true if want to hide window; otherwise, false.</param>
        /// <param name="milliseconds">
        /// The amount of time, in milliseconds, to wait for the associated process to exit.
        /// If value is null, will not wait.
        /// If value is less then zero, will wait indefinitely for the associated process to exit.
        /// The maximum is the largest possible value of a 32-bit integer, which represents infinity to the operating system.
        /// </param>
        /// <returns>The system-generated unique identifier of the process that is referenced by this System.Diagnostics.Process instance.</returns>
        [EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
        public static int ExecuteCmdLine(this string sourceCmd, Action onExited = null, bool runasAdmin = true,
            bool hidden = true, int? milliseconds = null)
        {
            var processStartInfo =
                new ProcessStartInfo(Path.Combine(Environment.SystemDirectory, "cmd.exe"))
                {
                    Arguments = $" /C {sourceCmd}",
                    CreateNoWindow = hidden,
                    ErrorDialog = true,
                    UseShellExecute = true,
                    WindowStyle = hidden ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal
                };
            if (runasAdmin)
                processStartInfo.Verb = "runas";
            var process = new Process
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true
            };
            process.Exited += (EventHandler) ((s, e) =>
            {
                if (onExited == null)
                    return;
                onExited();
            });
            process.Start();
            var num = -1;
            try
            {
                num = process.Id;
            }
            catch
            {
            }
            if (milliseconds.HasValue)
            {
                var nullable = milliseconds;
                if ((nullable.GetValueOrDefault() < 0 ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
                {
                    if (process.WaitForExit(milliseconds.Value))
                        process.Dispose();
                }
                else
                    process.WaitForExit();
            }
            return num;
        }

        /// <summary>Execute a file with arguments.</summary>
        /// <param name="sourceFile">A file to execute.</param>
        /// <param name="arguments">Command-line arguments to use when starting the application.</param>
        /// <param name="onExited">Occurs when the associated process exits.</param>
        /// <param name="runasAdmin">true to run as Administrator; false to run as current user.</param>
        /// <param name="hidden">true if want to hide window; otherwise, false.</param>
        /// <param name="milliseconds">
        /// The amount of time, in milliseconds, to wait for the associated process to exit.
        /// If value is null, will not wait.
        /// If value is less then zero, will wait indefinitely for the associated process to exit.
        /// The maximum is the largest possible value of a 32-bit integer, which represents infinity to the operating system.
        /// </param>
        /// <returns>The system-generated unique identifier of the process that is referenced by this System.Diagnostics.Process instance.</returns>
        [EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
        public static int Execute(this string sourceFile, string arguments = null, Action onExited = null,
            bool runasAdmin = true, bool hidden = true, int? milliseconds = null)
        {
            var processStartInfo = new ProcessStartInfo(sourceFile);
            processStartInfo.Arguments = arguments;
            processStartInfo.CreateNoWindow = hidden;
            processStartInfo.ErrorDialog = true;
            processStartInfo.UseShellExecute = true;
            processStartInfo.WindowStyle = hidden ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal;
            if (runasAdmin)
                processStartInfo.Verb = "runas";
            var process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            process.Exited += (EventHandler) ((s, e) =>
            {
                if (onExited == null)
                    return;
                onExited();
            });
            process.Start();
            var num = -1;
            try
            {
                num = process.Id;
            }
            catch
            {
            }
            if (milliseconds.HasValue)
            {
                var nullable = milliseconds;
                if ((nullable.GetValueOrDefault() < 0 ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
                {
                    if (process.WaitForExit(milliseconds.Value))
                        process.Dispose();
                }
                else
                    process.WaitForExit();
            }
            return num;
        }

        /// <summary>Bytes to Stream object.</summary>
        /// <param name="source">Bytes source.</param>
        /// <returns>Stream object.</returns>
        public static Stream ToStream(this byte[] source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return new MemoryStream(source);
        }

        /// <summary>
        /// Reads the bytes from the current stream and writes them to another stream.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
        public static void CopyStreamTo(this Stream source, Stream destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            var buffer = new byte[81920];
            int count;
            while ((count = source.Read(buffer, 0, buffer.Length)) != 0)
                destination.Write(buffer, 0, count);
        }

        /// <summary>
        /// Reads the bytes from the current stream and writes them to another stream.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="startPosition">The zero-based starting position of the source stream to be copied.</param>
        /// <param name="length">Length of the source stream to be copied.</param>
        /// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
        public static void CopyStreamTo(this Stream source, long startPosition, int length, Stream destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            source.Position = startPosition;
            var buffer = new byte[81920];
            var count1 = length;
            while (count1 > 0)
            {
                if (count1 <= buffer.Length)
                {
                    var count2 = source.Read(buffer, 0, count1);
                    if (count2 <= 0)
                        break;
                    destination.Write(buffer, 0, count2);
                    break;
                }
                var count3 = source.Read(buffer, 0, buffer.Length);
                if (count3 <= 0)
                    break;
                destination.Write(buffer, 0, count3);
                if (count3 != buffer.Length)
                    break;
                count1 -= buffer.Length;
            }
        }

        /// <summary>
        /// Formats the long length of a file to a more friendly string, e.g. "1.23 GB", "456 KB", etc.,
        /// </summary>
        /// <param name="fileSize">The file size for which to determine the format.</param>
        /// <returns>The resulting string.</returns>
        public static string ToFileSizeFriendlyString(this double fileSize)
        {
            if (fileSize < 0.0)
                return fileSize.ToString();
            if (fileSize >= 1099511627776.0)
                return (fileSize / 1099511627776.0).ToString("########0.00") + " TB";
            if (fileSize >= 1073741824.0)
                return (fileSize / 1073741824.0).ToString("########0.00") + " GB";
            if (fileSize >= 1048576.0)
                return (fileSize / 1048576.0).ToString("####0.00") + " MB";
            if (fileSize >= 1024.0)
                return (fileSize / 1024.0).ToString("####0.00") + " KB";
            return fileSize.ToString("####0") + " bytes";
        }

        /// <summary>
        /// Formats the long length of a file to a more friendly string, e.g. "1.23 GB", "456 KB", etc.,
        /// </summary>
        /// <param name="fileSize">The file size for which to determine the format.</param>
        /// <returns>The resulting string.</returns>
        public static string ToFileSizeFriendlyString(this float fileSize)
        {
            return ((double) fileSize).ToFileSizeFriendlyString();
        }

        /// <summary>
        /// Formats the long length of a file to a more friendly string, e.g. "1.23 GB", "456 KB", etc.,
        /// </summary>
        /// <param name="fileSize">The file size for which to determine the format.</param>
        /// <returns>The resulting string.</returns>
        public static string ToFileSizeFriendlyString(this long fileSize)
        {
            return ((double) fileSize).ToFileSizeFriendlyString();
        }

        /// <summary>
        /// Formats the long length of a file to a more friendly string, e.g. "1.23 GB", "456 KB", etc.,
        /// </summary>
        /// <param name="fileSize">The file size for which to determine the format.</param>
        /// <returns>The resulting string.</returns>
        public static string ToFileSizeFriendlyString(this int fileSize)
        {
            return ((double) fileSize).ToFileSizeFriendlyString();
        }
    }
}