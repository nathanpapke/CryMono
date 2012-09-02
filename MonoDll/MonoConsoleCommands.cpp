#include "stdafx.h"
#include "MonoConsoleCommands.h"
#include "MonoScriptSystem.h"

CMonoConsoleCommands::CMonoConsoleCommands()
{
	IConsole *pConsole = gEnv->pSystem->GetIConsole();
	if (pConsole)
	{
		pConsole->AddCommand("mono_reload", MonoReload, VF_CHEAT, "Reloads all scripts");
	}
}

CMonoConsoleCommands::~CMonoConsoleCommands()
{
	IConsole *pConsole = gEnv->pSystem->GetIConsole();
	if (pConsole)
	{
		pConsole->RemoveCommand("mono_reload");
	}
}

void CMonoConsoleCommands::MonoReload(IConsoleCmdArgs* pCmdArgs)
{
	//gEnv->pMonoScriptSystem->ReloadScriptManager();
	gEnv->pMonoScriptSystem->GetAppDomainManager()->CallMethod("Reload");

}