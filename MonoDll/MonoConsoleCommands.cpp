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
	auto currentDomain = mono_domain_get();
	// Always execute this in the root appdomain
	mono_domain_set(mono_domain_get_by_id(0), false);

	bool reloadSuccess = gEnv->pMonoScriptSystem->GetAppDomainManager()->CallMethod("Reload")->Unbox<bool>();
	
	// If the reload was succesful, the appdomain will be set to the new appdomain in CryBrary
	// If not, we need to reset it to our previous value here again
	if (!reloadSuccess)
	{
		mono_domain_set(currentDomain,false);
	}
}