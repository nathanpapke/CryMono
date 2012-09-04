/////////////////////////////////////////////////////////////////////////*
//Ink Studios Source File.
//Copyright (C), Ink Studios, 2011.
//////////////////////////////////////////////////////////////////////////
// AppDomain scriptbind to manage appdomains
//////////////////////////////////////////////////////////////////////////
// 14/07/2012 : Not created by Filip 'i59' Lundgren
////////////////////////////////////////////////////////////////////////*/
#ifndef __SCRIPTBIND_APPDOMAIN_H__
#define __SCRIPTBIND_APPDOMAIN_H__

#include <IMonoScriptBind.h>
#include "MonoCommon.h"

class CAppDomain : public IMonoScriptBind
{
public:
	CAppDomain();
	~CAppDomain() {}
protected:
	virtual const char *GetClassName() { return "NativeAppDomainMethods"; }

	static void SetScriptAppDomain(int appDomainId);
	static void Initialize(mono::object scriptManager);
};

#endif