///https://docs.microsoft.com/en-us/dotnet/framework/windows-services/walkthrough-creating-a-windows-service-application-in-the-component-designer

using System.Runtime.InteropServices;  

public Class MyNewService: public System.ServiceProcess.ServiceBase
{
private System.ComponentModel.IContainer components;
private System.Diagnostics.EventLog eventLog1;

public MyNewService()
{
    public enum ServiceState  
  {  
      SERVICE_STOPPED = 0x00000001,  
      SERVICE_START_PENDING = 0x00000002,  
      SERVICE_STOP_PENDING = 0x00000003,  
      SERVICE_RUNNING = 0x00000004,  
      SERVICE_CONTINUE_PENDING = 0x00000005,  
      SERVICE_PAUSE_PENDING = 0x00000006,  
      SERVICE_PAUSED = 0x00000007,  
  };
 
  [StructLayout(LayoutKind.Sequential)]  
  public struct ServiceStatus  
  {  
      public int dwServiceType;  
      public ServiceState dwCurrentState;  
      public int dwControlsAccepted;  
      public int dwWin32ExitCode;  
      public int dwServiceSpecificExitCode;  
      public int dwCheckPoint;  
      public int dwWaitHint;  
  };   
	InitializeComponent();
	eventLog1 = new System.Diagnostics.EventLog();
	if (!System.Diagnostics.EventLog.SourceExists("MySource")) 
	{         
			System.Diagnostics.EventLog.CreateEventSource(
				"MySource","MyNewLog");
	}
	eventLog1.Source = "MySource";
	eventLog1.Log = "MyNewLog";
}
protected override void OnStart(string[] args)
{
	eventLog1.WriteEntry("In OnStart");
    // Update the service state to Start Pending.  
    ServiceStatus serviceStatus = new ServiceStatus();  
    serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;  
    serviceStatus.dwWaitHint = 100000;  
    SetServiceStatus(this.ServiceHandle, ref serviceStatus);  

    // Set up a timer to trigger every minute.  
    System.Timers.Timer timer = new System.Timers.Timer();  
    timer.Interval = 60000; // 60 seconds  
    timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);  
    timer.Start();  

    // Update the service state to Running.  
    serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;  
    SetServiceStatus(this.ServiceHandle, ref serviceStatus);  
}
private int eventId = 1;
public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)  
{  
    // TODO: Insert monitoring activities here.  
    eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);  
} 
protected override void OnStop()
{
	eventLog1.WriteEntry("In onStop.");
}
protected override void OnContinue()
{
	eventLog1.WriteEntry("In OnContinue.");
}

[DllImport("advapi32.dll", SetLastError=true)]  
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

};

