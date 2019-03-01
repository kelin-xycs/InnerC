#include <windows.h>
#define _EXPORTDLL
#include "InnerC.h"

BOOL APIENTRY DllMain (HINSTANCE hInst     /* Library instance handle. */ ,
                       DWORD reason        /* Reason this function is being called. */ ,
                       LPVOID reserved     /* Not used. */ )
{
	switch (reason)
	{
		case DLL_PROCESS_ATTACH:
		break;

		case DLL_PROCESS_DETACH:
		break;

		case DLL_THREAD_ATTACH:
		break;

		case DLL_THREAD_DETACH:
		break;
	}

	/* Returns TRUE on success, FALSE on failure */
	return TRUE;
}

void CallFromDll(char* str)
{
	MessageBox(NULL,str,"",MB_OK);
}