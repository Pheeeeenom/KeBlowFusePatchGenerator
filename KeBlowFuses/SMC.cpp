#include "StdAfx.h"
#include "SMC.h"

BYTE _MessageBuffer[0x10];
BYTE _ResponseBuffer[0x10];

void SMC::PrepareBuffers()
{
	ZeroMemory(_MessageBuffer, sizeof(_MessageBuffer));
	ZeroMemory(_ResponseBuffer, sizeof(_ResponseBuffer));
}

void SMC::PowerCycle()
{
	PrepareBuffers();
	_MessageBuffer[0x00] = 0x82;
	_MessageBuffer[0x01] = 0x04;
	_MessageBuffer[0x02] = 0x30;
	_MessageBuffer[0x03] = 0x00;

	HalSendSMCMessage(_MessageBuffer, NULL);
}

void SMC::OpenTray()
{
	PrepareBuffers();
	_MessageBuffer[0x00] = 0x8B;
	_MessageBuffer[0x01] = 0x60;

	HalSendSMCMessage(_MessageBuffer, NULL);
}
