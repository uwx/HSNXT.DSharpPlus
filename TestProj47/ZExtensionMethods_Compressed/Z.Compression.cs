using System.IO;
using System.IO.Compression;
using System.Text;

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System.IO;
//using System.IO.Compression;
//using System.Text;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A byte[] extension method that decompress the byte array gzip to string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The byte array gzip to string.</returns>
        public static string DecompressGZip(this byte[] @this)
        {
            const int bufferSize = 1024;
            using (var memoryStream = new MemoryStream(@this))
            {
                using (var zipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    // Memory stream for storing the decompressed bytes
                    using (var outStream = new MemoryStream())
                    {
                        var buffer = new byte[bufferSize];
                        var totalBytes = 0;
                        int readBytes;
                        while ((readBytes = zipStream.Read(buffer, 0, bufferSize)) > 0)
                        {
                            outStream.Write(buffer, 0, readBytes);
                            totalBytes += readBytes;
                        }
                        return Encoding.Default.GetString(outStream.GetBuffer(), 0, totalBytes);
                    }
                }
            }
        }

        /// <summary>
        ///     A byte[] extension method that decompress the byte array gzip to string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The byte array gzip to string.</returns>
        public static string DecompressGZip(this byte[] @this, Encoding encoding)
        {
            const int bufferSize = 1024;
            using (var memoryStream = new MemoryStream(@this))
            {
                using (var zipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    // Memory stream for storing the decompressed bytes
                    using (var outStream = new MemoryStream())
                    {
                        var buffer = new byte[bufferSize];
                        var totalBytes = 0;
                        int readBytes;
                        while ((readBytes = zipStream.Read(buffer, 0, bufferSize)) > 0)
                        {
                            outStream.Write(buffer, 0, readBytes);
                            totalBytes += readBytes;
                        }
                        return encoding.GetString(outStream.GetBuffer(), 0, totalBytes);
                    }
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that creates a zip file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void CreateGZip(this FileInfo @this)
        {
            using (var originalFileStream = @this.OpenRead())
            {
                using (var compressedFileStream = File.Create(@this.FullName + ".gz"))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that creates a zip file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destination">Destination for the zip.</param>
        public static void CreateGZip(this FileInfo @this, string destination)
        {
            using (var originalFileStream = @this.OpenRead())
            {
                using (var compressedFileStream = File.Create(destination))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that creates a zip file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destination">Destination for the zip.</param>
        public static void CreateGZip(this FileInfo @this, FileInfo destination)
        {
            using (var originalFileStream = @this.OpenRead())
            {
                using (var compressedFileStream = File.Create(destination.FullName))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that extracts the g zip to directory described by
        ///     @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void ExtractGZipToDirectory(this FileInfo @this)
        {
            using (var originalFileStream = @this.OpenRead())
            {
                var newFileName = Path.GetFileNameWithoutExtension(@this.FullName);

                using (var decompressedFileStream = File.Create(newFileName))
                {
                    using (var decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that extracts the g zip to directory described by
        ///     @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destination">Destination for the.</param>
        public static void ExtractGZipToDirectory(this FileInfo @this, string destination)
        {
            using (var originalFileStream = @this.OpenRead())
            {
                using (var compressedFileStream = File.Create(destination))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that extracts the g zip to directory described by
        ///     @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destination">Destination for the.</param>
        public static void ExtractGZipToDirectory(this FileInfo @this, FileInfo destination)
        {
            using (var originalFileStream = @this.OpenRead())
            {
                using (var compressedFileStream = File.Create(destination.FullName))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        /// <summary>
        ///     A string extension method that compress the given string to GZip byte array.
        /// </summary>
        /// <param name="this">The stringToCompress to act on.</param>
        /// <returns>The string compressed into a GZip byte array.</returns>
        public static byte[] CompressGZip(this string @this)
        {
            var stringAsBytes = Encoding.Default.GetBytes(@this);
            using (var memoryStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                {
                    zipStream.Write(stringAsBytes, 0, stringAsBytes.Length);
                    zipStream.Close();
                    return memoryStream.ToArray();
                }
            }
        }

        /// <summary>
        ///     A string extension method that compress the given string to GZip byte array.
        /// </summary>
        /// <param name="this">The stringToCompress to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The string compressed into a GZip byte array.</returns>
        public static byte[] CompressGZip(this string @this, Encoding encoding)
        {
            var stringAsBytes = encoding.GetBytes(@this);
            using (var memoryStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                {
                    zipStream.Write(stringAsBytes, 0, stringAsBytes.Length);
                    zipStream.Close();
                    return memoryStream.ToArray();
                }
            }
        }

        /// <summary>
        ///     Creates a zip archive that contains the files and directories from the specified
        ///     directory.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destinationArchiveFileName">
        ///     The path of the archive to be created, specified as a
        ///     relative or absolute path. A relative path is interpreted as relative to the current working
        ///     directory.
        /// </param>
        public static void CreateZipFile(this DirectoryInfo @this, string destinationArchiveFileName)
        {
            ZipFile.CreateFromDirectory(@this.FullName, destinationArchiveFileName);
        }

        /// <summary>
        ///     Creates a zip archive that contains the files and directories from the specified
        ///     directory, uses the specified compression level, and optionally includes the base directory.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destinationArchiveFileName">
        ///     The path of the archive to be created, specified as a
        ///     relative or absolute path. A relative path is interpreted as relative to the current working
        ///     directory.
        /// </param>
        /// <param name="compressionLevel">
        ///     One of the enumeration values that indicates whether to
        ///     emphasize speed or compression effectiveness when creating the entry.
        /// </param>
        /// <param name="includeBaseDirectory">
        ///     true to include the directory name from
        ///     sourceDirectoryName at the root of the archive; false to include only the contents of the
        ///     directory.
        /// </param>
        public static void CreateZipFile(this DirectoryInfo @this, string destinationArchiveFileName,
            CompressionLevel compressionLevel, bool includeBaseDirectory)
        {
            ZipFile.CreateFromDirectory(@this.FullName, destinationArchiveFileName, compressionLevel,
                includeBaseDirectory);
        }

        /// <summary>
        ///     Creates a zip archive that contains the files and directories from the specified directory, uses the specified
        ///     compression level and character encoding for entry names, and optionally includes the base directory.
        /// </summary>
        /// <param name="this">
        ///     The path to the directory to be archived, specified as a relative or absolute path. A relative path
        ///     is interpreted as relative to the current working directory.
        /// </param>
        /// <param name="destinationArchiveFileName">
        ///     The path of the archive to be created, specified as a relative or absolute
        ///     path. A relative path is interpreted as relative to the current working directory.
        /// </param>
        /// <param name="compressionLevel">
        ///     One of the enumeration values that indicates whether to emphasize speed or compression
        ///     effectiveness when creating the entry.
        /// </param>
        /// <param name="includeBaseDirectory">
        ///     true to include the directory name from sourceDirectoryName at the root of the
        ///     archive; false to include only the contents of the directory.
        /// </param>
        /// <param name="entryNameEncoding">
        ///     The encoding to use when reading or writing entry names in this archive. Specify a
        ///     value for this parameter only when an encoding is required for interoperability with zip archive tools and
        ///     libraries that do not support UTF-8 encoding for entry names.
        /// </param>
        public static void CreateZipFile(this DirectoryInfo @this, string destinationArchiveFileName,
            CompressionLevel compressionLevel, bool includeBaseDirectory, Encoding entryNameEncoding)
        {
            ZipFile.CreateFromDirectory(@this.FullName, destinationArchiveFileName, compressionLevel,
                includeBaseDirectory, entryNameEncoding);
        }

        /// <summary>
        ///     Creates a zip archive that contains the files and directories from the specified
        ///     directory.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destinationArchiveFile">
        ///     The path of the archive to be created, specified as a
        ///     relative or absolute path. A relative path is interpreted as relative to the current working
        ///     directory.
        /// </param>
        public static void CreateZipFile(this DirectoryInfo @this, FileInfo destinationArchiveFile)
        {
            ZipFile.CreateFromDirectory(@this.FullName, destinationArchiveFile.FullName);
        }

        /// <summary>
        ///     Creates a zip archive that contains the files and directories from the specified
        ///     directory, uses the specified compression level, and optionally includes the base directory.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destinationArchiveFile">
        ///     The path of the archive to be created, specified as a
        ///     relative or absolute path. A relative path is interpreted as relative to the current working
        ///     directory.
        /// </param>
        /// <param name="compressionLevel">
        ///     One of the enumeration values that indicates whether to
        ///     emphasize speed or compression effectiveness when creating the entry.
        /// </param>
        /// <param name="includeBaseDirectory">
        ///     true to include the directory name from
        ///     sourceDirectoryName at the root of the archive; false to include only the contents of the
        ///     directory.
        /// </param>
        public static void CreateZipFile(this DirectoryInfo @this, FileInfo destinationArchiveFile,
            CompressionLevel compressionLevel, bool includeBaseDirectory)
        {
            ZipFile.CreateFromDirectory(@this.FullName, destinationArchiveFile.FullName, compressionLevel,
                includeBaseDirectory);
        }

        /// <summary>
        ///     Creates a zip archive that contains the files and directories from the specified
        ///     directory, uses the specified compression level and character encoding for entry names, and
        ///     optionally includes the base directory.
        /// </summary>
        /// <param name="this">
        ///     The path to the directory to be archived, specified as a relative or
        ///     absolute path. A relative path is interpreted as relative to the current working directory.
        /// </param>
        /// <param name="destinationArchiveFile">
        ///     The path of the archive to be created, specified as a
        ///     relative or absolute path. A relative path is interpreted as relative to the current working
        ///     directory.
        /// </param>
        /// <param name="compressionLevel">
        ///     One of the enumeration values that indicates whether to
        ///     emphasize speed or compression effectiveness when creating the entry.
        /// </param>
        /// <param name="includeBaseDirectory">
        ///     true to include the directory name from
        ///     sourceDirectoryName at the root of the archive; false to include only the contents of the
        ///     directory.
        /// </param>
        /// <param name="entryNameEncoding">
        ///     The encoding to use when reading or writing entry names in
        ///     this archive. Specify a value for this parameter only when an encoding is required for
        ///     interoperability with zip archive tools and libraries that do not support UTF-8 encoding for
        ///     entry names.
        /// </param>
        public static void CreateZipFile(this DirectoryInfo @this, FileInfo destinationArchiveFile,
            CompressionLevel compressionLevel, bool includeBaseDirectory, Encoding entryNameEncoding)
        {
            ZipFile.CreateFromDirectory(@this.FullName, destinationArchiveFile.FullName, compressionLevel,
                includeBaseDirectory, entryNameEncoding);
        }

        /// <summary>
        ///     Extracts all the files in the specified zip archive to a directory on the file system
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destinationDirectoryName">
        ///     The path to the directory in which to place the
        ///     extracted files, specified as a relative or absolute path. A relative path is interpreted as
        ///     relative to the current working directory.
        /// </param>
        public static void ExtractZipFileToDirectory(this FileInfo @this, string destinationDirectoryName)
        {
            ZipFile.ExtractToDirectory(@this.FullName, destinationDirectoryName);
        }

        /// <summary>
        ///     Extracts all the files in the specified zip archive to a directory on the file system and uses the specified
        ///     character encoding for entry names.
        /// </summary>
        /// <param name="this">The path to the archive that is to be extracted.</param>
        /// <param name="destinationDirectoryName">
        ///     The path to the directory in which to place the extracted files, specified as a
        ///     relative or absolute path. A relative path is interpreted as relative to the current working directory.
        /// </param>
        /// <param name="entryNameEncoding">
        ///     The encoding to use when reading or writing entry names in this archive. Specify a
        ///     value for this parameter only when an encoding is required for interoperability with zip archive tools and
        ///     libraries that do not support UTF-8 encoding for entry names.
        /// </param>
        public static void ExtractZipFileToDirectory(this FileInfo @this, string destinationDirectoryName,
            Encoding entryNameEncoding)
        {
            ZipFile.ExtractToDirectory(@this.FullName, destinationDirectoryName, entryNameEncoding);
        }

        /// <summary>Extracts all the files in the specified zip archive to a directory on the file system.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destinationDirectory">Pathname of the destination directory.</param>
        public static void ExtractZipFileToDirectory(this FileInfo @this, DirectoryInfo destinationDirectory)
        {
            ZipFile.ExtractToDirectory(@this.FullName, destinationDirectory.FullName);
        }

        /// <summary>
        ///     Extracts all the files in the specified zip archive to a directory on the file system
        ///     and uses the specified character encoding for entry names.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destinationDirectory">Pathname of the destination directory.</param>
        /// <param name="entryNameEncoding">
        ///     The encoding to use when reading or writing entry names in
        ///     this archive. Specify a value for this parameter only when an encoding is required for
        ///     interoperability with zip archive tools and libraries that do not support UTF-8 encoding for
        ///     entry names.
        /// </param>
        public static void ExtractZipFileToDirectory(this FileInfo @this, DirectoryInfo destinationDirectory,
            Encoding entryNameEncoding)
        {
            ZipFile.ExtractToDirectory(@this.FullName, destinationDirectory.FullName, entryNameEncoding);
        }

        /// <summary>
        ///     The path to the archive to open, specified as a relative or absolute path. A relative path is interpreted as
        ///     relative to the current working directory.
        /// </summary>
        /// <param name="this">
        ///     The path to the archive to open, specified as a relative or absolute path. A relative path is
        ///     interpreted as relative to the current working directory.
        /// </param>
        /// <returns>The opened zip archive.</returns>
        public static ZipArchive OpenReadZipFile(this FileInfo @this)
        {
            return ZipFile.OpenRead(@this.FullName);
        }

        /// <summary>Opens a zip archive at the specified path and in the specified mode.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="mode">
        ///     One of the enumeration values that specifies the actions that are allowed
        ///     on the entries in the opened archive.
        /// </param>
        /// <returns>A ZipArchive.</returns>
        public static ZipArchive OpenZipFile(this FileInfo @this, ZipArchiveMode mode)
        {
            return ZipFile.Open(@this.FullName, mode);
        }

        /// <summary>Opens a zip archive at the specified path and in the specified mode.</summary>
        /// <param name="this">
        ///     The path to the archive to open, specified as a relative or absolute
        ///     path. A relative path is interpreted as relative to the current working directory.
        /// </param>
        /// <param name="mode">
        ///     One of the enumeration values that specifies the actions that are allowed
        ///     on the entries in the opened archive.
        /// </param>
        /// <param name="entryNameEncoding">
        ///     The encoding to use when reading or writing entry names in
        ///     this archive. Specify a value for this parameter only when an encoding is required for
        ///     interoperability with zip archive tools and libraries that do not support UTF-8 encoding for
        ///     entry names.
        /// </param>
        /// <returns>A ZipArchive.</returns>
        public static ZipArchive OpenZipFile(this FileInfo @this, ZipArchiveMode mode, Encoding entryNameEncoding)
        {
            return ZipFile.Open(@this.FullName, mode, entryNameEncoding);
        }
    }
}