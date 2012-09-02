#ifndef __MONOCONSOLECOMMANDS_H__
#define __MONOCONSOLECOMMANDS_H__


class CMonoConsoleCommands
{
public:
	CMonoConsoleCommands();
	~CMonoConsoleCommands();

	static void MonoReload(IConsoleCmdArgs* pCmdArgs);
};

#endif // __MONOCONSOLECOMMANDS_H__