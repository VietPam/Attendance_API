import { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { Outlet } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { RootState } from "../store/store";

const Root = () => {
  const dispatch = useDispatch();
  const notify = useSelector((state: RootState) => state.notify);
  useEffect(() => {
    if (notify) {
      const { type, message } = notify;
      switch (type) {
        case "success":
          toast.success(message);
          break;
        case "error":
          toast.error(message);
          break;
        case "warning":
          toast.warning(message);
          break;
        default:
          break;
      }
      dispatch({ type: "UN_NOTIFY", payload: null });
    }
  }, [dispatch, notify]);
  return (
    <div>
      <Outlet />
      <ToastContainer
        position="top-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </div>
  );
};

export default Root;
