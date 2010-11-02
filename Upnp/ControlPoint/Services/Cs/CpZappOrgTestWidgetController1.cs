using System;
using System.Runtime.InteropServices;
using System.Text;
using Zapp;

namespace Zapp
{
    public class CpProxyZappOrgTestWidgetController1 : CpProxy, IDisposable
    {
        [DllImport("CpZappOrgTestWidgetController1")]
        static extern uint CpProxyZappOrgTestWidgetController1Create(uint aDeviceHandle);
        [DllImport("CpZappOrgTestWidgetController1")]
        static extern void CpProxyZappOrgTestWidgetController1Destroy(uint aHandle);
        [DllImport("CpZappOrgTestWidgetController1")]
        static extern unsafe void CpProxyZappOrgTestWidgetController1SyncCreateWidget(uint aHandle, char* aWidgetUdn);
        [DllImport("CpZappOrgTestWidgetController1")]
        static extern unsafe void CpProxyZappOrgTestWidgetController1BeginCreateWidget(uint aHandle, char* aWidgetUdn, CallbackActionComplete aCallback, IntPtr aPtr);
        [DllImport("CpZappOrgTestWidgetController1")]
        static extern unsafe int CpProxyZappOrgTestWidgetController1EndCreateWidget(uint aHandle, uint aAsync);
        [DllImport("CpZappOrgTestWidgetController1")]
        static extern unsafe void CpProxyZappOrgTestWidgetController1SyncRemoveWidget(uint aHandle, char* aWidgetUdn);
        [DllImport("CpZappOrgTestWidgetController1")]
        static extern unsafe void CpProxyZappOrgTestWidgetController1BeginRemoveWidget(uint aHandle, char* aWidgetUdn, CallbackActionComplete aCallback, IntPtr aPtr);
        [DllImport("CpZappOrgTestWidgetController1")]
        static extern unsafe int CpProxyZappOrgTestWidgetController1EndRemoveWidget(uint aHandle, uint aAsync);
        [DllImport("CpZappOrgTestWidgetController1")]
        static extern unsafe void CpProxyZappOrgTestWidgetController1SyncSetWidgetRegister(uint aHandle, char* aWidgetUdn, uint aRegisterIndex, uint aRegisterValue);
        [DllImport("CpZappOrgTestWidgetController1")]
        static extern unsafe void CpProxyZappOrgTestWidgetController1BeginSetWidgetRegister(uint aHandle, char* aWidgetUdn, uint aRegisterIndex, uint aRegisterValue, CallbackActionComplete aCallback, IntPtr aPtr);
        [DllImport("CpZappOrgTestWidgetController1")]
        static extern unsafe int CpProxyZappOrgTestWidgetController1EndSetWidgetRegister(uint aHandle, uint aAsync);
        [DllImport("ZappUpnp")]
        static extern unsafe void ZappFree(void* aPtr);

        private GCHandle iGch;

        public CpProxyZappOrgTestWidgetController1(CpDevice aDevice)
        {
            iHandle = CpProxyZappOrgTestWidgetController1Create(aDevice.Handle());
            iGch = GCHandle.Alloc(this);
        }

        public unsafe void SyncCreateWidget(string aWidgetUdn)
        {
			char* widgetUdn = (char*)Marshal.StringToHGlobalAnsi(aWidgetUdn);
			{
				CpProxyZappOrgTestWidgetController1SyncCreateWidget(iHandle, widgetUdn);
			}
			Marshal.FreeHGlobal((IntPtr)widgetUdn);
        }

        public unsafe void BeginCreateWidget(string aWidgetUdn, CallbackAsyncComplete aCallback)
        {
			char* widgetUdn = (char*)Marshal.StringToHGlobalAnsi(aWidgetUdn);
            GCHandle gch = GCHandle.Alloc(aCallback);
            IntPtr ptr = GCHandle.ToIntPtr(gch);
            CpProxyZappOrgTestWidgetController1BeginCreateWidget(iHandle, widgetUdn, iActionComplete, ptr);
			Marshal.FreeHGlobal((IntPtr)widgetUdn);
        }

        public unsafe void EndCreateWidget(uint aAsyncHandle)
        {
			{
				if (0 != CpProxyZappOrgTestWidgetController1EndCreateWidget(iHandle, aAsyncHandle))
				{
					throw(new ProxyError());
				}
			}
        }

        public unsafe void SyncRemoveWidget(string aWidgetUdn)
        {
			char* widgetUdn = (char*)Marshal.StringToHGlobalAnsi(aWidgetUdn);
			{
				CpProxyZappOrgTestWidgetController1SyncRemoveWidget(iHandle, widgetUdn);
			}
			Marshal.FreeHGlobal((IntPtr)widgetUdn);
        }

        public unsafe void BeginRemoveWidget(string aWidgetUdn, CallbackAsyncComplete aCallback)
        {
			char* widgetUdn = (char*)Marshal.StringToHGlobalAnsi(aWidgetUdn);
            GCHandle gch = GCHandle.Alloc(aCallback);
            IntPtr ptr = GCHandle.ToIntPtr(gch);
            CpProxyZappOrgTestWidgetController1BeginRemoveWidget(iHandle, widgetUdn, iActionComplete, ptr);
			Marshal.FreeHGlobal((IntPtr)widgetUdn);
        }

        public unsafe void EndRemoveWidget(uint aAsyncHandle)
        {
			{
				if (0 != CpProxyZappOrgTestWidgetController1EndRemoveWidget(iHandle, aAsyncHandle))
				{
					throw(new ProxyError());
				}
			}
        }

        public unsafe void SyncSetWidgetRegister(string aWidgetUdn, uint aRegisterIndex, uint aRegisterValue)
        {
			char* widgetUdn = (char*)Marshal.StringToHGlobalAnsi(aWidgetUdn);
			{
				CpProxyZappOrgTestWidgetController1SyncSetWidgetRegister(iHandle, widgetUdn, aRegisterIndex, aRegisterValue);
			}
			Marshal.FreeHGlobal((IntPtr)widgetUdn);
        }

        public unsafe void BeginSetWidgetRegister(string aWidgetUdn, uint aRegisterIndex, uint aRegisterValue, CallbackAsyncComplete aCallback)
        {
			char* widgetUdn = (char*)Marshal.StringToHGlobalAnsi(aWidgetUdn);
            GCHandle gch = GCHandle.Alloc(aCallback);
            IntPtr ptr = GCHandle.ToIntPtr(gch);
            CpProxyZappOrgTestWidgetController1BeginSetWidgetRegister(iHandle, widgetUdn, aRegisterIndex, aRegisterValue, iActionComplete, ptr);
			Marshal.FreeHGlobal((IntPtr)widgetUdn);
        }

        public unsafe void EndSetWidgetRegister(uint aAsyncHandle)
        {
			{
				if (0 != CpProxyZappOrgTestWidgetController1EndSetWidgetRegister(iHandle, aAsyncHandle))
				{
					throw(new ProxyError());
				}
			}
        }

        public void Dispose()
        {
            DoDispose(true);
        }

        ~CpProxyZappOrgTestWidgetController1()
        {
            DoDispose(false);
        }

        private void DoDispose(bool aDisposing)
        {
            uint handle;
            lock (this)
            {
                if (iHandle == 0)
                {
                    return;
                }
                handle = iHandle;
                iHandle = 0;
            }
            CpProxyZappOrgTestWidgetController1Destroy(handle);
            iGch.Free();
            if (aDisposing)
            {
                GC.SuppressFinalize(this);
            }
            else
            {
                DisposeProxy();
            }
        }
    }
}
