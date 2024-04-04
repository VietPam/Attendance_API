/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable react/prop-types */
import { useState } from "react";
import { useDispatch } from "react-redux";
import { CONFIRMATION_MODAL_CLOSE_TYPES } from "../../utils/globalConstantUtil";
import { ResetPassword } from "../../apis/api_function";
import { notify } from "../../store/reducers/notify_reducers";
import { closeModal } from "../../store/reducers/modalSlice";

function ConfirmationModalBody({ extraObject }: { extraObject: any }) {
  const dispatch = useDispatch();
  const [loading, setLoading] = useState(false);
  // eslint-disable-next-line no-unused-vars
  const { message, type, _id } = extraObject;

  const closeModalHandler = () => {
    dispatch(closeModal());
  };

  const proceedWithYes = async () => {
    setLoading(true);
    if (type) {
      try {
        let response;
        if (type === CONFIRMATION_MODAL_CLOSE_TYPES.EMAIL_SEND) {
          response = await ResetPassword(_id);
        }

        if (response) {
          if (type === CONFIRMATION_MODAL_CLOSE_TYPES.EMAIL_SEND)
            dispatch(
              notify({
                message: "Gửi email thành công!",
                type: "success",
              })
            );
          else
            dispatch(notify({ message: "Xoá thành công!", type: "success" }));
          // if (type === CONFIRMATION_MODAL_CLOSE_TYPES.DECREE_DELETE)
          //   dispatch(deleteDecree(index));
          // setTimeout(() => {
          //   window.location.reload();
          // }, 1000);
        } else {
          dispatch(notify({ message: "Xoá thất bại!", type: "error" }));
        }
      } catch (error) {
        console.log("error delete");
      }
      setLoading(false);

      closeModalHandler();
    }
  };

  return (
    <>
      {loading ? (
        <div className="flex justify-center items-center">
          <span className="loading loading-lg"></span>
        </div>
      ) : (
        <>
          <p className=" text-xl my-8 text-center">{message}</p>
          <form method="dialog">
            {/* if there is a button in form, it will close the modal */}
            <button
              className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
              onClick={closeModalHandler}
            >
              ✕
            </button>
          </form>

          <form method="dialog">
            {/* if there is a button in form, it will close the modal */}
            <div className="flex justify-center gap-1">
              <button className="btn btn-error" onClick={closeModalHandler}>
                Close
              </button>
              <button
                className="btn bg-tim-color text-white hover:text-black"
                onClick={proceedWithYes}
              >
                {/* <span className="loading loading-infinity loading-md"></span> */}
                Submit
              </button>
            </div>
          </form>
        </>
      )}
    </>
  );
}

export default ConfirmationModalBody;
