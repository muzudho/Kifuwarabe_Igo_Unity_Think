//# include <windows.h>	// rand() 等を使用するために。
//# include <tchar.h>		// Unicode対応の _T() 関数を使用するために。
using n090_core____.Core;


namespace n090_core____
{
    public class CoreImpl : Core
    {
        // printf()の代用関数。
        public void PRT(const _TCHAR* format, ...)
        {
            va_list argList;
            int len;
            static _TCHAR text[PRT_LEN_MAX];
            DWORD nw;

            if (this->hConsoleWindow == INVALID_HANDLE_VALUE) {
                return;
            }
            va_start(argList, format);
            len = _vsnwprintf(text, PRT_LEN_MAX - 1, format, argList);
            va_end(argList);

            if (len< 0 || len >= PRT_LEN_MAX) {
                return;
            }
            WriteConsole(this->hConsoleWindow, text, (DWORD)wcslen(text), &nw, NULL);
        }

        public void YieldWindowsSystem(void)
        {
            MSG msg;

            if (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
            {
                TranslateMessage(&msg);
                DispatchMessage(&msg);
            }
        }
    }
}