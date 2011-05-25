// Manual test program for exercising Topology Layer 3
//

#include <TestFramework.h>
#include <OptionParser.h>
#include <OhNetTypes.h>
#include <Discovery.h>
#include <Thread.h>
#include <Timer.h>
#include <OsWrapper.h>
#include "CpTopology3.h"

using namespace OpenHome::Net;
using namespace OpenHome::Net::TestFramework;

class TopologyLogger : public ICpTopology3Handler
{
public:
    TopologyLogger();
	virtual void RoomAdded(CpTopology3Room& aRoom);
    virtual void RoomChanged(CpTopology3Room& aRoom);
    virtual void RoomRemoved(CpTopology3Room& aRoom);
	virtual void RoomStandbyChanged(CpTopology3Room& aRoom);
	virtual void RoomSourceChanged(CpTopology3Room& aRoom);
    virtual void RoomVolumeLimitChanged(CpTopology3Room& aRoom);
	virtual void RoomVolumeChanged(CpTopology3Room& aRoom);
    virtual void RoomBalanceChanged(CpTopology3Room& aRoom);
    virtual void RoomFadeChanged(CpTopology3Room& aRoom);
	virtual void RoomMuteChanged(CpTopology3Room& aRoom);
private:
    void PrintRoomInfo(const char* aPrologue, const CpTopology3Room& aRoom);
    void PrintSourceInfo(const CpTopology3Room& aRoom);
};

TopologyLogger::TopologyLogger()
{
}

void TopologyLogger::RoomAdded(CpTopology3Room& aRoom)
{
    Print("\n");
    PrintRoomInfo("Room Added         ", aRoom);
    Print("\n");
    PrintSourceInfo(aRoom);
}

void TopologyLogger::RoomChanged(CpTopology3Room& aRoom)
{
    Print("\n");
    PrintRoomInfo("Source List Changed ", aRoom);
    Print("\n");
    PrintSourceInfo(aRoom);
}

void TopologyLogger::RoomRemoved(CpTopology3Room& aRoom)
{
    PrintRoomInfo("Room Removed        ", aRoom);
    Print("\n");
}

void TopologyLogger::RoomStandbyChanged(CpTopology3Room& aRoom)
{
    PrintRoomInfo("Standby Changed     ", aRoom);
    Print(aRoom.Standby() ? "true" : "false");
    Print("\n");
}

void TopologyLogger::RoomSourceChanged(CpTopology3Room& aRoom)
{
    PrintRoomInfo("Source Changed", aRoom);
    Print(aRoom.CurrentSourceName());
    Print("\n");
}

void TopologyLogger::RoomVolumeLimitChanged(CpTopology3Room& aRoom)
{
    PrintRoomInfo("Vol Limit Changed   ", aRoom);
    Print("%u\n", aRoom.VolumeLimit());
}

void TopologyLogger::RoomVolumeChanged(CpTopology3Room& aRoom)
{
    PrintRoomInfo("Volume Changed      ", aRoom);
    Print("%u\n", aRoom.Volume());
}

void TopologyLogger::RoomBalanceChanged(CpTopology3Room& aRoom)
{
    PrintRoomInfo("Balance Changed     ", aRoom);
    Print("%u\n", aRoom.Balance());
}

void TopologyLogger::RoomFadeChanged(CpTopology3Room& aRoom)
{
    PrintRoomInfo("Fade Changed        ", aRoom);
    Print("%u\n", aRoom.Fade());
}

void TopologyLogger::RoomMuteChanged(CpTopology3Room& aRoom)
{
    PrintRoomInfo("Mute Changed        ", aRoom);
    Print(aRoom.Mute() ? "true" : "false");
    Print("\n");
}

void TopologyLogger::PrintSourceInfo(const CpTopology3Room& aRoom)
{
	TUint count = aRoom.SourceCount();

  	Print("===============================================\n");
	
	for (TUint i = 0; i < count; i++) {
    	Print("%u. ", i);
    	Print(aRoom.SourceName(i));
    	Print(" ");
    	Print(aRoom.SourceType(i));
    	Print("\n");
	}

  	Print("===============================================\n");
   	Print("\n");
}

void TopologyLogger::PrintRoomInfo(const char* aPrologue, const CpTopology3Room& aRoom)
{
    Print("%s ", aPrologue);
    Print(aRoom.Name());
    Print(" ");
}


void OpenHome::Net::TestFramework::Runner::Main(TInt aArgc, TChar* aArgv[], InitialisationParams* aInitParams)
{
    OptionParser parser;

    OptionUint duration("-d", "--duration", 30, "Number of seconds to run the test");
    OptionUint adapter("-i", "--interface", 0, "index of network adapter to use");

    parser.AddOption(&duration);
    parser.AddOption(&adapter);

    if (!parser.Parse(aArgc, aArgv) || parser.HelpDisplayed()) {
        return;
    }

    UpnpLibrary::Initialise(aInitParams);
    UpnpLibrary::StartCp();

    std::vector<NetworkInterface*>* ifs = Os::NetworkListInterfaces(false);
    ASSERT(ifs->size() > 0 && adapter.Value() < ifs->size());
    UpnpLibrary::SetCurrentSubnet(*(*ifs)[adapter.Value()]);
    TIpAddress addr = (*ifs)[adapter.Value()]->Address();
    for (TUint i=0; i<ifs->size(); i++) {
        delete (*ifs)[i];
    }
    delete ifs;
    Endpoint endpt(0, addr);
    Endpoint::AddressBuf buf;
    endpt.AppendAddress(buf);
    Print("Using network interface %s\n\n", buf.Ptr());

    // Debug::SetLevel(Debug::kTopology);
    // Debug::SetLevel(Debug::kAll);

    TopologyLogger logger;

    CpTopology3* topology = new CpTopology3(logger);

    if (topology != NULL) {
        Blocker* blocker = new Blocker;
        blocker->Wait(duration.Value());
        delete blocker;
    }

    delete topology;

    UpnpLibrary::Close();

}
