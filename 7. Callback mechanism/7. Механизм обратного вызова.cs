using System;
namespace Delegate
{
    // Объявляем делегат
    public delegate void AccountStateHandler(string message);

    class Account
    {
        int _sum; // Переменная для хранения суммы
        int _percentage; // Переменная для хранения процента

        public Account(int sum, int percentage)
        {
            _sum = sum;
            _percentage = percentage;
        }

        public int CurrentSum
        {
            get { return _sum; }
        }

        public void Put(int sum)
        {
            _sum += sum;
        }

        public void Withdraw(int sum)
        {
            if (sum <= _sum)
            {
                _sum -= sum;

                del?.Invoke("Сумма " + sum.ToString() + " снята со счета");
            }
            else
            {
                del?.Invoke("Недостаточно денег на счете");
            }
        }

        public int Percentage
        {
            get { return _percentage; }
        }

        // Создаем переменную делегата
        AccountStateHandler del;

        /*
         * Когда компилятор C# обрабатывает тип делегата, 
         * он автоматически генерирует запечатанный (sealed) класс, 
         * унаследованный от System.MulticastDelegate. 
         * Этот класс (в сочетании с его базовым классом System.Delegate) 
         * предоставляет необходимую инфраструктуру для делегата, 
         * чтобы хранить список методов, подлежащих вызову.
         */
        // Регистрируем делегат
        public void RegisterHandler(AccountStateHandler _del)
        {
            // метод Combine объединяет делегаты _del и del в один, 
            // который потом присваивается переменной del
            System.Delegate mainDel = System.Delegate.Combine(del, _del);
            del = mainDel as AccountStateHandler;

            // сокращенная форма добавления 
            // del += _del; // добавляем делегат
        }

        // Отмена регистрации делегата
        public void UnregisterHandler(AccountStateHandler _del)
        {
            // метод Remove возвращает делегат, из списка вызовов которого удален делегат _del
            System.Delegate mainDel = System.Delegate.Remove(del, _del);
            del = mainDel as AccountStateHandler;

            // сокращенная форма удаления 
            // del -= _del; // добавляем делегат
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account(200, 6);
     
            account.RegisterHandler(Show_Message);
            account.RegisterHandler(Color_Message);
  
            account.Withdraw(100);
            account.Withdraw(150);

            account.UnregisterHandler(Color_Message);
 
            account.Withdraw(50);
        }
    
        private static void Show_Message(string message)
        {
            Console.WriteLine(message);
        }
        private static void Color_Message(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
