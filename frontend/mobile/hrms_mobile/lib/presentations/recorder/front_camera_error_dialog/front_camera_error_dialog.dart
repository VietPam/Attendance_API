import 'package:flutter/material.dart';
import 'package:hrms_mobile/theme/app_decoration.dart';
import 'package:hrms_mobile/theme/theme_helper.dart';
import 'package:hrms_mobile/utils/image_constant.dart';
import 'package:hrms_mobile/utils/size_utils.dart';
import 'package:hrms_mobile/widgets/custom_image_view.dart';
import 'package:hrms_mobile/widgets/custom_outlined_button.dart';

class FrontCameraErrorDialog extends StatelessWidget {
  const FrontCameraErrorDialog({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    mediaQueryData = MediaQuery.of(context);
    return Container(
        width: 304.h,
        padding: EdgeInsets.symmetric(horizontal: 43.h, vertical: 32.v),
        decoration: AppDecoration.fillWhiteA
            .copyWith(borderRadius: BorderRadiusStyle.roundedBorder13),
        child: Column(
            mainAxisSize: MainAxisSize.min,
            mainAxisAlignment: MainAxisAlignment.end,
            children: [
              SizedBox(height: 17.v),
              CustomImageView(
                  imagePath: ImageConstant.imgClose,
                  height: 67.adaptSize,
                  width: 67.adaptSize,
                  onTap: () {
                    onTapImgClose(context);
                  }),
              SizedBox(height: 24.v),
              Text("Oops, error!", style: theme.textTheme.titleMedium),
              SizedBox(height: 6.v),
              Container(
                  width: 179.h,
                  margin: EdgeInsets.only(left: 19.h, right: 17.h),
                  child: Text("Please contact IT helpdesk for support",
                      maxLines: 2,
                      overflow: TextOverflow.ellipsis,
                      textAlign: TextAlign.center,
                      style: theme.textTheme.bodySmall)),
              SizedBox(height: 25.v),
              CustomOutlinedButton(
                onPressed: () {
                  Navigator.pop(context);
                },
                text: "Return".toUpperCase(),
                buttonStyle: ButtonStyle(backgroundColor:
                    MaterialStateProperty.resolveWith((states) {
                  // If the button is pressed, return green, otherwise blue
                  if (states.contains(MaterialState.pressed)) {
                    return Color(0xff484C7F);
                  }
                  return Color(0xff484C7F);
                })),
              )
            ]));
  }

  /// Navigates back to the previous screen.
  onTapImgClose(BuildContext context) {
    Navigator.pop(context);
  }
}
