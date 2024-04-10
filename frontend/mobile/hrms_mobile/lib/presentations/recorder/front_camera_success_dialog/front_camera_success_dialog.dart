import 'package:flutter/material.dart';
import 'package:flutter_tts/flutter_tts.dart';
import 'package:hrms_mobile/theme/app_decoration.dart';
import 'package:hrms_mobile/theme/custom_text_style.dart';
import 'package:hrms_mobile/theme/theme_helper.dart';
import 'package:hrms_mobile/utils/image_constant.dart';
import 'package:hrms_mobile/utils/size_utils.dart';
import 'package:hrms_mobile/widgets/custom_icon_button.dart';
import 'package:hrms_mobile/widgets/custom_image_view.dart';
import 'package:hrms_mobile/widgets/custom_outlined_button.dart';
import 'package:intl/intl.dart';

// ignore_for_file: must_be_immutable
class FrontCameraSuccessDialog extends StatelessWidget {
  FrontCameraSuccessDialog({Key? key, this.mapArg})
      : super(
          key: key,
        );
  Map<String, dynamic>? mapArg;

  @override
  Widget build(BuildContext context) {
    mediaQueryData = MediaQuery.of(context);

    return Container(
      width: 304.h,
      padding: EdgeInsets.symmetric(
        horizontal: 17.h,
        vertical: 20.v,
      ),
      decoration: AppDecoration.fillWhiteA.copyWith(
        borderRadius: BorderRadiusStyle.roundedBorder13,
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          if (mapArg!['attendance_state'] == "OnTime")
            CustomIconButton(
              onTap: () async {
                FlutterTts flutterTts = FlutterTts();
                await flutterTts.speak("Cảm ơn ${mapArg!['employee_name']}");
                Future.delayed(Duration(milliseconds: 1000), () async {
                  // await flutterTts.();
                });
              },
              height: 64.adaptSize,
              width: 64.adaptSize,
              padding: EdgeInsets.all(17.h),
              decoration: IconButtonStyleHelper.fillOnError,
              child: CustomImageView(
                imagePath: ImageConstant.imgCheckmark,
              ),
            ),
          if (mapArg!['attendance_state'] == "Late")
            Padding(
              padding: EdgeInsets.only(left: 29.h),
              child: CustomIconButton(
                onTap: () async {
                  FlutterTts flutterTts = FlutterTts();
                  await flutterTts.speak("Cảm ơn ${mapArg!['employee_name']}");
                  Future.delayed(Duration(milliseconds: 1000), () async {
                    await flutterTts.stop();
                  });
                },
                height: 64.adaptSize,
                width: 64.adaptSize,
                padding: EdgeInsets.all(12.h),
                decoration: IconButtonStyleHelper.fillAmberA,
                child: CustomImageView(
                  imagePath: ImageConstant.imgSearch,
                ),
              ),
            ),
          // ],
          // ),
          SizedBox(height: 6.v),
          Text(
            "Attendance success",
            textAlign: TextAlign.center,
            style: CustomTextStyles.titleLargePoppinsBluegray900,
          ),
          SizedBox(height: 6.v),
          Row(
            children: [
              Text(
                "Time:",
                style: theme.textTheme.titleMedium,
              ),
              Text(
                DateFormat('dd-MM-yyyy')
                    .format(DateTime.parse(mapArg!['time'].toString())),
                // mapArg!['time'].toString().split('.')[0],
                style: theme.textTheme.displayMedium,
              ),
            ],
          ),
          Row(
            children: [
              Text(
                "Status:",
                style: theme.textTheme.titleMedium,
              ),
              Text(
                "${mapArg!['attendance_state']}",
                style: theme.textTheme.bodySmall,
              ),
            ],
          ),
          SizedBox(height: 2.v),
          Row(
            children: [
              Text(
                "Name:",
                style: theme.textTheme.titleMedium,
              ),
              Text(
                "${mapArg!['employee_name']}",
                style: theme.textTheme.bodySmall,
              ),
            ],
          ),
          Row(
            children: [
              Text(
                "Department:",
                style: theme.textTheme.titleMedium,
              ),
              Flexible(
                child: Text(
                  "${mapArg!['department_name']}",
                  style: theme.textTheme.bodySmall,
                ),
              ),
            ],
          ),
          SizedBox(height: 10.v),
          CustomOutlinedButton(
            onPressed: () {
              Navigator.pop(context);
            },
            buttonStyle: ButtonStyle(
                backgroundColor: MaterialStateProperty.resolveWith((states) {
              // If the button is pressed, return green, otherwise blue
              if (states.contains(MaterialState.pressed)) {
                return Color(0xff484C7F);
              }
              return Color(0xff484C7F);
            })),
            text: "Return ".toUpperCase(),
            margin: EdgeInsets.symmetric(horizontal: 27.h),
          ),
          SizedBox(height: 2.v),
        ],
      ),
    );
  }
}
