using System;
using System.IO;
using System.Text;
using System.Threading;

class Demo
{
    FileStream f;
    byte[] buf;
    AsyncCallback callback;

    public void UserInput()
    {
        string s;
        do
        {
            Console.WriteLine("Введите строку.");
            s = Console.ReadLine();
        } while (s.Length != 0);
    }

    public void OnCompletedRead(IAsyncResult ar) // содержит сведения о завершении операции
    {
        int bytes = f.EndRead(ar);
        Thread.Sleep(3000);       
        string str = Encoding.UTF8.GetString(buf);
        Console.WriteLine(str);
        Console.WriteLine("Считано " + bytes);
        f.Close();
    }

    public void AsyncRead()
    {
        FileInfo[] fi;
        try
        {
            DirectoryInfo di = new DirectoryInfo("../../../");
            fi = di.GetFiles("*.cs");
            long length = fi[0].Length;
            buf = new byte[length];

            f = new FileStream(fi[0].FullName, FileMode.Open, FileAccess.Read, FileShare.Read, buf.Length, true); 
            // файл открывается в асинхронном режиме
            callback = new AsyncCallback(OnCompletedRead); // экземпляр стандартного делегата AsyncCallback
            f.BeginRead(buf, 0, buf.Length, callback, null);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }
}

class MainClass
{
    public static void Main()
    {
        Demo d = new Demo();
        d.AsyncRead();
        d.UserInput();
    }
}

