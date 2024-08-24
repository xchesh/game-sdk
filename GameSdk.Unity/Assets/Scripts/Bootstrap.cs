using Cysharp.Threading.Tasks;
using GameSdk.Sources.Core.Loggers;
using GameSdk.Services.Authentication;
using GameSdk.Services.GraphicQuality;
using GameSdk.Services.InApp;
using GameSdk.Services.InternetReachability;
using GameSdk.Services.RemoteConfig;
using Project.Common.UnityContainer;
using UnityEngine;

[JetBrains.Annotations.UsedImplicitly]
public class Bootstrap : IBootstrap
{
    private readonly ISystemLogger _systemLogger;
    private readonly IAuthenticationService _authenticationService;
    private readonly IInAppService _inAppService;
    private readonly IInternetReachabilityService _internetReachabilityService;
    private readonly IGraphicQualityService _graphicQualityService;
    private readonly IRemoteConfigService _remoteConfigService;

    public Bootstrap(
        ISystemLogger systemLogger,
        IAuthenticationService authenticationService,
        IInAppService inAppService,
        IInternetReachabilityService internetReachabilityService,
        IGraphicQualityService graphicQualityService,
        IRemoteConfigService remoteConfigService)
    {
        _systemLogger = systemLogger;
        _authenticationService = authenticationService;
        _inAppService = inAppService;
        _internetReachabilityService = internetReachabilityService;
        _graphicQualityService = graphicQualityService;
        _remoteConfigService = remoteConfigService;
    }

    public void Boot()
    {
        Initialize().Forget();
    }

    private async UniTaskVoid Initialize()
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        _systemLogger.Log(LogType.Log, "Bootstrap", "Start initialization...");

        _internetReachabilityService.Initialize();

        await _authenticationService.Initialize();
        await _remoteConfigService.Initialize();

        _inAppService.Initialize();
        _graphicQualityService.Initialize();

        sw.Stop();
        _systemLogger.Log(LogType.Log, "Bootstrap", $"Initialization completed in {sw.ElapsedMilliseconds} ms");
    }
}
