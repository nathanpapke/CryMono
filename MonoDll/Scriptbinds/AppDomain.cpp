#include "StdAfx.h"
#include "AppDomain.h"

#include "MonoScriptSystem.h"

CAppDomain::CAppDomain()
{
	REGISTER_METHOD(SetScriptAppDomain);
	REGISTER_METHOD(Initialize);
}

void CAppDomain::SetScriptAppDomain(int appDomainId)
{
	MonoDomain* currentDomain = mono_domain_get();
	MonoDomain* pDomain = mono_domain_get_by_id(appDomainId);
	mono_domain_set(pDomain, true);
	currentDomain = mono_domain_get();
}

void CAppDomain::Initialize(mono::object scriptManager)
{
	static_cast<CScriptSystem *>(gEnv->pMonoScriptSystem)->SetScriptManager(*scriptManager);
}