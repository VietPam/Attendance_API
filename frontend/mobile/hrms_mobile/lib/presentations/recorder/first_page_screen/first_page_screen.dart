import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:hrms_mobile/presentations/recorder/front_camera_error_dialog/front_camera_error_dialog.dart';
import 'package:hrms_mobile/presentations/recorder/front_camera_success_dialog/front_camera_success_dialog.dart';
import 'package:hrms_mobile/theme/theme_helper.dart';
import 'package:hrms_mobile/utils/image_constant.dart';
import 'package:hrms_mobile/utils/size_utils.dart';
import 'package:hrms_mobile/widgets/custom_image_view.dart';
import 'package:hrms_mobile/widgets/custom_outlined_button.dart';
import 'package:http/http.dart' as http;
import 'package:flutter_easyloading/flutter_easyloading.dart';
import 'package:intl/intl.dart';
import 'package:barcode_scan2/barcode_scan2.dart';

class FirstPageScreen extends StatefulWidget {
  const FirstPageScreen({Key? key})
      : super(
          key: key,
        );

  @override
  State<FirstPageScreen> createState() => _FirstPageScreenState();
}

class _FirstPageScreenState extends State<FirstPageScreen> {
  ScanResult? scanResult;

  final _flashOnController = TextEditingController(text: 'Flash on');
  final _flashOffController = TextEditingController(text: 'Flash off');
  final _cancelController = TextEditingController(text: 'Cancel');

  var _aspectTolerance = 0.00;
  var _numberOfCameras = 0;
  var _selectedCamera = 1;
  var _useAutoFocus = true;
  var _autoEnableFlash = false;
  static final _possibleFormats = BarcodeFormat.values.toList()
    ..removeWhere((e) => e == BarcodeFormat.unknown);

  List<BarcodeFormat> selectedFormats = [..._possibleFormats];
  @override
  Widget build(BuildContext context) {
    mediaQueryData = MediaQuery.of(context);

    return SafeArea(
      child: Scaffold(
        body: SingleChildScrollView(
          child: Container(
            // width: double.maxFinite,
            padding: EdgeInsets.only(
              left: 20.h,
              top: 83.v,
              right: 20.h,
            ),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Align(
                  alignment: Alignment.center,
                  child: Text(
                    "Attendance Recorder",
                    textAlign: TextAlign.center,
                    style: theme.textTheme.headlineLarge,
                  ),
                ),
                SizedBox(
                  height: 10.h,
                ),
                CustomImageView(
                  imagePath: ImageConstant.imgLogoNobg1,
                  height: 223.v,
                  width: 243.h,
                  alignment: Alignment.bottomCenter,
                ),
                SizedBox(height: 35.v),
                Text(
                  "Day: ${DateTime.now().day} - ${DateTime.now().month} - ${DateTime.now().year}",
                  style: theme.textTheme.headlineSmall,
                ),
                SizedBox(height: 17.v),
                Text(
                  "${DateFormat("HH:mm").format(DateTime.now())}",
                  style: theme.textTheme.displayLarge,
                ),
                SizedBox(height: 35.v),
                CustomOutlinedButton(
                  buttonStyle: ButtonStyle(backgroundColor:
                      MaterialStateProperty.resolveWith((states) {
                    // If the button is pressed, return green, otherwise blue
                    if (states.contains(MaterialState.pressed)) {
                      return Color(0xff484C7F);
                    }
                    return Color(0xff484C7F);
                  })),
                  text: "Open camera".toUpperCase(),
                  onPressed: () {
                    _scan();
                  },
                ),
                SizedBox(height: 5.v),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Future<void> _scan() async {
    try {
      final result = await BarcodeScanner.scan(
        options: ScanOptions(
          strings: {
            'cancel': _cancelController.text,
            'flash_on': _flashOnController.text,
            'flash_off': _flashOffController.text,
          },
          restrictFormat: selectedFormats,
          useCamera: _selectedCamera,
          autoEnableFlash: _autoEnableFlash,
          android: AndroidOptions(
            aspectTolerance: _aspectTolerance,
            useAutoFocus: _useAutoFocus,
          ),
        ),
      );
      setState(() => scanResult = result);
      EasyLoading.show();
      if (scanResult != null && (scanResult!.rawContent != "")) {
        checkin(scanResult!.rawContent).then((value) {
          if (value.statusCode == 200) {
            showDialog(
                context: context,
                builder: (context) => AlertDialog(
                        content: FrontCameraSuccessDialog(
                      mapArg: jsonDecode(value.body),
                    )));
            EasyLoading.dismiss();
          }
        }, onError: (v) {
          showDialog(
            context: context,
            builder: (context) => AlertDialog(
              content: Text(v.toString()),
            ),
          );
          EasyLoading.dismiss();
        });
      } else {
        showDialog(
          context: context,
          builder: (context) => AlertDialog(
            content: FrontCameraErrorDialog(),
          ),
        );
        EasyLoading.dismiss();
      }
    } on PlatformException catch (e) {
      setState(() {
        scanResult = ScanResult(
          rawContent: e.code == BarcodeScanner.cameraAccessDenied
              ? 'The user did not grant the camera permission!'
              : 'Unknown error: $e',
        );
      });
      showDialog(
        context: context,
        builder: (context) => AlertDialog(
          content: FrontCameraErrorDialog(),
        ),
      );
      EasyLoading.dismiss();
    }
  }

  Future<http.Response> checkin(String token) {
    return http.post(
      Uri.parse('https://se100-main.azurewebsites.net/api/Attendance/checkin'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{'token': token}),
    );
  }
}
