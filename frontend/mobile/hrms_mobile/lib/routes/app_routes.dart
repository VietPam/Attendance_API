import 'package:flutter/material.dart';
import 'package:hrms_mobile/presentations/recorder/first_page_screen/first_page_screen.dart';

class AppRoutes {
  static const String qrPageScreen = '/qr_page_screen';

  static const String loginScreen = '/login_screen';

  static const String successPageScreen = '/success_page_screen';

  static const String firstPageScreen = '/first_page_screen';

  static const String frontCameraScreen = '/front_camera_screen';

  static const String successPageOneScreen = '/success_page_one_screen';

  static const String frontCameraLoadingScreen = '/front_camera_loading_screen';

  static const String appNavigationScreen = '/app_navigation_screen';

  static Map<String, WidgetBuilder> routes = {
    //loginScreen: (context) => LoginScreen(),
    // qrPageScreen: (context) => QrPageScreen(),
    // successPageScreen: (context) => SuccessPageScreen(),
     firstPageScreen: (context) => FirstPageScreen(),
    // successPageOneScreen: (context) => SuccessPageOneScreen(),
    // frontCameraLoadingScreen: (context) => FrontCameraLoadingScreen(),
    // appNavigationScreen: (context) => AppNavigationScreen()
  };
}
