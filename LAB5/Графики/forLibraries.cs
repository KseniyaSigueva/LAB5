using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ЛР2._2
{
    public class forLibraries
    {
        /*РЕЖИМЫ ОШИБОК (взято с официальной документации Microsoft)*/
        [Flags]
        public enum ErrorModes : uint
        {
            //
            SYSTEM_DEFAULT = 0x0,
            //Выключить отображени окна сообщения о критической ошибке
            SEM_FAILCRITICALERRORS = 0x0001,
            //Автоматическое исправление ошибок выравнивания памяти
            SEM_NOALIGNMENTFAULTEXCEPT = 0x0004,
            //Выключить отображение диалогового окна Windows Error Reporting
            SEM_NOGPFAULTERRORBOX = 0x0002,
            //OpenFile не отобразит сообщение об ошибке в случае невозможности загрузки файла
            SEM_NOOPENFILEERRORBOX = 0x8000
        }

        /*МЕТОДЫ ДЛЯ ЗАГРУЗКИ БИБЛИОТЕКИ И ИЗВЛЕЧЕНИЯ ИЗ НЕЕ ФУНКЦИЙ*/
        //Загружает библиотеку в адресное пространство
        [DllImport("kernel32.dll")] public static extern IntPtr LoadLibrary(string path);
        //Извлекаем адрес функции из загруженной раннее библиотеки
        [DllImport("kernel32.dll")] public static extern IntPtr GetProcAddress(IntPtr hLib, string name);
        //Метод для обработки ошибок, который выдает (или не выдает) исключения в соответствии с теми режимами, которые указаны в ErrorModes
        [DllImport("kernel32.dll")] public static extern ErrorModes SetErrorMode(ErrorModes modes);

        //Устанавливаем соглашения о вызове для делегатов, они будут указывать на методы функций
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate double _timeDelegate(double a, double b);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate double _amplitudeDelegate(string func);
        private readonly _timeDelegate _time;
        private readonly _amplitudeDelegate _amplitude;

        //Это методы для получения амплитуды и времени
        public double time(double a, double b) => _time(a, b);
        public double amplitude(string func) => _amplitude(func);

        //Получаем методы из библиотек
        public forLibraries(string path)
        {
            IntPtr hLib = GetLibrary(path);
            if (hLib != IntPtr.Zero)
            {
                _time = GetFunc<_timeDelegate>("time", hLib);
                _amplitude = GetFunc<_amplitudeDelegate>("amplitude", hLib);
            }
            else
                throw new Exception();
        }

        //Получаем библиотеку, опуская отображение сообщений о критических ошибках
        public IntPtr GetLibrary(string path)
        {
            var oldMode = SetErrorMode(ErrorModes.SEM_FAILCRITICALERRORS);
            try
            {
                return LoadLibrary(path);
            }
            finally
            {
                SetErrorMode(oldMode);
            }
        }

        //Если удалось извлечь библиотеку, то преобразуем указатель на неуправляемую функцию в делегат; T здесь применяется, поскольку мы не знаем наверняка, что возвращает извлекаемая функция
        public T GetFunc<T>(string funcName, IntPtr hLib) where T : class
        {
            IntPtr address = GetProcAddress(hLib, funcName);
            if (address != IntPtr.Zero)
                return Marshal.GetDelegateForFunctionPointer(address, typeof(T)) as T;
            else
                throw new Exception();
        }
    }
}
