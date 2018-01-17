using System.Reflection;
using System.IO;


namespace CommonLibrary.Tests.Common
{
    public class ContentLoader
    {
        public const string TestDllName = "ComLib.Tests";


        /// <summary>
        /// e.g. Csv\Csv1.csv.
        /// Where Csv is the folder under the Content folder.
        /// </summary>
        /// <param name="fullFilePath">\Csv\Csv1.csv</param>
        /// <returns></returns>
        public static string GetTextFileContent(string fullFilePath)
        {
            var path = string.Format("CommonLibrary.Tests.Content.{0}", fullFilePath);
            var current = Assembly.GetExecutingAssembly();
            var stream = current.GetManifestResourceStream(path);
            if (stream == null) throw new FileNotFoundException(path);

            var reader = new StreamReader(stream);
            var content = reader.ReadToEnd();
            return content;
        }
    }
}
