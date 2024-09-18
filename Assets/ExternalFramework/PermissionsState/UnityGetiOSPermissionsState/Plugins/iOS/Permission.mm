#import "Permission.h"
#import <AVFoundation/AVFoundation.h>
#import <AVFoundation/AVCaptureDevice.h>
#import <AssetsLibrary/AssetsLibrary.h>
#import <Photos/Photos.h>
#import <CoreTelephony/CTCellularData.h>

@implementation Permission

//首次调用获取联网权限为系统启动即获取，这里不需要写了

//获取联网权限准许状态
extern "C" void GetNetworkingPermissionState()
{
    CTCellularData *cellularData = [[CTCellularData alloc]init];
    CTCellularDataRestrictedState status = cellularData.restrictedState;
    if (status == kCTCellularDataRestricted){
        NSLog(@"%@",@"联网准许");
    }else{
        NSLog(@"%@",@"联网不准许");
    }
}

//首次调用获取麦克风权限
extern "C" void GetMicrophonePermission()
{
     [AVCaptureDevice requestAccessForMediaType:AVMediaTypeAudio completionHandler:^(bool granted) {
           if (granted) {
               NSLog(@"%@",@"麦克风准许");
          }else{
               NSLog(@"%@",@"麦克风不准许");
          }
     }];
}

//获取麦克风权限准许状态
extern "C" bool GetMicrophonePermissionState()
{
    GetMicrophonePermission();
    AVAuthorizationStatus status = [AVCaptureDevice authorizationStatusForMediaType:AVMediaTypeAudio];

    if (status == AVAuthorizationStatusAuthorized){
        NSLog(@"%@",@"麦克风准许");
    }else{
        NSLog(@"%@",@"麦克风不准许");
    }
    return (status == AVAuthorizationStatusAuthorized);
}

//首次调用获取相机权限
extern "C" void GetCameraPermission()
{
    [AVCaptureDevice requestAccessForMediaType:AVMediaTypeVideo completionHandler:^(BOOL granted) {
        if (granted) {
            NSLog(@"%@",@"相机准许");
        }else{
            NSLog(@"%@",@"相机不准许");
        }
    }];
}

//获取相机权限准许状态
extern "C" bool GetCameraPermissionState()
{
    GetCameraPermission();
     AVAuthorizationStatus status = [AVCaptureDevice authorizationStatusForMediaType:AVMediaTypeVideo];
        if (status == AVAuthorizationStatusAuthorized){
            NSLog(@"%@",@"相机准许");
        }else{
            NSLog(@"%@",@"相机不准许");
        }
    return (status == AVAuthorizationStatusAuthorized);
}

//首次调用获取相册权限
extern "C" void GetPhotoPermission()
{
    [PHPhotoLibrary requestAuthorization:^(PHAuthorizationStatus status) {
        if (status == PHAuthorizationStatusAuthorized) {
            NSLog(@"%@",@"相册准许");
        }else{
            NSLog(@"%@",@"相册不准许");
        }
        }];
}

//获取相册权限准许状态
extern "C" bool GetPhotoPermissionState()
{
    GetPhotoPermission();
    PHAuthorizationStatus status = [PHPhotoLibrary authorizationStatus];
        if (status == PHAuthorizationStatusAuthorized) {
            NSLog(@"%@",@"相册准许");
        }else{
            NSLog(@"%@",@"相册不准许");
        }
        return (status == PHAuthorizationStatusAuthorized);
}

//打开本程序的设置
extern "C" void OpenAppSettings()
{
    NSURL*url=[NSURL URLWithString:UIApplicationOpenSettingsURLString];
    if([[UIApplication sharedApplication]canOpenURL:url]){
        if ([[[UIDevice currentDevice] systemVersion] floatValue] >= 10.0) {
        //设备系统为IOS 10.0或者以上的
        [[UIApplication sharedApplication] openURL:url options:@{} completionHandler:nil];
        }else{
            //设备系统为IOS 10.0以下的
                [[UIApplication sharedApplication]openURL:url];
        }
    }
}
@end
