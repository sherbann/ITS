using System.Text.RegularExpressions;

namespace ITS.Domain
{
    internal class StreamHelper
    {
        public static string GetPath(string fileName) // private
        {
            //returns string that represent the path to a file in the
            //executable folder of solution
            //Executable folder is " ...ITS\ITS\bin\Debug\net6.0\".
            return Directory.GetCurrentDirectory() + "\\" + fileName;
        }
        public static StreamReader GetReader(string fileName) //Private
        {
            //Create the StreamReader object, sr, using the path file.
            StreamReader sr = new StreamReader(GetPath(fileName));
            return sr;
        }
        public static StreamWriter GetWriter(string fileName, bool replaceFile)
        {
            // To implement...
            //Create StreamWriter object, sw, using path to file and replaceFile
            //argument value.If replaceFile is 'true' file is NOT replaced it it
            //exist . If replaceFile is 'false' file is replaced if it exist.
            return new StreamWriter(GetPath(fileName), replaceFile);
        }
    }
    internal static class Helper
    {
        public static bool CheckPassword(string pass1, string pass2)
        {
            return pass1 == pass2;
        }
        public static bool EmailFormed(string email)
        {
            string emailRegExp = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+" + // Note operator '+'
                @"(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]" + // Note operator '+'
                @"*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            return Regex.IsMatch(email, emailRegExp);
            //Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            //return regex.Match(email).Success;
        }
    }
}
